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
using System.Diagnostics;
using System.Security.Principal;

namespace ZPLMaker_AutoUpdate
{
    public partial class frm_SplashScreen : DevExpress.XtraEditors.XtraForm
    {
        #region "Variables"
        String CopyFilesPath = "\\150.200.3.194\\shared\\ZPL Maker\\ZPL Maker";
        String CopyFilesApp = @"\\150.200.3.194\\shared\\ZPL Maker\\ZPL Maker\\ZPLMaker.exe";
        String MyFilesPath = "C:\\ZPL Maker";
        String MyFilesApp = "C:\\ZPL Maker\\ZPLMaker.exe";
        int fileCount = 0;
        String CurrFileName;
        Thread _thread;
        public string _replicateAssembly { get; set; }
        public string _myAssembly { get; set; }
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
                _replicateAssembly = Assembly.LoadFile(MyFilesApp).GetName().Version.ToString();
                //_myAssembly = Assembly.LoadFile(CopyFilesApp).GetName().Version.ToString();
                var versionInfo = FileVersionInfo.GetVersionInfo(CopyFilesApp);
                _myAssembly = versionInfo.FileVersion;
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
                //MessageBox.Show(ex.Message);
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

            try
            {
                string sourcePath = @"\\150.200.3.194\Shared\ZPL Maker\ZPL Maker";
                string targetPath = @"C:\ZPL Maker";
                string fileName = string.Empty;
                string destFile = string.Empty;
                int q = 3;
                Thread.Sleep(1000);
                GetCurrentMessage("StartUp");
                CheckIfFolderExistsElseCreate();
                if (ValidateAssembly() == true)
                {
                    Thread.Sleep(1000);
                    lblDisplayMessage.Text = "Opening ZPL Maker...";
                    lblPercentage.Text = "100%";
                    Thread.Sleep(2000);
                    RunZPLMaker();
                }
                else
                {
                    RenameFileName();
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
                        //File.Delete(MyFilesApp);
                        //var sourceFile = new FileInfo(sourcePath);
                        //sourceFile.CopyTo(newPath, true);
                        //if(File.Exists(MyFilesApp))
                        //{
                        //    File.Delete(MyFilesApp);
                        //}
                        //System.IO.File.Copy(newPath, newPath.Replace(sourcePath, targetPath), true);
                        System.IO.File.Copy(newPath, newPath.Replace(sourcePath, targetPath), true);
                        lblFileCount.Text = q + "/" + fileCount.ToString();
                        Decimal _percentage = decimal.Divide(q, fileCount) * 100;
                        lblPercentage.Text = _percentage.ToString("#") + "%";
                        q += 1;
                    }
                    //File.SetAttributes(MyFilesApp, FileAttributes.Normal);
                    //File.Delete(MyFilesApp);
                    //Process[] proc = Process.GetProcessesByName("ZPLMaker.exe");
                    //proc[0].Kill();
                    //var proce = Process.GetProcesses()
                    //                .Where(pr => pr.ProcessName == "ZPLMaker");
                    //foreach (var process in proce)
                    //{
                    //    process.Kill();
                    //}

                    //string[] files = System.IO.Directory.GetFiles(sourcePath);
                    //// Copy the files and overwrite destination files if they already exist. 
                    //foreach (string s in files)
                    //{
                    //    // Use static Path methods to extract only the file name from the path.
                    //    Thread.Sleep(10);
                    //    lblDisplayMessage.Text = "Copying Files...";
                    //    fileName = System.IO.Path.GetFileName(s);
                    //    destFile = System.IO.Path.Combine(targetPath, fileName);
                    //    System.IO.File.Copy(s, destFile, true);
                    //    System.IO.File.SetAttributes(s, FileAttributes.Normal);
                    //    lblFileCount.Text = q + "/" + fileCount.ToString();
                    //    Decimal _percentage = decimal.Divide(q, fileCount) * 100;
                    //    lblPercentage.Text = _percentage.ToString("#") + "%";
                    //    q += 1;
                    //}
                    Thread.Sleep(1000);
                    lblDisplayMessage.Text = "Opening ZPL Maker...";
                    RunZPLMaker();
                }
            }
            catch (Exception ex)
            {
                XtraMessageBox.Show(ex.Message,"ZPL Maker", MessageBoxButtons.OK, MessageBoxIcon.Warning);
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
            this.Visible = false;
            Process.Start(MyFilesApp);
            System.Windows.Forms.Application.Exit();
        }
        void RenameFileName()
        {
            string sourceFile = @"C:\ZPL Maker\ZPLMaker.exe";
            string sourceFile1 = @"C:\ZPL Maker\ZPLMaker.exe.config";
            string sourceFile2 = @"C:\ZPL Maker\ZPLMaker.pdb";
            // Create a FileInfo  
            System.IO.FileInfo fi = new System.IO.FileInfo(sourceFile);
            System.IO.FileInfo fi1 = new System.IO.FileInfo(sourceFile1);
            System.IO.FileInfo fi2 = new System.IO.FileInfo(sourceFile2);
            // Check if file is there  
            if (fi.Exists)
            {
                // Move file with a new name. Hence renamed.  
                fi.MoveTo(@"C:\ZPL Maker\Cache\ZPLMaker"+ _myAssembly + ".exe");
            }
            if (fi1.Exists)
            {
                // Move file with a new name. Hence renamed.  
                fi1.MoveTo(@"C:\ZPL Maker\Cache\ZPLMaker.exe" + _myAssembly + ".config");
            }
            if (fi2.Exists)
            {
                // Move file with a new name. Hence renamed.  
                fi2.MoveTo(@"C:\ZPL Maker\Cache\ZPLMaker" + _myAssembly + ".pdb");
            }
        }
        private void timer1_Tick(object sender, EventArgs e)
        {
           
        }
    }
}