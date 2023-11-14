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
using Библиотека.Classes;
using MySql.Data.MySqlClient;

namespace Библиотека
{
    public partial class AddLibrarian : Form
    {
        public AddLibrarian()
        {
            InitializeComponent();
        }
        private bool isChange = false;
        private string idChange;
        public string IDCHANGE
        {
            set
            {
                idChange = value;
                isChange = true;
                FillBoxes(value);
            }
        }
        private void FillBoxes(string id)
        {
            DB db = new DB();
            string query = $"SELECT * FROM Библиотекарь WHERE `Код библиотекаря` = {id}";
            db.openConnection();
            using (MySqlDataAdapter adapter = new MySqlDataAdapter(query, db.getConnection()))
            {
                DataTable dataTable = new DataTable();
                adapter.Fill(dataTable);
                textBox1.Text = dataTable.Rows[0][1].ToString();
                textBox2.Text = dataTable.Rows[0][2].ToString();
                textBox3.Text = dataTable.Rows[0][3].ToString();
                label1.Text = "Изменение";
                button1.Text = "Сохранить";
            }
            db.closeConnection();
        }
        private void button2_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            DB db = new DB();
            if (String.IsNullOrWhiteSpace(textBox1.Text) ||
                        String.IsNullOrWhiteSpace(textBox2.Text) ||
                        String.IsNullOrWhiteSpace(textBox3.Text))
                MessageBox.Show("Необходимо заполнить все данные!", "Ошибка!");
            else
            {
                if (!isChange)
                {
                    string query = "INSERT INTO Библиотекарь(Фамилия,Имя,Отчество) VALUES(@Fam,@Imya,@Otchestvo)";
                    db.openConnection();
                    using (MySqlCommand command = new MySqlCommand(query, db.getConnection()))
                    {
                        command.Parameters.AddWithValue("@Fam", textBox1.Text);
                        command.Parameters.AddWithValue("@Imya", textBox2.Text);
                        command.Parameters.AddWithValue("@Otchestvo", textBox3.Text);

                        command.ExecuteNonQuery();
                        this.DialogResult = DialogResult.OK;
                    }
                }
                else
                {
                    string query = $"UPDATE Библиотекарь SET Фамилия=@Fam,Имя=@Imya,Отчество=@Otchestvo WHERE `Код библиотекаря`= {idChange}";
                    db.openConnection();
                    using (MySqlCommand command = new MySqlCommand(query, db.getConnection()))
                    {
                        command.Parameters.AddWithValue("@Fam", textBox1.Text);
                        command.Parameters.AddWithValue("@Imya", textBox2.Text);
                        command.Parameters.AddWithValue("@Otchestvo", textBox3.Text);

                        command.ExecuteNonQuery();
                        this.DialogResult = DialogResult.OK;
                        db.closeConnection();
                    }
                }
            }
        }
    }
}
