using System;
using System.IO;
using System.Net;

using System.Windows.Forms;

namespace MangaReader.Classes
{
    class ImageDownloader
    {
        /// <summary>
        /// Downloads a file from a given URL (used for image downloading in this program)
        /// </summary>
        /// <param name="url"></param>
        /// <returns></returns>
        public static byte[] GetDataByteArray(string url)
        {
            // create final byte array storage for use later
            //
            byte[] DataByteArray = new byte[0];

            try
            {
                // create a web request to open the specified url
                //
                WebRequest req = WebRequest.Create(url);

                // get a response from the opened url
                //
                WebResponse response = req.GetResponse();
                
                // create a memory stream from the response
                //
                Stream stream = response.GetResponseStream();

                // create a download buffer to store parts of the data
                //
                byte[] buffer = new byte[1024];

                // get the total size of the download so we can use it to progress report
                //
                int dataLength = (int)response.ContentLength;

                // image is downloaded to memory using a TFTP-like pattern
                MemoryStream memStream = new MemoryStream();
                while (true)
                {
                    // read the data from the stream
                    //
                    int bytesRead = stream.Read(buffer, 0, buffer.Length);

                    // if the stream is empty then we are done
                    //
                    if (bytesRead == 0)
                    {
                        break;
                    }
                    else
                    {
                        // if we found data in the stream then write it to memory
                        //
                        memStream.Write(buffer, 0, bytesRead);
                    }
                }

                // add the written memory to a byte array which we will return
                //
                DataByteArray = memStream.ToArray();

                // close our objects to prevent memory leaks
                //
                stream.Close();
                memStream.Close();
            }
            catch (Exception)
            {
                MessageBox.Show("There was an error accessing the URL.");
            }

            return DataByteArray;
        }
    }
}
