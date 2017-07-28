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

        private void Form1_Load(object sender, EventArgs e)
        {
            var watch = System.Diagnostics.Stopwatch.StartNew();

            // Create Manga Object
            //
            Manga manga = new Manga();

            // set the title of our manga
            //
            manga.Title = "Naruto";

            // run our parser to get all the manga chapters for the manga we've selected
            //
            manga.Chapters = GetAllMangaChapters(manga.Title);

            // reverse the chapters so they're in order
            //
            manga.Chapters.Reverse();

            int ctl = 400;
            int ptl = 1;

            // get all page links for a given chapter
            //
            manga.Chapters[ctl].ChapterPages = GetAllChapterPages(manga.Chapters[ctl]);

            // show a page in the form
            //
            pictureBox1.Image = GetPageImage(manga.Chapters[ctl].ChapterPages[ptl]);
        }
    }
}
