using System;
using System.Collections;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Resources;
using System.Windows.Forms;

namespace TestingInstaller
{
    partial class Form4 : Form
    {
        private ResourceReader resourceReader;
        private string installDirectory;
        private string resourcesName = "embedded_resources.resources";
        private Timer installationTimer;
        private int timerTickCount = 0;

        public Form4()
        {
            InitializeComponent();
            installationTimer = new Timer();
            installationTimer.Interval = 500; // Example interval
            installationTimer.Tick += InstallationTimer_Tick;
        }

        private void Form4_Load(object sender, EventArgs e)
        {
            string[] args = Environment.GetCommandLineArgs();
            if (args.Length > 1 && args[1] == "/install" && args.Length > 2)
            {
                installDirectory = args[2];
                label1.Visible = true;
                progressBar1.Visible = true;
                installationTimer.Start();
                PerformInstallation();
            }
            else if (args.Length > 1 && args[1] == "/install")
            {
                FolderBrowserDialog folderDialog = new FolderBrowserDialog();
                folderDialog.Description = "Select the installation directory:";
                if (folderDialog.ShowDialog() == DialogResult.OK)
                {
                    installDirectory = folderDialog.SelectedPath;
                    label1.Visible = true;
                    progressBar1.Visible = true;
                    installationTimer.Start();
                    PerformInstallation();
                }
                else
                {
                    MessageBox.Show("Installation cancelled.", "Cancelled", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    this.Close();
                }
            }
            else
            {
                MessageBox.Show("This installer should be launched with the '/install' command.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                this.Close();
            }
        }

        private void PerformInstallation()
        {
            try
            {
                Assembly currentAssembly = Assembly.GetExecutingAssembly();
                using (Stream stream = currentAssembly.GetManifestResourceStream(currentAssembly.GetName().Name + "." + resourcesName))
                {
                    if (stream != null)
                    {
                        resourceReader = new ResourceReader(stream);
                        int installedCount = 0;
                        int totalFiles = 0;

                        // Basic attempt to count (might not be accurate initially)
                        foreach (DictionaryEntry entry in resourceReader)
                        {
                            if (entry.Key.ToString().StartsWith("InstallFiles/") && entry.Key.ToString() != "InstallFiles/TestingInstaller.exe")
                            {
                                totalFiles++;
                            }
                        }
                        progressBar1.Invoke((MethodInvoker)(() => progressBar1.Maximum = totalFiles));

                        stream.Position = 0;
                        resourceReader = new ResourceReader(stream);

                        foreach (DictionaryEntry entry in resourceReader)
                        {
                            string resourceName = entry.Key.ToString();
                            if (resourceName.StartsWith("InstallFiles/") && resourceName != "InstallFiles/TestingInstaller.exe")
                            {
                                string fileName = resourceName.Substring("InstallFiles/".Length).Replace('/', '\\');
                                byte[] fileData = (byte[])entry.Value;
                                string fullPath = Path.Combine(installDirectory, fileName);

                                try
                                {
                                    Directory.CreateDirectory(Path.GetDirectoryName(fullPath));
                                    File.WriteAllBytes(fullPath, fileData);
                                    installedCount++;
                                    progressBar1.Invoke((MethodInvoker)(() => progressBar1.Value = installedCount));
                                    label1.Invoke((MethodInvoker)(() => label1.Text = $"Installing: {fileName}"));
                                    // Thread.Sleep(50);
                                }
                                catch (Exception ex)
                                {
                                    MessageBox.Show($"Error installing {fileName}: {ex.Message}", "Installation Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                                }
                            }
                        }

                        this.Invoke((MethodInvoker)(() =>
                        {
                            MessageBox.Show("Installation complete.", "Installation Successful", MessageBoxButtons.OK, MessageBoxIcon.Information);
                            this.Close();
                        }));
                    }
                    else
                    {
                        MessageBox.Show("Error: Embedded resources not found.", "Installation Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                        this.Close();
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show($"An error occurred during installation: {ex.Message}", "Installation Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                this.Close();
            }
            finally
            {
                resourceReader?.Close();
                installationTimer.Stop();
            }
        }

        private void btnCancel_Click(object sender, EventArgs e)
        {
            installationTimer.Stop();
            MessageBox.Show("Installation cancelled.", "Cancelled", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            this.Close();
        }

        private void InstallationTimer_Tick(object sender, EventArgs e)
        {
            // Very basic timer tick - could be used for a simple animation
            timerTickCount++;
            if (timerTickCount % 4 == 0)
            {
                label1.Text = "Installing...";
            }
            else if (timerTickCount % 4 == 1)
            {
                label1.Text = "Installing .";
            }
            else if (timerTickCount % 4 == 2)
            {
                label1.Text = "Installing ..";
            }
            else if (timerTickCount % 4 == 3)
            {
                label1.Text = "Installing ...";
            }
            // progressBar1.Value = (timerTickCount % (progressBar1.Maximum + 1)); // Very crude progress attempt
        }
    }
}