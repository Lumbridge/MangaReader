using System;
using System.Windows.Forms;

using MangaReader.Classes;

using static MangaReader.Classes.ParseMethods;
using System.Threading.Tasks;

namespace MangaReader.Forms
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }

        // Create Manga Object
        //
        Manga manga = new Manga();

        int ctl = 0; // chapter to load
        int ptl = 0; // page to load

        private void Form1_Load(object sender, EventArgs e)
        {
            // set the title of our manga
            //
            manga.Title = "Hunter X Hunter";

            // run our parser to get all the manga chapters for the manga we've selected
            //
            manga.Chapters = GetAllMangaChapters(manga.Title);

            // reverse the chapters so they're in order
            //
            manga.Chapters.Reverse();

            // get all page links for a given chapter
            //
            manga.Chapters[ctl].ChapterPages = GetAllChapterPages(manga.Chapters[ctl]);

            // show a page in the form
            //
            pictureBox1.Image = GetPageImage(manga.Chapters[ctl].ChapterPages[ptl]);

            UpdateWindowTitle();
        }

        private void button_Right_Click(object sender, EventArgs e)
        {
            if (ptl != 0)
            {
                ptl--;
                pictureBox1.Image = GetPageImage(manga.Chapters[ctl].ChapterPages[ptl]);
                UpdateWindowTitle();
            }
            else
                MessageBox.Show("This is the first page!");
        }

        private void button_Left_Click(object sender, EventArgs e)
        {
            if (ptl < int.Parse(manga.Chapters[ctl].PageCount) - 1)
            {
                ptl++;
                pictureBox1.Image = GetPageImage(manga.Chapters[ctl].ChapterPages[ptl]);
                UpdateWindowTitle();
            }
            else
                MessageBox.Show("End of chapter.");
        }

        private void UpdateWindowTitle()
        {
            Text = "Manga Reader - " + manga.Title + ", Chapter " + manga.Chapters[ctl].ChapterNumber + ", Page " + manga.Chapters[ctl].ChapterPages[ptl].PageNumber;
        }
    }
}
