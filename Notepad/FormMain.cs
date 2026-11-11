using System;
using System.Windows.Forms;

namespace Notepad
{
    public partial class FormMain : Form
    {
        const string PROGRAM_NAME_SHORT = "Blocco note";
        const string PROGRAM_NAME_FULL = "Blocco note di Windows";

        string fileName = "Senza nome";
        string lastSavedContent = string.Empty;

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
            string prepend = rtbMain.Text != lastSavedContent ? "*" : "";
            Text = $"{prepend}{fileName} - {PROGRAM_NAME_FULL}";
        }

        private void nuovoToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (rtbMain.Text != lastSavedContent)
            {
                // c'è qualcosa da salvare
                DialogResult rispostaMessageBox = MessageBox.Show(
                        $"Salvare le modifiche a {fileName}?",
                        "Blocco note",
                        MessageBoxButtons.YesNoCancel
                    );
                switch (rispostaMessageBox)
                {
                    case DialogResult.Yes:
                        // TODO:: salvo
                        CreoNuovo();
                        break;
                    case DialogResult.No:
                        CreoNuovo();
                        break;
                }
            }
        }

        private void CreoNuovo()
        {
            fileName = "Senza nome";
            rtbMain.Text = "";
            lastSavedContent = "";
        }
    }
}
