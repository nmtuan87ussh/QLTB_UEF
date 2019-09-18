using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data;

namespace QLTB_UEF
{
    class TaiKhoan
    {
        Database db;
        public TaiKhoan()
        {
            db = new Database();
        }
        public String getAccount(string username, string pass)
        {
            string strSQL = "SELECT COUNT(*) FROM TaiKhoan WHERE MaTaiKhoan = '" + username + "' AND MaKhau = '" + pass + "'";
            DataTable dt = db.Execute(strSQL); //Goi phuong thuc truy xuat du lieu              
            return dt.Rows[0][0].ToString();
        }
        public DataTable LayLoaiNguoiDungFromAccount(string username)
        {
            string strSQL = "Select lnd.MaLoaiNguoiDung,lnd.PhanQuyen from TaiKhoan tk,LoaiNguoiDung lnd where tk.MaLoaiNguoiDung = lnd.MaLoaiNguoiDung and tk.MaTaiKhoan = '" + username + "'";
            DataTable dt = db.Execute(strSQL);
            return dt;
        }
        public void changePass(string username, string confirmpass)
        {
            string strSQL = "UPDATE TaiKhoan SET MaKhau = '" + confirmpass + "' WHERE MaTaiKhoan = '" + username + "'";
            db.ExecuteNonQuery(strSQL); //Goi phuong thuc cap nhat du lieu
        }
    }
}
