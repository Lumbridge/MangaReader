using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MangaReader.Classes
{
    class MangaChapter
    {
        public string Title { get; set; }
        public string PageLink { get; set; }
        public string ChapterNumber { get; set; }
        public string PageCount { get; set; }

        public void Print()
        {
            Console.WriteLine("Title: {0}, Link: {1}, Chapter Number: {2}, Page Count: {3}", Title, PageLink, ChapterNumber, PageCount);
        }
    }
}
