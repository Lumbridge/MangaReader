using System;
using System.IO;
using System.Drawing;
using System.Threading.Tasks;
using System.Collections.Generic;

using HtmlAgilityPack;
using System.Net;

using static MangaReader.Classes.Common;
using static MangaReader.Classes.ImageDownloader;

namespace MangaReader.Classes
{
    class ParseMethods
    {
        public static List<MangaChapter> GetAllMangaChapters(string MangaTitle)
        {
            try
            {
                // create temporary list to store found chapters
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
                        //
                        Chapters.Add(new MangaChapter()
                        {
                            Title = chapterTitle,
                            ChapterLink = chapterLink,
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

        public static List<Page> GetAllChapterPages(MangaChapter chapter)
        {
            List<Page> pages = new List<Page>();

            // get the page count in this chapter
            //
            int p = int.Parse(GetChapterPageCount(chapter));

            // get page information for each page in the chapter
            //
            for(int i = 1; i < p + 1; i++)
            {
                // create these as local variables so we can use them as a start point for the image link creation
                //
                string pageNumber = i.ToString();
                string pageLink = chapter.ChapterLink + "/page-" + i;

                // Format the image link using the page link as a start point
                //
                string imageLink = pageLink.Insert(7, "cdn.");
                imageLink = imageLink.Insert(imageLink.IndexOf("/Manga-"), "/mangas");
                imageLink = imageLink.Replace("page-" + pageNumber, "");
                for (int j = 0; j < 3 - pageNumber.Length; j++)
                    imageLink = imageLink.Insert(imageLink.Length, "0");
                imageLink = imageLink = imageLink.Insert(imageLink.Length, pageNumber + ".jpg");
                imageLink = imageLink.Replace(".me", ".com");

                // add a new page object to the collection
                //
                pages.Add(new Page
                {
                    PageNumber = pageNumber,
                    PageLink = pageLink,
                    ImageLink = imageLink
                });
            }

            // return all the pages
            //
            return pages;
        }

        public static Image GetPageImage(Page page)
        {
            // downloads a stream of bytes using the download data function (from imageloader class)
            //
            byte[] imageData = GetDataByteArray(page.ImageLink);
            
            // create a memory stream object using the byte array
            //
            MemoryStream stream = new MemoryStream(imageData);

            // finally create an image object using the memory stream
            //
            Image img = Image.FromStream(stream);

            // close the memory stream to prevent memory leaks
            //
            stream.Close();

            // return the image object
            //
            return img;
        }

        public static string GetChapterPageCount(MangaChapter chapter)
        {
            // create the manga page url
            //
            string url = chapter.ChapterLink;

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
                    chapter.PageCount = t;
                });
            }
            catch
            {
                // maybe the chapter is announced but not yet released
                //
                chapter.PageCount = "0";
            }

            // return the chapter object with appended page count
            //
            return chapter.PageCount;
        }
    }
}
