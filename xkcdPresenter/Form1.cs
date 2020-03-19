using System;
using System.Net;
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

            int randInt;
            do {
                randInt = rand.Next(1, maxComicNumber + 1);
            }
            while (randInt == currentComicNumber);

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

        private async void onLoad(object sender, EventArgs e)
        {
            await updateComic();
        }

        private async Task updateComic(int comicNumber = 0)
        {
            buttonPrev.Enabled = false;
            buttonNext.Enabled = false;

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
                if (currentComicNumber > 1)
                {
                    buttonPrev.Enabled = true;
                }

                if (currentComicNumber < maxComicNumber)
                {
                    buttonNext.Enabled = true;
                }


                labelTitle.Text = $"#{currentComic.Num}: {currentComic.Title}";

                Console.WriteLine($"Attempting to import image: {currentComic.Img}");
                if (currentComic.Img != null)
                {
                    try
                    {
                        
                        Image comicImage = imageFromUri(currentComic.Img);
                        //pictureComic.MaximumSize = comicImage.Size;
                        
                        pictureComic.Image = imageFromUri(currentComic.Img);
                    }
                    catch (Exception e)
                    {
                        labelTitle.Text = "Could not load comic";
                    }
                }
            }
            catch (Exception e)
            {
                Debug.WriteLine(e.Message + Environment.NewLine + e.StackTrace);
            }

        }

        private Image imageFromUri(Uri imageUri)
        {
            using (WebClient wc = new WebClient())
            {

                Image curImage = new Bitmap(wc.OpenRead(imageUri));
                return curImage;

            }
            
        }

    }
}
