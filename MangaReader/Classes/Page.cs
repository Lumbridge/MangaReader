using System;
using System.IO;
using System.Drawing;

using static MangaReader.Classes.ImageDownloader;

namespace MangaReader.Classes
{
    class Page
    {
        // public
        public string PageNumber { get; set; }
        public string PageLink { get; set; }
        public string ImageLink { get; set; }
        public bool PageDownloaded { get; set; }

        // private
        private Image _PageImage { get; set; }

        /// <summary>
        /// returns the image of the page
        /// </summary>
        public Image PageImage
        {
            get
            {
                // check if we've downloaded the page before in this instance
                // if we've downloaded it then we skip downloading and just return the image
                //
                if(_PageImage == null)
                {
                    // downloads a stream of bytes using the download data function (from imageloader class)
                    //
                    byte[] imageData = GetDataByteArray(ImageLink);

                    // create a memory stream object using the byte array
                    //
                    MemoryStream stream = new MemoryStream(imageData);

                    // finally create an image object using the memory stream
                    //
                    Image img = Image.FromStream(stream);

                    // close the memory stream to prevent memory leaks
                    //
                    stream.Close();

                    // add the downloaded image to the page object
                    //
                    _PageImage = img;

                    // make the page image as downloaded
                    //
                    PageDownloaded = true;
                }

                // return the image object
                //
                return _PageImage;
            }
            set { _PageImage = value; }
        }

        /// <summary>
        /// page constructor
        /// </summary>
        public Page()
        {
            PageNumber = string.Empty;
            PageLink = string.Empty;
            ImageLink = string.Empty;
            _PageImage = null;
        }

        /// <summary>
        /// prints some information about the current page to the console
        /// </summary>
        public void Print()
        {
            Console.WriteLine("Page Number: {0}, Page Link: {1}, ImageLink: {2}", PageNumber, PageLink, ImageLink);
        }
    }
}
