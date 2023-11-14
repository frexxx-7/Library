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
    public partial class AddBook : Form
    {
        public AddBook()
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
            string query = $"SELECT * FROM Книга WHERE `Код книги` = {id}";
            db.openConnection();
            using (MySqlDataAdapter adapter = new MySqlDataAdapter(query, db.getConnection()))
            {
                DataTable dataTable = new DataTable();
                adapter.Fill(dataTable);
                textBox1.Text = dataTable.Rows[0][1].ToString();
                textBox2.Text = dataTable.Rows[0][2].ToString();
                textBox3.Text = dataTable.Rows[0][3].ToString();
                textBox4.Text = dataTable.Rows[0][4].ToString();
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
                           String.IsNullOrWhiteSpace(textBox3.Text) ||
                           String.IsNullOrWhiteSpace(textBox4.Text))
                MessageBox.Show("Необходимо заполнить все данные!", "Ошибка!");
            else
            {
                if (!isChange)
                {
                    string query = "INSERT INTO Книга(Название,Автор,Жанр,`Год издательства`) VALUES(@name,@avtor,@jan,@year)";
                    db.openConnection();
                    using (MySqlCommand command = new MySqlCommand(query, db.getConnection()))
                    {
                        command.Parameters.AddWithValue("@name", textBox1.Text);
                        command.Parameters.AddWithValue("@avtor", textBox2.Text);
                        command.Parameters.AddWithValue("@jan", textBox3.Text);
                        command.Parameters.AddWithValue("@year", textBox4.Text);

                        command.ExecuteNonQuery();
                        this.DialogResult = DialogResult.OK;
                    }
                    db.closeConnection();
                }
                else
                {
                    string query = $"UPDATE Книга SET `Название`=@name,`Автор`=@avtor,`Жанр`=@jan,`Год издательства`=@year WHERE `Код книги` = {idChange}";
                    db.openConnection();
                    using (MySqlCommand command = new MySqlCommand(query, db.getConnection()))
                    {
                        command.Parameters.AddWithValue("@name", textBox1.Text);
                        command.Parameters.AddWithValue("@avtor", textBox2.Text);
                        command.Parameters.AddWithValue("@jan", textBox3.Text);
                        command.Parameters.AddWithValue("@year", textBox4.Text);
                        command.ExecuteNonQuery();
                        db.closeConnection();
                        this.DialogResult = DialogResult.OK;
                    }
                }
            }
        }
    }
}
