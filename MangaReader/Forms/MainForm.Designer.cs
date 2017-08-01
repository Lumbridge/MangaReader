namespace MangaReader.Forms
{
    partial class MainForm
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
            this.comboBox_MangaTitle = new System.Windows.Forms.ComboBox();
            this.comboBox_MangaChapter = new System.Windows.Forms.ComboBox();
            this.comboBox_MangaPage = new System.Windows.Forms.ComboBox();
            this.button_BeginReading = new System.Windows.Forms.Button();
            this.button_DownloadEntireManga = new System.Windows.Forms.Button();
            this.SuspendLayout();
            // 
            // comboBox_MangaTitle
            // 
            this.comboBox_MangaTitle.FormattingEnabled = true;
            this.comboBox_MangaTitle.Location = new System.Drawing.Point(12, 12);
            this.comboBox_MangaTitle.Name = "comboBox_MangaTitle";
            this.comboBox_MangaTitle.Size = new System.Drawing.Size(258, 21);
            this.comboBox_MangaTitle.TabIndex = 0;
            this.comboBox_MangaTitle.SelectedIndexChanged += new System.EventHandler(this.comboBox_MangaTitle_SelectedIndexChanged);
            // 
            // comboBox_MangaChapter
            // 
            this.comboBox_MangaChapter.FormattingEnabled = true;
            this.comboBox_MangaChapter.Location = new System.Drawing.Point(12, 39);
            this.comboBox_MangaChapter.Name = "comboBox_MangaChapter";
            this.comboBox_MangaChapter.Size = new System.Drawing.Size(258, 21);
            this.comboBox_MangaChapter.TabIndex = 1;
            this.comboBox_MangaChapter.SelectedIndexChanged += new System.EventHandler(this.comboBox_MangaChapter_SelectedIndexChanged);
            // 
            // comboBox_MangaPage
            // 
            this.comboBox_MangaPage.FormattingEnabled = true;
            this.comboBox_MangaPage.Location = new System.Drawing.Point(12, 66);
            this.comboBox_MangaPage.Name = "comboBox_MangaPage";
            this.comboBox_MangaPage.Size = new System.Drawing.Size(258, 21);
            this.comboBox_MangaPage.TabIndex = 2;
            this.comboBox_MangaPage.SelectedIndexChanged += new System.EventHandler(this.comboBox_MangaPage_SelectedIndexChanged);
            // 
            // button_BeginReading
            // 
            this.button_BeginReading.Location = new System.Drawing.Point(12, 93);
            this.button_BeginReading.Name = "button_BeginReading";
            this.button_BeginReading.Size = new System.Drawing.Size(258, 31);
            this.button_BeginReading.TabIndex = 3;
            this.button_BeginReading.Text = "Begin Reading";
            this.button_BeginReading.UseVisualStyleBackColor = true;
            this.button_BeginReading.Click += new System.EventHandler(this.button_BeginReading_Click);
            // 
            // button_DownloadEntireManga
            // 
            this.button_DownloadEntireManga.Location = new System.Drawing.Point(12, 130);
            this.button_DownloadEntireManga.Name = "button_DownloadEntireManga";
            this.button_DownloadEntireManga.Size = new System.Drawing.Size(258, 31);
            this.button_DownloadEntireManga.TabIndex = 4;
            this.button_DownloadEntireManga.Text = "Download Entire Manga";
            this.button_DownloadEntireManga.UseVisualStyleBackColor = true;
            this.button_DownloadEntireManga.Click += new System.EventHandler(this.button_DownloadEntireManga_Click);
            // 
            // MainForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(284, 175);
            this.Controls.Add(this.button_DownloadEntireManga);
            this.Controls.Add(this.button_BeginReading);
            this.Controls.Add(this.comboBox_MangaPage);
            this.Controls.Add(this.comboBox_MangaChapter);
            this.Controls.Add(this.comboBox_MangaTitle);
            this.Name = "MainForm";
            this.Text = "Manga Reader Main Menu";
            this.Load += new System.EventHandler(this.MainForm_Load);
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.ComboBox comboBox_MangaTitle;
        private System.Windows.Forms.ComboBox comboBox_MangaChapter;
        private System.Windows.Forms.ComboBox comboBox_MangaPage;
        private System.Windows.Forms.Button button_BeginReading;
        private System.Windows.Forms.Button button_DownloadEntireManga;
    }
}

