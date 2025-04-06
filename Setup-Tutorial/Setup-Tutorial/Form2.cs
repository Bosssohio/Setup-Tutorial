using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace TestingInstaller
{
    public partial class Form2 : Form
    {
        public string SelectedLanguage { get; private set; }
        public Form2()
        {
            InitializeComponent();
        }

        private void button6_Click(object sender, EventArgs e)
        {
            if (listBox1.SelectedItem != null)
            {
                SelectedLanguage = listBox1.SelectedItem.ToString();
                this.DialogResult = DialogResult.OK; // Set DialogResult to O
                this.Close();
            }
            else
            {
                MessageBox.Show("Please select a language before proceeding.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }
        }

        private void Form2_Load(object sender, EventArgs e)
        {
            listBox1.Items.Add("English");
            listBox1.Items.Add("Spanish");
            listBox1.Items.Add("French");
            listBox1.Items.Add("Vietnamese");
        }

        private void button5_Click(object sender, EventArgs e)
        {
            this.Close();
        }
    }
}
