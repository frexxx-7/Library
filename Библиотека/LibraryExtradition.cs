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
using System.Configuration;

namespace Библиотека
{
    public partial class LibraryExtradition : Form
    {
        public string result;
        public LibraryExtradition()
        {
            InitializeComponent();
            RefreshTable();
            RefreshTable1();
            RefreshTable2();
            RefreshTable3();
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

        public void RefreshTable1()
        {
            string query = "SELECT Книга.[Код книги] AS [Код книги], Книга.[Название] AS [Название], Книга.[Автор] AS [Автор], " +
                "Книга.[Жанр] AS [Жанр], Книга.[Год издательства] AS [Год издательства] FROM Книга";
            var connectionString = ConfigurationManager.ConnectionStrings["Библиотека.Properties.Settings.LibraryConnectionString"].ConnectionString;
            using (OleDbDataAdapter adapter = new OleDbDataAdapter(query, connectionString))
            {
                DataTable dataTable = new DataTable();
                adapter.Fill(dataTable);
                dataGridView2.DataSource = dataTable;
            }
            dataGridView2.Columns[0].Visible = false;
        }
        public void RefreshTable2()
        {
            string query = "SELECT Библиотекарь.[Код библиотекаря] AS [Код библиотекаря], Библиотекарь.[Имя] AS [Имя], Библиотекарь.[Фамилия] AS [Фамилия], " +
                "Библиотекарь.[Отчество] AS [Отчество] FROM Библиотекарь";
            var connectionString = ConfigurationManager.ConnectionStrings["Библиотека.Properties.Settings.LibraryConnectionString"].ConnectionString;
            using (OleDbDataAdapter adapter = new OleDbDataAdapter(query, connectionString))
            {
                DataTable dataTable = new DataTable();
                adapter.Fill(dataTable);
                dataGridView3.DataSource = dataTable;
            }
            dataGridView3.Columns[0].Visible = false;
        }
        public void RefreshTable3()
        {
            string query = "SELECT Читатель.[Код читателя] AS [Код читателя], Читатель.[Имя] AS [Имя], Читатель.[Фамилия] AS [Фамилия], " +
                "Читатель.[Отчество] AS [Отчество], Читатель.[Номер телефона] AS [Номер телефона] FROM Читатель";
            var connectionString = ConfigurationManager.ConnectionStrings["Библиотека.Properties.Settings.LibraryConnectionString"].ConnectionString;
            using (OleDbDataAdapter adapter = new OleDbDataAdapter(query, connectionString))
            {
                DataTable dataTable = new DataTable();
                adapter.Fill(dataTable);
                dataGridView4.DataSource = dataTable;
            }
            dataGridView4.Columns[0].Visible = false;
        }
        private void LibraryExtradition_Load(object sender, EventArgs e)
        {

        }

        private void dataGridView1_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {

        }

        private void button3_Click(object sender, EventArgs e)
        {
            if (dataGridView1.SelectedRows.Count > 0)
            {
                string query = $"DELETE FROM Выдача WHERE  [Код выдачи] =  {dataGridView1.SelectedRows[0].Cells[0].Value.ToString()}";
                if (MessageBox.Show("Вы действительно хотите удалить заявку?", "Подтверждение удаления",
                MessageBoxButtons.YesNo, MessageBoxIcon.Warning) == DialogResult.Yes)
                {
                    using (OleDbConnection connection = new OleDbConnection(ConfigurationManager.ConnectionStrings["Библиотека.Properties.Settings.LibraryConnectionString"].ConnectionString))
                    using (OleDbCommand command = new OleDbCommand(query, connection))
                    {
                        connection.Open();
                        command.ExecuteNonQuery();
                    }
                    MessageBox.Show("Запись успешно удалена!", "Успех!");
                    RefreshTable();
                }
            }
        }

        private void button4_Click(object sender, EventArgs e)
        {
            dataGridView1.Sort(dataGridView1.Columns[7], ListSortDirection.Ascending);
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

        private void button7_Click(object sender, EventArgs e)
        {
            for (int i = 0; i < dataGridView2.RowCount; i++)
            {
                dataGridView2.Rows[i].Selected = false;
                for (int j = 0; j < dataGridView2.ColumnCount; j++)
                    if (dataGridView2.Rows[i].Cells[j].Value != null)
                        if (dataGridView2.Rows[i].Cells[j].Value.ToString().Contains(textBox2.Text))
                        {
                            dataGridView2.Rows[i].Selected = true;
                            break;
                        }
            }
        }

        private void button12_Click(object sender, EventArgs e)
        {
            for (int i = 0; i < dataGridView3.RowCount; i++)
            {
                dataGridView3.Rows[i].Selected = false;
                for (int j = 0; j < dataGridView3.ColumnCount; j++)
                    if (dataGridView3.Rows[i].Cells[j].Value != null)
                        if (dataGridView3.Rows[i].Cells[j].Value.ToString().Contains(textBox3.Text))
                        {
                            dataGridView3.Rows[i].Selected = true;
                            break;
                        }
            }
        }

        private void button17_Click(object sender, EventArgs e)
        {
            for (int i = 0; i < dataGridView4.RowCount; i++)
            {
                dataGridView4.Rows[i].Selected = false;
                for (int j = 0; j < dataGridView4.ColumnCount; j++)
                    if (dataGridView4.Rows[i].Cells[j].Value != null)
                        if (dataGridView4.Rows[i].Cells[j].Value.ToString().Contains(textBox4.Text))
                        {
                            dataGridView4.Rows[i].Selected = true;
                            break;
                        }
            }
        }

        private void button1_Click(object sender, EventArgs e)
        {
            if (new AddExtradition().ShowDialog() == DialogResult.OK)
            {
                RefreshTable();
                MessageBox.Show("Запись успешно добавлена!", "Успех!");
            }
        }

        private void button2_Click(object sender, EventArgs e)
        {
            if (dataGridView1.SelectedRows.Count > 0)
            {
                AddExtradition addRent = new AddExtradition();
                addRent.IDCHANGE = dataGridView1.SelectedRows[0].Cells[0].Value.ToString();
                if (addRent.ShowDialog() == DialogResult.OK)
                {
                    RefreshTable();
                    MessageBox.Show("Запись успешно изменена!", "Успех!");
                }
            }
        }

        private void button10_Click(object sender, EventArgs e)
        {
            if (dataGridView2.SelectedRows.Count > 0)
            {
                AddBook addClient = new AddBook();
                addClient.IDCHANGE = dataGridView2.SelectedRows[0].Cells[0].Value.ToString();
                if (addClient.ShowDialog() == DialogResult.OK)
                {
                    RefreshTable1();
                    MessageBox.Show("Запись успешно изменена!", "Успех!");
                }
            }
        }

        private void button9_Click(object sender, EventArgs e)
        {
            if (dataGridView2.SelectedRows.Count > 0)
            {
                string query = $"DELETE FROM Книга WHERE  [Код книги] =  {dataGridView2.SelectedRows[0].Cells[0].Value.ToString()}";
                if (MessageBox.Show("Вы действительно хотите удалить заявку?", "Подтверждение удаления",
                MessageBoxButtons.YesNo, MessageBoxIcon.Warning) == DialogResult.Yes)
                {
                    using (OleDbConnection connection = new OleDbConnection(ConfigurationManager.ConnectionStrings["Библиотека.Properties.Settings.LibraryConnectionString"].ConnectionString))
                    using (OleDbCommand command = new OleDbCommand(query, connection))
                    {
                        connection.Open();
                        command.ExecuteNonQuery();
                    }
                    MessageBox.Show("Запись успешно удалена!", "Успех!");
                    RefreshTable1();
                }
            }
        }

        private void button14_Click(object sender, EventArgs e)
        {
            if (dataGridView3.SelectedRows.Count > 0)
            {
                string query = $"DELETE FROM Библиотекарь WHERE  [Код библиотекаря] =  {dataGridView3.SelectedRows[0].Cells[0].Value.ToString()}";
                if (MessageBox.Show("Вы действительно хотите удалить заявку?", "Подтверждение удаления",
                MessageBoxButtons.YesNo, MessageBoxIcon.Warning) == DialogResult.Yes)
                {
                    using (OleDbConnection connection = new OleDbConnection(ConfigurationManager.ConnectionStrings["Библиотека.Properties.Settings.LibraryConnectionString"].ConnectionString))
                    using (OleDbCommand command = new OleDbCommand(query, connection))
                    {
                        connection.Open();
                        command.ExecuteNonQuery();
                    }
                    MessageBox.Show("Запись успешно удалена!", "Успех!");
                    RefreshTable2();
                }
            }
        }

        private void button19_Click(object sender, EventArgs e)
        {
            if (dataGridView4.SelectedRows.Count > 0)
            {
                string query = $"DELETE FROM Читатель WHERE  [Код читателя] =  {dataGridView4.SelectedRows[0].Cells[0].Value.ToString()}";
                if (MessageBox.Show("Вы действительно хотите удалить заявку?", "Подтверждение удаления",
                MessageBoxButtons.YesNo, MessageBoxIcon.Warning) == DialogResult.Yes)
                {
                    using (OleDbConnection connection = new OleDbConnection(ConfigurationManager.ConnectionStrings["Библиотека.Properties.Settings.LibraryConnectionString"].ConnectionString))
                    using (OleDbCommand command = new OleDbCommand(query, connection))
                    {
                        connection.Open();
                        command.ExecuteNonQuery();
                    }
                    MessageBox.Show("Запись успешно удалена!", "Успех!");
                    RefreshTable3();
                }
            }
        }

        private void button8_Click(object sender, EventArgs e)
        {
            dataGridView2.Sort(dataGridView2.Columns[1], ListSortDirection.Ascending);
        }

        private void button13_Click(object sender, EventArgs e)
        {
            dataGridView3.Sort(dataGridView3.Columns[1], ListSortDirection.Ascending);
        }

        private void button18_Click(object sender, EventArgs e)
        {
            dataGridView4.Sort(dataGridView4.Columns[1], ListSortDirection.Ascending);
        }

        private void button11_Click(object sender, EventArgs e)
        {
            if (new AddBook().ShowDialog() == DialogResult.OK)
            {
                RefreshTable1();
                MessageBox.Show("Запись успешно добавлена!", "Успех!");
            }
        }

        private void button16_Click(object sender, EventArgs e)
        {
            if (new AddLibrarian().ShowDialog() == DialogResult.OK)
            {
                RefreshTable2();
                MessageBox.Show("Запись успешно добавлена!", "Успех!");
            }
        }

        private void button15_Click(object sender, EventArgs e)
        {
            if (dataGridView3.SelectedRows.Count > 0)
            {
                AddLibrarian addSotrudnik = new AddLibrarian();
                addSotrudnik.IDCHANGE = dataGridView3.SelectedRows[0].Cells[0].Value.ToString();
                if (addSotrudnik.ShowDialog() == DialogResult.OK)
                {
                    RefreshTable2();
                    MessageBox.Show("Запись успешно изменена!", "Успех!");
                }
            }
        }

        private void button21_Click(object sender, EventArgs e)
        {
            if (new AddReader().ShowDialog() == DialogResult.OK)
            {
                RefreshTable3();
                MessageBox.Show("Запись успешно добавлена!", "Успех!");
            }
        }

        private void button20_Click(object sender, EventArgs e)
        {
            if (dataGridView4.SelectedRows.Count > 0)
            {
                AddReader addAvto = new AddReader();
                addAvto.IDCHANGE = dataGridView4.SelectedRows[0].Cells[0].Value.ToString();
                if (addAvto.ShowDialog() == DialogResult.OK)
                {
                    RefreshTable3();
                    MessageBox.Show("Запись успешно изменена!", "Успех!");
                }
            }
        }

        private void button6_Click(object sender, EventArgs e)
        {
            Library rentCar = new Library();
            rentCar.result = this.result;
            rentCar.Show();
            this.Close();
        }

        private void button22_Click(object sender, EventArgs e)
        {
            this.Close();
        }
    }
}
