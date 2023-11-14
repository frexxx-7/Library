using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Data.OleDb;
using System.Data.SqlClient;

namespace Библиотека
{
    public partial class Autorization : Form
    {
        OleDbConnection con;
        OleDbCommand cmd;
        OleDbDataReader dr;
        public Autorization()
        {
            InitializeComponent();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            con = new OleDbConnection(@"Provider=Microsoft.ACE.OLEDB.12.0;Data Source=|DataDirectory|\Библиотека.accdb");
            cmd = new OleDbCommand();
            con.Open();
            cmd.Connection = con;
            string str = "SELECT * FROM Авторизация where Логин='" + textBox1.Text + "' AND Пароль='" + textBox2.Text + "'";
            cmd.CommandText = str;

            dr = cmd.ExecuteReader();

            if (dr.Read())
            {
                Library library = new Library();
                MessageBox.Show("Добро пожаловать " + textBox1.Text);
                
                library.result = textBox1.Text;
                this.Hide();
                
                library.ShowDialog();
            }
            else
            {
                MessageBox.Show("Неправильный логин или пароль");
            }
            con.Close();
        }

        private void button3_Click(object sender, EventArgs e)
        {
            Environment.Exit(0);
        }

        private void Autorization_Load(object sender, EventArgs e)
        {
        }

        private void button2_Click(object sender, EventArgs e)
        {
            this.Hide();
            var regictrartion = new Registration();
            regictrartion.Show();
        }
    }
}
