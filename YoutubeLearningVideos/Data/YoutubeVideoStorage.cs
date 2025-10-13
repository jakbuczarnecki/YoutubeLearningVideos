using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Runtime.Serialization;
using System.Runtime.Serialization.Json;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using System.Xml.Linq;
using System.Web;

namespace YoutubeLearningVideos.Data
{
    public class YoutubeVideoStorage
    {
        static readonly string StorageFileName = "D:\\YoutubeLearningVideos\\YoutubeLearningVideos\\YoutubeStorage.xml";

        List<YoutubeVideoItem> items = new List<YoutubeVideoItem>();
        public List<YoutubeVideoItem> Items
        {
            get { return items; }
        }

        public YoutubeVideoStorage()
        {

        }

        public void Init()
        {
            Load();

            if (!items.Any())
            {
                Import();
            }
        }

        private void Import()
        {
            string toWatchPath = @"D:\PythonWorkplace\AI Youtube strony";
            string watchedPath = @"D:\PythonWorkplace\checked";

            if (Directory.Exists(toWatchPath) && Directory.Exists(watchedPath))
            {
                var files = Directory.GetFiles(toWatchPath, "*.url").Union(Directory.GetFiles(watchedPath, "*.url")).ToArray();
                if (files.Any())
                {
                    foreach (var file in files)
                    {
                        var info = new FileInfo(file);
                        if (info.Exists)
                        {
                            var content = File.ReadAllLines(file);
                            var urlLine = content.FirstOrDefault(c => c.StartsWith("URL="));
                            if (!string.IsNullOrEmpty(urlLine))
                            {
                                urlLine = urlLine.Replace("URL=", string.Empty);
                                if (items.Any(i => i.Url.ToLower() == urlLine.ToLower()))
                                {
                                    continue;
                                }

                                var item = new YoutubeVideoItem
                                {
                                    Url = urlLine,
                                    Title = info.Name.Replace(info.Extension, ""),
                                    CreateDate = info.CreationTime,
                                    ProcessedDate = null,
                                    //VideoDate = null,
                                    Status = EnumYoutubeVideoStatuses.New,
                                };

                                if (file.ToLower().StartsWith(toWatchPath.ToLower()))
                                {
                                    item.Status = EnumYoutubeVideoStatuses.New;
                                }
                                else
                                {
                                    item.Status = EnumYoutubeVideoStatuses.Completed;
                                    item.ProcessedDate = info.LastWriteTime;
                                }

                                items.Add(item);
                            }
                        }
                    }
                }
            }

            Save();
        }

        public void Load()
        {
            items = new List<YoutubeVideoItem>();
            if (File.Exists(StorageFileName))
            {
                var doc = XDocument.Load(StorageFileName);
                foreach (var item in doc.Root.Elements())
                {
                    var videoItem = new YoutubeVideoItem();
                    videoItem.CreateDate = DateTime.Parse(item.Element("CreateDate").Value);
                    videoItem.Url = item.Element("Url").Value;
                    videoItem.Title = item.Element("Title").Value;
                    videoItem.Status = (EnumYoutubeVideoStatuses)Enum.Parse(typeof(EnumYoutubeVideoStatuses), item.Element("Status").Value);
                    //if (item.Element("VideoDate") != null && !string.IsNullOrEmpty(item.Element("VideoDate").Value))
                    //    videoItem.VideoDate = DateTime.Parse(item.Element("VideoDate").Value);
                    if (item.Element("ProcessedDate") != null && !string.IsNullOrEmpty(item.Element("ProcessedDate").Value))
                        videoItem.ProcessedDate = DateTime.Parse(item.Element("ProcessedDate").Value);
                    if (item.Element("VideoPosition") != null)
                    {
                        videoItem.VideoPositions = item.Element("VideoPosition").Value;
                    }
                    if (item.Element("SearchLink") != null && !string.IsNullOrEmpty(item.Element("SearchLink").Value))
                    {
                        if (bool.TryParse(item.Element("SearchLink").Value, out var searchLink))
                        {
                            videoItem.SearchLink = searchLink;
                        }
                    }
                  
                    if (item.Element("AdditionalInfo")!=null && !string.IsNullOrEmpty(item.Element("AdditionalInfo").Value))
                    {
                        // to jest tylko do importu ze starej wersji
                        videoItem.AdditionalInfo = item.Element("AdditionalInfo").Value;
                    }

                    items.Add(videoItem);
                }
            }
        }

        public void Save()
        {
            var doc = new XDocument();
            var root = new XElement("YoutubeVideos");
            doc.Add(root);
            foreach (var item in items)
            {
                var itemElement = new XElement("YoutubeVideoItem");
                itemElement.Add(new XElement("CreateDate", item.CreateDate));
                itemElement.Add(new XElement("Url", item.Url));
                itemElement.Add(new XElement("Title", item.Title));
                itemElement.Add(new XElement("Status", item.Status.ToString()));
                //if (item.VideoDate.HasValue)
                //{
                //    itemElement.Add(new XElement("VideoDate", item.VideoDate.Value));
                //}
                if (item.ProcessedDate.HasValue)
                {
                    itemElement.Add(new XElement("ProcessedDate", item.ProcessedDate.Value));
                }
                if (!string.IsNullOrEmpty(item.VideoPositions))
                {
                    itemElement.Add(new XElement("VideoPosition", item.VideoPositions));
                }
                if (!string.IsNullOrEmpty(item.AdditionalInfo))
                {
                    itemElement.Add(new XElement("AdditionalInfo", item.AdditionalInfo));
                }
                itemElement.Add(new XElement("SearchLink", item.SearchLink));

                root.Add(itemElement);
            }
            doc.Save(StorageFileName);
        }


        public void AddLink(string url)
        {
            if (items.Any(i =>  i.Url.ToLower() == url.ToLower()))
            {
                throw new Exception("Link already exists");
            }

            if (string.IsNullOrWhiteSpace(url))
            {
                throw new Exception("URL is empty");
            }

            var searchLink = !url.ToLower().Contains("watch?");

            if (searchLink)
            {
                var uri = new Uri(url);
                var query = HttpUtility.ParseQueryString(uri.Query);
                var search = query["search_query"];

                var item = new YoutubeVideoItem();
                item.CreateDate = DateTime.Now;
                item.Url = url;
                item.Title = search;
                item.SearchLink = true;

                items.Add(item);
            }
            else
            {
                // 1) Wyciągnij VIDEO_ID z różnych wariantów URL
                if (!TryGetYouTubeVideoId(url, out var videoId))
                    throw new Exception("To nie wygląda na prawidłowy link do wideo YouTube.");

                //// 2) Duplikaty sprawdzaj po VideoId (nie po całym URL)
                //if (items.Any(i => string.Equals(i.VideoId, videoId, StringComparison.OrdinalIgnoreCase)))
                //    throw new Exception("Ten film już istnieje (duplikat VideoId).");

                // 3) Znormalizowany, kanoniczny URL
                var canonicalUrl = $"https://www.youtube.com/watch?v={videoId}";

                // 4) Pobierz tytuł – najpierw oEmbed, a jeśli się nie uda, z <title>
                var title = GetYouTubeTitle(videoId) ?? GetHtmlTitle(canonicalUrl) ?? "YouTube";

                var item = new YoutubeVideoItem();
                item.CreateDate = DateTime.Now;
                item.Url = url;
                item.Title = title;
                item.SearchLink = false;

                items.Add(item);
            }

            Save();
        }

        private static bool TryGetYouTubeVideoId(string url, out string videoId)
        {
            videoId = null;
            if (string.IsNullOrWhiteSpace(url)) return false;

            // 1) watch?v=XXXXXXXXXXX
            var watch = Regex.Match(url, @"(?:v=)([A-Za-z0-9_\-]{11})");
            if (watch.Success) { videoId = watch.Groups[1].Value; return true; }

            // 2) youtu.be/XXXXXXXXXXX
            var shortUrl = Regex.Match(url, @"youtu\.be/([A-Za-z0-9_\-]{11})");
            if (shortUrl.Success) { videoId = shortUrl.Groups[1].Value; return true; }

            // 3) embed/XXXXXXXXXXX
            var embed = Regex.Match(url, @"embed/([A-Za-z0-9_\-]{11})");
            if (embed.Success) { videoId = embed.Groups[1].Value; return true; }

            // 4) shorts/XXXXXXXXXXX
            var shorts = Regex.Match(url, @"shorts/([A-Za-z0-9_\-]{11})");
            if (shorts.Success) { videoId = shorts.Groups[1].Value; return true; }

            return false;
        }

        [DataContract]
        private class OEmbedResponse
        {
            [DataMember(Name = "title")]
            public string Title { get; set; }
        }

        // OEmbed: brak API key, bardzo stabilne
        private static string GetYouTubeTitle(string videoId)
        {
            var oembedUrl = $"https://www.youtube.com/oembed?url=https://www.youtube.com/watch?v={videoId}&format=json";
            try
            {
                var req = (HttpWebRequest)WebRequest.Create(oembedUrl);
                req.Method = "GET";
                req.UserAgent = "Mozilla/5.0"; // YouTube bywa wybredny bez UA

                using (var resp = (HttpWebResponse)req.GetResponse())
                using (var stream = resp.GetResponseStream())
                {
                    var ser = new DataContractJsonSerializer(typeof(OEmbedResponse));
                    var data = (OEmbedResponse)ser.ReadObject(stream);
                    return string.IsNullOrWhiteSpace(data?.Title) ? null : data.Title.Trim();
                }
            }
            catch
            {
                return null; // polecimy fallbackiem
            }
        }

        // Fallback: pobierz <title> z HTML
        private static string GetHtmlTitle(string url)
        {
            try
            {
                using (var wc = new WebClient())
                {
                    wc.Headers.Add(HttpRequestHeader.UserAgent, "Mozilla/5.0");
                    wc.Encoding = System.Text.Encoding.UTF8;
                    var html = wc.DownloadString(url);

                    var m = Regex.Match(html, @"<title>(.*?)</title>", RegexOptions.IgnoreCase | RegexOptions.Singleline);
                    if (!m.Success) return null;

                    var raw = m.Groups[1].Value.Trim();
                    var decoded = HttpUtility.HtmlDecode(raw);

                    // YouTube zwykle ma "Tytuł – YouTube" lub "Tytuł - YouTube"
                    decoded = Regex.Replace(decoded, @"\s*[-–]\s*YouTube\s*$", "", RegexOptions.IgnoreCase);
                    return decoded;
                }
            }
            catch
            {
                return null;
            }
        }

        public void DeleteVideo(IEnumerable<YoutubeVideoItem> items)
        {
            if (items == null || !items.Any())
                return;

            foreach (var item in items)
            {
                this.items.Remove(item);
            }

            Save();
        }

        public void StartRandom(IEnumerable<YoutubeVideoItem> items)
        {
            var pending = items.Where(i => i.Status == EnumYoutubeVideoStatuses.New).ToArray();
            
            if (pending.Any())
            {
                var random = new Random();
                var index = random.Next(0, pending.Length);
                var item = pending[index];
                item.Status = EnumYoutubeVideoStatuses.Running;
                Save();
            }
        }
    }
}
