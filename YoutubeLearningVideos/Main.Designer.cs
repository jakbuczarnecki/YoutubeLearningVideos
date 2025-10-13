namespace YoutubeLearningVideos
{
    partial class Main
    {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(Main));
            this.gridControl1 = new DevExpress.XtraGrid.GridControl();
            this.gridView1 = new DevExpress.XtraGrid.Views.Grid.GridView();
            this.panelControl1 = new DevExpress.XtraEditors.PanelControl();
            this.bStartRandom = new DevExpress.XtraEditors.SimpleButton();
            this.rgFilters = new DevExpress.XtraEditors.RadioGroup();
            this.labelControl1 = new DevExpress.XtraEditors.LabelControl();
            this.bAddLink = new DevExpress.XtraEditors.SimpleButton();
            this.teAddLink = new DevExpress.XtraEditors.TextEdit();
            this.panelControl2 = new DevExpress.XtraEditors.PanelControl();
            this.lcInfo = new DevExpress.XtraEditors.LabelControl();
            this.bOpenUrl = new DevExpress.XtraEditors.SimpleButton();
            this.bDeleteVideo = new DevExpress.XtraEditors.SimpleButton();
            ((System.ComponentModel.ISupportInitialize)(this.gridControl1)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.gridView1)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.panelControl1)).BeginInit();
            this.panelControl1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.rgFilters.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.teAddLink.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.panelControl2)).BeginInit();
            this.panelControl2.SuspendLayout();
            this.SuspendLayout();
            // 
            // gridControl1
            // 
            this.gridControl1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.gridControl1.Location = new System.Drawing.Point(0, 94);
            this.gridControl1.MainView = this.gridView1;
            this.gridControl1.Name = "gridControl1";
            this.gridControl1.Size = new System.Drawing.Size(1129, 430);
            this.gridControl1.TabIndex = 0;
            this.gridControl1.ViewCollection.AddRange(new DevExpress.XtraGrid.Views.Base.BaseView[] {
            this.gridView1});
            // 
            // gridView1
            // 
            this.gridView1.GridControl = this.gridControl1;
            this.gridView1.Name = "gridView1";
            this.gridView1.OptionsView.ShowGroupPanel = false;
            // 
            // panelControl1
            // 
            this.panelControl1.Controls.Add(this.bStartRandom);
            this.panelControl1.Controls.Add(this.rgFilters);
            this.panelControl1.Controls.Add(this.labelControl1);
            this.panelControl1.Controls.Add(this.bAddLink);
            this.panelControl1.Controls.Add(this.teAddLink);
            this.panelControl1.Dock = System.Windows.Forms.DockStyle.Top;
            this.panelControl1.Location = new System.Drawing.Point(0, 0);
            this.panelControl1.Name = "panelControl1";
            this.panelControl1.Size = new System.Drawing.Size(1129, 94);
            this.panelControl1.TabIndex = 1;
            // 
            // bStartRandom
            // 
            this.bStartRandom.Location = new System.Drawing.Point(683, 45);
            this.bStartRandom.Name = "bStartRandom";
            this.bStartRandom.Size = new System.Drawing.Size(108, 27);
            this.bStartRandom.TabIndex = 3;
            this.bStartRandom.Text = "Start random";
            this.bStartRandom.Click += new System.EventHandler(this.bStartRandom_Click);
            // 
            // rgFilters
            // 
            this.rgFilters.Location = new System.Drawing.Point(5, 43);
            this.rgFilters.Name = "rgFilters";
            this.rgFilters.Properties.Items.AddRange(new DevExpress.XtraEditors.Controls.RadioGroupItem[] {
            new DevExpress.XtraEditors.Controls.RadioGroupItem(0, "Running"),
            new DevExpress.XtraEditors.Controls.RadioGroupItem(1, "Pending"),
            new DevExpress.XtraEditors.Controls.RadioGroupItem(2, "Completed"),
            new DevExpress.XtraEditors.Controls.RadioGroupItem(100, "Search")});
            this.rgFilters.Size = new System.Drawing.Size(671, 31);
            this.rgFilters.TabIndex = 2;
            // 
            // labelControl1
            // 
            this.labelControl1.Location = new System.Drawing.Point(5, 14);
            this.labelControl1.Name = "labelControl1";
            this.labelControl1.Size = new System.Drawing.Size(41, 13);
            this.labelControl1.TabIndex = 2;
            this.labelControl1.Text = "Add link:";
            // 
            // bAddLink
            // 
            this.bAddLink.Location = new System.Drawing.Point(683, 10);
            this.bAddLink.Name = "bAddLink";
            this.bAddLink.Size = new System.Drawing.Size(108, 27);
            this.bAddLink.TabIndex = 1;
            this.bAddLink.Text = "Add";
            this.bAddLink.Click += new System.EventHandler(this.bAddLink_Click);
            // 
            // teAddLink
            // 
            this.teAddLink.Location = new System.Drawing.Point(55, 9);
            this.teAddLink.Name = "teAddLink";
            this.teAddLink.Size = new System.Drawing.Size(621, 28);
            this.teAddLink.TabIndex = 0;
            this.teAddLink.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.teAddLink_KeyPress);
            this.teAddLink.KeyUp += new System.Windows.Forms.KeyEventHandler(this.teAddLink_KeyUp);
            // 
            // panelControl2
            // 
            this.panelControl2.Controls.Add(this.lcInfo);
            this.panelControl2.Controls.Add(this.bOpenUrl);
            this.panelControl2.Controls.Add(this.bDeleteVideo);
            this.panelControl2.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.panelControl2.Location = new System.Drawing.Point(0, 524);
            this.panelControl2.Name = "panelControl2";
            this.panelControl2.Size = new System.Drawing.Size(1129, 42);
            this.panelControl2.TabIndex = 2;
            // 
            // lcInfo
            // 
            this.lcInfo.Location = new System.Drawing.Point(13, 14);
            this.lcInfo.Name = "lcInfo";
            this.lcInfo.Size = new System.Drawing.Size(18, 13);
            this.lcInfo.TabIndex = 2;
            this.lcInfo.Text = "Info";
            // 
            // bOpenUrl
            // 
            this.bOpenUrl.Location = new System.Drawing.Point(135, 9);
            this.bOpenUrl.Name = "bOpenUrl";
            this.bOpenUrl.Size = new System.Drawing.Size(75, 23);
            this.bOpenUrl.TabIndex = 1;
            this.bOpenUrl.Text = "Open";
            this.bOpenUrl.Click += new System.EventHandler(this.bOpenUrl_Click);
            // 
            // bDeleteVideo
            // 
            this.bDeleteVideo.Location = new System.Drawing.Point(216, 9);
            this.bDeleteVideo.Name = "bDeleteVideo";
            this.bDeleteVideo.Size = new System.Drawing.Size(75, 23);
            this.bDeleteVideo.TabIndex = 0;
            this.bDeleteVideo.Text = "Delete";
            this.bDeleteVideo.Click += new System.EventHandler(this.bDeleteVideo_Click);
            // 
            // Main
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1129, 566);
            this.Controls.Add(this.gridControl1);
            this.Controls.Add(this.panelControl2);
            this.Controls.Add(this.panelControl1);
            this.IconOptions.SvgImage = ((DevExpress.Utils.Svg.SvgImage)(resources.GetObject("Main.IconOptions.SvgImage")));
            this.Name = "Main";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Youtube Learning Videos";
            this.Load += new System.EventHandler(this.Main_Load);
            ((System.ComponentModel.ISupportInitialize)(this.gridControl1)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.gridView1)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.panelControl1)).EndInit();
            this.panelControl1.ResumeLayout(false);
            this.panelControl1.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.rgFilters.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.teAddLink.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.panelControl2)).EndInit();
            this.panelControl2.ResumeLayout(false);
            this.panelControl2.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion

        private DevExpress.XtraGrid.GridControl gridControl1;
        private DevExpress.XtraGrid.Views.Grid.GridView gridView1;
        private DevExpress.XtraEditors.PanelControl panelControl1;
        private DevExpress.XtraEditors.LabelControl labelControl1;
        private DevExpress.XtraEditors.SimpleButton bAddLink;
        private DevExpress.XtraEditors.TextEdit teAddLink;
        private DevExpress.XtraEditors.RadioGroup rgFilters;
        private DevExpress.XtraEditors.PanelControl panelControl2;
        private DevExpress.XtraEditors.SimpleButton bDeleteVideo;
        private DevExpress.XtraEditors.LabelControl lcInfo;
        private DevExpress.XtraEditors.SimpleButton bOpenUrl;
        private DevExpress.XtraEditors.SimpleButton bStartRandom;
    }
}

