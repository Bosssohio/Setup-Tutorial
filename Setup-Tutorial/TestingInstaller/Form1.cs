using System;
using System.IO;
using System.Windows.Forms;

namespace TestingInstaller
{
    public partial class Form1 : Form
    {
        public string CurrentLanguage { get; private set; }
        private string installationPath;

        public Form1(string selectedLanguage)
        {
            InitializeComponent();
            CurrentLanguage = selectedLanguage;
            // Find lblSelectedLanguage control
            foreach (Control control in panel1.Controls)
            {
                if (control is Label && control.Name == "lblSelectedLanguage")
                {
                    lblSelectedLanguage = (Label)control;
                    break;
                }
            }
            UpdateUIForLanguage();
            txtDestinationFolder.Text = Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.ProgramFiles), "YourAppName");
            installationPath = txtDestinationFolder.Text; // Initialize installationPath
        }

        private void UpdateUIForLanguage()
        {
            // Use the CurrentLanguage property to set the text of your labels,
            // button texts, etc., based on the selected language.

            switch (CurrentLanguage)
            {
                case "English":
                    label1.Text = "Choose Destination Folders";
                    label2.Text = "Where should the application be installed?";
                    button1.Text = "Browse...";
                    button3.Text = "Next";
                    button2.Text = "Exit";
                    // ... set other English text ...
                    break;
                case "Spanish":
                    label1.Text = "Elegir carpetas de destino";
                    label2.Text = "¿Dónde debería instalarse la aplicación?";
                    button1.Text = "Examinar...";
                    button3.Text = "Siguiente";
                    button2.Text = "Salir";
                    // ... set other Spanish text ...
                    break;
                case "French":
                    label1.Text = "Choisir les dossiers de destination";
                    label2.Text = "Où l'application doit-elle être installée ?";
                    button1.Text = "Parcourir...";
                    button3.Text = "Suivant";
                    button2.Text = "Quitter";
                    // ... set other French text ...
                    break;
                case "Vietnamese":
                    label1.Text = "Chọn thư mục đích";
                    label2.Text = "Ứng dụng sẽ được cài đặt ở đâu?";
                    button1.Text = "Duyệt...";
                    button3.Text = "Tiếp tục";
                    button2.Text = "Thoát";
                    // ... set other Vietnamese text ...
                    break;
                default:
                    // Set default language (e.g., English)
                    label1.Text = "Choose Destination Folders";
                    label2.Text = "Where should the application be installed?";
                    button1.Text = "Browse...";
                    button3.Text = "Next";
                    button2.Text = "Exit";
                    break;
            }

            // Update the "Selected Language" label
            if (lblSelectedLanguage != null)
            {
                lblSelectedLanguage.Text = $"Selected Language: {CurrentLanguage}";
            }
        }

        private void button1_Click(object sender, EventArgs e)
        {
            FolderBrowserDialog folderBrowserDialog = new FolderBrowserDialog();
            folderBrowserDialog.Description = "Select the installation folder";

            if (folderBrowserDialog.ShowDialog() == DialogResult.OK)
            {
                txtDestinationFolder.Text = folderBrowserDialog.SelectedPath;
            }
        }

        private void btnNext_Click(object sender, EventArgs e)
        {
            installationPath = txtDestinationFolder.Text;

            if (!Directory.Exists(installationPath))
            {
                MessageBox.Show("The specified folder does not exist.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }

            this.Hide();
            using (var form3 = new Form3())
            {
                form3.InstallPath = installationPath; // Make sure 'installationPath' has a valid value
                form3.SelectedLanguage = CurrentLanguage;
                DialogResult result = form3.ShowDialog();

                if (result == DialogResult.OK)
                {
                    // Installation is done in Form3
                }
                else if (result == DialogResult.Retry) // User clicked "Back"
                {
                    this.Show();
                }
                else
                {
                    // User closed Form3
                    this.Close(); // Or handle as needed
                }
            }
        }

        private void btnExit_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void textBox1_TextChanged(object sender, EventArgs e)
        {
            // You can add logic here if needed
        }

        private void button3_Click(object sender, EventArgs e)
        {
            btnNext_Click(sender, e); // Call the same logic as btnNext
        }

        private void button2_Click(object sender, EventArgs e)
        {
            btnExit_Click(sender, e); // Call the same logic as btnExit
        }

        private void label1_Click(object sender, EventArgs e)
        {
            // You can add logic here if needed
        }

        private void label2_Click(object sender, EventArgs e)
        {
            // You can add logic here if needed
        }

        private void lblSelectedLanguage_Click(object sender, EventArgs e)
        {

        }
    }
}