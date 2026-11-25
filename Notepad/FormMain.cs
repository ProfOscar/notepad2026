using System;
using System.IO;
using System.Windows.Forms;

namespace Notepad
{
    public partial class FormMain : Form
    {
        #region costanti

        const string PROGRAM_NAME_SHORT = "Blocco note";
        const string PROGRAM_NAME_FULL = "Blocco note di Windows";

        #endregion

        #region variabili

        string filePath = "";
        string fileName = "Senza nome";
        string lastSavedContent = string.Empty;
        bool isCancelSave = false;

        #endregion

        #region inizializzazioni

        public FormMain()
        {
            InitializeComponent();
            SetTitle();
        }

        #endregion

        #region interazioni_utente

        private void nuovoToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (rtbMain.Text != lastSavedContent)
            {
                // c'è qualcosa da salvare
                DialogResult rispostaMessageBox = MessageBox.Show(
                        $"Salvare le modifiche a {fileName}?",
                        PROGRAM_NAME_SHORT,
                        MessageBoxButtons.YesNoCancel
                    );
                switch (rispostaMessageBox)
                {
                    case DialogResult.Yes:
                        salvaToolStripMenuItem_Click(sender, e);
                        if (!isCancelSave) CreateNew();
                        break;
                    case DialogResult.No:
                        CreateNew();
                        break;
                }
            }
        }
        
        private void salvaToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (filePath != "")
                SaveFile(filePath, rtbMain.Text);
            else
                salvaconnomeToolStripMenuItem_Click(sender, e);
        }

        private void salvaconnomeToolStripMenuItem_Click(object sender, EventArgs e)
        {
            isCancelSave = false;
            DialogResult rispostaDialog = saveFileDialog.ShowDialog();
            if (rispostaDialog == DialogResult.OK)
                SaveFile(saveFileDialog.FileName, rtbMain.Text);
            else
                isCancelSave = true;
        }

        private void esciToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Close();
        }

        #endregion

        #region gestori_evento

        private void rtbMain_TextChanged(object sender, EventArgs e)
        {
            SetTitle();
        }

        private void FormMain_FormClosing(object sender, FormClosingEventArgs e)
        {
            if (lastSavedContent != rtbMain.Text)
            {
                // c'è qualcosa da salvare
                DialogResult rispostaMessageBox = MessageBox.Show(
                        $"Salvare le modifiche a {fileName}?",
                        PROGRAM_NAME_SHORT,
                        MessageBoxButtons.YesNoCancel
                    );
                switch (rispostaMessageBox)
                {
                    case DialogResult.Yes:
                        salvaToolStripMenuItem_Click(sender, e);
                        if (isCancelSave) e.Cancel = true;
                        break;
                    case DialogResult.Cancel:
                        // annullo la chiusura della form
                        e.Cancel = true;
                        break;
                }

            }
        }

        #endregion

        #region funzioni_custom

        private void SetTitle()
        {
            string prepend = rtbMain.Text != lastSavedContent ? "*" : "";
            Text = $"{prepend}{fileName} - {PROGRAM_NAME_FULL}";
        }

        private void CreateNew()
        {
            filePath = "";
            fileName = "Senza nome";
            rtbMain.Text = "";
            lastSavedContent = "";
            SetTitle();
        }

        private void SaveFile(string file, string content)
        {
            try
            {
                File.WriteAllText(file, content);
                filePath = file;
                fileName = Path.GetFileName(file);
                lastSavedContent = content;
                SetTitle();
            }
            catch (Exception)
            {
                MessageBox.Show("Salvataggio non riuscito.",
                    "Salva file", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }
        }

        #endregion
    }
}
