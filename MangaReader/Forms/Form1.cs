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

            Console.WriteLine("Chapters loaded, getting chapter pages.");

            var watch = System.Diagnostics.Stopwatch.StartNew();

            // get the total number of pages for each chapter
            // 
            int totalPages = 0;

            foreach (var c in manga.Chapters)
                c.PageCount = GetChapterPageCount(c);

            watch.Stop();
            var elapsedMs = watch.ElapsedMilliseconds;


            // print all the chapters for the chosen manga
            //
            foreach (MangaChapter c in manga.Chapters)
                c.Print();

            Console.WriteLine("{0} has {1} chapters and {2} total pages.", manga.Title, manga.Chapters.Count, totalPages);

            Console.WriteLine("Completed in {0} ms", elapsedMs);
        }
    }
}
