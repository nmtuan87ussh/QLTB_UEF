using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data;
using System.Data.SqlClient;
namespace QLTB_UEF
{
    class ThietBi
    {
        Database db;
        public ThietBi()
        {
            db = new Database();
        }
        public DataTable LayDSThietBi()
        {
            string strSQL = "Select TB.MaThietBi, TB.TenThietBi, TB.SoLuongNhap,TB.TinhTrang,TB.NgayNhap, LTB.TenLoaiThietBi,DVT.TenDonViTinh,NCC.TenNhaCungCap,K.TenKho,PB.TenPhongBan,TB.MoTa From ThietBi TB, LoaiThietBi LTB, DonViTinh DVT, NhaCungCap NCC, Kho K, PhongBan PB Where TB.MaLoaiThietBi = LTB.MaLoaiThietBi AND TB.MaDonViTinh = DVT.MaDonViTinh AND TB.MaNhaCungCap = NCC.MaNhaCungCap AND TB.MaKho = K.MaKho AND TB.MaPhongBan = PB.MaPhongBan"; 
            DataTable dt = db.Execute(strSQL); //Goi phuong thuc truy xuat du lieu              
            return dt; 
        }
        public DataTable LoaiThietBi() { 
            string strSQL = "Select * from LoaiThietBi"; 
            DataTable dt = db.Execute(strSQL); 
            return dt; 
        }
        public DataTable DonViTinh()
        {
            string strSQL = "Select * from DonViTinh";
            DataTable dt = db.Execute(strSQL);
            return dt;
        }
        public DataTable NhaCungCap()
        {
            string strSQL = "Select * from NhaCungCap";
            DataTable dt = db.Execute(strSQL);
            return dt;
        }
        public DataTable Kho()
        {
            string strSQL = "Select * from Kho";
            DataTable dt = db.Execute(strSQL);
            return dt;
        }
        public DataTable PhongBan()
        {
            string strSQL = "Select * from PhongBan";
            DataTable dt = db.Execute(strSQL);
            return dt;
        }
        public void XoaThietBi(string mathietbi) 
        {
            string sql = "Delete from ThietBi where MaThietBi = '" + mathietbi + "'";
            db.ExecuteNonQuery(sql); 
        }
        public void ThemThietBi(string MaThietBi, string TenThietBi, int SoLuongNhap, string TinhTrang, string NgayNhap, string MaLoaiThietBi, string MaDonViTinh, string MaNhaCungCap, string MaKho, string MaPhongBan, string MoTa) 
        { 
            string sql = string.Format("Insert Into ThietBi  Values('{0}',N'{1}',{2},N'{3}',N'{4}','{5}','{6}','{7}','{8}','{9}',N'{10}')", MaThietBi, TenThietBi, SoLuongNhap, TinhTrang, NgayNhap, MaLoaiThietBi, MaDonViTinh, MaNhaCungCap, MaKho, MaPhongBan, MoTa); 
            db.ExecuteNonQuery(sql); 
        }
        public void CapNhatThietBi(string MaThietBi, string TenThietBi, int SoLuongNhap, string TinhTrang, string NgayNhap, string MaLoaiThietBi, string MaDonViTinh, string MaNhaCungCap, string MaKho, string MaPhongBan, string MoTa)
        {
            string sql = string.Format("Update ThietBi  Set TenThietBi = N'{0}',SoLuongNhap = {1},TinhTrang = N'{2}',NgayNhap = N'{3}',MaLoaiThietBi = '{4}',MaDonViTinh = '{5}',MaNhaCungCap = '{6}',MaKho = '{7}',MaPhongBan = '{8}',MoTa = N'{9}' where MaThietBi='{10}'", TenThietBi, SoLuongNhap, TinhTrang, NgayNhap, MaLoaiThietBi, MaDonViTinh, MaNhaCungCap, MaKho, MaPhongBan, MoTa, MaThietBi);
            db.ExecuteNonQuery(sql);
        }
        public String checkMaThietBi(string MaThietBi)
        {
            string strSQL = "SELECT COUNT(*) FROM ThietBi WHERE MaThietBi = '" + MaThietBi + "'";
            DataTable dt = db.Execute(strSQL); //Goi phuong thuc truy xuat du lieu              
            return dt.Rows[0][0].ToString();
        }
        public DataTable getMaLoaiThietBi(string TenLoaiThietBi)
        {
            string strSQL = "Select * from LoaiThietBi ltb where ltb.TenLoaiThietBi = N'" + TenLoaiThietBi + "'";
            DataTable dt = db.Execute(strSQL);
            return dt;
        }
        public DataTable getMaDonViTinh(string TenDonViTinh)
        {
            string strSQL = "Select * from DonViTinh dvt where dvt.TenDonViTinh = N'" + TenDonViTinh + "'";
            DataTable dt = db.Execute(strSQL);
            return dt;
        }
        public DataTable getMaNhaCungCap(string TenNhaCungCap)
        {
            string strSQL = "Select * from NhaCungCap ncc where ncc.TenNhaCungCap = N'" + TenNhaCungCap + "'";
            DataTable dt = db.Execute(strSQL);
            return dt;
        }
        public DataTable getMaKho(string TenKho)
        {
            string strSQL = "Select * from Kho k where k.TenKho = N'" + TenKho + "'";
            DataTable dt = db.Execute(strSQL);
            return dt;
        }
        public DataTable getMaPhongBan(string TenPhongBan)
        {
            string strSQL = "Select * from PhongBan pb,ThietBi tb where pb.TenPhongBan = N'" + TenPhongBan + "'";
            DataTable dt = db.Execute(strSQL);
            return dt;
        }
    }
}
