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
using Библиотека.Classes;
using MySql.Data.MySqlClient;

namespace Библиотека
{
    public partial class Autorization : Form
    {
        public Autorization()
        {
            InitializeComponent();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            DB db = new DB();
            MySqlCommand command = new MySqlCommand("SELECT * FROM Авторизация where Логин='" + textBox1.Text + "' AND Пароль='" + textBox2.Text + "'", db.getConnection());
            db.openConnection();

            MySqlDataReader reader = command.ExecuteReader();

            if (reader.Read())
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
            db.closeConnection();
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
