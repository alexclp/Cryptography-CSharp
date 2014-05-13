using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace CryptographyProject
{
    public partial class LoginForm : Form
    {
        public LoginForm()
        {
            InitializeComponent();
            //SqlManager.InsertData();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            string encryptedUserName = Cryptographer.EncryptString(this.userTextBox.Text);
            string encryptedPassword = Cryptographer.EncryptString(this.passwordTextBox.Text);

            Console.WriteLine("encrypted user and pass = {0}, {1}", encryptedUserName, encryptedPassword);

            string decryptedUserName = Cryptographer.DecryptString(encryptedUserName);
            string decryptedPassword = Cryptographer.DecryptString(encryptedPassword);

            Console.WriteLine("decrypyted user and pass = {0}, {1}", decryptedUserName, decryptedPassword);

            CheckPassword(this.passwordTextBox.Text);
        }

        private void CheckPassword(string toCompare)
        {
            string fromDb = SqlManager.GetPassword();
            Console.WriteLine("string from database = {0}", fromDb);
            string encryptedPassword = Cryptographer.DecryptString(fromDb);

            if (toCompare == encryptedPassword)
            {
                MessageBox.Show("Login successful");
            }
            else
            {
                MessageBox.Show("Login failed");
            }
        }
    }
}
