using System.Collections.Generic;

namespace MangaReader.Classes
{
    class GlobalVariables
    {
        //
        // global variables
        //

        // a collection of all manga we can scrape from the internet
        //
        public static List<Manga> loadedManga = new List<Manga>();

        // keeps track of the selected manga
        //
        public static int pos = -1;

        // keeps track of the chapter the user is reading
        //
        public static int ctl = 0;

        // keeps track of the page the user is reading
        //
        public static int ptl = 0;
    }
}
