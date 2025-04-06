using IWshRuntimeLibrary; // Add this line
using Microsoft.Win32;
using System;
using System.IO;
using System.Reflection;
using System.Windows.Forms;
namespace TestingInstaller
{
    public partial class Form3 : Form
    {
        public string InstallPath { get; set; }
        public string SelectedLanguage { get; set; }
        public bool CreateDesktopShortcut { get; private set; }
        private string customTargetPath = "";
        private System.Windows.Forms.Button btnBrowseTarget; // Add this line (if you haven't already)
        private System.Windows.Forms.TextBox txtTargetPath;


        public Form3()
        {
            InitializeComponent();
        }

        private void Form3_Load(object sender, EventArgs e)
        {
            // Optionally display the selected install path or language
        }

        private void btnBack_Click(object sender, EventArgs e)
        {
            this.DialogResult = DialogResult.Retry; // Use Retry to signal "go back"
            this.Close();
        }

        private void btnNext_Click(object sender, EventArgs e)
        {
            CreateDesktopShortcut = chkCreateDesktopShortcut.Checked;
            this.Hide(); // Hide Form3

            using (var form4 = new Form4())
            {
                DialogResult result = form4.ShowDialog(); // Show Form4 modally

                if (result == DialogResult.OK)
                {
                    // Installation/waiting complete in Form4
                    PerformRegistryWrite(); // Call the registry writing method here
                    MessageBox.Show("Installation process completed.", "Finished", MessageBoxButtons.OK, MessageBoxIcon.Information);
                }
                else if (result == DialogResult.Cancel)
                {
                    // User cancelled in Form4
                    MessageBox.Show("Installation cancelled.", "Cancelled", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                }
            }

            this.Close(); // Close Form3 after Form4 is closed
        }

        private void CreateCustomShortcut()
        {
            try
            {
                string shortcutName = string.IsNullOrWhiteSpace(txtShortcutName.Text) ? "My Custom Shortcut" : txtShortcutName.Text;
                string shortcutPath = Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.DesktopDirectory), $"{shortcutName}.lnk");
                IWshShell shell = new WshShell(); // Use the interface instead of the class
                IWshShortcut shortcut = (IWshShortcut)shell.CreateShortcut(shortcutPath);

                // Use the user-selected target path if available, otherwise default to the installed app
                shortcut.TargetPath = string.IsNullOrEmpty(customTargetPath) ? Path.Combine(InstallPath, "YourApplication.exe") : this.txtTargetPath.Text;
                shortcut.WorkingDirectory = string.IsNullOrEmpty(customTargetPath) ? InstallPath : Path.GetDirectoryName(customTargetPath);
                shortcut.Description = "Launch Custom Application"; // You can customize this
                shortcut.IconLocation = shortcut.TargetPath + ",0"; // Try to use the target's icon
                shortcut.Save();
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Failed to create desktop shortcut: {ex.Message}", "Warning", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }
        }

        private void txtShortcutName_TextChanged(object sender, EventArgs e)
        {

        }

        private void chkCreateDesktopShortcut_CheckedChanged(object sender, EventArgs e)
        {

        }

        private void button1_Click(object sender, EventArgs e)
        {
            OpenFileDialog openFileDialog = new OpenFileDialog();
            openFileDialog.Title = "Select Target Application";
            openFileDialog.Filter = "Executable Files (*.exe)|*.exe|All Files (*.*)|*.*";
            openFileDialog.InitialDirectory = Environment.GetFolderPath(Environment.SpecialFolder.ProgramFiles); // Suggest Program Files

            if (openFileDialog.ShowDialog() == DialogResult.OK)
            {
                customTargetPath = openFileDialog.FileName;
                txtTargetPath.Text = customTargetPath;
            }
        }

        private void PerformRegistryWrite()
        {
            string registryKeyPath = @"Software\TestingInstaller\installlol"; // Or your actual key path
            try
            {
                using (RegistryKey key = Registry.CurrentUser.CreateSubKey(registryKeyPath))
                {
                    if (key != null)
                    {
                        key.SetValue("InstallPath", InstallPath);
                        key.SetValue("Version", "1.0.0"); // Replace with your version
                        key.SetValue("InstallDate", DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss"));
                        key.SetValue("Installed", 1, RegistryValueKind.DWord); // THIS IS THE LINE
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Error writing to registry: {ex.Message}", "Registry Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }

            if (CreateDesktopShortcut)
            {
                CreateCustomShortcut();
            }
        }
    }
}