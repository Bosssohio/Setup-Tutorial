using Microsoft.Win32;
using System;
using System.Windows.Forms;
using TestingInstaller;

internal static class Program
{
    [STAThread]
    static void Main()
    {
        Application.EnableVisualStyles();
        Application.SetCompatibleTextRenderingDefault(false);

        string registryKeyPath = @"Software\TestingInstaller\installlol";
        using (RegistryKey key = Registry.CurrentUser.OpenSubKey(registryKeyPath))
        {
            if (key != null)
            {
                object installedValue = key.GetValue("Installed");
                if (installedValue != null && installedValue is int installed && installed == 1)
                {
                    // Application is already installed
                    Application.Run(new Form5());
                    return; // Important to exit Main here
                }
            }

            // Application is not installed or the flag is not set
            string selectedLanguage = null;
            using (var languageForm = new Form2())
            {
                if (languageForm.ShowDialog() == DialogResult.OK)
                {
                    selectedLanguage = languageForm.SelectedLanguage;
                    Application.Run(new Form1(selectedLanguage));
                }
                else
                {
                    Application.Exit();
                }
            }
        }
    }
}