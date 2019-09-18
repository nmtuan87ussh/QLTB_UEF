using System;
using System.Collections.Generic;
using System.Data;
using System.Drawing;
using System.IO;
using System.Text;
using System.Windows.Forms;
using OfficeOpenXml;

namespace QLTB_UEF
{
    public partial class frmQuanLyDanhMuc : Form
    {
        private QREncoder QRCodeEncoder;
        private Bitmap QRCodeImage;
        private Rectangle QRCodeImageArea = new Rectangle();
        ThietBi tb = new ThietBi();
        public bool themmoi = false;
        public frmQuanLyDanhMuc()
        {
            InitializeComponent();
        }
        void setNull() 
        {
            txtMaThietBi.Text = "";
            txtTenThietBi.Text = "";
            txtSoLuong.Text = "";
            txtTinhTrang.Text = "";
            txtMoTa.Text = "";
        }
        void setButton(bool val) { 
            btnThem.Enabled = val; 
            btnXoa.Enabled = val; 
            btnSua.Enabled = val; 
            btnThoat.Enabled = val; 
            btnLuu.Enabled = !val; 
            btnHuy.Enabled = !val; 
        }
        void HienthiThietBi() 
        { 
            DataTable dt = tb.LayDSThietBi(); 
            for (int i = 0; i < dt.Rows.Count; i++) 
            { 
                ListViewItem lvi = lsvThietBi.Items.Add(dt.Rows[i][0].ToString()); 
                lvi.SubItems.Add(dt.Rows[i][1].ToString()); 
                lvi.SubItems.Add(dt.Rows[i][2].ToString()); 
                lvi.SubItems.Add(dt.Rows[i][3].ToString()); 
                lvi.SubItems.Add(dt.Rows[i][4].ToString()); 
                lvi.SubItems.Add(dt.Rows[i][5].ToString());
                lvi.SubItems.Add(dt.Rows[i][6].ToString());
                lvi.SubItems.Add(dt.Rows[i][7].ToString());
                lvi.SubItems.Add(dt.Rows[i][8].ToString());
                lvi.SubItems.Add(dt.Rows[i][9].ToString());
                lvi.SubItems.Add(dt.Rows[i][10].ToString());
            } 
        }
        void HienthiLoaiThietBi() {
            DataTable dt = tb.LoaiThietBi(); 
            cboTenLoaiThietBi.DataSource = dt;
            cboTenLoaiThietBi.DisplayMember = "TenLoaiThietBi";
            cboTenLoaiThietBi.ValueMember = "MaLoaiThietBi"; 
        }
        void HienthiDonViTinh()
        {
            DataTable dt = tb.DonViTinh();
            cboTenDonViTinh.DataSource = dt;
            cboTenDonViTinh.DisplayMember = "TenDonViTinh";
            cboTenDonViTinh.ValueMember = "MaDonViTinh";
        }
        void HienthiNhaCungCap()
        {
            DataTable dt = tb.NhaCungCap();
            cboTenNhaCungCap.DataSource = dt;
            cboTenNhaCungCap.DisplayMember = "TenNhaCungCap";
            cboTenNhaCungCap.ValueMember = "MaNhaCungCap";
        }
        void HienthiKho()
        {
            DataTable dt = tb.Kho();
            cboTenKho.DataSource = dt;
            cboTenKho.DisplayMember = "TenKho";
            cboTenKho.ValueMember = "MaKho";
        }
        void HienthiPhongBan()
        {
            DataTable dt = tb.PhongBan();
            cboTenPhongBan.DataSource = dt;
            cboTenPhongBan.DisplayMember = "TenPhongBan";
            cboTenPhongBan.ValueMember = "MaPhongBan";
        }
        private void frmQuanLyDanhMuc_Load(object sender, EventArgs e)
        {
            txtMaThietBi.Enabled = false;
            setNull(); 
            setButton(true);
            HienthiDonViTinh();
            HienthiKho();
            HienthiLoaiThietBi();
            HienthiNhaCungCap();
            HienthiPhongBan();
            HienthiThietBi();
            // program title
            Text = "QRCodeEncoderDemo - " + QRCode.VersionNumber + " \u00a9 2013-2018 Uzi Granot. All rights reserved.";

#if DEBUG
            // current directory
            string CurDir = Environment.CurrentDirectory;
            string WorkDir = CurDir.Replace("bin\\Debug", "Work");
            if (WorkDir != CurDir && Directory.Exists(WorkDir)) Environment.CurrentDirectory = WorkDir;
#endif

            // create encoder object
            QRCodeEncoder = new QREncoder();

            // load program state
            ProgramState.LoadState();

            // load error correction combo box
            ErrorCorrectionComboBox.Items.Add("L (7%)");
            ErrorCorrectionComboBox.Items.Add("M (15%)");
            ErrorCorrectionComboBox.Items.Add("Q (25%)");
            ErrorCorrectionComboBox.Items.Add("H (30%)");
            ErrorCorrectionComboBox.SelectedIndex = ErrorCorrectionComboBox.FindStringExact("H (30%)");

            // force resize
            frmQuanLyDanhMuc_Resize(sender, e);
            return;
        }

        private void lsvThietBi_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (lsvThietBi.SelectedIndices.Count > 0)
            {
                txtMaThietBi.Text = lsvThietBi.SelectedItems[0].SubItems[0].Text;
                txtTenThietBi.Text = lsvThietBi.SelectedItems[0].SubItems[1].Text;
                txtSoLuong.Text = lsvThietBi.SelectedItems[0].SubItems[2].Text;
                txtTinhTrang.Text = lsvThietBi.SelectedItems[0].SubItems[3].Text;
                //Chuyen sang kieu dateTime  
                dtPNgayNhap.Value = DateTime.Parse(lsvThietBi.SelectedItems[0].SubItems[4].Text);
                //Tìm vị trí của Ten Loai Thiet Bi trong Combobox                 
                cboTenLoaiThietBi.SelectedIndex = cboTenLoaiThietBi.FindString(lsvThietBi.SelectedItems[0].SubItems[5].Text);
                cboTenDonViTinh.SelectedIndex = cboTenDonViTinh.FindString(lsvThietBi.SelectedItems[0].SubItems[6].Text);
                cboTenNhaCungCap.SelectedIndex = cboTenNhaCungCap.FindString(lsvThietBi.SelectedItems[0].SubItems[7].Text);
                cboTenKho.SelectedIndex = cboTenKho.FindString(lsvThietBi.SelectedItems[0].SubItems[8].Text);
                cboTenPhongBan.SelectedIndex = cboTenPhongBan.FindString(lsvThietBi.SelectedItems[0].SubItems[9].Text);
                txtMoTa.Text = lsvThietBi.SelectedItems[0].SubItems[10].Text;
            }
        }

        private void EncodeButton_Click(object sender, EventArgs e)
        {
            // get error correction code
            ErrorCorrection ErrorCorrection = (ErrorCorrection)ErrorCorrectionComboBox.SelectedIndex;

            // get data for QR Code
            StringBuilder b = new StringBuilder();
            if (lsvThietBi.SelectedIndices.Count > 0)
            {
                b.AppendLine(this.label1.Text + ":" + lsvThietBi.SelectedItems[0].SubItems[0].Text);
                b.AppendLine(this.label2.Text + ":" + lsvThietBi.SelectedItems[0].SubItems[1].Text);
                b.AppendLine(this.label3.Text + ":" + lsvThietBi.SelectedItems[0].SubItems[2].Text);
                b.AppendLine(this.label4.Text + ":" + lsvThietBi.SelectedItems[0].SubItems[3].Text);
                b.AppendLine(this.label5.Text + ":" + lsvThietBi.SelectedItems[0].SubItems[4].Text);
                b.AppendLine(this.label6.Text + ":" + lsvThietBi.SelectedItems[0].SubItems[5].Text);
                b.AppendLine(this.label7.Text + ":" + lsvThietBi.SelectedItems[0].SubItems[6].Text);
                b.AppendLine(this.label8.Text + ":" + lsvThietBi.SelectedItems[0].SubItems[7].Text);
                b.AppendLine(this.label9.Text + ":" + lsvThietBi.SelectedItems[0].SubItems[8].Text);
                b.AppendLine(this.label10.Text + ":" + lsvThietBi.SelectedItems[0].SubItems[9].Text);
                b.AppendLine(this.label12.Text + ":" + lsvThietBi.SelectedItems[0].SubItems[10].Text);
            }
            else {
                MessageBox.Show("You don't choose data!");
                return;
            }
            string Data = b.ToString();
            // save state
            ProgramState.State.EncodeErrorCorrection = ErrorCorrection;
            ProgramState.State.EncodeData = Data;
            ProgramState.SaveState();

            // disable buttons
            EnableButtons(false);

            try
            {
                // multi segment
                if (SeparatorCheckBox.Checked && Data.IndexOf('|') >= 0)
                {
                    string[] Segments = Data.Split(new char[] { '|' }, StringSplitOptions.RemoveEmptyEntries);

                    // encode data
                    QRCodeEncoder.Encode(ErrorCorrection, Segments);
                }

                // single segment
                else
                {
                    // encode data
                    QRCodeEncoder.Encode(ErrorCorrection, Data);
                }

                // create bitmap
                QRCodeImage = QRCodeToBitmap.CreateBitmap(QRCodeEncoder, 4, 8);
            }

            catch (Exception Ex)
            {
                MessageBox.Show("Encoding exception.\r\n" + Ex.Message);
            }

            // enable buttons
            EnableButtons(true);

            // repaint panel
            Invalidate();
            return;
        }

        private void SaveImageButton_Click(object sender, EventArgs e)
        {
            // save QR code image screen
            if (QRCodeImage != null)
            {
                SaveImage Dialog = new SaveImage(QRCodeEncoder);
                Dialog.ShowDialog(this);
            }
            return;
        }

        private void DefaultButton_Click(object sender, EventArgs e)
        {
            ProgramState.SetDefaultState();
            QRCodeImage = null;
            Invalidate();
            return;
        }
        private void EnableButtons(bool Enabled)
        {
            EncodeButton.Enabled = Enabled;
            SaveImageButton.Enabled = QRCodeImage != null && Enabled;
            DefaultButton.Enabled = Enabled;
            return;
        }

        private void frmQuanLyDanhMuc_Resize(object sender, EventArgs e)
        {
            if (ClientSize.Width == 0) return;

            //// center header label
            //HeaderLabel.Left = (ClientSize.Width - HeaderLabel.Width) / 2;

            //// put data text box at the bottom of client area
            //DataTextBox.Top = ClientSize.Height - DataTextBox.Height - 8;
            //DataTextBox.Width = ClientSize.Width - 2 * DataTextBox.Left;

            //// put data label above text box
            //DataLabel.Top = DataTextBox.Top - DataLabel.Height - 3;

            //// put separator check box above and to the right of the text box
            //SeparatorCheckBox.Top = DataTextBox.Top - SeparatorCheckBox.Height - 3;
            //SeparatorCheckBox.Left = DataTextBox.Right - SeparatorCheckBox.Width;

            //// put buttons half way between header and data text
            //ButtonsGroupBox.Top = (DataLabel.Top + HeaderLabel.Bottom - ButtonsGroupBox.Height) / 2;

            // image area
            QRCodeImageArea.X = ButtonsGroupBox.Right + 160;
            QRCodeImageArea.Y = HeaderLabel.Bottom + 4;
            QRCodeImageArea.Width = 200;// ClientSize.Width - QRCodeImageArea.X - 4;
            QRCodeImageArea.Height = 200;//DataLabel.Top - QRCodeImageArea.Y - 4;

            // force re-paint
            Invalidate();
            return;
        }

        private void frmQuanLyDanhMuc_Paint(object sender, PaintEventArgs e)
        {
            // no image
            if (QRCodeImage == null) return;

            // calculate image area width and height to preserve aspect ratio
            Rectangle ImageRect = new Rectangle
            {
                Height = (QRCodeImageArea.Width * QRCodeImage.Height) / QRCodeImage.Width
            };
            if (ImageRect.Height <= QRCodeImageArea.Height)
            {
                ImageRect.Width = QRCodeImageArea.Width;
            }
            else
            {
                ImageRect.Width = (QRCodeImageArea.Height * QRCodeImage.Width) / QRCodeImage.Height;
                ImageRect.Height = QRCodeImageArea.Height;
            }

            // calculate position
            ImageRect.X = QRCodeImageArea.X + (QRCodeImageArea.Width - ImageRect.Width) / 2;
            ImageRect.Y = QRCodeImageArea.Y + (QRCodeImageArea.Height - ImageRect.Height) / 2;
            e.Graphics.DrawImage(QRCodeImage, ImageRect);
            return;
        }

        private void btnXuatExcel_Click(object sender, EventArgs e)
        {
            using (ExcelPackage excel = new ExcelPackage())
            {
                excel.Workbook.Worksheets.Add("Worksheet1");
                excel.Workbook.Worksheets.Add("Worksheet2");
                excel.Workbook.Worksheets.Add("Worksheet3");
                
                List<string[]> headerRow = new List<string[]>()
                {
                  new string[] { "STT", this.label1.Text, this.label2.Text, this.label3.Text,
                  this.label4.Text, this.label5.Text, this.label6.Text, this.label7.Text,
                  this.label8.Text, this.label9.Text, this.label10.Text, this.label12.Text
                  }
                };
                // Determine the header range (e.g. A1:D1)
                string headerRange = "A1:" + Char.ConvertFromUtf32(headerRow[0].Length + 64) + "1";

                // Target a worksheet
                var worksheet = excel.Workbook.Worksheets["Worksheet1"];

                // Popular header row data
                worksheet.Cells[headerRange].LoadFromArrays(headerRow);
                //Row Styling
                worksheet.Cells[headerRange].Style.Font.Bold = true;
                worksheet.Cells[headerRange].Style.Font.Size = 14;
                worksheet.Cells[headerRange].Style.Font.Color.SetColor(System.Drawing.Color.Blue);
                worksheet.Cells[headerRange].AutoFitColumns();
                int row = 2;
                int column = 1;
                foreach (ListViewItem lvi in lsvThietBi.Items)
                {
                    worksheet.Cells[row, 1].Value = row - 1;
                    worksheet.Cells[row, 1].AutoFitColumns();
                    column = 2;
                    foreach (ListViewItem.ListViewSubItem lvs in lvi.SubItems)
                    {
                        worksheet.Cells[row, column].Value = lvs.Text;
                        worksheet.Cells[row, column].AutoFitColumns();
                        column++;
                    }
                    row++;
                }
                FolderBrowserDialog fl = new FolderBrowserDialog();
                fl.SelectedPath = @"D:";
                fl.ShowNewFolderButton = true;
                if (fl.ShowDialog() == DialogResult.OK)
                {
                    FileInfo excelFile = new FileInfo(fl.SelectedPath + @"\ExportExcel.xlsx");
                    excel.SaveAs(excelFile);
                    MessageBox.Show("Export excel done!");
                    return;
                }
            }
        }

        private void btnThem_Click(object sender, EventArgs e)
        {
            txtMaThietBi.Enabled = true;
            themmoi = true; 
            setButton(false);
            txtMaThietBi.Focus();
        }

        private void btnXoa_Click(object sender, EventArgs e)
        {
            if (lsvThietBi.SelectedIndices.Count > 0) 
            { 
                DialogResult dr = MessageBox.Show("Bạn có chắc xóa thiết bị này không?", "Xóa Thiết Bị", MessageBoxButtons.YesNo, MessageBoxIcon.Question); 
                if (dr == DialogResult.Yes) 
                { 
                    tb.XoaThietBi(lsvThietBi.SelectedItems[0].SubItems[0].Text);
                    lsvThietBi.Items.RemoveAt(lsvThietBi.SelectedIndices[0]); 
                    setNull(); 
                } 
            } 
            else MessageBox.Show("Bạn phải chọn mẩu tin cần xóa");
        }

        private void btnSua_Click(object sender, EventArgs e)
        {
            if (lsvThietBi.SelectedIndices.Count > 0)
            {
                themmoi = false;
                setButton(false);
            }
            else
                MessageBox.Show("Bạn phải chọn thiết bị cần cập nhật", "Sửa Thiết Bị");
        }

        private void btnLuu_Click(object sender, EventArgs e)
        {
            string ngaynhap = String.Format("{0:MM/dd/yyyy}", dtPNgayNhap.Value);
            //Lay ma loai thiet bi dua vao ten loai thiet bi
            DataTable dt1 = tb.getMaLoaiThietBi(cboTenLoaiThietBi.Text);
            string MaLoaiThietBi = dt1.Rows[0][0].ToString();
            //Lay ma don vi tinh dua vao ten don vi tinh
            DataTable dt2 = tb.getMaDonViTinh(cboTenDonViTinh.Text);
            string MaDonViTinh = dt2.Rows[0][0].ToString();
            //Lay ma nha cung cap dua vao ten nha cung cap
            DataTable dt3 = tb.getMaNhaCungCap(cboTenNhaCungCap.Text);
            string MaNhaCungCap = dt3.Rows[0][0].ToString();
            //Lay ma kho dua vao ten kho
            DataTable dt4 = tb.getMaKho(cboTenKho.Text);
            string MaKho = dt4.Rows[0][0].ToString();
            //Lay ma phong ban dua vao ten phong ban
            DataTable dt5 = tb.getMaPhongBan(cboTenPhongBan.Text);
            string MaPhongBan = dt5.Rows[0][0].ToString();
            if (themmoi)
            {
                if (tb.checkMaThietBi(txtMaThietBi.Text) == "1")//Kiem tra them moi bi trung ma thiet bi
                {
                    MessageBox.Show("Mã thiết bị đã tồn tại, vui lòng thêm mã thiết bị khác!");
                    return;
                }
                else
                {
                    tb.ThemThietBi(txtMaThietBi.Text, txtTenThietBi.Text, int.Parse(txtSoLuong.Text), txtTinhTrang.Text, ngaynhap, MaLoaiThietBi, MaDonViTinh, MaNhaCungCap, MaKho, MaPhongBan, txtMoTa.Text);
                    MessageBox.Show("Thêm mới thiết bị thành công");
                }
            }
            else {
                tb.CapNhatThietBi(txtMaThietBi.Text, txtTenThietBi.Text, int.Parse(txtSoLuong.Text), txtTinhTrang.Text, ngaynhap, MaLoaiThietBi, MaDonViTinh, MaNhaCungCap, MaKho, MaPhongBan, txtMoTa.Text);
                MessageBox.Show("Cập nhật thiết bị thành công");
            }
            txtMaThietBi.Enabled = false;
            lsvThietBi.Items.Clear();
            HienthiThietBi();
            setNull();
            setButton(true);
        }

        private void btnHuy_Click(object sender, EventArgs e)
        {
            setButton(true);
        }

        private void btnThoat_Click(object sender, EventArgs e)
        {
            this.Dispose();
        }
    }
}
