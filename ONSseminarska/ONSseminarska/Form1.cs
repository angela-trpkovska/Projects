using System;
using System.Collections.Generic;
using System.Drawing;
using System.Windows.Forms;
using Accord.Imaging;
using Accord.Imaging.Filters;
using Accord.Math;
using AForge;




namespace ONSseminarska
{

  public partial class Form1 : Form
    {
        SaveFileDialog sDlg = new SaveFileDialog();

       
          private Bitmap img1;
          private Bitmap img2;
          //private Bitmap img3;

          Image panoramapic;
          
        double zoomFactor = 1.0;

      
     
         //nizi za tockite od interes
         private IntPoint[] harrisPoints1;
         private IntPoint[] harrisPoints2;

         // correlated points
         private IntPoint[] correlationPoints1;
         private IntPoint[] correlationPoints2;

         // The homography matrix estimated by RANSAC
         private MatrixH homography;
        
       
         List<PictureBox> listPB;
        
        public Form1()
        {
            InitializeComponent();
            listPB = new List<PictureBox>();
           
            listPB.Add(pictureBox1);
            listPB.Add(pictureBox2);
            listPB.Add(pictureBox6);
            listPB.Add(pictureBox7);


            button7.Visible = false;
        }

        private void button1_Click(object sender, EventArgs e)
        {
            OpenFileDialog oDlg = new OpenFileDialog();
          
          if (oDlg.ShowDialog() == DialogResult.OK)
            {
                foreach (PictureBox pb in listPB)
                {
                  if (pb.Image == null)
                    {
                        pb.Load(oDlg.FileName);
                        Image img = pb.Image;

                        //Adjust the image size after loading it to Picture box
                        if (pb.Width < img.Width && pb.Height < img.Height)
                        {
                            pb.SizeMode = PictureBoxSizeMode.Zoom;
                        }
                        else
                        {
                            pb.SizeMode = PictureBoxSizeMode.Normal;
                        }
                        break;
                    }
                }
           }
        }

        private void Form1_Load(object sender, EventArgs e)
        {

        }

        //funkcija koja sluzi za soedinuvanje na dve sliki
        protected Image CombineImages(Bitmap img1,Bitmap img2)
        {
            //1.Pronaoganje na tockite od interes,so koristenje na Harris Corners Detector so threshold=1000
            HarrisCornersDetector harris = new HarrisCornersDetector(0.04f, 1000f);
            harrisPoints1 = harris.ProcessImage(img1).ToArray();
            harrisPoints2 = harris.ProcessImage(img2).ToArray();
            
            Bitmap img1mark = new PointsMarker(harrisPoints1).Apply(img1);
            Bitmap img2mark = new PointsMarker(harrisPoints2).Apply(img2);

            Concatenate concatenate = new Concatenate(img1mark);
            Image resultImage = concatenate.Apply(img2mark);
      
            //2.Correlation Matching
            //Otkako ke gi najdeme tockite od interes potrebno e da gi korelirame,za toa koristime maximum correlation rule za da gi opredelime sovpaganjata pomegju dvete sliki.
            //Se analizira prozorec od tri pikseli okolu sekoja tocka vo prvata slika i ja korelirame so prozorec od tri pikseli okolu sekoja druga tocka vo vtorata slika.
            //Tockite koi imaat maksimalna dvonasocna korelacija ke bidat zemeni kako parovi na tocki koi se sovpagjaat.
            
            CorrelationMatching matcher = new CorrelationMatching(3);
            IntPoint[][] matches = matcher.Match(img1, img2, harrisPoints1, harrisPoints2);

          
            correlationPoints1 = matches[0];
            correlationPoints2 = matches[1];


        
            Concatenate concat = new Concatenate(img1);
            Bitmap img3 = concat.Apply(img2);
            resultImage = img3;
            

            //3.Robust Homography Estimation
            //Otkako imame dve mnozestva od korelacioni tocki potrebno ni e da definirame model koj ke gi translira tockite od ednoto vo drugoto mnozestvo.
            //Potrebno ni e nekoj tip na image transformation koj ke se koristi za da ja proketirame edna od slikite vrz drugata taka sto ke se sovpagjaat povekjeto od korelacionite tocki,odnosno potrebna ni e homography matrix.
          

            //Da se dobie robusten model od podatocite,go koristime metodot RANSAC.Toa e iterativen metod za presmetuvanje na robusten parametar  koj ke odgovara na matematickiot model od mnozestvata na nabljuduvanite podatoci.
            //Osnovnata pretpostavka na ovoj metod e deka podatocite sodrzat inliers,t.e podatoci cija distribucija moze da se objasni so nekoj matematicki model i outliers,podatoci koi ne se vklopuvaat vo matematickiot model.
            //Outliers se tocki koi moze da se pojavat poradi nekakov sum,nekorektni podatoci i sl.
          
            //Za problemot so homography estimation, RANSAC proveruva povekje modeli i go zema onoj koj ima najmnogu sopvaganja na tocki.Na ovoj nacin RANSAC gi ostava oni tocki koi se sovpagjaat na dvete sliki,a onie koi ne se sovpagjaat gi klasira kako outliers.

            //  Create the homography matrix using a robust estimator
            RansacHomographyEstimator ransac = new RansacHomographyEstimator(0.001, 0.99);
            homography = ransac.Estimate(correlationPoints1, correlationPoints2);

            // Plot RANSAC results against correlation results
            IntPoint[] inliers1 = correlationPoints1.Submatrix(ransac.Inliers);
            IntPoint[] inliers2 = correlationPoints2.Submatrix(ransac.Inliers);

           
            Concatenate concat3 = new Concatenate(img1);
            Bitmap img33 = concat3.Apply(img2);
            
            resultImage = img33;
       
            pictureBox3.SizeMode = PictureBoxSizeMode.AutoSize;

            //4.Gradient Blending
            //Posledniot cekor e da gi spoime dvete sliki.Za toa koristime linear gradient alpha blending od centarot na edna slika do drugata.
            Blend blend = new Blend(homography, img1);
            resultImage = blend.Apply(img2);
            pictureBox3.SizeMode = PictureBoxSizeMode.Zoom;

            return resultImage;

         }


        private void button2_Click_1(object sender, EventArgs e)
        {
            img1 = new Bitmap(pictureBox1.Image);
            img2 = new Bitmap(pictureBox2.Image);
            pictureBox3.SizeMode = PictureBoxSizeMode.Zoom;

            
           Image result12 = CombineImages(img1, img2);
           Bitmap img12 = new Bitmap(result12);

           Bitmap img3 = new Bitmap(pictureBox6.Image);

         
           Image result23 = CombineImages(img12, img3);
           Bitmap img23 = new Bitmap(result23);

       
           Bitmap img4 = new Bitmap(pictureBox7.Image);
           Image final = CombineImages(img23, img4);
           pictureBox3.Image = final;

           panoramapic = final;

           button7.Visible = true;
        }


        private void checkBox1_CheckedChanged_1(object sender, EventArgs e)
        {
            if (checkBox1.Checked)
            {
                
                Bitmap bmap = new Bitmap(pictureBox3.Image);
                Color c;
                for (int i = 0; i < bmap.Width; i++)
                {
                    for (int j = 0; j < bmap.Height; j++)
                    {
                        c = bmap.GetPixel(i, j);
                        int nPixelR = 0;
                        int nPixelG = 0;
                        int nPixelB = 0;
                        
                        nPixelR = c.R;
                        nPixelG = c.G - 255;
                        nPixelB = c.B - 255;
                       

                        nPixelR = Math.Max(nPixelR, 0);
                        nPixelR = Math.Min(255, nPixelR);

                        nPixelG = Math.Max(nPixelG, 0);
                        nPixelG = Math.Min(255, nPixelG);

                        nPixelB = Math.Max(nPixelB, 0);
                        nPixelB = Math.Min(255, nPixelB);

                        bmap.SetPixel(i, j, Color.FromArgb((byte)nPixelR, (byte)nPixelG, (byte)nPixelB));
                    }
                }
               
                pictureBox3.Image = bmap;
            }
         }




        

        private void checkBox2_CheckedChanged(object sender, EventArgs e)
        {
           
            Bitmap bmap = new Bitmap(pictureBox3.Image);
            Color c;
            for (int i = 0; i < bmap.Width; i++)
            {
                for (int j = 0; j < bmap.Height; j++)
                {
                    c = bmap.GetPixel(i, j);
                    int nPixelR = 0;
                    int nPixelG = 0;
                    int nPixelB = 0;
                    
                    nPixelR = c.R - 255;
                    nPixelG = c.G;
                    nPixelB = c.B - 255;
                   

                    nPixelR = Math.Max(nPixelR, 0);
                    nPixelR = Math.Min(255, nPixelR);

                    nPixelG = Math.Max(nPixelG, 0);
                    nPixelG = Math.Min(255, nPixelG);

                    nPixelB = Math.Max(nPixelB, 0);
                    nPixelB = Math.Min(255, nPixelB);

                    bmap.SetPixel(i, j, Color.FromArgb((byte)nPixelR, (byte)nPixelG, (byte)nPixelB));
                }
            }
          
            pictureBox3.Image = bmap;
        }

        private void checkBox3_CheckedChanged(object sender, EventArgs e)
        {
          

            Bitmap bmap = new Bitmap(pictureBox3.Image);
            Color c;
            for (int i = 0; i < bmap.Width; i++)
            {
                for (int j = 0; j < bmap.Height; j++)
                {
                    c = bmap.GetPixel(i, j);
                    int nPixelR = 0;
                    int nPixelG = 0;
                    int nPixelB = 0;
                   
                    nPixelR = c.R - 255;
                    nPixelG = c.G - 255;
                    nPixelB = c.B;
                

                    nPixelR = Math.Max(nPixelR, 0);
                    nPixelR = Math.Min(255, nPixelR);

                    nPixelG = Math.Max(nPixelG, 0);
                    nPixelG = Math.Min(255, nPixelG);

                    nPixelB = Math.Max(nPixelB, 0);
                    nPixelB = Math.Min(255, nPixelB);

                    bmap.SetPixel(i, j, Color.FromArgb((byte)nPixelR, (byte)nPixelG, (byte)nPixelB));
                }
            }
          
            pictureBox3.Image = bmap;

        }

        private void button3_Click(object sender, EventArgs e)
        {
            if (sDlg.ShowDialog() == DialogResult.OK )
            {
                string filename = sDlg.FileName+".jpg";
               
                using (System.IO.FileStream fstream = new System.IO.FileStream(filename, System.IO.FileMode.Create))
                {
                    System.Drawing.Image image = pictureBox3.Image;
                    image.Save(fstream, System.Drawing.Imaging.ImageFormat.Jpeg);
                    fstream.Close();
                }
            }
        }

        
        


        private void checkBox4_CheckedChanged(object sender, EventArgs e)
        {
           Bitmap bmap = new Bitmap(pictureBox3.Image);
            Color c;
            for (int i = 0; i < bmap.Width; i++)
            {
                for (int j = 0; j < bmap.Height; j++)
                {
                    c = bmap.GetPixel(i, j);
                    byte gray = (byte)(.299 * c.R + .587 * c.G + .114 * c.B);

                    bmap.SetPixel(i, j, Color.FromArgb(gray, gray, gray));
                }
            }
            pictureBox3.Image = bmap;

        }

        private void button4_Click(object sender, EventArgs e)
        {
            int brightness = Convert.ToInt32(textBox1.Text);
            Bitmap bmap = new Bitmap(pictureBox3.Image);
            if (brightness < -255) 
                brightness = -255;
            if (brightness > 255) 
                brightness = 255;
            Color c;
            for (int i = 0; i < bmap.Width; i++)
            {
                for (int j = 0; j < bmap.Height; j++)
                {
                    c = bmap.GetPixel(i, j);
                    int cR = c.R + brightness;
                    int cG = c.G + brightness;
                    int cB = c.B + brightness;

                    if (cR < 0) 
                        cR = 1;
                    if (cR > 255) 
                        cR = 255;

                    if (cG < 0) 
                        cG = 1;
                    if (cG > 255) 
                        cG = 255;

                    if (cB < 0) 
                        cB = 1;
                    if (cB > 255) 
                        cB = 255;

                    bmap.SetPixel(i, j, Color.FromArgb((byte)cR, (byte)cG, (byte)cB));
                }
            }
            pictureBox3.Image = bmap;
        }

        private void button5_Click(object sender, EventArgs e)
        {
            //int contrast = Convert.ToInt16(textBox2.Text);
            double contrast = Convert.ToDouble(textBox2.Text);
            Bitmap bmap = new Bitmap(pictureBox3.Image);

            if (contrast < -100)
                contrast = -100;
            if (contrast > 100) 
                contrast = 100;

            contrast = (100.0 + contrast) / 100.0;
            contrast *= contrast;
            Color c;
            for (int i = 0; i < bmap.Width; i++)
            {
                for (int j = 0; j < bmap.Height; j++)
                {
                    c = bmap.GetPixel(i, j);
                    double pR = c.R / 255.0;
                    pR -= 0.5;
                    pR *= contrast;
                    pR += 0.5;
                    pR *= 255;

                    if (pR < 0) 
                        pR = 0;
                    if (pR > 255) 
                        pR = 255;

                    double pG = c.G / 255.0;
                    pG -= 0.5;
                    pG *= contrast;
                    pG += 0.5;
                    pG *= 255;
                    if (pG < 0) 
                        pG = 0;
                    if (pG > 255) 
                        pG = 255;

                    double pB = c.B / 255.0;
                    pB -= 0.5;
                    pB *= contrast;
                    pB += 0.5;
                    pB *= 255;
                    if (pB < 0) pB = 0;
                    if (pB > 255) pB = 255;

                    bmap.SetPixel(i, j, Color.FromArgb((byte)pR, (byte)pG, (byte)pB));
                }
            }
            pictureBox3.Image = bmap;
        }

        private void checkBox5_CheckedChanged(object sender, EventArgs e)
        {
            zoomFactor = 0.5;
            Bitmap originalBitmap = new Bitmap(pictureBox3.Image);

            Size newSize = new Size((int)(originalBitmap.Width * zoomFactor), (int)(originalBitmap.Height * zoomFactor));
            Bitmap bmp = new Bitmap(originalBitmap, newSize);
            pictureBox3.Image = bmp;
            pictureBox3.SizeMode = PictureBoxSizeMode.StretchImage;
       
           
            
        }

        private void checkBox6_CheckedChanged(object sender, EventArgs e)
        {
            zoomFactor = 1;
            Bitmap originalBitmap = new Bitmap(pictureBox3.Image);

            Size newSize = new Size((int)(originalBitmap.Width * zoomFactor), (int)(originalBitmap.Height * zoomFactor));
            Bitmap bmp = new Bitmap(originalBitmap, newSize);
            pictureBox3.Image = bmp;
            pictureBox3.SizeMode = PictureBoxSizeMode.StretchImage;


            
        }

        private void checkBox7_CheckedChanged(object sender, EventArgs e)
        {

            zoomFactor = 1.5;
            Bitmap originalBitmap = new Bitmap(pictureBox3.Image);

            Size newSize = new Size((int)(originalBitmap.Width * zoomFactor), (int)(originalBitmap.Height * zoomFactor));
            Bitmap bmp = new Bitmap(originalBitmap, newSize);
            pictureBox3.Image = bmp;
            //pictureBox3.SizeMode = PictureBoxSizeMode.StretchImage;




        }

      
     
        private void checkBox9_CheckedChanged(object sender, EventArgs e)
        {
            Bitmap temp = new Bitmap(pictureBox3.Image);
            Bitmap bmap = (Bitmap)temp.Clone();
            Color c;
            for (int i = 0; i < bmap.Width; i++)
            {
                for (int j = 0; j < bmap.Height; j++)
                {
                    c = bmap.GetPixel(i, j);
                    bmap.SetPixel(i, j, Color.FromArgb(255 - c.R, 255 - c.G, 255 - c.B));
                }
            }
            pictureBox3.Image = bmap;
        }

        private void button6_Click(object sender, EventArgs e)
        {
            pictureBox1.Image = null;
            pictureBox2.Image = null;
            pictureBox3.Image = null;
            pictureBox6.Image = null;
            pictureBox7.Image = null;
        }

        private void button7_Click(object sender, EventArgs e)
        {
           

            checkBox1.Checked = false;
            checkBox2.Checked = false;
            checkBox3.Checked = false;
            checkBox4.Checked = false;
            checkBox9.Checked = false;
            checkBox11.Checked = false;
            checkBox12.Checked = false;
            checkBox13.Checked = false;

            pictureBox3.Image = panoramapic;


        }

        private void checkBox10_CheckedChanged(object sender, EventArgs e)
        {
            
            RotateFlipType rotateFlipType = RotateFlipType.Rotate90FlipX;
            Bitmap temp = new Bitmap(pictureBox3.Image);
            Bitmap bmap = (Bitmap)temp.Clone();
            bmap.RotateFlip(rotateFlipType);
            pictureBox3.Image = bmap;
        }

        private void checkBox11_CheckedChanged(object sender, EventArgs e)
        {
            if (checkBox11.Checked)
            {
                RotateFlipType rotateFlipType = RotateFlipType.Rotate90FlipNone;
                Bitmap temp = new Bitmap(pictureBox3.Image);
                Bitmap bmap = (Bitmap)temp.Clone();
                bmap.RotateFlip(rotateFlipType);
                pictureBox3.Image = bmap;
            }
        }

        private void checkBox12_CheckedChanged(object sender, EventArgs e)
        {

            if (checkBox12.Checked)
            {
                RotateFlipType rotateFlipType = RotateFlipType.Rotate180FlipNone;
                Bitmap temp = new Bitmap(pictureBox3.Image);
                Bitmap bmap = (Bitmap)temp.Clone();
                bmap.RotateFlip(rotateFlipType);
                pictureBox3.Image = bmap;
            }
        }

        private void checkBox13_CheckedChanged(object sender, EventArgs e)
        {
            if (checkBox13.Checked)
            {
                RotateFlipType rotateFlipType = RotateFlipType.Rotate270FlipNone;
                Bitmap temp = new Bitmap(pictureBox3.Image);
                Bitmap bmap = (Bitmap)temp.Clone();
                bmap.RotateFlip(rotateFlipType);
                pictureBox3.Image = bmap;
            }
        }

      
    }
}
