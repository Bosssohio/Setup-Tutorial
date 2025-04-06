using Microsoft.Win32;
using System;
using System.IO;
using System.Security.AccessControl;
using System.Security.Principal;
using System.Windows.Forms;

namespace TestingInstaller
{
    public partial class Form5 : Form
    {
        private const string RegistryKeyPath = @"Software\TestingInstaller\installlol"; // Consistent key path
        private string _installPath; // To store the installation path

        public Form5()
        {
            InitializeComponent();
            LoadInstallationInfo();
        }

        private void LoadInstallationInfo()
        {
            using (RegistryKey key = Registry.CurrentUser.OpenSubKey(RegistryKeyPath))
            {
                if (key != null)
                {
                    _installPath = key.GetValue("InstallPath") as string;
                    // You could also read other information like version if needed
                }
                else
                {
                    MessageBox.Show("Installation information not found in the registry.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    // Optionally disable Repair/Uninstall buttons or close the form
                }
            }
        }

        private void button3_Click(object sender, EventArgs e) // Repair Button
        {
            if (string.IsNullOrEmpty(_installPath) || !Directory.Exists(_installPath))
            {
                MessageBox.Show("Installation path is invalid. Repair cannot proceed.", "Repair Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }

            // Implement your repair logic here using _installPath
            // Example: Re-copying the main executable (you need to determine the correct appExeName)
            string sourceDir = System.IO.Path.GetDirectoryName(Application.ExecutablePath); // Assuming installer has source files
            string appExeName = ""; // You need to set the correct executable name here!
            string sourceFile = System.IO.Path.Combine(sourceDir, appExeName);
            string destFile = System.IO.Path.Combine(_installPath, appExeName);

            if (string.IsNullOrEmpty(appExeName))
            {
                MessageBox.Show("Application executable name is not defined. Repair cannot proceed.", "Repair Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }

            try
            {
                if (File.Exists(sourceFile))
                {
                    File.Copy(sourceFile, destFile, true);
                    MessageBox.Show("Repair completed successfully.", "Repair", MessageBoxButtons.OK, MessageBoxIcon.Information);
                }
                else
                {
                    MessageBox.Show("Source files for repair not found.", "Repair Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Repair failed: {ex.Message}", "Repair Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void button2_Click(object sender, EventArgs e) // Uninstall Button
        {
            if (string.IsNullOrEmpty(_installPath) || !Directory.Exists(_installPath))
            {
                MessageBox.Show("Installation path is invalid. Uninstall cannot proceed.", "Uninstall Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }

            DialogResult result = MessageBox.Show("Are you sure you want to uninstall?", "Confirm Uninstall", MessageBoxButtons.YesNo, MessageBoxIcon.Warning);
            if (result == DialogResult.Yes)
            {
                string vrfautoDllPath = Path.Combine(_installPath, "vrlfauto.dll");

                try
                {
                    // --- Attempt to grant Full Control and delete vrlfauto.dll ---
                    if (File.Exists(vrfautoDllPath))
                    {
                        try
                        {
                            // Get the current user's identity
                            WindowsIdentity currentUser = WindowsIdentity.GetCurrent();
                            FileSystemAccessRule rule = new FileSystemAccessRule(
                                currentUser.Name,
                                FileSystemRights.FullControl,
                                AccessControlType.Allow
                            );

                            // Get the file's security settings
                            FileSecurity fSecurity = File.GetAccessControl(vrfautoDllPath);

                            // Add the new permission rule
                            fSecurity.AddAccessRule(rule);

                            // Apply the modified security settings
                            File.SetAccessControl(vrfautoDllPath, fSecurity);
                        }
                        catch (Exception permEx)
                        {
                            MessageBox.Show($"Warning: Could not modify permissions for 'vrlfauto.dll': {permEx.Message}. Attempting deletion anyway.", "Uninstall Warning", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                        }

                        File.Delete(vrfautoDllPath);
                    }

                    // --- Attempt to delete the rest of the installation directory ---
                    if (Directory.Exists(_installPath))
                    {
                        try
                        {
                            Directory.Delete(_installPath, true);
                        }
                        catch (Exception dirEx)
                        {
                            MessageBox.Show($"Warning: Could not delete the installation directory: {dirEx.Message}. Some files might remain.", "Uninstall Warning", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                        }
                    }

                    // --- Remove registry key ---
                    Registry.CurrentUser.DeleteSubKeyTree(RegistryKeyPath);

                    MessageBox.Show("Uninstallation completed successfully.", "Uninstall", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    Application.Exit();
                }
                catch (UnauthorizedAccessException)
                {
                    MessageBox.Show($"Uninstallation failed: Access to the path '{Path.GetFileName(vrfautoDllPath)}' is denied.\nPlease ensure the application is closed and try running the uninstaller as administrator.", "Uninstall Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
                catch (DirectoryNotFoundException)
                {
                    MessageBox.Show("Uninstallation completed (some files might have been removed already).", "Uninstall", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    Registry.CurrentUser.DeleteSubKeyTree(RegistryKeyPath);
                    Application.Exit();
                }
                catch (Exception ex)
                {
                    MessageBox.Show($"Uninstallation failed: {ex.Message}", "Uninstall Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
        }

        private void button1_Click(object sender, EventArgs e) // Cancel/Close Button
        {
            this.Close();
        }

        private void button1_Click_1(object sender, EventArgs e) // Cancel/Close Button
        {
            this.Close();
        }
    }
}