using System;
using System.Drawing;

namespace MangaReader.Classes
{
    class Page
    {
        public string PageNumber { get; set; }
        public string PageLink { get; set; }
        public string ImageLink { get; set; }
        public Image PageImage { get; set; }

        public void Print()
        {
            Console.WriteLine("Page Number: {0}, Page Link: {1}, ImageLink: {2}", PageNumber, PageLink, ImageLink);
        }
    }
}
