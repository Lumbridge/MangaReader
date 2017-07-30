using System;
using System.Collections.Generic;
using System.Windows.Forms;

using MangaReader.Classes;

using static MangaReader.Classes.Common;

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
            if (ptl != 0) // previous page of same chapter
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
            else if (ctl == 0 && ptl == 0) // first page of first chapter
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
            if (ptl < manga.Chapters[ctl].PageCount - 1) // next page of same chapter
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
            else if (ctl == manga.TotalChapters && ptl == manga.Chapters[ctl].PageCount)
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
    }
}