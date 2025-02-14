namespace ZPLMaker
{
    partial class frmZPL
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(frmZPL));
            this.panelControl3 = new DevExpress.XtraEditors.PanelControl();
            this.btnRedraw = new DevExpress.XtraEditors.SimpleButton();
            this.btnOpenFile = new DevExpress.XtraEditors.SimpleButton();
            this.panelControl1 = new DevExpress.XtraEditors.PanelControl();
            this.gridZPLCon = new DevExpress.XtraGrid.GridControl();
            this.gridZPL = new DevExpress.XtraGrid.Views.BandedGrid.BandedGridView();
            this.gridBand3 = new DevExpress.XtraGrid.Views.BandedGrid.GridBand();
            this.imgZPL = new DevExpress.XtraEditors.Repository.RepositoryItemPictureEdit();
            this.imgZpl1 = new DevExpress.XtraEditors.Repository.RepositoryItemImageEdit();
            this.gridView2 = new DevExpress.XtraGrid.Views.Grid.GridView();
            this.panelControl2 = new DevExpress.XtraEditors.PanelControl();
            this.zplPanel = new DevExpress.Utils.Design.ImagePanel();
            this.flyoutPanel1 = new DevExpress.Utils.FlyoutPanel();
            this.btnDownloadJPG = new DevExpress.XtraEditors.SimpleButton();
            this.btnDownloadPNG = new DevExpress.XtraEditors.SimpleButton();
            this.btnDownloadPDF = new DevExpress.XtraEditors.SimpleButton();
            this.btnDownload = new DevExpress.XtraEditors.SimpleButton();
            this.panelControl4 = new DevExpress.XtraEditors.PanelControl();
            this.btnPrint = new DevExpress.XtraEditors.SimpleButton();
            ((System.ComponentModel.ISupportInitialize)(this.panelControl3)).BeginInit();
            this.panelControl3.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.panelControl1)).BeginInit();
            this.panelControl1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.gridZPLCon)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.gridZPL)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.imgZPL)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.imgZpl1)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.gridView2)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.panelControl2)).BeginInit();
            this.panelControl2.SuspendLayout();
            this.zplPanel.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.flyoutPanel1)).BeginInit();
            this.flyoutPanel1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.panelControl4)).BeginInit();
            this.panelControl4.SuspendLayout();
            this.SuspendLayout();
            // 
            // panelControl3
            // 
            this.panelControl3.Controls.Add(this.btnRedraw);
            this.panelControl3.Controls.Add(this.btnOpenFile);
            this.panelControl3.Dock = System.Windows.Forms.DockStyle.Top;
            this.panelControl3.Location = new System.Drawing.Point(0, 0);
            this.panelControl3.Name = "panelControl3";
            this.panelControl3.Size = new System.Drawing.Size(1096, 58);
            this.panelControl3.TabIndex = 5;
            // 
            // btnRedraw
            // 
            this.btnRedraw.ImageOptions.Image = ((System.Drawing.Image)(resources.GetObject("btnRedraw.ImageOptions.Image")));
            this.btnRedraw.Location = new System.Drawing.Point(106, 12);
            this.btnRedraw.Name = "btnRedraw";
            this.btnRedraw.Size = new System.Drawing.Size(86, 35);
            this.btnRedraw.TabIndex = 5;
            this.btnRedraw.Text = "Redraw";
            this.btnRedraw.Visible = false;
            this.btnRedraw.Click += new System.EventHandler(this.btnRedraw_Click);
            // 
            // btnOpenFile
            // 
            this.btnOpenFile.ImageOptions.Image = ((System.Drawing.Image)(resources.GetObject("btnOpenFile.ImageOptions.Image")));
            this.btnOpenFile.Location = new System.Drawing.Point(12, 12);
            this.btnOpenFile.Name = "btnOpenFile";
            this.btnOpenFile.Size = new System.Drawing.Size(88, 35);
            this.btnOpenFile.TabIndex = 4;
            this.btnOpenFile.Text = "Open File";
            this.btnOpenFile.Click += new System.EventHandler(this.btnOpenFile_Click);
            // 
            // panelControl1
            // 
            this.panelControl1.Controls.Add(this.gridZPLCon);
            this.panelControl1.Dock = System.Windows.Forms.DockStyle.Left;
            this.panelControl1.Location = new System.Drawing.Point(0, 58);
            this.panelControl1.Name = "panelControl1";
            this.panelControl1.Size = new System.Drawing.Size(763, 517);
            this.panelControl1.TabIndex = 6;
            // 
            // gridZPLCon
            // 
            this.gridZPLCon.Dock = System.Windows.Forms.DockStyle.Fill;
            this.gridZPLCon.Location = new System.Drawing.Point(2, 2);
            this.gridZPLCon.MainView = this.gridZPL;
            this.gridZPLCon.Name = "gridZPLCon";
            this.gridZPLCon.RepositoryItems.AddRange(new DevExpress.XtraEditors.Repository.RepositoryItem[] {
            this.imgZPL,
            this.imgZpl1});
            this.gridZPLCon.Size = new System.Drawing.Size(759, 513);
            this.gridZPLCon.TabIndex = 23;
            this.gridZPLCon.UseEmbeddedNavigator = true;
            this.gridZPLCon.ViewCollection.AddRange(new DevExpress.XtraGrid.Views.Base.BaseView[] {
            this.gridZPL,
            this.gridView2});
            this.gridZPLCon.Click += new System.EventHandler(this.gridZPLCon_Click);
            // 
            // gridZPL
            // 
            this.gridZPL.Appearance.BandPanel.Font = new System.Drawing.Font("Tahoma", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.gridZPL.Appearance.BandPanel.Options.UseFont = true;
            this.gridZPL.Appearance.BandPanel.Options.UseTextOptions = true;
            this.gridZPL.Appearance.BandPanel.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center;
            this.gridZPL.Bands.AddRange(new DevExpress.XtraGrid.Views.BandedGrid.GridBand[] {
            this.gridBand3});
            this.gridZPL.FocusRectStyle = DevExpress.XtraGrid.Views.Grid.DrawFocusRectStyle.RowFullFocus;
            this.gridZPL.GridControl = this.gridZPLCon;
            this.gridZPL.HorzScrollStep = 1;
            this.gridZPL.HorzScrollVisibility = DevExpress.XtraGrid.Views.Base.ScrollVisibility.Always;
            this.gridZPL.Name = "gridZPL";
            this.gridZPL.OptionsView.RowAutoHeight = true;
            this.gridZPL.OptionsView.ShowAutoFilterRow = true;
            this.gridZPL.OptionsView.ShowGroupPanel = false;
            this.gridZPL.OptionsView.ShowIndicator = false;
            this.gridZPL.VertScrollVisibility = DevExpress.XtraGrid.Views.Base.ScrollVisibility.Always;
            // 
            // gridBand3
            // 
            this.gridBand3.AppearanceHeader.Font = new System.Drawing.Font("Tahoma", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.gridBand3.AppearanceHeader.Options.UseFont = true;
            this.gridBand3.Caption = "ZPL";
            this.gridBand3.Name = "gridBand3";
            this.gridBand3.VisibleIndex = 0;
            this.gridBand3.Width = 1087;
            // 
            // imgZPL
            // 
            this.imgZPL.CustomHeight = 50;
            this.imgZPL.Name = "imgZPL";
            // 
            // imgZpl1
            // 
            this.imgZpl1.Buttons.AddRange(new DevExpress.XtraEditors.Controls.EditorButton[] {
            new DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Combo)});
            this.imgZpl1.ButtonsStyle = DevExpress.XtraEditors.Controls.BorderStyles.Simple;
            this.imgZpl1.Name = "imgZpl1";
            this.imgZpl1.ShowDropDown = DevExpress.XtraEditors.Controls.ShowDropDown.Never;
            // 
            // gridView2
            // 
            this.gridView2.GridControl = this.gridZPLCon;
            this.gridView2.Name = "gridView2";
            this.gridView2.OptionsView.ShowAutoFilterRow = true;
            this.gridView2.OptionsView.ShowGroupPanel = false;
            this.gridView2.OptionsView.ShowIndicator = false;
            // 
            // panelControl2
            // 
            this.panelControl2.Controls.Add(this.zplPanel);
            this.panelControl2.Dock = System.Windows.Forms.DockStyle.Top;
            this.panelControl2.Location = new System.Drawing.Point(763, 58);
            this.panelControl2.Name = "panelControl2";
            this.panelControl2.Size = new System.Drawing.Size(333, 470);
            this.panelControl2.TabIndex = 7;
            // 
            // zplPanel
            // 
            this.zplPanel.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(224)))), ((int)(((byte)(224)))), ((int)(((byte)(224)))));
            this.zplPanel.Controls.Add(this.flyoutPanel1);
            this.zplPanel.Dock = System.Windows.Forms.DockStyle.Fill;
            this.zplPanel.Location = new System.Drawing.Point(2, 2);
            this.zplPanel.Name = "zplPanel";
            this.zplPanel.Size = new System.Drawing.Size(329, 466);
            this.zplPanel.TabIndex = 1;
            // 
            // flyoutPanel1
            // 
            this.flyoutPanel1.Controls.Add(this.btnDownloadJPG);
            this.flyoutPanel1.Controls.Add(this.btnDownloadPNG);
            this.flyoutPanel1.Controls.Add(this.btnDownloadPDF);
            this.flyoutPanel1.Location = new System.Drawing.Point(200, 346);
            this.flyoutPanel1.Name = "flyoutPanel1";
            this.flyoutPanel1.OptionsButtonPanel.ButtonPanelContentAlignment = System.Drawing.ContentAlignment.MiddleCenter;
            this.flyoutPanel1.OptionsButtonPanel.ButtonPanelLocation = DevExpress.Utils.FlyoutPanelButtonPanelLocation.Top;
            this.flyoutPanel1.OwnerControl = this.btnDownload;
            this.flyoutPanel1.Size = new System.Drawing.Size(126, 116);
            this.flyoutPanel1.TabIndex = 9;
            // 
            // btnDownloadJPG
            // 
            this.btnDownloadJPG.ImageOptions.Image = ((System.Drawing.Image)(resources.GetObject("btnDownloadJPG.ImageOptions.Image")));
            this.btnDownloadJPG.Location = new System.Drawing.Point(3, 77);
            this.btnDownloadJPG.Name = "btnDownloadJPG";
            this.btnDownloadJPG.Size = new System.Drawing.Size(120, 35);
            this.btnDownloadJPG.TabIndex = 0;
            this.btnDownloadJPG.Text = "JPG";
            this.btnDownloadJPG.Click += new System.EventHandler(this.btnDownloadJPG_Click);
            // 
            // btnDownloadPNG
            // 
            this.btnDownloadPNG.ImageOptions.Image = ((System.Drawing.Image)(resources.GetObject("btnDownloadPNG.ImageOptions.Image")));
            this.btnDownloadPNG.Location = new System.Drawing.Point(3, 40);
            this.btnDownloadPNG.Name = "btnDownloadPNG";
            this.btnDownloadPNG.Size = new System.Drawing.Size(120, 35);
            this.btnDownloadPNG.TabIndex = 1;
            this.btnDownloadPNG.Text = "PNG";
            this.btnDownloadPNG.Click += new System.EventHandler(this.btnDownloadPNG_Click);
            // 
            // btnDownloadPDF
            // 
            this.btnDownloadPDF.ImageOptions.Image = ((System.Drawing.Image)(resources.GetObject("btnDownloadPDF.ImageOptions.Image")));
            this.btnDownloadPDF.Location = new System.Drawing.Point(3, 3);
            this.btnDownloadPDF.Name = "btnDownloadPDF";
            this.btnDownloadPDF.Size = new System.Drawing.Size(120, 35);
            this.btnDownloadPDF.TabIndex = 2;
            this.btnDownloadPDF.Text = "PDF";
            // 
            // btnDownload
            // 
            this.btnDownload.ImageOptions.Image = ((System.Drawing.Image)(resources.GetObject("btnDownload.ImageOptions.Image")));
            this.btnDownload.Location = new System.Drawing.Point(6, 5);
            this.btnDownload.Name = "btnDownload";
            this.btnDownload.Size = new System.Drawing.Size(120, 35);
            this.btnDownload.TabIndex = 2;
            this.btnDownload.Text = "Download";
            this.btnDownload.Click += new System.EventHandler(this.btnDownload_Click);
            // 
            // panelControl4
            // 
            this.panelControl4.Controls.Add(this.btnDownload);
            this.panelControl4.Controls.Add(this.btnPrint);
            this.panelControl4.Dock = System.Windows.Forms.DockStyle.Fill;
            this.panelControl4.Location = new System.Drawing.Point(763, 528);
            this.panelControl4.Name = "panelControl4";
            this.panelControl4.Size = new System.Drawing.Size(333, 47);
            this.panelControl4.TabIndex = 8;
            // 
            // btnPrint
            // 
            this.btnPrint.ImageOptions.Image = ((System.Drawing.Image)(resources.GetObject("btnPrint.ImageOptions.Image")));
            this.btnPrint.Location = new System.Drawing.Point(132, 5);
            this.btnPrint.Name = "btnPrint";
            this.btnPrint.Size = new System.Drawing.Size(115, 35);
            this.btnPrint.TabIndex = 3;
            this.btnPrint.Text = "Print";
            // 
            // frmZPL
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1096, 575);
            this.Controls.Add(this.panelControl4);
            this.Controls.Add(this.panelControl2);
            this.Controls.Add(this.panelControl1);
            this.Controls.Add(this.panelControl3);
            this.Name = "frmZPL";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "ZPL";
            this.Load += new System.EventHandler(this.btnZPL_Load);
            ((System.ComponentModel.ISupportInitialize)(this.panelControl3)).EndInit();
            this.panelControl3.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.panelControl1)).EndInit();
            this.panelControl1.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.gridZPLCon)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.gridZPL)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.imgZPL)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.imgZpl1)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.gridView2)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.panelControl2)).EndInit();
            this.panelControl2.ResumeLayout(false);
            this.zplPanel.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.flyoutPanel1)).EndInit();
            this.flyoutPanel1.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.panelControl4)).EndInit();
            this.panelControl4.ResumeLayout(false);
            this.ResumeLayout(false);

        }

        #endregion

        private DevExpress.XtraEditors.PanelControl panelControl3;
        private DevExpress.XtraEditors.SimpleButton btnOpenFile;
        private DevExpress.XtraEditors.PanelControl panelControl1;
        private DevExpress.XtraGrid.GridControl gridZPLCon;
        private DevExpress.XtraGrid.Views.BandedGrid.BandedGridView gridZPL;
        private DevExpress.XtraGrid.Views.BandedGrid.GridBand gridBand3;
        private DevExpress.XtraGrid.Views.Grid.GridView gridView2;
        private DevExpress.XtraEditors.Repository.RepositoryItemPictureEdit imgZPL;
        private DevExpress.XtraEditors.SimpleButton btnRedraw;
        private DevExpress.XtraEditors.Repository.RepositoryItemImageEdit imgZpl1;
        private DevExpress.XtraEditors.PanelControl panelControl2;
        private DevExpress.Utils.Design.ImagePanel zplPanel;
        private DevExpress.XtraEditors.PanelControl panelControl4;
        private DevExpress.XtraEditors.SimpleButton btnDownload;
        private DevExpress.XtraEditors.SimpleButton btnPrint;
        private DevExpress.Utils.FlyoutPanel flyoutPanel1;
        private DevExpress.XtraEditors.SimpleButton btnDownloadJPG;
        private DevExpress.XtraEditors.SimpleButton btnDownloadPNG;
        private DevExpress.XtraEditors.SimpleButton btnDownloadPDF;
    }
}