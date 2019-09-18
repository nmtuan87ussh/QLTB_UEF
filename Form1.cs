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
    public partial class Form1 : Form
    {
        TaiKhoan tk = new TaiKhoan();
        DataTable dt;
        string strUser;
        string strPass;
        public Form1()
        {
            InitializeComponent();
        }
        public Form1(string username, string password):this()
        {
            strUser = username;
            strPass = password;
            dt = tk.LayLoaiNguoiDungFromAccount(strUser);
            lbQuyen.Text = "Quyền truy cập: " + dt.Rows[0][1].ToString();
            switch(dt.Rows[0][1].ToString())
            {
                case "Admin":
                    decentralizationUserToolStripMenuItem.Enabled = true;
                    break;
                case "Quản lý":
                    decentralizationUserToolStripMenuItem.Enabled = false;
                    break;
                case "Nhân viên":
                    decentralizationUserToolStripMenuItem.Enabled = false;
                    break;
                default:
                    decentralizationUserToolStripMenuItem.Enabled = false;
                    break;
            }
        }
        private void quảnLýDanhMụcToolStripMenuItem_Click(object sender, EventArgs e)
        {
            frmQuanLyDanhMuc f = new frmQuanLyDanhMuc();
            f.ShowDialog();
        }

        private void Form1_Load(object sender, EventArgs e)
        {
        }

        private void btnLogout_Click(object sender, EventArgs e)
        {
            this.Dispose();
        }

        private void đăngXuấtHệThốngToolStripMenuItem_Click(object sender, EventArgs e)
        {
            this.Dispose();
        }

        private void phânQuyềnNgườiDùngToolStripMenuItem_Click(object sender, EventArgs e)
        {

        }

        private void đổiMậtKhẩuToolStripMenuItem_Click(object sender, EventArgs e)
        {
            frmChangePassword fcp = new frmChangePassword(strUser, strPass);
            fcp.ShowDialog();
        }
    }
}
