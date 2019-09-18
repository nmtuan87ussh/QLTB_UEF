using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace QLTB_UEF
{
    public partial class FormLogIn : Form
    {
        TaiKhoan tk = new TaiKhoan();
        MD5C md5 = new MD5C();
        public FormLogIn()
        {
            InitializeComponent();
        }

        private void btnSubmit_Click(object sender, EventArgs e)
        {
            if (LogInValidator(txtPassword.Text, txtUserName.Text))
            {
                //this.DialogResult = DialogResult.OK;
                Form1 f1 = new Form1(txtUserName.Text, txtPassword.Text);
                f1.ShowDialog();
            }
            else
            {
                MessageBox.Show("Invalid username or password!");
            }
        }

        private void btnCancel_Click(object sender, EventArgs e)
        {
            this.DialogResult = DialogResult.Cancel;
        }
        private bool LogInValidator(string password, string username)
        {
            // your validation function that returns a 'bool
            // goes here
            // ????
            bool check = false;
            //Console.WriteLine(md5.MD5Hash(password));
            if (tk.getAccount(username, md5.MD5Hash(password)) == "1")
            {
                check = true;
            }
            return check;
        }
    }
}
