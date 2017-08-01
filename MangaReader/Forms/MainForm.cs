using System;
using System.IO;
using System.Windows.Forms;
using System.Collections.Generic;

using MangaReader.Classes;

using static MangaReader.Classes.Manga;
using static MangaReader.Classes.Common;
using static MangaReader.Classes.GlobalVariables;

namespace MangaReader.Forms
{
    public partial class MainForm : Form
    {
        public MainForm()
        {
            InitializeComponent();
        }

        private void MainForm_Load(object sender, EventArgs e)
        {
            // get a list of all manga on eatmanga website
            //
            List<string> MangaList = GetMangaList("http://eatmanga.me/Manga-Scan/");

            // add all manga titles to the combobox
            //
            foreach (var t in MangaList)
                loadedManga.Add(new Manga() { Title = t });

            foreach (var g in loadedManga)
                comboBox_MangaTitle.Items.Add(g.Title);

            // check that we have items in the collection before forcing
            // the index
            if(MangaList.Count > 0)
                comboBox_MangaTitle.SelectedIndex = 0;
        }
        
        private void comboBox_MangaTitle_SelectedIndexChanged(object sender, EventArgs e)
        {
            // clear the chapter box of items
            //
            comboBox_MangaChapter.Items.Clear();
            
            // clear the page box of items
            //
            comboBox_MangaPage.Items.Clear();

            // get the position in the mangalist of the manga we want to load
            //
            pos = GetIndexInList(loadedManga, comboBox_MangaTitle.Text);

            // populate the chapter combo box with the chapters of the manga we're searching for
            //
            foreach (var c in loadedManga[pos].Chapters)
            {
                comboBox_MangaChapter.Items.Add(c.ChapterTitle);
                c.Print();
            }
        }

        private void comboBox_MangaChapter_SelectedIndexChanged(object sender, EventArgs e)
        {
            // check that we've loaded manga pages before setting the default index
            // to avoid throwing exceptions
            //
            if (comboBox_MangaPage.Items.Count > 0)
                comboBox_MangaPage.SelectedIndex = 0;

            // clear the pages combobox
            //
            comboBox_MangaPage.Items.Clear();

            // add all the chapter's pages to the combo box
            //
            foreach (var p in loadedManga[pos].Chapters[comboBox_MangaChapter.SelectedIndex].ChapterPages)
                comboBox_MangaPage.Items.Add(p.PageNumber);

            // set the chapter to load to the combo box selection we've made
            //
            ctl = comboBox_MangaChapter.SelectedIndex;
        }

        private void comboBox_MangaPage_SelectedIndexChanged(object sender, EventArgs e)
        {
            // set the page to load to the page combo box selection we've made
            //
            ptl = comboBox_MangaPage.SelectedIndex;
        }

        private void button_BeginReading_Click(object sender, EventArgs e)
        {
            // create the reading window form
            //
            ReadingForm rf = new ReadingForm();

            // show the reading window form
            //
            rf.ShowDialog();
        }

        private void button_DownloadEntireManga_Click(object sender, EventArgs e)
        {
            // create save file dialog object
            //
            SaveFileDialog s = new SaveFileDialog();

            // set the save directory to the title of the manga we're going to be downloading
            //
            s.FileName = comboBox_MangaTitle.Text;

            // show the dialog and get the result back
            //
            var dialogResult = s.ShowDialog();

            // action if the user presses ok on the dialog window
            //
            if(dialogResult == DialogResult.OK)
            {
                // create the directory if it doesn't exist
                //
                if (!Directory.Exists(s.FileName))
                    Directory.CreateDirectory(s.FileName);

                // call the download function to download all chapters & pages to file
                //
                loadedManga[pos].DownloadChaptersToFile(s.FileName);
            }
        }
    }
}
