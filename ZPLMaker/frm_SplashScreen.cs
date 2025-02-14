using DevExpress.XtraEditors;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Forms;
using DevExpress.UserSkins;

namespace ZPLMaker
{
    public partial class frm_SplashScreen : DevExpress.XtraEditors.XtraForm
    {
        #region "Variables"
        String CopyFilesPath = "\\150.200.3.194\\shared\\ZPL Maker\\ZPL Maker";
        String MyFilesPath = "C:\\ZPL Maker";
        String MyFilesApp = "C:\\ZPL Maker\\ZPLMaker.exe";
        int fileCount = 0;
        String CurrFileName;
        Thread _thread;
        public string _replicateAssembly { get; set; }
        #endregion
        public frm_SplashScreen()
        {
            InitializeComponent();
        }

        private void frm_SplashScreen_Load(object sender, EventArgs e)
        {
            System.Windows.Forms.Control.CheckForIllegalCrossThreadCalls = false;
            fileCounter();
            backgroundWorker1.RunWorkerAsync();
            //StartUp();
        }
        private void StartUp()
        {
            
        }

        private string GetCurrentMessage(string _crrntMsg)
        {
            if (_crrntMsg == "StartUp")
            {
                lblDisplayMessage.Text = "Getting assembly information";
            }
            if (_crrntMsg == "CreatingFolder")
            {
                lblDisplayMessage.Text = "Creating Folder";
            }
            if (_crrntMsg == "Fetch")
            {
                lblDisplayMessage.Text = "Fetching files";
            }
            return lblDisplayMessage.Text;
        }

        private bool ValidateAssembly()
        {
            try
            {
                string _myAssembly = Assembly.GetExecutingAssembly().GetName().Version.ToString();
                //string _replicateAssembly = Assembly.LoadFile(MyFilesApp).GetName().Version.ToString();
                _replicateAssembly = Assembly.LoadFile(MyFilesApp).GetName().Version.ToString();
                bool IsValid = false;

                if (_myAssembly == _replicateAssembly)
                {
                    IsValid = true;
                }
                else
                {
                    IsValid = false;
                }

                return IsValid;
            }
            
            catch (Exception ex)
            {
                return false;
            }
        }
        private void CheckIfFolderExistsElseCreate()
        {
            if (!Directory.Exists(MyFilesPath))
            {
                Thread.Sleep(1000);
                GetCurrentMessage("CreatingFolder");
                Directory.CreateDirectory(MyFilesPath);
            }

        }
        private static void CopyFilesRecursively(string sourcePath, string targetPath)
        {
            //Now Create all of the directories
            foreach (string dirPath in Directory.GetDirectories(sourcePath, "*", SearchOption.AllDirectories))
            {
                Directory.CreateDirectory(dirPath.Replace(sourcePath, targetPath));
                
            }
            //Copy all the files & Replaces any files with the same name
            foreach (string newPath in Directory.GetFiles(sourcePath, "*.*", SearchOption.AllDirectories))
            {
                File.Copy(newPath, newPath.Replace(sourcePath, targetPath), true);
                String CurrFileName = System.IO.Path.GetFileName(newPath);
            }
        }
        void fileCounter()
        {
            int _percentage = 0;
            string _path = @"\" + CopyFilesPath;
            fileCount = Directory.EnumerateFiles(_path, "*.*", SearchOption.AllDirectories).Count();
            _percentage = fileCount / 100;
            lblFileCount.Text = "0/"+ fileCount.ToString();
        }

        private void backgroundWorker1_DoWork(object sender, DoWorkEventArgs e)
        {

            string sourcePath = "C:\\Users\\032902\\Documents\\Projects\\ZPLMaker\\ZPLMaker\\bin\\Debug";
            string targetPath= "C:\\ZPL Maker";
            int q = 2;
            Thread.Sleep(1000);
            GetCurrentMessage("StartUp");
            CheckIfFolderExistsElseCreate();
            if (ValidateAssembly())
            {
                Thread.Sleep(1000);
                lblDisplayMessage.Text = "Opening ZPL Maker...";
                lblPercentage.Text = "100%";
                Thread.Sleep(2000);
                RunZPLMaker();
            }
            else
            {
                foreach (string dirPath in Directory.GetDirectories(sourcePath, "*", SearchOption.AllDirectories))
            {
                Thread.Sleep(1000);
                lblDisplayMessage.Text = "Creating Directories...";
                Directory.CreateDirectory(dirPath.Replace(sourcePath, targetPath));
            }
            //Copy all the files & Replaces any files with the same name
            foreach (string newPath in Directory.GetFiles(sourcePath, "*.*", SearchOption.AllDirectories))
            {
                Thread.Sleep(100);
                lblDisplayMessage.Text = "Copying Files...";
                File.Copy(newPath, newPath.Replace(sourcePath, targetPath), true);
                lblFileCount.Text = q + "/" + fileCount.ToString();
                Decimal _percentage = decimal.Divide(q, fileCount) * 100;
                lblPercentage.Text = _percentage.ToString("#") + "%";
                q += 1;
            }
            Thread.Sleep(1000);
            lblDisplayMessage.Text = "Opening ZPL Maker...";
            RunZPLMaker();
            }

        }

        private void btnClose_Click(object sender, EventArgs e)
        {
            if (XtraMessageBox.Show("Do you really want to close?" + Environment.NewLine + "It may affect the fetching of files", "ZPL Maker",MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.Yes)
            {
                this.Dispose();
            }
        }
        void RunZPLMaker()
        {
            ZPLMaker _zplMaker = new ZPLMaker();
            this.Visible = false;
            _zplMaker.ShowDialog();
            System.Windows.Forms.Application.Exit();
        }
        private void timer1_Tick(object sender, EventArgs e)
        {
           
        }
    }
}