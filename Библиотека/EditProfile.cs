using MySql.Data.MySqlClient;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Reflection.Emit;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using Библиотека.Classes;

namespace Библиотека
{
    public partial class EditProfile : Form
    {
        private string idAdditionalInfo;
        public EditProfile(string idAdditionalInfo)
        {
            InitializeComponent();
            this.idAdditionalInfo= idAdditionalInfo;
            combobox(comboBox1, "SELECT `Код`, concat(дом, улица, город, страна) FROM `Адрес`", "concat(дом, улица, город, страна)", "Код");
            loadInfoUser();
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
                if(c.Items.Count > 0)
                    c.SelectedIndex = 0;
            }
            db.closeConnection();
        }

        private void loadInfoUser()
        {
            DB db = new DB();
            string query = $"SELECT `Авторизация`.*, `информацияпользователя`.*, concat(`Дом`,`Улица`,`Город`,`Страна`) FROM Авторизация " +
                $"left join `информацияпользователя` on `информацияпользователя`.`Код` = `Авторизация`.`Код информации пользователя` " +
                $"left join `адрес` on `адрес`.`код` = `информацияпользователя`.`код адреса`" +
                $"WHERE `Авторизация`.`Код` = {Library.idUser}";
            db.openConnection();
            using (MySqlDataAdapter adapter = new MySqlDataAdapter(query, db.getConnection()))
            {
                DataTable dataTable = new DataTable();
                adapter.Fill(dataTable);
                textBox5.Text = dataTable.Rows[0][1].ToString();
                textBox4.Text = dataTable.Rows[0][2].ToString();
                textBox2.Text = dataTable.Rows[0][5].ToString();
                textBox1.Text = dataTable.Rows[0][6].ToString();
                textBox3.Text = dataTable.Rows[0][7].ToString();
                comboBox1.Text = dataTable.Rows[0][9].ToString();
            }
            db.closeConnection();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            DB db = new DB();
            if (String.IsNullOrWhiteSpace(textBox1.Text) ||
                           String.IsNullOrWhiteSpace(textBox2.Text) ||
                           String.IsNullOrWhiteSpace(textBox3.Text) ||
                           String.IsNullOrWhiteSpace(textBox4.Text) ||
                           String.IsNullOrWhiteSpace(textBox5.Text))
                MessageBox.Show("Необходимо заполнить все данные!", "Ошибка!");
            else
            {
                if (idAdditionalInfo != "")
                {
                    string query = $"UPDATE информацияпользователя SET `Имя`=@name,`Фамилия`=@surname,`Отчество`=@patronymic,`Код адреса`=@idAddress WHERE `Код` = {idAdditionalInfo}";
                    db.openConnection();
                    using (MySqlCommand command = new MySqlCommand(query, db.getConnection()))
                    {
                        command.Parameters.AddWithValue("@name", textBox2.Text);
                        command.Parameters.AddWithValue("@surname", textBox1.Text);
                        command.Parameters.AddWithValue("@patronymic", textBox3.Text);
                        command.Parameters.AddWithValue("@idAddress", comboBox1.SelectedValue);
                        command.ExecuteNonQuery();
                        db.closeConnection();
                        this.DialogResult = DialogResult.OK;
                    }
                }
                else
                {
                    string query = $"insert into информацияпользователя(`Имя`,`Фамилия`,`Отчество`,`Код адреса`) values (@name, @surname, @patronymic, @idAddress); " +
                        $"update `Авторизация` set `Код информации пользователя` = (Select LAST_INSERT_ID()) " +
                        $"where `Авторизация`.`Код` = {Library.idUser}";
                    db.openConnection();
                    using (MySqlCommand command = new MySqlCommand(query, db.getConnection()))
                    {
                        command.Parameters.AddWithValue("@name", textBox2.Text);
                        command.Parameters.AddWithValue("@surname", textBox1.Text);
                        command.Parameters.AddWithValue("@patronymic", textBox3.Text);
                        command.Parameters.AddWithValue("@idAddress", comboBox1.SelectedValue);
                        command.ExecuteScalar();
                        db.closeConnection();
                        this.DialogResult = DialogResult.OK;
                    }
                }
                string query2 = $"UPDATE Авторизация SET `Логин`=@login,`Пароль`=@password WHERE `Код` = {Library.idUser}";
                db.openConnection();
                using (MySqlCommand command = new MySqlCommand(query2, db.getConnection()))
                {
                    command.Parameters.AddWithValue("@login", textBox5.Text);
                    command.Parameters.AddWithValue("@password", textBox4.Text);
                    command.ExecuteNonQuery();
                    db.closeConnection();
                    this.DialogResult = DialogResult.OK;
                }
            }
            this.Hide();
            new Profile().Show();
        }

        private void button2_Click(object sender, EventArgs e)
        {
            new AddAddress().Show();
        }

        private void EditProfile_FormClosed(object sender, FormClosedEventArgs e)
        {
            Application.Exit();
        }
    }
}
