using System;
using System.Net;
using System.Threading.Tasks;
using System.Collections.Generic;

using HtmlAgilityPack;

using static MangaReader.Classes.Common;

namespace MangaReader.Classes
{
    public class MangaChapter
    {
        // public variables
        //
        public string ChapterTitle { get; set; }
        public string ChapterLink { get; set; }
        public string ChapterNumber { get; set; }

        //private variables
        //
        private List<Page> _ChapterPages { get; set; }
        private int _PageCount { get; set; }

        /// <summary>
        /// chapter constructor
        /// </summary>
        public MangaChapter()
        {
            ChapterTitle = string.Empty;
            ChapterLink = string.Empty;
            ChapterNumber = string.Empty;
            PageCount = -1;

            _PageCount = -1;
            _ChapterPages = null;
        }

        /// <summary>
        /// returns all page information on the pages in the current chapter
        /// </summary>
        public List<Page> ChapterPages
        {
            get
            {
                if (_ChapterPages == null)
                {
                    // create a temporary list to store the pages we collect
                    //
                    List<Page> pages = new List<Page>();

                    // get the page count in this chapter
                    //
                    int p = PageCount;

                    // get page information for each page in the chapter
                    //
                    for (int i = 1; i < p + 1; i++)
                    {
                        // create these as local variables so we can use them as a start point for the image link creation
                        //
                        int pageNumber = i;
                        string pageLink = ChapterLink + "/page-" + i;

                        //
                        // scrape the page to find the image link
                        // (have to do this because we don't know the file type .png/.jpg & page number could be formatted as 001/0001/01)
                        //

                        // create the webclient object
                        //
                        WebClient webClient = new WebClient();

                        // download the webpage and store it
                        //
                        string page = webClient.DownloadString(pageLink);

                        // create our html document to store the html data
                        //
                        HtmlDocument doc = new HtmlDocument();

                        // load the html data to be parsed
                        //
                        doc.LoadHtml(page);

                        // create a local variable to store the parsed image link
                        //
                        string imageLink = string.Empty;

                        // create a local variable to store parsed file extension
                        //
                        string fileExtension = string.Empty;

                        try
                        {
                            // create a node using the path we created
                            //
                            var nodes = doc.DocumentNode.SelectNodes("//div[@id='main_content']");

                            // search through all of the nodes for one with the data we need
                            Parallel.ForEach(nodes, (n, state) => 
                            {
                                if (n.InnerHtml.Contains(pageNumber.ToString()))
                                {
                                    // strip away the data around the link
                                    //
                                    var temp = GetSubstringByString("<div align=\"center\"><a href=", "alt=\"", n.InnerHtml);
                                    temp = temp.Substring(temp.IndexOf("responsive\" src="));
                                    temp = temp.Replace("\"", "");
                                    temp = temp.Replace("responsive src=", "");

                                    // give the parsed link to the local variable we made
                                    //
                                    imageLink = temp;

                                    // get the file extension by making a substring of the last . in the link string
                                    //
                                    fileExtension = imageLink.Substring(imageLink.LastIndexOf('.'));

                                    // stop the parallel operation earlier because we've got the image link
                                    //
                                    state.Stop();
                                }
                            });
                        }
                        catch
                        {
                            // maybe the chapter is announced but not yet released
                            //
                            _PageCount = 0;
                        }

                        // add a new page object to the collection
                        //
                        pages.Add(new Page
                        {
                            PageNumber = pageNumber,
                            PageLink = pageLink,
                            ImageLink = imageLink,
                            FileExtension = fileExtension
                        });
                    }

                    // give the local pages object to the main object collection
                    //
                    _ChapterPages = pages;
                }

                // return all the pages
                //
                return _ChapterPages;
            }
            set
            {
                _ChapterPages = value;
            }
        }

        /// <summary>
        /// returns the page count of the current chapter
        /// </summary>
        public int PageCount
        {
            get
            {
                if(_PageCount == -1)
                {
                    // create the manga page url
                    //
                    string url = ChapterLink;

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
                    try
                    {
                        Parallel.ForEach(doc.DocumentNode.SelectNodes("//select[@id='pages']"), node =>
                        {
                            // create a substring of the innerhtml text we found
                            //
                            string t = node.InnerHtml.Substring(node.InnerHtml.LastIndexOf(">") + 1);

                            // add the parsed substring to the chapter object
                            //
                            _PageCount = int.Parse(t);
                        });
                    }
                    catch
                    {
                        // maybe the chapter is announced but not yet released
                        //
                        _PageCount = 0;
                    }
                }

                // return the chapter object with appended page count
                //
                return _PageCount;
            }
            set
            {
                _PageCount = value;
            }
        }

        /// <summary>
        /// prints some information about the current chapter to the console
        /// </summary>
        public void Print()
        {
            Console.WriteLine("Title: {0}, Link: {1}, Chapter Number: {2}, Page Count: {3}", ChapterTitle, ChapterLink, ChapterNumber, PageCount);
        }
    }
}
