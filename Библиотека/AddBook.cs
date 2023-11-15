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
            combobox(comboBox1, "SELECT `Код`, `ФИО автора` FROM `Автор книги`", "ФИО автора", "Код");
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
        public void combobox(ComboBox c, string query, string displaymember, string valuemember)
        {
            DB db = new DB();
            db.openConnection();
            using (MySqlDataAdapter adapter = new MySqlDataAdapter(query, db.getConnection()))
            {
                System.Data.DataTable datatable = new System.Data.DataTable();
                adapter.Fill(datatable);
                c.DataSource = datatable;
                c.DisplayMember = displaymember;
                c.ValueMember = valuemember;
                if(c.Items.Count>0)
                    c.SelectedIndex = 0;
            }
            db.closeConnection();
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
                textBox3.Text = dataTable.Rows[0][2].ToString();
                textBox4.Text = dataTable.Rows[0][3].ToString();
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
                           String.IsNullOrWhiteSpace(textBox3.Text) ||
                           String.IsNullOrWhiteSpace(textBox4.Text))
                MessageBox.Show("Необходимо заполнить все данные!", "Ошибка!");
            else
            {
                if (!isChange)
                {
                    string query = "INSERT INTO Книга(Название,`Код автора`,Жанр,`Год издательства`) VALUES(@name,@avtor,@jan,@year)";
                    db.openConnection();
                    using (MySqlCommand command = new MySqlCommand(query, db.getConnection()))
                    {
                        command.Parameters.AddWithValue("@name", textBox1.Text);
                        command.Parameters.AddWithValue("@avtor", comboBox1.SelectedValue);
                        command.Parameters.AddWithValue("@jan", textBox3.Text);
                        command.Parameters.AddWithValue("@year", textBox4.Text);

                        command.ExecuteNonQuery();
                        this.DialogResult = DialogResult.OK;
                    }
                    db.closeConnection();
                }
                else
                {
                    string query = $"UPDATE Книга SET `Название`=@name,`Код автора`=@avtor,`Жанр`=@jan,`Год издательства`=@year WHERE `Код книги` = {idChange}";
                    db.openConnection();
                    using (MySqlCommand command = new MySqlCommand(query, db.getConnection()))
                    {
                        command.Parameters.AddWithValue("@name", textBox1.Text);
                        command.Parameters.AddWithValue("@avtor", comboBox1.SelectedValue);
                        command.Parameters.AddWithValue("@jan", textBox3.Text);
                        command.Parameters.AddWithValue("@year", textBox4.Text);
                        command.ExecuteNonQuery();
                        db.closeConnection();
                        this.DialogResult = DialogResult.OK;
                    }
                }
            }
        }

        private void button3_Click(object sender, EventArgs e)
        {
            new AddAuthor().Show();
        }
    }
}
