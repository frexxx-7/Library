using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Configuration;
using System.Data.OleDb;

namespace Библиотека
{
    public partial class Registration : Form
    {
        public Registration()
        {
            InitializeComponent();
        }

        private void button2_Click(object sender, EventArgs e)
        {
            this.Close();
            var library = new Autorization();
            library.Show();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            if (String.IsNullOrWhiteSpace(textBox1.Text) ||
                        String.IsNullOrWhiteSpace(textBox2.Text))
                MessageBox.Show("Необходимо заполнить все данные!", "Ошибка!");
            else
            {
                    string query = "INSERT INTO Авторизация(Логин,Пароль) VALUES(@Login,@Password)";
                    using (OleDbConnection connection = new OleDbConnection(ConfigurationManager.ConnectionStrings["Библиотека.Properties.Settings.LibraryConnectionString"].ConnectionString))
                    using (OleDbCommand command = new OleDbCommand(query, connection))
                    {
                        connection.Open();
                        command.Parameters.AddWithValue("@Login", textBox1.Text);
                        command.Parameters.AddWithValue("@Password", textBox2.Text);


                        command.ExecuteNonQuery();
                        this.DialogResult = DialogResult.OK;
                    }
                
                
            }
            this.Close();
            var library = new Autorization();
            library.Show();
        }
    }
}
