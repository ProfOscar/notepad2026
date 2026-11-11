using System;
using System.Windows.Forms;

namespace Notepad
{
    public partial class FormMain : Form
    {
        const string PROGRAM_NAME = "Blocco note di Windows";

        string fileName = "Senza nome";
        string lastContentSaved = string.Empty;

        public FormMain()
        {
            InitializeComponent();
            setTitle();
        }

        private void esciToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Close();
        }

        private void rtbMain_TextChanged(object sender, EventArgs e)
        {
            setTitle();
        }

        private void setTitle()
        {
            string prepend = rtbMain.Text != lastContentSaved ? "*" : "";
            Text = $"{prepend}{fileName} - {PROGRAM_NAME}";
        }
    }
}
