using System;
using System.Net;
using System.Threading;
using System.Threading.Tasks;
using System.Collections.Generic;

using HtmlAgilityPack;

using static MangaReader.Classes.Common;

namespace MangaReader.Classes
{
    public class Manga
    {
        // public
        public string Title { get; set; }

        // private
        private int _TotalChapters { get; set; }
        private List<MangaChapter> _Chapters { get; set; }

        /// <summary>
        /// manga book constructor
        /// </summary>
        public Manga()
        {
            Title = string.Empty;

            Chapters = null;
            _Chapters = null;
            _TotalChapters = -1;
        }

        /// <summary>
        /// returns a list of all chapter titles, numbers and links
        /// </summary>
        public List<MangaChapter> Chapters
        {
            get
            {
                if(_Chapters == null)
                {
                    try
                    {
                        // create a local version of the title so we can manipulate it for use in links
                        //
                        string MangaTitle = Title;

                        // create temporary list to store found chapters
                        //
                        List<MangaChapter> temp = new List<MangaChapter>();

                        // check for whitespace before creating the page link
                        //
                        if (MangaTitle.Contains(" "))
                            MangaTitle = MangaTitle.Replace(" ", "-");

                        // create the manga page url
                        //
                        string url = "http://eatmanga.me/Manga-Scan/" + MangaTitle + "/";

                        // create the webclient object
                        //
                        WebClient webClient = new WebClient();

                        // download the webpage and store it
                        //
                        string page = webClient.DownloadString(url);

                        // create our html document to store the html data
                        //
                        HtmlDocument doc = new HtmlDocument();

                        // load the html data to be parsed
                        //
                        doc.LoadHtml(page);

                        // drill down into the html to find the data we need
                        //
                        foreach (HtmlNode node in doc.DocumentNode.SelectNodes("//ul[@id='updates']"))
                        {
                            foreach (HtmlNode node2 in node.SelectNodes(".//li[@class='title']"))
                            {
                                // add the html to a local variable so we can manipulate it without changing the original
                                //
                                string t = node2.InnerHtml;

                                // get the title of the chapter
                                //
                                string chapterTitle = GetSubstringByString("/\" title=\"", ", Read", t);

                                // get the link of the chapter page
                                //
                                string chapterLink = "http://eatmanga.me/Manga-Scan/" + MangaTitle + "/" + GetSubstringByString(MangaTitle + "/", "/\" title=", t);

                                // get the chapter number
                                //
                                string chapterNumber = chapterTitle.Substring(chapterTitle.LastIndexOf(MangaTitle) + MangaTitle.Length + 1).Trim();

                                if (chapterNumber.Contains(" "))
                                    chapterNumber = chapterNumber.Remove(chapterNumber.IndexOf(' ') + 1).Trim();

                                // add our parsed information to a chapter object
                                //
                                temp.Add(new MangaChapter()
                                {
                                    ChapterTitle = chapterTitle,
                                    ChapterLink = chapterLink,
                                    ChapterNumber = int.Parse(chapterNumber)
                                });
                            }
                        }

                        // reverse the list so it appears in order in the combo box
                        //
                        temp.Reverse();

                        // add our temporary chapter list to the main object collection
                        //
                        _Chapters = temp;
                    }
                    catch
                    {
                        // there was an error getting the chapters e.g. no internet
                        //
                        Console.WriteLine("Error fetching manga, check the title spelling.");
                        
                        // allocate an empty list to the main object collection so we don't attempt to get the chapters infinitely
                        //
                        _Chapters = new List<MangaChapter>();
                    }
                }

                // return the main chapter object collection
                //
                return _Chapters;
            }
            set
            {
                _Chapters = value;
            }
        }

        public int TotalChapters
        {
            get
            {
                // check if total chapters has been counter before
                //
                if(_TotalChapters == -1)
                {
                    // create a local int for us to increment in the parallel foreach
                    //
                    int temp = 0;

                    // count all the chapters in the collection
                    //
                    Parallel.ForEach(Chapters, c =>
                    {
                        Interlocked.Increment(ref temp);
                    });

                    // update the manga chapter count
                    //
                    _TotalChapters = temp;
                }
                
                // return the chapter count
                //
                return _TotalChapters;
            }
            set
            {
                _TotalChapters = value;
            }
        }
    }
}
