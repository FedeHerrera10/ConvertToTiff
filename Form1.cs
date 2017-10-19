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
    public partial class Form1 : Form
    {
        private string ruta_tif = ConfigurationManager.AppSettings.Get("ruta_tif");
        public Form1()
        {
            InitializeComponent();
        }

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
                txtDorso.Text = "";
                txtFrontal.Text = "";

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
            PictureBox pctb1 = new PictureBox();
            pctb1.Image=Image.FromFile( txtFrontal.Text);
            PictureBox pctb2 = new PictureBox();
            pctb2.Image = Image.FromFile(txtDorso.Text);
            Image[] arrayImages = new Image[2];
            arrayImages[0] = pctb1.Image;
            arrayImages[1] = pctb2.Image;
            string path = ruta_tif + "\\image.tif";
            DoExistingFileSave(arrayImages,path);
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
            return true;
        }
    }
}
