using DevExpress.XtraGrid.Columns;
using DevExpress.XtraGrid.Views.Base;
using DevExpress.XtraPrinting.Export.Rtf;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Diagnostics;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using YoutubeLearningVideos.Data;
using static DevExpress.Utils.Svg.CommonSvgImages;

namespace YoutubeLearningVideos {
    public partial class Main : DevExpress.XtraEditors.XtraForm {

        YoutubeVideoStorage storage = new YoutubeVideoStorage();

        public Main() {
            InitializeComponent();
        }

        IEnumerable<YoutubeVideoItem> GetItems()
        {
            var items = storage.Items.AsEnumerable();

            if (!string.IsNullOrEmpty(gridView1.FindFilterText))
            {
                var filterText = gridView1.FindFilterText.ToLower();
                items = items.Where(i => i.Title.ToLower().Contains(filterText) || i.Url.ToLower().Contains(filterText));
            }
            return items;
        }

        private void LoadVideos()
        {
            var items = GetItems();

            var selectedItem = rgFilters.Properties.Items[rgFilters.SelectedIndex];
            var value = int.Parse(selectedItem.Value.ToString());

            bStartRandom.Enabled = true;

            switch (value)
            {
                case 0:
                    items = items.Where(i => !i.SearchLink && i.Status == EnumYoutubeVideoStatuses.Running);
                    break;

                case 1:
                    items = items.Where(i => !i.SearchLink && (i.Status == EnumYoutubeVideoStatuses.New));
                    break;

                case 2:
                    items = items.Where(i => !i.SearchLink && (i.Status == EnumYoutubeVideoStatuses.Rejected || i.Status == EnumYoutubeVideoStatuses.Completed));
                    break;

                case 100:
                    items = items.Where(i => i.SearchLink);
                    break;
            }


            //var tags= new Dictionary<string, int>();

            //var keywords = new[] {
            //    "flutter",
            //    "mcp",
            //    "fastapi",
            //    "python",
            //    "langchain",
            //    "langgraph",
            //    "agent",
            //    "agents",
            //    "ai",
            //    "rag",
            //    "lamaindex",
            //    "ollama",
            //    "llama",
            //    "openai",
            //    "vector",
            //    "vectorstore",
            //    "chroma",
            //    "elasticsearch",
            //    "mongodb",                
            //};

            //foreach (var item in items)
            //{
            //    var parts = item.Title.ToLower().Split(' ').Where(p=>!string.IsNullOrEmpty(p)).ToList();
            //    foreach (var p in parts)
            //    {
            //        if (p.All(c => char.IsDigit(c) || char.IsSymbol(c)))
            //        {
            //            continue;
            //        }

            //        if (!p.Any(c => char.IsLetter(c)))
            //        {
            //            continue;
            //        }

            //        if (keywords.Any(c => c.ToLower() == p))
            //        {

            //            if (tags.ContainsKey(p))
            //            {
            //                tags[p]++;
            //            }
            //            else
            //            {
            //                tags.Add(p, 1);
            //            }
            //        }
            //    }
            //}
            //var topTags = tags.OrderByDescending(t => t.Value).Take(20).Select(t => t.Key).ToList();

            //rgTags.SelectedIndexChanged -= RgTags_SelectedIndexChanged;
            //rgTags.Properties.Items.Clear();
            //rgTags.Properties.Items.Add(new DevExpress.XtraEditors.Controls.RadioGroupItem(string.Empty, "All"));
            //foreach (var t in topTags)
            //{
            //    rgTags.Properties.Items.Add(new DevExpress.XtraEditors.Controls.RadioGroupItem(t, t));
            //}
            //rgTags.SelectedIndexChanged += RgTags_SelectedIndexChanged;

            gridControl1.DataSource = items.ToList();
            gridControl1.RefreshDataSource();

            if (value == 100)
            {
                foreach (GridColumn col in gridView1.Columns)
                {
                    col.Visible = col.FieldName == "Title" || col.FieldName == "Url";
                }
            }
            else
            {
                foreach (GridColumn col in gridView1.Columns)
                {
                    col.Visible = col.FieldName != "SearchLink";
                }
            }

            gridView1.BestFitColumns();

            lcInfo.Text = $"Items: {items.Count()}";
        }

        private void Main_Load(object sender, EventArgs e)
        {
            rgFilters.SelectedIndex = -1;

            rgFilters.SelectedIndexChanged += rgFilters_SelectedIndexChanged;

            storage.Init();

            if (storage.Items.Any(i=>i.Status == EnumYoutubeVideoStatuses.Running))
            {
                rgFilters.SelectedIndex = 0;
            }
            else
            {
                rgFilters.SelectedIndex = 1;
            }

            gridView1.Columns["CreateDate"].SortOrder = DevExpress.Data.ColumnSortOrder.Descending;
            gridView1.CellValueChanged += GridView1_CellValueChanged;
            gridView1.OptionsView.ColumnAutoWidth = false;
            gridView1.OptionsFind.AlwaysVisible = true;

            gridView1.ShowingEditor += GridView1_ShowingEditor;
            gridView1.DoubleClick += GridView1_DoubleClick;
            gridView1.ColumnFilterChanged += GridView1_ColumnFilterChanged;
        }

        bool isFiltering = false;
        private void GridView1_ColumnFilterChanged(object sender, EventArgs e)
        {
            if (isFiltering)
                return;

            isFiltering = true;
            try
            {
                LoadVideos();
                var f = gridView1.FindFilterText;
                gridView1.ApplyFindFilter(f);
            }
            finally
            {
                isFiltering = false;
            }
        }

        private void GridView1_DoubleClick(object sender, EventArgs e)
        {
            bOpenUrl.PerformClick();
        }

        private void GridView1_ShowingEditor(object sender, CancelEventArgs e)
        {
            if (gridView1.FocusedColumn != null)
            {
                switch (gridView1.FocusedColumn.FieldName)
                {
                    case "CreateDate":
                    case "Url":
                    case "Title":
                        e.Cancel = true;
                        break;
                        
                }
            }
        }

        private void GridView1_CellValueChanged(object sender, CellValueChangedEventArgs e)
        {
            storage.Save();
            LoadVideos();
        }

        private void bAddLink_Click(object sender, EventArgs e)
        {
            if (string.IsNullOrEmpty(teAddLink.Text))
            {
                MessageBox.Show("Please provide a link.");
                return;
            }

            var selectedItem = rgFilters.Properties.Items[rgFilters.SelectedIndex];
            var value = int.Parse(selectedItem.Value.ToString());

            try
            {
                storage.AddLink(teAddLink.Text);              
                LoadVideos();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);                
            }
            finally
            {
                teAddLink.Text = string.Empty;
            }
        }

        private void rgFilters_SelectedIndexChanged(object sender, EventArgs e)
        {
            LoadVideos();
        }

        private void bDeleteVideo_Click(object sender, EventArgs e)
        {
            var selectedVideos = new List<YoutubeVideoItem>();
            var selection = gridView1.GetSelectedRows();
            foreach (var s in selection)
            {
                var item = gridView1.GetRow(s) as YoutubeVideoItem;
                if (item != null)
                {
                    selectedVideos.Add(item);
                }
            }

            if (!selectedVideos.Any())
            {
                MessageBox.Show("Please select at least one video to delete.");
                return;
            }

            if (selectedVideos.Any())
            {
                if (MessageBox.Show($"Are you sure you want to delete {selectedVideos.Count} selected videos?", "Confirm Deletion", MessageBoxButtons.YesNo) == DialogResult.Yes)
                {
                    storage.DeleteVideo(selectedVideos);
                    LoadVideos();
                }
            }
        }

        private void bOpenUrl_Click(object sender, EventArgs e)
        {
            var selectedVideos = new List<YoutubeVideoItem>();
            var selection = gridView1.GetSelectedRows();
            foreach (var s in selection)
            {
                var item = gridView1.GetRow(s) as YoutubeVideoItem;
                if (item != null)
                {
                    selectedVideos.Add(item);
                }
            }

            if (selectedVideos.Count!=1)
            {
                MessageBox.Show("Please select one video.");
                return;
            }

            Process.Start(selectedVideos[0].Url);
        }

        private void teAddLink_KeyPress(object sender, KeyPressEventArgs e)
        {
        }

        private void teAddLink_KeyUp(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {                
                e.Handled = true;

                bAddLink.PerformClick();
            }
        }

        private void bStartRandom_Click(object sender, EventArgs e)
        {
            storage.StartRandom(GetItems());
            if (rgFilters.SelectedIndex == 0)
            {
                LoadVideos();
            }
            else
            {
                rgFilters.SelectedIndex = 0;
            }
        }
    }
}
