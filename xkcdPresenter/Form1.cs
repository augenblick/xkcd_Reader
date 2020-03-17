using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Diagnostics;

namespace xkcdPresenter
{
    public partial class Form1 : Form
    {

        private int maxComicNumber;
        private int currentComicNumber;

        public Form1()
        {
            InitializeComponent();
            updateComic();
        }

        private async void buttonPrev_Click(object sender, EventArgs e)
        {
            if (buttonPrev.Enabled)
            {
               await updateComic(currentComicNumber - 1);
            }
        }

        private async void buttonRand_Click(object sender, EventArgs e)
        {
            Random rand = new Random();

            int randInt = rand.Next(1, maxComicNumber + 1);
            await updateComic(randInt);
            return;
        }

        private async void buttonNext_Click(object sender, EventArgs e)
        {
            if (buttonNext.Enabled)
            {
                await updateComic(currentComicNumber + 1);
            }
        }

        private async Task updateComic(int comicNumber = 0)
        {
            ApiHelper xkcdHelper = new ApiHelper();
            try
            {
                Comic currentComic = await xkcdHelper.retrieveXkcd(comicNumber);


                if (comicNumber == 0)
                {
                    // we've retrieved the most recent comic
                    maxComicNumber = currentComic.Num;
                }

                currentComicNumber = currentComic.Num;

                // enable or disable buttons if necessary
                if (currentComicNumber == 1)
                {
                    buttonPrev.Enabled = false;
                }
                else
                {
                    buttonPrev.Enabled = true;
                }
                if (currentComicNumber == maxComicNumber)
                {
                    buttonNext.Enabled = false;
                }
                else
                {
                    buttonNext.Enabled = true;
                }

                labelTitle.Text = $"#{currentComic.Num}: {currentComic.Title}";
                //pictureComic.LoadAsync(currentComic.Image.ToString());
            }
            catch (Exception e)
            {
                Debug.WriteLine(e.Message + Environment.NewLine + e.StackTrace);
            }

        }
    }
}
