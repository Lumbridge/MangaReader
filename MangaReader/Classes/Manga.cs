using System;
using System.IO;
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
        /// downloads all chapters & pages to the hard drive
        /// </summary>
        /// <param name="savePath"></param>
        public void DownloadChaptersToFile(string savePath)
        {
            foreach(MangaChapter c in Chapters)
            {
                // create a new folder for each chapter
                //
                Directory.CreateDirectory(@savePath + "\\" + c.ChapterTitle);

                // creates a local directory variable to make code easier to read
                //
                string currentDirectory = @savePath + "\\" + c.ChapterTitle + '\\';

                foreach(Page p in c.ChapterPages)
                {
                    // create a local current page variable to be used in the download file function
                    //
                    string currentPage = currentDirectory + p.PageNumber + p.FileExtension;

                    // open a webclient instance
                    //
                    WebClient client = new WebClient();

                    // download the page image using the local link we created
                    //
                    client.DownloadFile(p.ImageLink, currentPage);
                    Console.WriteLine("Downloaded: {0}", currentPage);
                }
            }
        }

        /// <summary>
        /// returns a list of all chapter titles, numbers and links
        /// </summary>
        public List<MangaChapter> Chapters
        {
            get
            {
                // create a local version of the title so we can manipulate it for use in links
                //
                string MangaTitle = Title;

                // create temporary list to store found chapters
                //
                List<MangaChapter> temp = new List<MangaChapter>();

                // create our html document to store the html data
                //
                HtmlDocument doc = new HtmlDocument();

                if (_Chapters == null)
                {
                    try
                    {
                        // check for whitespace before creating the page link
                        //
                        if (MangaTitle.Contains(" "))
                            MangaTitle = MangaTitle.Replace(" ", "-");

                        // create the manga page url
                        //
                        string url = "http://eatmanga.me/Manga-Scan/" + MangaTitle + "/";

                        Console.WriteLine(url);

                        // create the webclient object
                        //
                        WebClient webClient = new WebClient();

                        // download the webpage and store it
                        //
                        string page = webClient.DownloadString(url);
                        
                        // load the html data to be parsed
                        //
                        doc.LoadHtml(page);
                    }
                    catch
                    {
                        Console.WriteLine("Failed to download webpage");
                    }

                    try
                    {
                        // drill down into the html to find the data we need
                        //
                        var nodes = doc.DocumentNode.SelectNodes("//div[@class='col-xs-8']");

                        foreach(var node in nodes)
                        {
                            // add the html to a local variable so we can manipulate it without changing the original
                            //
                            string t = node.InnerHtml;

                            // get the title of the chapter
                            //
                            string chapterTitle = node.InnerText.Trim();

                            // get the link of the chapter page
                            //
                            string chapterLink = "http://eatmanga.me/Manga-Scan/" + MangaTitle + "/" + GetSubstringByString(MangaTitle + "/", "/\" title=", t);
                            
                            // get the number of spaces in the manga title
                            //
                            int x = GetOccuranceOfCharacter(MangaTitle, '-') + 1;

                            // get the index of the final space so we can get the chapter number
                            //
                            int y = GetIndexOfAfterOccuranceCount(chapterTitle, ' ', x) + 1;

                            // get the chapter number
                            //
                            string chapterNumber = chapterTitle.Substring(y);
                            
                            if (chapterNumber.Contains(" "))
                                chapterNumber = chapterNumber.Remove(chapterNumber.IndexOf(' ') + 1).Trim();

                            // add our parsed information to a chapter object
                            //
                            temp.Add(new MangaChapter()
                            {
                                ChapterTitle = chapterTitle,
                                ChapterLink = chapterLink,
                                ChapterNumber = chapterNumber
                            });
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

        /// <summary>
        /// returns the total number of chapters in the manga
        /// </summary>
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

        /// <summary>
        /// returns the index of a god in the manga list
        /// </summary>
        /// <param name="mangaList"></param>
        /// <param name="searchTitle"></param>
        /// <returns></returns>
        public static int GetIndexInList(List<Manga> mangaList, string searchTitle)
        {
            int index = -1;

            Parallel.For(0, mangaList.Count, i =>
            {
                if (mangaList[i].Title == searchTitle)
                    index = i;
            });

            return index;
        }
    }
}
