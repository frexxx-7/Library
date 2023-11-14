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
    public partial class CheckExtradition : Form
    {
        public CheckExtradition()
        {
            InitializeComponent();
            RefreshTable();
        }
        public void RefreshTable()
        {
            string query = "SELECT Выдача.[Код выдачи] AS [Код выдачи], Выдача.[Дата оформления] AS [Дата оформления], " +
                "Выдача.[Дата возврата план] AS [Дата возврата план], Выдача.[Дата возврата факт] AS [Дата возврата факт]," +
                "Выдача.[Код книги] AS [Код книги], Выдача.[Код читателя] AS [Код читателя], Выдача.[Код библеотекаря] AS [Код библиотекаря], Книга.[Название] AS [Название Книги], " +
                "Читатель.[Фамилия] AS [Фамилия читателя], Библиотекарь.[Фамилия] AS [Фамилия Библиотекаря] " +
                "FROM (((Выдача INNER JOIN Книга ON Выдача.[Код книги] = Книга.[Код книги]) INNER JOIN " +
                "Библиотекарь ON Выдача.[Код библеотекаря] = Библиотекарь.[Код библиотекаря]) INNER JOIN " +
                "Читатель ON Выдача.[Код читателя] = Читатель.[Код читателя])";
            var connectionString = ConfigurationManager.ConnectionStrings["Библиотека.Properties.Settings.LibraryConnectionString"].ConnectionString;
            using (OleDbDataAdapter adapter = new OleDbDataAdapter(query, connectionString))
            {
                DataTable dataTable = new DataTable();
                adapter.Fill(dataTable);
                dataGridView1.DataSource = dataTable;
            }
            dataGridView1.Columns[0].Visible = false;
            dataGridView1.Columns[4].Visible = false;
            dataGridView1.Columns[5].Visible = false;
            dataGridView1.Columns[6].Visible = false;
        }
        private void button2_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            Library rentCar = new Library();
            rentCar.Show();
            this.Close();
        }

        private void button5_Click(object sender, EventArgs e)
        {
            for (int i = 0; i < dataGridView1.RowCount; i++)
            {
                dataGridView1.Rows[i].Selected = false;
                for (int j = 0; j < dataGridView1.ColumnCount; j++)
                    if (dataGridView1.Rows[i].Cells[j].Value != null)
                        if (dataGridView1.Rows[i].Cells[j].Value.ToString().Contains(textBox1.Text))
                        {
                            dataGridView1.Rows[i].Selected = true;
                            break;
                        }
            }
        }
    }
}
