using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Библиотека
{
    public partial class Library : Form
    {
        public string result;
        public Library()
        {
            InitializeComponent();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            var libraryExtradition = new LibraryExtradition();
            libraryExtradition.result = this.result;
            this.Hide();
            libraryExtradition.Show();
        }

        private void button3_Click(object sender, EventArgs e)
        {
            Environment.Exit(0);
        }

        private void button2_Click(object sender, EventArgs e)
        {
            CheckExtradition addExtradition = new CheckExtradition();
            addExtradition.Show();
            this.Close();
        }

        private void Library_Load(object sender, EventArgs e)
        {
            if (result != "Admin")
                button1.Enabled = false;
            else
                button1.Enabled = true;
        }
    }
}
