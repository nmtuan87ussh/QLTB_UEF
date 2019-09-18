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
    public partial class frmChangePassword : Form
    {
        TaiKhoan tk = new TaiKhoan();
        MD5C md5 = new MD5C();
        string strUser;
        string strPass;
        public frmChangePassword()
        {
            InitializeComponent();
        }
        public frmChangePassword(string username, string password):this()
        {
            strUser = username;
            strPass = password;
        }

        private void frmChangePassword_Load(object sender, EventArgs e)
        {
            txtUserName.Text = strUser;
            txtUserName.Enabled = false;
        }

        private void btnChangePassword_Click(object sender, EventArgs e)
        {
            if(!txtPassword.Text.Equals(strPass)) {
                MessageBox.Show("Nhập mật khẩu chưa đúng!");
                return;
            }
            else if(!txtNewPassword.Text.Equals(txtCofirmPass.Text)){
                MessageBox.Show("Xác nhận mật khẩu chưa khớp với mật khẩu mới!");
                return;
            }
            else if(txtNewPassword.Text.Equals("") || txtCofirmPass.Text.Equals(""))
            {
                MessageBox.Show("Mật khẩu không được để trống!");
                return;
            }
            else {
                tk.changePass(strUser, md5.MD5Hash(txtCofirmPass.Text));
                MessageBox.Show("Đổi mật khẩu thành công!");
                this.Dispose();
                return;
            }
        }

        private void btnCancel_Click(object sender, EventArgs e)
        {
            this.Dispose();
        }
    }
}
