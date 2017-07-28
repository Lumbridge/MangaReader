using System;
using System.Threading.Tasks;
using System.Collections.Generic;

using HtmlAgilityPack;
using System.Net;

using static MangaReader.Classes.Common;

namespace MangaReader.Classes
{
    class ParseMethods
    {
        public static List<MangaChapter> GetAllMangaChapters(string MangaTitle)
        {
            try
            {
                // create temporary list to store found skins
                //
                List<MangaChapter> Chapters = new List<MangaChapter>();

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
                        string t = node2.InnerHtml;

                        string chapterTitle = GetSubstringByString("/\" title=\"", ", Read", t);
                        string chapterLink = "http://eatmanga.me/Manga-Scan/" + MangaTitle + "/" + GetSubstringByString(MangaTitle + "/", "/\" title=", t);
                        string chapterNumber = chapterTitle.Substring(chapterTitle.LastIndexOf(MangaTitle) + MangaTitle.Length + 1).Trim();

                        // extract the title and link sub strings and create a new chapter object
                        Chapters.Add(new MangaChapter()
                        {
                            Title = chapterTitle,
                            PageLink = chapterLink,
                            ChapterNumber = chapterNumber
                        });
                    }
                }
                return Chapters;
            }
            catch
            {
                Console.WriteLine("Error fetching manga, check the title spelling.");
                return new List<MangaChapter>();
            }
        }

        public static string GetChapterPageCount(MangaChapter chapter)
        {
            // create the manga page url
            //
            string url = chapter.PageLink;

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
                    string t = node.InnerHtml.Substring(node.InnerHtml.LastIndexOf(">") + 1);

                    chapter.PageCount = t;
                });
            }
            catch
            {
                chapter.PageCount = "0";

                //return chapter;
            }

            return chapter.PageCount;
        }
    }

}
