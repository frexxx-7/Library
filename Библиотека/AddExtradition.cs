﻿using System;
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
    public partial class AddExtradition : Form
    {
        public AddExtradition()
        {
            InitializeComponent();
            combobox(comboBox1, "SELECT [Код книги], Название FROM [Книга]", "Название", "Код книги");
            combobox1(comboBox2, "SELECT [Код читателя], Фамилия FROM [Читатель]", "Фамилия", "Код читателя");
            combobox2(comboBox3, "SELECT [Код библиотекаря], Фамилия FROM [Библиотекарь]", "Фамилия", "Код библиотекаря");
        }
        public void combobox(ComboBox c, string query, string displaymember, string valuemember)
        {
            var connectionString = ConfigurationManager.ConnectionStrings["Библиотека.Properties.Settings.LibraryConnectionString"].ConnectionString;
            using (OleDbDataAdapter adapter = new OleDbDataAdapter(query, connectionString))
            {
                System.Data.DataTable datatable = new System.Data.DataTable();
                adapter.Fill(datatable);
                c.DataSource = datatable;
                c.DisplayMember = displaymember;
                c.ValueMember = valuemember;
                c.SelectedIndex = 0;
            }
        }
        public void combobox1(ComboBox c, string query, string displaymember, string valuemember)
        {
            var connectionString = ConfigurationManager.ConnectionStrings["Библиотека.Properties.Settings.LibraryConnectionString"].ConnectionString;
            using (OleDbDataAdapter adapter = new OleDbDataAdapter(query, connectionString))
            {
                System.Data.DataTable datatable = new System.Data.DataTable();
                adapter.Fill(datatable);
                c.DataSource = datatable;
                c.DisplayMember = displaymember;
                c.ValueMember = valuemember;
                c.SelectedIndex = 0;
            }
        }
        private void combobox2(ComboBox c, string query, string displaymember, string valuemember)
        {
            var connectionString = ConfigurationManager.ConnectionStrings["Библиотека.Properties.Settings.LibraryConnectionString"].ConnectionString;
            using (OleDbDataAdapter adapter = new OleDbDataAdapter(query, connectionString))
            {
                System.Data.DataTable datatable = new System.Data.DataTable();
                adapter.Fill(datatable);
                c.DataSource = datatable;
                c.DisplayMember = displaymember;
                c.ValueMember = valuemember;
                c.SelectedIndex = 0;
            }
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
            string query = $"SELECT * FROM Выдача WHERE [Код выдачи] = {id}";
            using (OleDbDataAdapter adapter = new OleDbDataAdapter(query, ConfigurationManager.ConnectionStrings["Библиотека.Properties.Settings.LibraryConnectionString"].ConnectionString))
            {
                DataTable dataTable = new DataTable();
                adapter.Fill(dataTable);
                dateTimePicker1.Value= DateTime.Parse(dataTable.Rows[0][1].ToString());
                dateTimePicker2.Value= DateTime.Parse(dataTable.Rows[0][2].ToString());
                dateTimePicker3.Value = DateTime.Parse(dataTable.Rows[0][3].ToString());
                comboBox1.Text = dataTable.Rows[0][4].ToString();
                comboBox2.Text = dataTable.Rows[0][5].ToString();
                comboBox3.Text = dataTable.Rows[0][6].ToString();
                label1.Text = "Изменение";
                button1.Text = "Сохранить изменения";
            }
        }
        private void label3_Click(object sender, EventArgs e)
        {

        }

        private void button2_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void button1_Click(object sender, EventArgs e)
        {
                if (!isChange)
                {
                    string query = "INSERT INTO Выдача([Дата оформления],[Дата возврата план],[Дата возврата факт],[Код книги],[Код читателя],[Код библеотекаря]) VALUES(@dateof,@datevf,@datevp,@exbook,@exread,@exlib)";
                    using (OleDbConnection connection = new OleDbConnection(ConfigurationManager.ConnectionStrings["Библиотека.Properties.Settings.LibraryConnectionString"].ConnectionString))
                    using (OleDbCommand command = new OleDbCommand(query, connection))
                    {
                        connection.Open();
                        command.Parameters.AddWithValue("@dateof", dateTimePicker1.Value.ToString("dd-MM-yyyy"));
                        command.Parameters.AddWithValue("@datevf", dateTimePicker2.Value.ToString("dd-MM-yyyy"));
                        command.Parameters.AddWithValue("@datevp", dateTimePicker3.Value.ToString("dd-MM-yyyy"));
                        command.Parameters.AddWithValue("@exbook", comboBox1.SelectedValue);
                        command.Parameters.AddWithValue("@exread", comboBox2.SelectedValue);
                        command.Parameters.AddWithValue("@exlib", comboBox3.SelectedValue);

                    command.ExecuteNonQuery();
                        this.DialogResult = DialogResult.OK;
                    }
                }
                else
                {
                    string query = $"UPDATE Выдача SET [Дата оформления]=@dateof,[Дата возврата план]=@datevf,[Дата возврата факт]=@datevp,[Код книги]=@exbook,[Код читателя]=@exread,[Код библеотекаря]=@exlib WHERE [Код выдачи]= {idChange}";
                    using (OleDbConnection connection = new OleDbConnection(ConfigurationManager.ConnectionStrings["Библиотека.Properties.Settings.LibraryConnectionString"].ConnectionString))
                    //Передача параметров в звапрос
                    using (OleDbCommand command = new OleDbCommand(query, connection))
                    {
                        connection.Open();
                    command.Parameters.AddWithValue("@dateof", dateTimePicker1.Value.ToString("dd-MM-yyyy"));
                    command.Parameters.AddWithValue("@datevf", dateTimePicker2.Value.ToString("dd-MM-yyyy"));
                    command.Parameters.AddWithValue("@datevp", dateTimePicker3.Value.ToString("dd-MM-yyyy"));
                    command.Parameters.AddWithValue("@exbook", comboBox1.SelectedValue);
                    command.Parameters.AddWithValue("@exread", comboBox2.SelectedValue);
                    command.Parameters.AddWithValue("@exlib", comboBox3.SelectedValue);

                    command.ExecuteNonQuery();
                        this.DialogResult = DialogResult.OK;
                    }
                }
            }
    }
}
