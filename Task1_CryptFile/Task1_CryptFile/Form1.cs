using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Task1_CryptFile
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }

        //readonly
        readonly CspParameters _cspp = new CspParameters();
        RSACryptoServiceProvider _rsa;

        // Path variables for source, encryption, and
        // decryption folders. Must end with a backslash. D:\Станкин\онит\курсовая
        const string EncrFolder = @"B:\Task1Crypto\Encrypt\";
        const string DecrFolder = @"B:\Task1Crypto\Decrypt\";
        const string SrcFolder = @"B:\Task1Crypto\";

        // Public key file
        const string PubKeyFile = @"D:\Task1Crypto\rsaPublicKey.txt";

        // Key container name for
        // private/public key value pair.
        const string KeyName = "Key01";

        //Aes encrypt
        private void button1_Click(object sender, EventArgs e)
        {
            if (_rsa is null)
            {
                MessageBox.Show("Key not set.");
            }
            else
            {
                OpenFileDialog encrDir = new OpenFileDialog();
                // Display a dialog box to select a file to encrypt.
                encrDir.InitialDirectory = EncrFolder;
                if (encrDir.ShowDialog() == DialogResult.OK)
                {
                    string fName = encrDir.FileName;
                    if (fName != null)
                    {
                        // Pass the file name without the path.
                        AesCrypto.ToAes(new FileInfo(fName),_rsa,EncrFolder);
                    }
                }
            }
        }
        //Des encrypt
        private void button2_Click(object sender, EventArgs e)
        {
            string sKey = textBox1.Text;
            OpenFileDialog openFileDialog1 = new OpenFileDialog();
            SaveFileDialog saveFileDialog1 = new SaveFileDialog();

            if (openFileDialog1.ShowDialog() == DialogResult.OK)
            {
                string file = openFileDialog1.FileName;
                saveFileDialog1.Filter = "des files |*.des";
                if (saveFileDialog1.ShowDialog() == DialogResult.OK)
                {
                    string destination = saveFileDialog1.FileName;
                    
                        DesCrypto.ToDes(file, destination, sKey);
                }
            }
        }
        //Aes decrypt
        private void button3_Click(object sender, EventArgs e)
        {
            if (_rsa is null)
            {
                MessageBox.Show("Key not set.");
            }
            else
            {
                OpenFileDialog decrDir = new OpenFileDialog();
                // Display a dialog box to select the encrypted file.
                decrDir.InitialDirectory = EncrFolder;
                if (decrDir.ShowDialog() == DialogResult.OK)
                {
                    string fName = decrDir.FileName;
                    if (fName != null)
                    {
                        AesCrypto.FromAes(new FileInfo(fName),_rsa,DecrFolder);
                    }
                }
            }
        }

        //Des decrypt
        private void button4_Click(object sender, EventArgs e)
        {
            string sKey = textBox1.Text;
            OpenFileDialog openFileDialog1 = new OpenFileDialog();
            SaveFileDialog saveFileDialog1 = new SaveFileDialog();
            openFileDialog1.Filter = "des files |*.des";
            if (openFileDialog1.ShowDialog() == DialogResult.OK)
            {
                string file = openFileDialog1.FileName;
                saveFileDialog1.Filter = "txt files |*.txt";
                if (saveFileDialog1.ShowDialog() == DialogResult.OK)
                {
                        string destination = saveFileDialog1.FileName;
                        DesCrypto.FromDes(file, destination, sKey);
                }
            }
        }

        private void button5_Click(object sender, EventArgs e)
        {
            _cspp.KeyContainerName = KeyName;
            _rsa = new RSACryptoServiceProvider(_cspp)
            {
                PersistKeyInCsp = true
            };
            label2.Text = "Ключ создан";
            /* label2.Text = _rsa.PublicOnly
                 ? $"Key: {_cspp.KeyContainerName} - Public Only"
                 : $"Key: {_cspp.KeyContainerName} - Full Key Pair"; */
        }
    }
}
