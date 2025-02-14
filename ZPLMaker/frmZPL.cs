using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Linq;
using System.Threading.Tasks;
using System.Windows.Forms;
using DevExpress.XtraEditors;
using System.IO;
using ExcelDataReader;
using System.Net;
using System.Drawing.Imaging;
using ZPLMaker;

namespace ZPLMaker
{
    public partial class frmZPL : DevExpress.XtraEditors.XtraForm
    {
        public frmZPL()
        {
            InitializeComponent();
        }
        public string _directoryPath;
        private void btnZPL_Load(object sender, EventArgs e)
        {

        }

        private void btnOpenFile_Click(object sender, EventArgs e)
        {
            DataTable dtExcel = new DataTable();
            DataTable dtErrorLogs = new DataTable();
            dtExcel.TableName = "DocumentElement";
            dtErrorLogs.TableName = "DocumentElement";
            string[] excelCols = { };
            string colName = "";
            bool IsValid = true;
            try
            {
                using (OpenFileDialog dialog = new OpenFileDialog())
                {
                    dialog.Filter = "Excel Files|*.xlsx;*.xls;";
                    if (dialog.ShowDialog() == DialogResult.OK)
                    {
                        FileStream stream = File.Open(dialog.FileName, FileMode.Open, FileAccess.Read);

                        IExcelDataReader excelReader = ExcelReaderFactory.CreateReader(stream);
                        DataSet result = excelReader.AsDataSet(new ExcelDataSetConfiguration()
                        {
                            ConfigureDataTable = (_) => new ExcelDataTableConfiguration()
                            {
                                UseHeaderRow = true
                            }
                        });

                        dtExcel = result.Tables[0];
                        gridZPLCon.DataSource = dtExcel;
                        //gridZPL.BestFitColumns();

                        foreach(DataColumn column in dtExcel.Columns)
                        {
                            //if(column.ColumnName != "ZPLResult")
                            //{
                                gridZPL.Columns[column.ColumnName].OptionsColumn.AllowEdit = false;
                                gridZPL.Columns[column.ColumnName].OptionsColumn.ReadOnly = true;
                            //}
                        }
                        //gridZPL.Columns["ZPLResult"].ColumnEdit = imgZpl1;
                    } 
                    else
                    {
                        return;
                    }
                }

            }
            catch (Exception E)
            {
                MessageBox.Show("An error encountered during Upload. Please contact IT Sysdev." + Environment.NewLine + E.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        void GetZPL()
        {
            for (int q = 0; q < gridZPL.RowCount; q++)
            {
                byte[] zpl = Encoding.UTF8.GetBytes(gridZPL.GetRowCellValue(q, "ZPLText").ToString());

                // adjust print density (12dpmm), label width (1 inches), label height (1 inches), and label index (0) as necessary
                var request = (HttpWebRequest)WebRequest.Create($"http://api.labelary.com/v1/printers/8dpmm/labels/2.5x2.5/0/");
                request.Method = "POST";
                //request.Accept = "application/jpg";
                request.ContentType = "application/x-www-form-urlencoded";
                request.ContentLength = zpl.Length;

                var requestStream = request.GetRequestStream();
                requestStream.Write(zpl, 0, zpl.Length);
                requestStream.Close();

                try
                {
                    var response = (HttpWebResponse)request.GetResponse();
                    var responseStream = response.GetResponseStream();
                    var fileStream = File.Create("zplimg.jpg"); // change file name for PNG images
                    responseStream.CopyTo(fileStream);
                    var _img = Image.FromStream(fileStream);
                    //zplPanel.Image = _img;
                    imgZpl1.Images = _img;
                    //imgZPL1.BackgroundImage = _img;
                    //imgZPL1.BackgroundImageLayout = ImageLayout.Zoom;
                    gridZPL.SetRowCellValue(q, "ZPLResult", imgZpl1.Images);
                    responseStream.Close();
                    fileStream.Close();
                }
                catch (Exception e)
                {
                    MessageBox.Show(e.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
        }

        private void btnRedraw_Click(object sender, EventArgs e)
        {
            //GetZPL();
        }

        private void gridZPLCon_Click(object sender, EventArgs e)
        {
            if(gridZPL.RowCount == 0)
            {
                return;
            }

            byte[] zpl = Encoding.UTF8.GetBytes(gridZPL.GetRowCellValue(gridZPL.FocusedRowHandle, "ZPLText").ToString());

            // adjust print density (12dpmm), label width (1 inches), label height (1 inches), and label index (0) as necessary
            var request = (HttpWebRequest)WebRequest.Create($"http://api.labelary.com/v1/printers/8dpmm/labels/2.5x2.5/0/");
            request.Method = "POST";
            //request.Accept = "application/jpg";
            request.ContentType = "application/x-www-form-urlencoded";
            request.ContentLength = zpl.Length;

            var requestStream = request.GetRequestStream();
            requestStream.Write(zpl, 0, zpl.Length);
            requestStream.Close();

            try
            {
                var response = (HttpWebResponse)request.GetResponse();
                var responseStream = response.GetResponseStream();
                var fileStream = File.Create("zplimg.jpg"); // change file name for PNG images
                responseStream.CopyTo(fileStream);
                var _img = Image.FromStream(fileStream);
                zplPanel.BackgroundImage = _img;
                zplPanel.BackgroundImageLayout = ImageLayout.Zoom;
                responseStream.Close();
                fileStream.Close();
            }
            catch (Exception a)
            {
                MessageBox.Show(a.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void btnDownload_Click(object sender, EventArgs e)
        {
            flyoutPanel1.Options.AnchorType = (DevExpress.Utils.Win.PopupToolWindowAnchor)DevExpress.Utils.Win.PopupToolWindowAnimation.Fade;
            flyoutPanel1.ShowBeakForm();
        }

        private void btnDownloadPNG_Click(object sender, EventArgs e)
        {
            try
            {
                ZPLMaker _zplMaker = new ZPLMaker();
                string dp = _zplMaker.txtDirectoryPath.Text;
                if (zplPanel.BackgroundImage != null)
                {
                    if (_directoryPath != "")
                    {
                        Bitmap bmp = new Bitmap(zplPanel.BackgroundImage, DPIConversion(Convert.ToInt32(decimal.Parse("2.5"))), DPIConversion(Convert.ToInt32(decimal.Parse("2.5"))));
                        bmp.Save(_directoryPath + "\\"+ gridZPL.GetRowCellValue(gridZPL.FocusedRowHandle, "SKUCode").ToString() + ".png", ImageFormat.Png);
                        XtraMessageBox.Show("Image successfuly saved.", "ZPL Viewer", MessageBoxButtons.OK, MessageBoxIcon.Information);
                        return;
                    }
                    XtraMessageBox.Show("Please select Destination Path first.", "ZPL Viewer", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                }
            }
            catch (Exception ex)
            {
                XtraMessageBox.Show(ex.Message, "ZPL Viewer", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
        }

        private void btnDownloadJPG_Click(object sender, EventArgs e)
        {
            try
            {
                ZPLMaker _zplMaker = new ZPLMaker();
                string dp = _zplMaker.txtDirectoryPath.Text;
                if (zplPanel.BackgroundImage != null)
                {
                    if (_directoryPath != "")
                    {
                        Bitmap bmp = new Bitmap(zplPanel.BackgroundImage, DPIConversion(Convert.ToInt32(decimal.Parse("2.5"))), DPIConversion(Convert.ToInt32(decimal.Parse("2.5"))));
                        bmp.Save(_directoryPath + "\\label.jpeg", ImageFormat.Jpeg);
                        XtraMessageBox.Show("Image successfuly saved.", "ZPL Viewer", MessageBoxButtons.OK, MessageBoxIcon.Information);
                        return;
                    }
                    XtraMessageBox.Show("Please select Destination Path first.", "ZPL Viewer", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                }
            }
            catch (Exception ex)
            {
                XtraMessageBox.Show(ex.Message, "ZPL Viewer", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
        }
        private int DPIConversion(int _pxlvl)
        {
            int _pxl = 96;
            int _in = 0;
            int _pxlValue = 0;

            _in = _pxlvl;
            _pxlValue = _in * _pxl;

            return _pxlValue;
        }
    }
}