using DevExpress.XtraEditors;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using BinaryKits.Zpl.Label;
using BinaryKits.Zpl.Label.Elements;
using System.Net;
using System.IO;
using static DevExpress.Utils.MVVM.Services.DocumentManagerService;
using System.Drawing.Imaging;
using System.Collections;
using DevExpress.XtraEditors.DXErrorProvider;
using System;
using System.Management;
using System.Reflection;
using System.Net.NetworkInformation;

namespace ZPLMaker
{
    public partial class ZPLMaker : DevExpress.XtraEditors.XtraForm
    {
        public ZPLMaker()
        {
            InitializeComponent();
        }

        private void ZPLMaker_Load(object sender, EventArgs e)
        {   
            LoadDirectoryPath();
            btnPrint.Text = "Print";
            this.Text = this.Text + " v" + Assembly.GetExecutingAssembly().GetName().Version.ToString();
        }

        private void btnRefresh_Click(object sender, EventArgs e)
        {
            if (txtZPLText.Text == "" || txtZPLText.Text == string.Empty) { return; }

            byte[] zpl = Encoding.UTF8.GetBytes(txtZPLText.Text);

            // adjust print density (12dpmm), label width (1 inches), label height (1 inches), and label index (0) as necessary
            var request = (HttpWebRequest)WebRequest.Create($"http://api.labelary.com/v1/printers/" + cmbDensity.Text + "/labels/" + txtWidth.Text + "x" + txtLength.Text + "/" + txtShowLabel.Text + "/");
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
                zplPanel.BackgroundImage = _img;
                zplPanel.BackgroundImageLayout = ImageLayout.Zoom;
                responseStream.Close();
                fileStream.Close();
            }
            catch (WebException ex)
            {
                //Console.WriteLine("Error: {0}", ex.Status);
                XtraMessageBox.Show(ex.Message, "ZPL Maker", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }
        }

        private void btnRotate_Click(object sender, EventArgs e)
        {
            if (zplPanel.BackgroundImage != null)
            {
                zplPanel.BackgroundImage.RotateFlip(RotateFlipType.Rotate90FlipXY);
                zplPanel.Refresh();
            }
        }

        private void btnPrint_Click(object sender, EventArgs e)
        {
            try
            {
                if (zplPanel.BackgroundImage != null)
                {
                    if (txtZPLText.Text == "") { return; }
                    if (btnPrint.Text == "Print") { XtraMessageBox.Show("Please select Printer." + Environment.NewLine + "Click 'Set As Default' button.", "ZPL Maker", MessageBoxButtons.OK, MessageBoxIcon.Warning); return; }
                    if (lblConnectionStatus.Text == "x")
                    {
                        XtraMessageBox.Show("Printer is not available." + Environment.NewLine + "Please check the configuration", "ZPL Viewer", MessageBoxButtons.OK, MessageBoxIcon.Error);
                        return;
                    }
                    int qty = int.Parse(txtPrintQuantity.Text);
                    for (int q = 0; q < qty; q++)
                    {
                        PrintZPLviaSocket(txtZPLText.Text);
                    }
                }
            }
            catch(Exception ex)
            {
                XtraMessageBox.Show(ex.Message, "ZPL Viewer", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
        }
        private void PrintZPLviaSocket(string zpl)
        {
            string ipAddress = txtIPAddress.Text;
            int port = int.Parse(txtPort.Text);

            using (System.Net.Sockets.TcpClient client = new System.Net.Sockets.TcpClient())
            {
                client.Connect(ipAddress, port);
                using (StreamWriter writer = new StreamWriter(client.GetStream()))
                {
                    writer.Write(zpl);
                    writer.Flush();
                    writer.Close();
                }
                client.Close();
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
        void IPConStat(bool _stat)
        {
            if (_stat == true)
            {
                lblConnectionStatus.Text = "✓";
                lblConnectionStatus.ForeColor = Color.Green;
            }
            else
            {
                lblConnectionStatus.Text = "x";
                lblConnectionStatus.ForeColor = Color.Red;
            }
        }
        public bool ValidateZPL(string ipString)
        {
            //if (String.IsNullOrWhiteSpace(ipString))
            //{
            //    return false;
            //}

            //string[] splitValues = ipString.Split('.');
            //if (splitValues.Length != 4)
            //{
            //    return false;
            //}

            //byte tempForParsing;

            //return splitValues.All(r => byte.TryParse(r, out tempForParsing));

            Ping ping = new Ping();
            string ip = txtIPAddress.Text;
            IPAddress address = IPAddress.Parse(ip);
            PingReply pingReply = ping.Send(address);
            if(pingReply.Status != IPStatus.Success)
            {
                return false;
            }
            return true;
        }
        private void GetAllPrinterList()
        {
            ManagementScope objScope = new ManagementScope(ManagementPath.DefaultPath); //For the local Access
            objScope.Connect();

            SelectQuery selectQuery = new SelectQuery();
            selectQuery.QueryString = "Select * from win32_Printer";
            ManagementObjectSearcher MOS = new ManagementObjectSearcher(objScope, selectQuery);
            ManagementObjectCollection MOC = MOS.Get();
            foreach (ManagementObject mo in MOC)
            {
                //cmbPrinterList.Items.Add(mo["Name"].ToString());
                cmbPrinterList.Properties.Items.Add(mo["Name"].ToString());
            }
        }

        private void tcMain_Click(object sender, EventArgs e)
        {
            if (tcMain.SelectedTabPageIndex == 1)
            {
                GetAllPrinterList();
            }
        }

        private void btnCheckIfOnline_Click(object sender, EventArgs e)
        {
            ManagementScope objScope = new ManagementScope(ManagementPath.DefaultPath); //For the local Access
            objScope.Connect();

            SelectQuery selectQuery = new SelectQuery();
            selectQuery.QueryString = "Select * from win32_Printer";
            ManagementObjectSearcher MOS = new ManagementObjectSearcher(objScope, selectQuery);
            ManagementObjectCollection MOC = MOS.Get();
            foreach (ManagementObject mo in MOC)
            {
                string name = mo["Name"].ToString();
                
                if(name == mo["Name"].ToString())
                {
                    int state = Int32.Parse(mo["ExtendedPrinterStatus"].ToString());
                    if ((state == 1) || //Other
                       (state == 2) || //Unknown
                       (state == 7) || //Offline
                       (state == 9) || //error
                       (state == 11) //Not Available
                       )
                    {
                        txtPrinterStatus.Text = "Offline";
                    }

                    state = Int32.Parse(mo["DetectedErrorState"].ToString());
                    if (state != 2) //No error
                    {
                        txtPrinterStatus.Text = "Online";
                    }
                }
            }
        }

        private void btnZPLSetAsDefault_Click(object sender, EventArgs e)
        {
            if (lblConnectionStatus.Text != "x")
            {
                btnPrint.Text = "Print as ZPL";
            }
        }

        private void btnOtherSetAsDefault_Click(object sender, EventArgs e)
        {
            if(txtPrinterStatus.Text == "Online")
            {
                btnPrint.Text = "Print as Other";
            }
        }

        private void btnDownloadJPG_Click(object sender, EventArgs e)
        {
            
            try
            {
                if (zplPanel.BackgroundImage != null)
                {
                    if (txtDirectoryPath.Text != "")
                    {
                        Bitmap bmp = new Bitmap(zplPanel.BackgroundImage, DPIConversion(Convert.ToInt32(decimal.Parse(txtWidth.Text))), DPIConversion(Convert.ToInt32(decimal.Parse(txtLength.Text))));
                        bmp.Save(txtDirectoryPath.Text + "\\label.jpeg", ImageFormat.Jpeg);
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

        private void btnDownload_Click(object sender, EventArgs e)
        {
            flyoutPanel1.Options.AnchorType = (DevExpress.Utils.Win.PopupToolWindowAnchor)DevExpress.Utils.Win.PopupToolWindowAnimation.Fade;
            flyoutPanel1.ShowBeakForm();
        }

        private void btnDownloadPNG_Click(object sender, EventArgs e)
        {
            try
            {
                if (zplPanel.BackgroundImage != null)
                {
                    if (txtDirectoryPath.Text != "")
                    {
                        Bitmap bmp = new Bitmap(zplPanel.BackgroundImage, DPIConversion(Convert.ToInt32(decimal.Parse(txtWidth.Text))), DPIConversion(Convert.ToInt32(decimal.Parse(txtLength.Text))));
                        bmp.Save(txtDirectoryPath.Text + "\\label.png", ImageFormat.Png);
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

        private void btnChangeLocation_Click(object sender, EventArgs e)
        {
            //String hostName = System.Environment.MachineName;
            String DirectoryPath = @"C:\ZPL Maker\DirectoryPath.txt";
            FolderBrowserDialog folderDlg = new FolderBrowserDialog();
            folderDlg.ShowNewFolderButton = true;
            DialogResult result = folderDlg.ShowDialog();
            if (result == DialogResult.OK)
            {
                var filePath = folderDlg.SelectedPath;
                txtDirectoryPath.Text = filePath;
            }
            if (File.Exists(DirectoryPath))
            {
                System.IO.File.WriteAllText(DirectoryPath, txtDirectoryPath.Text);
            }
            else
            {
                // Create a new file
                using (FileStream fs = File.Create(DirectoryPath))
                {
                    System.IO.File.WriteAllText(DirectoryPath, txtDirectoryPath.Text);
                }

                
            }

        }
        void LoadDirectoryPath()
        {
            try
            {
                String DirectoryPath = @"C:\ZPL Maker\DirectoryPath.txt";
                // Open the stream and read it back.
                if (File.Exists(DirectoryPath))
                {
                    using (StreamReader sr = File.OpenText(DirectoryPath))
                    {
                        string s = "";
                        while ((s = sr.ReadLine()) != null)
                        {
                            txtDirectoryPath.Text = s;
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                XtraMessageBox.Show(ex.Message, "ZPL Viewer", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
        }
        private void btnDownloadPDF_Click(object sender, EventArgs e)
        {
            try
            {
                if (zplPanel.BackgroundImage != null)
                {
                    XtraMessageBox.Show("PDF download not available yet.", "ZPL Viewer", MessageBoxButtons.OK, MessageBoxIcon.Information);
                }
            }
            catch (Exception ex)
            {
                XtraMessageBox.Show(ex.Message, "ZPL Viewer", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
        }

        private void btnTestConnection_Click(object sender, EventArgs e)
        {
            try
            {
                if (ValidateZPL(txtIPAddress.Text))
                {
                    IPConStat(true);
                }
                else
                {
                    IPConStat(false);
                }
            }
            catch (Exception ex)
            {
                XtraMessageBox.Show(ex.Message, "ZPL Viewer", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }

        }

        private void btnOpenFile_Click(object sender, EventArgs e)
        {

        }

        private void btnZPL_Click(object sender, EventArgs e)
        {
            frmZPL _frm = new frmZPL();
            _frm._directoryPath = txtDirectoryPath.Text;
            _frm.ShowDialog();
        }
    }
}