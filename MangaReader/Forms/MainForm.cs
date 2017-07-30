using System;
using System.Collections.Generic;
using System.Windows.Forms;

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

            manga.Title = comboBox_MangaTitle.Text;
        }
        
        private void comboBox_MangaTitle_SelectedIndexChanged(object sender, EventArgs e)
        {
            // clear the chapter box of items
            //
            comboBox_MangaChapter.Items.Clear();
            
            // clear the page box of items
            //
            comboBox_MangaPage.Items.Clear();

            // set the new title of the manga we're searching for chapters on
            //
            manga.Title = comboBox_MangaTitle.Text;
            
            Console.WriteLine(manga.Title);

            // populate the chapter combo box with the chapters of the manga
            // we're searching for
            //
            foreach (var c in manga.Chapters)
            {
                comboBox_MangaChapter.Items.Add(c.ChapterTitle);
                Console.WriteLine(c.ChapterTitle);
            }
        }

        private void comboBox_MangaChapter_SelectedIndexChanged(object sender, EventArgs e)
        {
            // clear the pages combobox
            //
            comboBox_MangaPage.Items.Clear();

            // set the new title of the manga we're searching for chapters on
            //
            manga.Title = comboBox_MangaTitle.Text;

            foreach (var p in manga.Chapters[comboBox_MangaChapter.SelectedIndex].ChapterPages)
                comboBox_MangaPage.Items.Add(p.PageNumber);

            ctl = comboBox_MangaChapter.SelectedIndex;
        }

        private void comboBox_MangaPage_SelectedIndexChanged(object sender, EventArgs e)
        {
            ptl = comboBox_MangaPage.SelectedIndex;
        }

        private void button_BeginReading_Click(object sender, EventArgs e)
        {
            ReadingForm rf = new ReadingForm(this);
            rf.ShowDialog();
        }
    }
}
