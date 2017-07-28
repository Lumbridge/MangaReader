using System;
using System.Collections.Generic;

namespace MangaReader.Classes
{
    class MangaChapter
    {
        public string Title { get; set; }
        public string ChapterLink { get; set; }
        public string ChapterNumber { get; set; }
        public string PageCount { get; set; }
        public List<Page> ChapterPages { get; set; }

        public void Print()
        {
            Console.WriteLine("Title: {0}, Link: {1}, Chapter Number: {2}, Page Count: {3}", Title, ChapterLink, ChapterNumber, PageCount);
        }
    }
}
