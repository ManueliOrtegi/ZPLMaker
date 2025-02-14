using DevExpress.XtraSplashScreen;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.IO;
using System.Text;
using System.Windows.Forms;

namespace ZPLMaker
{
    public partial class frmSS : SplashScreen
    {
        public frmSS()
        {
            InitializeComponent();
        }

        #region Overrides

        public override void ProcessCommand(Enum cmd, object arg)
        {
            base.ProcessCommand(cmd, arg);
        }

        #endregion

        public enum SplashScreenCommand
        {
        }
        private string GetCurrentMessage(string _crrntMsg) 
        {
            if (_crrntMsg == "Loading")
            {
                lblDisplayMessage.Text = "Getting information";
            }
            return lblDisplayMessage.Text;
        }
        private void frmSS_Load(object sender, EventArgs e)
        {
            GetCurrentMessage("Loading");
            CopyFilesRecursively("C:\\Users\\032902\\Documents\\Projects\\ZPLMaker\\ZPLMaker\\bin\\Debug", "C:\\ZPL Maker");
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
            }
        }
    }
}