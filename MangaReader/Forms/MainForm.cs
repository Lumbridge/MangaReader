using System;
using System.IO;
using System.Windows.Forms;
using System.Collections.Generic;

using MangaReader.Classes;

using static MangaReader.Classes.Common;

namespace MangaReader.Forms
{
    public partial class MainForm : Form
    {
        // public variables
        //
        public Manga manga = new Manga();

        public int ctl = 0; // chapter to load
        public int ptl = 0; // page to load

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
                comboBox_MangaTitle.Items.Add(t);

            // check that we have items in the collection before forcing
            // the index
            if(MangaList.Count > 0)
                comboBox_MangaTitle.SelectedIndex = 0;

            // set the default title of the manga object
            //
            manga.Title = comboBox_MangaTitle.Text;
        }
        
        private void comboBox_MangaTitle_SelectedIndexChanged(object sender, EventArgs e)
        {
            // we nullify the chapters collection to make the chapters
            // getter method download the chapters of the new manga we've selected
            // if we don't do this then the chapters which are currently loaded won't change
            //
            manga.Chapters = null;

            // clear the chapter box of items
            //
            comboBox_MangaChapter.Items.Clear();
            
            // clear the page box of items
            //
            comboBox_MangaPage.Items.Clear();

            // set the new title of the manga we're searching for chapters on
            //
            manga.Title = comboBox_MangaTitle.Text;

            // populate the chapter combo box with the chapters of the manga
            // we're searching for
            //
            foreach (var c in manga.Chapters)
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
            if(comboBox_MangaPage.Items.Count > 0)
                comboBox_MangaPage.SelectedIndex = 0;

            // clear the pages combobox
            //
            comboBox_MangaPage.Items.Clear();

            // set the new title of the manga we're searching for chapters on
            //
            manga.Title = comboBox_MangaTitle.Text;

            // add all the chapter's pages to the combo box
            //
            foreach (var p in manga.Chapters[comboBox_MangaChapter.SelectedIndex].ChapterPages)
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
            ReadingForm rf = new ReadingForm(this);

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

                // create a string to pass into the download function as the path
                // to save downloaded manga chapters
                //
                //string t = s.FileName;
                
                // remove the file name at the end of the path so we just get a directory
                //
                //t = t.Remove(t.LastIndexOf('\\') + 1);

                // call the download function to download all chapters & pages to file
                //
                manga.DownloadChaptersToFile(s.FileName);
            }
        }
    }
}
