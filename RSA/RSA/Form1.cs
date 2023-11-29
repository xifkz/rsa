using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Security.Cryptography.X509Certificates;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace RSA
{
    public partial class Form1 : Form
    {
        string publicKey, privateKey;

        public Form1()
        {
            InitializeComponent();
        }

        private void btnEncrypt_Click(object sender, EventArgs e)
        {
            txtEncrypted.Text = Crypto.TextToRsa(txtPlain.Text, publicKey);
        }

        private void btnDecrypt_Click(object sender, EventArgs e)
        {
            txtPlain.Text = Crypto.RsaToText(txtEncrypted.Text, privateKey);
        }

        private void btnBrowse_Click(object sender, EventArgs e)
        {
            var browse = new OpenFileDialog();

            if(browse.ShowDialog() == DialogResult.OK)
            {
                txtPath.Text = browse.FileName;
            }
        }

        private void btnEncryptFile_Click(object sender, EventArgs e)
        {
            if (File.Exists(txtPath.Text) == false)
                return;

            Crypto.FileToRsa(txtPath.Text, publicKey);
        }

        private void btnDecryptFile_Click(object sender, EventArgs e)
        {
            if (File.Exists(txtPath.Text) == false)
                return;

            Crypto.RsaToFile(txtPath.Text, privateKey);
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            Crypto.GenerateKeys(out publicKey, out privateKey);
        }
    }
}
