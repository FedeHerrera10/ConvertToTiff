using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Drawing.Imaging;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Configuration;

namespace ConverToTif
{
    public partial class FormConvert : Form
    {
        private string ruta_tif = ConfigurationManager.AppSettings.Get("ruta_tif");
        private string path_result = "";
        
        public FormConvert()
        {
            InitializeComponent();

            if (ruta_tif.Trim().Equals(""))
            {
                ruta_tif = getPathDefault();
            }
        }
        private int intCurrPage = 0;
        private void button1_Click(object sender, EventArgs e)
        {
            OpenFileDialog openFileDialog1 = new OpenFileDialog();
            openFileDialog1.InitialDirectory = "c:\\";
            openFileDialog1.Filter = "Image Files (JPG,PNG,GIF)|*.JPG;*.PNG;*.GIF"; ;
            openFileDialog1.FilterIndex = 2;
            openFileDialog1.RestoreDirectory = true;

            if (openFileDialog1.ShowDialog() == DialogResult.OK)
            {
                try
                {
                    string path_img_frontal = openFileDialog1.FileName;
                    txtFrontal.Text = path_img_frontal;
                    
                    
                }
                catch (Exception ex)
                {
                    MessageBox.Show("Error: No se pudo abrir la imagen: " + ex.Message,"Error..",MessageBoxButtons.OK,MessageBoxIcon.Error);
                }
            }
        }

        private string getPathDefault()
        {
            

            string path_temporal = Path.GetTempPath() + Path.DirectorySeparatorChar + "ConvertTIF";
            if(!Directory.Exists(path_temporal))
              {
                Directory.CreateDirectory(path_temporal);
            }
            return path_temporal;
        }

        private void btnDorso_Click(object sender, EventArgs e)

        {
            OpenFileDialog openFileDialog1 = new OpenFileDialog();
            openFileDialog1.InitialDirectory = "c:\\";
            openFileDialog1.Filter = "Image Files (JPG,PNG,GIF)|*.JPG;*.PNG;*.GIF"; ;
            openFileDialog1.FilterIndex = 2;
            openFileDialog1.RestoreDirectory = true;

            if (openFileDialog1.ShowDialog() == DialogResult.OK)
            {
                try
                {
                    string path_img_dorso = openFileDialog1.FileName;
                    txtDorso.Text = path_img_dorso;


                }
                catch (Exception ex)
                {
                    MessageBox.Show("Error: No se pudo abrir la imagen: " + ex.Message, "Error..", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
        }

       private void DoExistingFileSave(Image[] imagenes, string loc)
        {
            try
            {
                Image[] scannedImages = new Image[2];
                scannedImages = imagenes;
                SaveMultipage(scannedImages, loc, "TIFF");
                MessageBox.Show("Conversion con exito", "Exito..", MessageBoxButtons.OK, MessageBoxIcon.Information);
               

            }
            catch (Exception ee)
            {
                MessageBox.Show(ee.Message,"Error..",MessageBoxButtons.OK,MessageBoxIcon.Error);
            }
        }
         private bool SaveMultipage(Image[] bmp, string location, string type)
        {
            if (bmp != null)
            {
                try
                {
                    ImageCodecInfo codecInfo = get_codec_forstring(type);

                    for (int i = 0; i < bmp.Length; i++)
                    {
                        if (bmp[i] == null)
                            break;
                    }

                    if (bmp.Length == 1)
                    {

                        EncoderParameters iparams = new EncoderParameters(1);
                        System.Drawing.Imaging.Encoder iparam = System.Drawing.Imaging.Encoder.Compression;
                        EncoderParameter iparamPara = new EncoderParameter(iparam, (long)(EncoderValue.CompressionCCITT4));
                        iparams.Param[0] = iparamPara;
                        bmp[0].Save(location, codecInfo, iparams);
                    }
                    else if (bmp.Length > 1)
                    {
                        System.Drawing.Imaging.Encoder saveEncoder;
                        System.Drawing.Imaging.Encoder compressionEncoder;
                        EncoderParameter SaveEncodeParam;
                        EncoderParameter CompressionEncodeParam;
                        EncoderParameters EncoderParams = new EncoderParameters(2);

                        saveEncoder = System.Drawing.Imaging.Encoder.SaveFlag;
                        compressionEncoder = System.Drawing.Imaging.Encoder.Compression;

                        SaveEncodeParam = new EncoderParameter(saveEncoder, (long)EncoderValue.MultiFrame);
                        CompressionEncodeParam = new EncoderParameter(compressionEncoder, (long)EncoderValue.CompressionCCITT4);
                        EncoderParams.Param[0] = CompressionEncodeParam;
                        EncoderParams.Param[1] = SaveEncodeParam;

                        File.Delete(location);
                        bmp[0].Save(location, codecInfo, EncoderParams);

                        for (int i = 1; i < bmp.Length; i++)
                        {
                            if (bmp[i] == null) break;

                            SaveEncodeParam = new EncoderParameter(saveEncoder, (long)EncoderValue.FrameDimensionPage);
                            CompressionEncodeParam = new EncoderParameter(compressionEncoder, (long)EncoderValue.CompressionCCITT4);
                            EncoderParams.Param[0] = CompressionEncodeParam;
                            EncoderParams.Param[1] = SaveEncodeParam;
                            bmp[0].SaveAdd(bmp[i], EncoderParams);
                        }

                        SaveEncodeParam = new EncoderParameter(saveEncoder, (long)EncoderValue.Flush);
                        EncoderParams.Param[0] = SaveEncodeParam;
                        bmp[0].SaveAdd(EncoderParams);
                    }
                    return true;
                }
                catch (Exception ex)
                {
                    MessageBox.Show(ex.ToString(),"Error",MessageBoxButtons.OK,MessageBoxIcon.Error);
                    return false;
                }
            }
            else
            {
                return false;
            }
        }
        private ImageCodecInfo get_codec_forstring(string type)
        {
            ImageCodecInfo[] info = ImageCodecInfo.GetImageEncoders();

            for (int i = 0; i < info.Length; i++)
            {
                string EnumName = type.ToString();
                if (info[i].FormatDescription.Equals(EnumName))
                {
                    return info[i];
                }
            }
            return null;
        }

        private void btnConvertir_Click(object sender, EventArgs e)
        {
            if (!verificar_path()) return;
            merge_image();
            this.toolStripStatusLabel1.Text = "Success";
        }

        private void merge_image()
        {
            PictureBox pctb1 = new PictureBox();
            pctb1.Image = Image.FromFile(txtFrontal.Text);
            PictureBox pctb2 = new PictureBox();
            pctb2.Image = Image.FromFile(txtDorso.Text);
            Image[] arrayImages = new Image[2];
            arrayImages[0] = pctb1.Image;
            arrayImages[1] = pctb2.Image;
            string path = ruta_tif +"\\"+ Path.GetRandomFileName().ToString()+".tif";
            path_result = path;
            DoExistingFileSave(arrayImages, path);
            groupBox2.Visible = true;
            intCurrPage = 0; // reseting the counter
            RefreshImage(); // refreshing and showing the new file

        }
        private bool verificar_path()
        {
            if (txtFrontal.Text.Trim().Equals(""))
            {
                errorProvider1.SetError(txtFrontal, "El campo no puede ser blanco");
                return false;
            }
            if (txtDorso.Text.Trim().Equals(""))
            {
                errorProvider1.SetError(txtDorso, "El campo no puede ser blanco");
                return false;
            }
            
            if(!File.Exists(txtDorso.Text))
            {
                errorProvider1.SetError(txtDorso, "La imagen no se encontro");
                return false;
            }

            if (!File.Exists(txtFrontal.Text))
            {
                errorProvider1.SetError(txtDorso, "La imagen no se encontro");
                return false;
            }

            return true;
        }

        private void Form1_Load(object sender, EventArgs e)
        {
          
        }

        public void RefreshImage()
        {
            Image myImg; // setting the selected tiff
            Image myBmp; // a new occurance of Image for viewing

            myImg = System.Drawing.Image.FromFile(path_result); // setting the image from a file

            int intPages = myImg.GetFrameCount(System.Drawing.Imaging.FrameDimension.Page); // getting the number of pages of this tiff
            intPages--; // the first page is 0 so we must correct the number of pages to -1
          // lblNumPages.Text = Convert.ToString(intPages); // showing the number of pages
           // lblCurrPage.Text = Convert.ToString(intCurrPage); // showing the number of page on which we're on

            myImg.SelectActiveFrame(System.Drawing.Imaging.FrameDimension.Page, intCurrPage); // going to the selected page

            myBmp = new Bitmap(myImg, pictureBox1.Width, pictureBox1.Height); // setting the new page as an image
            // Description on Bitmap(SOURCE, X,Y)

            pictureBox1.Image = myBmp; // showing the page in the pictureBox1

        }








        private void toolStripLabel1_Click(object sender, EventArgs e)
        {
            FormSetting fSetting = new FormSetting();
            fSetting.ShowDialog();
        }

        private void btnAtras_Click(object sender, EventArgs e)
        {
            if (intCurrPage == 0) // it stops here if you reached the bottom, the first page of the tiff
            { intCurrPage = 0; }
            else
            {
                intCurrPage--; // if its not the first page, then go to the previous page
                RefreshImage(); // refresh the image on the selected page
            }
        }

        private void btnSiguiente_Click(object sender, EventArgs e)
        {
            if (intCurrPage == 1) // if you have reached the last page it ends here
                                                                  // the "-1" should be there for normalizing the number of pages
            { intCurrPage = 1; }
            else
            {
                intCurrPage++;
                RefreshImage();
            }
        }

        private void toolStripLabel2_Click(object sender, EventArgs e)
        {
            string text_dorso = txtDorso.Text.Trim();
            string text_front = txtFrontal.Text.Trim();

            if ((text_dorso.Equals("")) || (text_front.Equals("")))
            {
                DialogResult dialogResult = MessageBox.Show("Se perderan los cambios no guardados.¿Desea salir?","Info", MessageBoxButtons.YesNo, MessageBoxIcon.Question);
                if(dialogResult==DialogResult.Yes)
                {
                    pictureBox1.Image = null;
                    txtDorso.Text = "";
                    txtFrontal.Text = "";
                    groupBox2.Visible = false;
                }
            }

               
            
        }
    }
}
