using System;
using System.Windows.Forms;

using MangaReader.Classes;

namespace MangaReader.Forms
{
    public partial class ReadingForm : Form
    {
        private int ctl = 0, ptl = 0;
        private Manga manga;
        private MainForm _MainFormHandle;

        public ReadingForm(MainForm mf)
        {
            InitializeComponent();

            _MainFormHandle = mf;
            ctl = mf.ctl;
            ptl = mf.ptl;
            manga = mf.manga;
        }

        private void ReadingForm_Load(object sender, EventArgs e)
        {
            // load the page image into the picturebox
            //
            pictureBox1.Image = manga.Chapters[ctl].ChapterPages[ptl].PageImage;

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
                pictureBox1.Image = manga.Chapters[ctl].ChapterPages[ptl].PageImage;

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
                ptl = manga.Chapters[ctl].PageCount - 1;

                // load the page image into the picturebox
                //
                pictureBox1.Image = manga.Chapters[ctl].ChapterPages[ptl].PageImage;

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
                pictureBox1.Image = manga.Chapters[ctl].ChapterPages[ptl].PageImage;

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
                pictureBox1.Image = manga.Chapters[ctl].ChapterPages[ptl].PageImage;

                // update the window text to reflect the current location in the manga
                //
                UpdateWindowTitle();
            }
        }

        private void UpdateWindowTitle()
        {
            Text = "Manga Reader - " + manga.Title + ", Chapter " + manga.Chapters[ctl].ChapterNumber + ", Page " + manga.Chapters[ctl].ChapterPages[ptl].PageNumber;
        }

        private bool isFirstChapter()
        {
            if (manga.Chapters[ctl].ChapterNumber == manga.Chapters[0].ChapterNumber)
                return true;
            else
                return false;
        }

        private bool isLastChapter()
        {
            if (manga.Chapters[ctl].ChapterNumber == manga.TotalChapters)
                return true;
            else
                return false;
        }

        private bool isFirstPageOfChapter()
        {
            if (manga.Chapters[ctl].ChapterPages[ptl].PageNumber == manga.Chapters[0].ChapterPages[0].PageNumber)
                return true;
            else
                return false;
        }

        private bool isLastPageOfChapter()
        {
            if (manga.Chapters[ctl].ChapterPages[ptl].PageNumber == manga.Chapters[ctl].ChapterPages[manga.Chapters[ctl].PageCount - 1].PageNumber)
                return true;
            else
                return false;
        }
    }
}