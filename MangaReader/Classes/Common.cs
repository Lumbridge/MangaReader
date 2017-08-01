using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using HtmlAgilityPack;
using System.Net;

namespace MangaReader.Classes
{
    class Common
    {
        /// <summary>
        /// Returns a substring between two sub-parts of the string
        /// </summary>
        /// <param name="a"></param>
        /// <param name="b"></param>
        /// <param name="c"></param>
        /// <returns></returns>
        public static string GetSubstringByString(string a, string b, string c)
        {
            try { return c.Substring((c.IndexOf(a) + a.Length), (c.IndexOf(b) - c.IndexOf(a) - a.Length)); }
            catch { Console.WriteLine("ERROR CREATING SUBSTRING"); return "ERROR CREATING SUBSTRING"; }
        }

        /// <summary>
        /// returns the number of occurances of a character within a string
        /// </summary>
        /// <param name="word"></param>
        /// <param name="character"></param>
        /// <returns></returns>
        public static int GetOccuranceOfCharacter(string word, char character)
        {
            // create a counter variable and set it to 0
            //
            int count = 0;

            // loop through each character in the word
            //
            foreach (char c in word)
                // if the character matches the character we're searching for then...
                //
                if (c == character)
                    // ... increment our counter
                    //
                    count++;

            // return the final count
            return count;
        }

        public static int GetIndexOfAfterOccuranceCount(string word, char character, int count)
        {
            int counter = 0;

            for(int i = 0; i < word.Length; i++)
            {
                if (word[i] == character)
                    counter++;

                if (counter == count)
                    return i;
            }

            // failed for some reason
            return -1;
        }

        /// <summary>
        /// gets a list of all the mangas from eatmanga (probably going to add more sources in future)
        /// </summary>
        /// <param name="url"></param>
        /// <returns></returns>
        public static List<string> GetMangaList(string url)
        {
            // create a temp list of mangas found
            //
            List<string> temp = new List<string>();

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

            // get the root of the nodes we want to look inside
            //
            var nodes = doc.DocumentNode.SelectNodes("//li[@class='item-dr']");

            // iterate through each of the root nodes
            //
            foreach (var n in nodes)
            {
                // look inside the strong tags for the manga title
                //
                Parallel.ForEach(n.SelectNodes(".//strong"), m =>
                {
                    // add the manga title to the list
                    //
                    temp.Add(m.InnerText);
                });
            }

            return temp;
        }
    }
}
