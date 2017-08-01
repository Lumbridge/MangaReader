using System;
using System.Windows.Forms;

using MangaReader.Classes;

using static MangaReader.Classes.GlobalVariables;

namespace MangaReader.Forms
{
    public partial class ReadingForm : Form
    {
        public ReadingForm()
        {
            InitializeComponent();
        }

        private void ReadingForm_Load(object sender, EventArgs e)
        {
            // load the page image into the picturebox
            //
            pictureBox1.Image = loadedManga[pos].Chapters[ctl].ChapterPages[ptl].PageImage;

            // update the window text to reflect the current location in the manga
            //
            UpdateWindowTitle();
        }

        private void button_Right_Click(object sender, EventArgs e)
        {
            if (!isFirstPageOfChapter()) // previous page of same chapter
            {
                // decrement the page to load index
                //
                ptl--;

                // load the page image into the picturebox
                //
                pictureBox1.Image = loadedManga[pos].Chapters[ctl].ChapterPages[ptl].PageImage;

                // update the window text to reflect the current location in the manga
                //
                UpdateWindowTitle();
            }
            else if (isFirstChapter() && isFirstPageOfChapter()) // first page of first chapter
            {
                // show message box with basic info
                //
                MessageBox.Show("This is the first chapter!");
            }
            else // last page of previous chapter
            {
                // decrement chapter to load index
                //
                ctl--;

                // set page to load to the last page of the previous chapter
                //
                ptl = loadedManga[pos].Chapters[ctl].PageCount - 1;

                // load the page image into the picturebox
                //
                pictureBox1.Image = loadedManga[pos].Chapters[ctl].ChapterPages[ptl].PageImage;

                // update the window text to reflect the current location in the manga
                //
                UpdateWindowTitle();
            }
        }

        private void button_Left_Click(object sender, EventArgs e)
        {
            if (!isLastPageOfChapter()) // next page of same chapter
            {
                // increment the page to load index
                //
                ptl++;

                // load the next page image into the picturebox
                //
                pictureBox1.Image = loadedManga[pos].Chapters[ctl].ChapterPages[ptl].PageImage;

                // update the window text to reflect the current location in the manga
                //
                UpdateWindowTitle();
            }
            else if (isLastPageOfChapter() && isLastChapter())
            {
                // show message box with basic info
                //
                MessageBox.Show("There are no more pages!");
            }
            else // first page of next chapter
            {
                // increment the chapter index
                //
                ctl++;

                // set the page index to 0 (first page of the next chapter)
                ptl = 0;

                // load the page image into the picturebox
                //
                pictureBox1.Image = loadedManga[pos].Chapters[ctl].ChapterPages[ptl].PageImage;

                // update the window text to reflect the current location in the manga
                //
                UpdateWindowTitle();
            }
        }

        /// <summary>
        /// updates the window text to give information on the location in the manga
        /// </summary>
        private void UpdateWindowTitle()
        {
            Text = "Manga Reader - " + loadedManga[pos].Title + ", Chapter " + loadedManga[pos].Chapters[ctl].ChapterNumber + ", Page " + loadedManga[pos].Chapters[ctl].ChapterPages[ptl].PageNumber;
        }

        /// <summary>
        /// returns whether the user is currently on the first chapter of the manga or not
        /// </summary>
        /// <returns></returns>
        private bool isFirstChapter()
        {
            return loadedManga[pos].Chapters[ctl].ChapterNumber == loadedManga[pos].Chapters[0].ChapterNumber ? true : false;
        }

        /// <summary>
        /// returns whether the user is currently on the last chapter of the manga or not
        /// </summary>
        /// <returns></returns>
        private bool isLastChapter()
        {
            return loadedManga[pos].Chapters[ctl].ChapterNumber == loadedManga[pos].Chapters[loadedManga[pos].TotalChapters - 1].ChapterNumber ? true : false;
        }

        /// <summary>
        /// returns whether the user is currently on the first page of the chapter of the manga or not
        /// </summary>
        /// <returns></returns>
        private bool isFirstPageOfChapter()
        {
            return loadedManga[pos].Chapters[ctl].ChapterPages[ptl].PageNumber == loadedManga[pos].Chapters[0].ChapterPages[0].PageNumber ? true : false;
        }

        /// <summary>
        /// returns whether the user is currently on the last page of the chapter of the manga or not
        /// </summary>
        /// <returns></returns>
        private bool isLastPageOfChapter()
        {
            return loadedManga[pos].Chapters[ctl].ChapterPages[ptl].PageNumber == loadedManga[pos].Chapters[ctl].ChapterPages[loadedManga[pos].Chapters[ctl].PageCount - 1].PageNumber ? true : false;
        }
    }
}