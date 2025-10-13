using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace YoutubeLearningVideos.Data
{
    public class YoutubeVideoItem
    {
        public string Title { get; set; }


        EnumYoutubeVideoStatuses status = EnumYoutubeVideoStatuses.New;
        public EnumYoutubeVideoStatuses Status
        {
            get { return status; }
            set
            {
                if (status != value)
                {
                    VideoPositions = null;
                    status = value;
                    switch (status)
                    {
                        case EnumYoutubeVideoStatuses.New:
                            ProcessedDate = null;
                            break;

                        case EnumYoutubeVideoStatuses.Rejected:
                        case EnumYoutubeVideoStatuses.Completed:
                            ProcessedDate = DateTime.Now;
                            break;
                    }
                }
            }
        }

        public string VideoPositions { get; set; }

        public DateTime CreateDate { get; set; }

        //public DateTime? VideoDate { get; set; }

        public DateTime? ProcessedDate { get; set; }

        public string AdditionalInfo { get; set; }

        public string Url { get; set; }

        

        public bool SearchLink { get; set; }
    }


    public enum EnumYoutubeVideoStatuses
    {
        New,
        Running,
        Completed,
        Rejected
    }
}
