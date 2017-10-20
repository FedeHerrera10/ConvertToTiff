using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Configuration;
using System.IO;

namespace ConverToTif
{
    public partial class FormSetting : Form
    {
        public FormSetting()
        {
            InitializeComponent();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            if (textBox1.Text.Trim().Equals(""))
            {
                errorProvider1.SetError(textBox1, "El campo no puede ser blanco");
                return;
            }
            if (SetApp(textBox1.Text))
            {
                MessageBox.Show("Successed", "Info", MessageBoxButtons.OK, MessageBoxIcon.Information);
                this.Close();
            }

            else
            {
                MessageBox.Show("Directory not exist", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);

            }
           
        }

        private bool SetApp(string path)
        {
            if (!Directory.Exists(path)) return false;

            ConfigurationManager.AppSettings.Set("ruta_tif",path);
            return true;
        }

    }
}
