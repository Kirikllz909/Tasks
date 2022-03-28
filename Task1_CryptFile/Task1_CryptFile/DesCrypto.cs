using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Task1_CryptFile
{
    internal class DesCrypto
    {
        public static void ToDes(string file, string destination, string sKey)
        {
            FileStream fsInput = new FileStream(file, FileMode.Open, FileAccess.Read);
            FileStream fsEncrypted = new FileStream(destination, FileMode.Create, FileAccess.Write);
            DESCryptoServiceProvider DES = new DESCryptoServiceProvider();
            try
            {
                DES.Key = ASCIIEncoding.ASCII.GetBytes(sKey);
                DES.IV = ASCIIEncoding.ASCII.GetBytes(sKey);
                ICryptoTransform desencrypt = DES.CreateEncryptor();
                CryptoStream cryptoStream = new CryptoStream(fsEncrypted, desencrypt, CryptoStreamMode.Write);
                byte[] bytearrayinput = new byte[fsInput.Length - 0];
                fsInput.Read(bytearrayinput, 0, bytearrayinput.Length);
                cryptoStream.Write(bytearrayinput, 0, bytearrayinput.Length);
                cryptoStream.Close();
            }
            catch
            {
                string msg = "Error: unable to crypto file. Key length should be 8 chars.";
                MessageBox.Show(msg, "Crypting error Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                fsInput.Close();
                fsEncrypted.Close();
                return;
            }
        
            fsInput.Close();
            fsEncrypted.Close();
        }
        public static void FromDes(string file, string destination, string sKey)
        {
            FileStream fsInput = new FileStream(file, FileMode.Open, FileAccess.Read);
            FileStream fsDecrypted = new FileStream(destination, FileMode.Create, FileAccess.Write);
            DESCryptoServiceProvider DES = new DESCryptoServiceProvider();
            try
            {
                DES.Key = ASCIIEncoding.ASCII.GetBytes(sKey);
                DES.IV = ASCIIEncoding.ASCII.GetBytes(sKey);
                ICryptoTransform desencrypt = DES.CreateDecryptor();
                CryptoStream cryptoStream = new CryptoStream(fsDecrypted, desencrypt, CryptoStreamMode.Write);
                byte[] bytearrayinput = new byte[fsInput.Length - 0];
                fsInput.Read(bytearrayinput, 0, bytearrayinput.Length);
                cryptoStream.Write(bytearrayinput, 0, bytearrayinput.Length);
                cryptoStream.Close();
            }
            catch
            {
                string msg = "Error: unable to decrypt file file";
                MessageBox.Show(msg, "Decrypt Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                fsInput.Close();
                fsDecrypted.Close();
                return;
            }
            fsInput.Close();
            fsDecrypted.Close();
        }
    }
}
