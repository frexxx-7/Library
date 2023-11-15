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
using Библиотека.Classes;
using MySql.Data.MySqlClient;
using Excel = Microsoft.Office.Interop.Excel;

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
            DB db = new DB();
            string query = "SELECT Выдача.`Код выдачи` AS `Код выдачи`, Выдача.`Дата оформления` AS `Дата оформления`, " +
                "Выдача.`Дата возврата план` AS `Дата возврата план`, Выдача.`Дата возврата факт` AS `Дата возврата факт`," +
                "Выдача.`Код книги` AS `Код книги`, Выдача.`Код читателя` AS `Код читателя`, Выдача.`Код библеотекаря` AS `Код библиотекаря`, Книга.`Название` AS `Название Книги`, " +
                "Читатель.`Фамилия` AS `Фамилия читателя`, Библиотекарь.`Фамилия` AS `Фамилия Библиотекаря` " +
                "FROM (((Выдача INNER JOIN Книга ON Выдача.`Код книги` = Книга.`Код книги`) INNER JOIN " +
                "Библиотекарь ON Выдача.`Код библеотекаря` = Библиотекарь.`Код библиотекаря`) INNER JOIN " +
                "Читатель ON Выдача.`Код читателя` = Читатель.`Код читателя`)";
            using (MySqlDataAdapter adapter = new MySqlDataAdapter(query, db.getConnection()))
            {
                db.openConnection();
                DataTable dataTable = new DataTable();
                adapter.Fill(dataTable);
                dataGridView1.DataSource = dataTable;
                db.closeConnection();
            }
            dataGridView1.Columns[0].Visible = false;
            dataGridView1.Columns[4].Visible = false;
            dataGridView1.Columns[5].Visible = false;
            dataGridView1.Columns[6].Visible = false;
        }

        public void RefreshTable1()
        {
            DB db = new DB();
            string query = "SELECT Книга.`Код книги` AS `Код книги`, Книга.`Название` AS `Название`, `Автор книги`.`ФИО автора` AS `Автор`, " +
                "Книга.`Жанр` AS `Жанр`, Книга.`Год издательства` AS `Год издательства` FROM Книга " +
                "left join `Автор книги` on `Автор книги`.`Код` = `Книга`.`Код автора`";
            db.openConnection();
            using (MySqlDataAdapter adapter = new MySqlDataAdapter(query, db.getConnection()))
            {
                DataTable dataTable = new DataTable();
                adapter.Fill(dataTable);
                dataGridView2.DataSource = dataTable;
            }
            db.closeConnection();
            dataGridView2.Columns[0].Visible = false;
        }
        public void RefreshTable2()
        {
            DB db = new DB();
            string query = "SELECT Библиотекарь.`Код библиотекаря` AS `Код библиотекаря`, Библиотекарь.`Имя` AS `Имя`, Библиотекарь.`Фамилия` AS `Фамилия`, " +
                "Библиотекарь.`Отчество` AS `Отчество` FROM Библиотекарь";
            db.openConnection();
            using (MySqlDataAdapter adapter = new MySqlDataAdapter(query, db.getConnection()))
            {
                DataTable dataTable = new DataTable();
                adapter.Fill(dataTable);
                dataGridView3.DataSource = dataTable;
            }
            db.closeConnection();
            dataGridView3.Columns[0].Visible = false;
        }
        public void RefreshTable3()
        {
            DB db = new DB();
            string query = "SELECT Читатель.`Код читателя` AS `Код читателя`, Читатель.`Имя` AS `Имя`, Читатель.`Фамилия` AS `Фамилия`, " +
                "Читатель.`Отчество` AS `Отчество`, Читатель.`Номер телефона` AS `Номер телефона` FROM Читатель";
            db.openConnection();
            using (MySqlDataAdapter adapter = new MySqlDataAdapter(query, db.getConnection()))
            {
                DataTable dataTable = new DataTable();
                adapter.Fill(dataTable);
                dataGridView4.DataSource = dataTable;
            }
            db.closeConnection();
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
            DB db = new DB();
            if (dataGridView1.SelectedRows.Count > 0)
            {
                string query = $"DELETE FROM Выдача WHERE  `Код выдачи` =  {dataGridView1.SelectedRows[0].Cells[0].Value.ToString()}";
                if (MessageBox.Show("Вы действительно хотите удалить заявку?", "Подтверждение удаления",
                MessageBoxButtons.YesNo, MessageBoxIcon.Warning) == DialogResult.Yes)
                {
                    using (MySqlCommand command = new MySqlCommand(query, db.getConnection()))
                    {
                        db.openConnection();
                        command.ExecuteNonQuery();
                    }
                    MessageBox.Show("Запись успешно удалена!", "Успех!");
                    RefreshTable();
                }
            }
            db.closeConnection();
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
            DB db = new DB();
            if (dataGridView2.SelectedRows.Count > 0)
            {
                string query = $"DELETE FROM Книга WHERE  `Код книги` =  {dataGridView2.SelectedRows[0].Cells[0].Value.ToString()}";
                if (MessageBox.Show("Вы действительно хотите удалить заявку?", "Подтверждение удаления",
                MessageBoxButtons.YesNo, MessageBoxIcon.Warning) == DialogResult.Yes)
                {
                    db.openConnection();
                    using (MySqlCommand command = new MySqlCommand(query, db.getConnection()))
                    {
                        command.ExecuteNonQuery();
                    }
                    MessageBox.Show("Запись успешно удалена!", "Успех!");
                    RefreshTable1();
                }
            }
            db.closeConnection();
        }

        private void button14_Click(object sender, EventArgs e)
        {
            DB db = new DB();
            if (dataGridView3.SelectedRows.Count > 0)
            {
                string query = $"DELETE FROM Библиотекарь WHERE  `Код библиотекаря` =  {dataGridView3.SelectedRows[0].Cells[0].Value.ToString()}";
                if (MessageBox.Show("Вы действительно хотите удалить заявку?", "Подтверждение удаления",
                MessageBoxButtons.YesNo, MessageBoxIcon.Warning) == DialogResult.Yes)
                {
                    db.openConnection();
                    using (MySqlCommand command = new MySqlCommand(query, db.getConnection()))
                    {
                        command.ExecuteNonQuery();
                    }
                    MessageBox.Show("Запись успешно удалена!", "Успех!");
                    RefreshTable2();
                }
            }
            db.closeConnection();
        }

        private void button19_Click(object sender, EventArgs e)
        {
            DB db = new DB();
            if (dataGridView4.SelectedRows.Count > 0)
            {
                string query = $"DELETE FROM Читатель WHERE  `Код читателя` =  {dataGridView4.SelectedRows[0].Cells[0].Value.ToString()}";
                if (MessageBox.Show("Вы действительно хотите удалить заявку?", "Подтверждение удаления",
                MessageBoxButtons.YesNo, MessageBoxIcon.Warning) == DialogResult.Yes)
                {
                    db.openConnection();
                    using (MySqlCommand command = new MySqlCommand(query, db.getConnection()))
                    {
                        command.ExecuteNonQuery();
                    }
                    db.closeConnection();
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

        private void LibraryExtradition_FormClosed(object sender, FormClosedEventArgs e)
        {
            Application.Exit();
        }

        private void button23_Click(object sender, EventArgs e)
        {
            Excel.Application excelApp = new Excel.Application();
            Excel.Workbook workbook = excelApp.Workbooks.Add();
            Excel.Worksheet worksheet = workbook.ActiveSheet;
            int countColumn = 1;
            for (int j = 0; j < dataGridView1.Columns.Count; j++)
            {
                if (dataGridView1.Columns[j].Visible)
                {
                    worksheet.Cells[1, countColumn] = dataGridView1.Columns[j].HeaderText;
                    countColumn++;
                }
            }
            for (int i = 0; i < dataGridView1.Rows.Count; i++)
            {
                countColumn = 1;
                for (int j = 0; j < dataGridView1.Columns.Count; j++)
                {
                    if (dataGridView1.Columns[j].Visible)
                    {
                        worksheet.Cells[i + 2, countColumn] = dataGridView1.Rows[i].Cells[j].Value;
                        countColumn++;
                    }
                }
            }
            SaveFileDialog saveFileDialog1 = new SaveFileDialog();
            saveFileDialog1.Filter = "Excel File|*.xlsx";
            saveFileDialog1.Title = "Сохранить Excel файл";
            saveFileDialog1.ShowDialog();
            if (saveFileDialog1.FileName != "")
            {
                workbook.SaveAs(saveFileDialog1.FileName);
            }
            workbook.Close();
            excelApp.Quit();
        }

        private void button24_Click(object sender, EventArgs e)
        {
            Excel.Application excelApp = new Excel.Application();
            Excel.Workbook workbook = excelApp.Workbooks.Add();
            Excel.Worksheet worksheet = workbook.ActiveSheet;
            int countColumn = 1;
            for (int j = 0; j < dataGridView2.Columns.Count; j++)
            {
                if (dataGridView2.Columns[j].Visible)
                {
                    worksheet.Cells[1, countColumn] = dataGridView2.Columns[j].HeaderText;
                    countColumn++;
                }
            }
            for (int i = 0; i < dataGridView2.Rows.Count; i++)
            {
                countColumn = 1;
                for (int j = 0; j < dataGridView2.Columns.Count; j++)
                {
                    if (dataGridView2.Columns[j].Visible)
                    {
                        worksheet.Cells[i + 2, countColumn] = dataGridView2.Rows[i].Cells[j].Value;
                        countColumn++;
                    }
                }
            }
            SaveFileDialog saveFileDialog1 = new SaveFileDialog();
            saveFileDialog1.Filter = "Excel File|*.xlsx";
            saveFileDialog1.Title = "Сохранить Excel файл";
            saveFileDialog1.ShowDialog();
            if (saveFileDialog1.FileName != "")
            {
                workbook.SaveAs(saveFileDialog1.FileName);
            }
            workbook.Close();
            excelApp.Quit();
        }

        private void button25_Click(object sender, EventArgs e)
        {
            Excel.Application excelApp = new Excel.Application();
            Excel.Workbook workbook = excelApp.Workbooks.Add();
            Excel.Worksheet worksheet = workbook.ActiveSheet;
            int countColumn = 1;
            for (int j = 0; j < dataGridView3.Columns.Count; j++)
            {
                if (dataGridView3.Columns[j].Visible)
                {
                    worksheet.Cells[1, countColumn] = dataGridView3.Columns[j].HeaderText;
                    countColumn++;
                }
            }
            for (int i = 0; i < dataGridView3.Rows.Count; i++)
            {
                countColumn = 1;
                for (int j = 0; j < dataGridView3.Columns.Count; j++)
                {
                    if (dataGridView3.Columns[j].Visible)
                    {
                        worksheet.Cells[i + 2, countColumn] = dataGridView3.Rows[i].Cells[j].Value;
                        countColumn++;
                    }
                }
            }
            SaveFileDialog saveFileDialog1 = new SaveFileDialog();
            saveFileDialog1.Filter = "Excel File|*.xlsx";
            saveFileDialog1.Title = "Сохранить Excel файл";
            saveFileDialog1.ShowDialog();
            if (saveFileDialog1.FileName != "")
            {
                workbook.SaveAs(saveFileDialog1.FileName);
            }
            workbook.Close();
            excelApp.Quit();
        }

        private void button26_Click(object sender, EventArgs e)
        {
            Excel.Application excelApp = new Excel.Application();
            Excel.Workbook workbook = excelApp.Workbooks.Add();
            Excel.Worksheet worksheet = workbook.ActiveSheet;
            int countColumn = 1;
            for (int j = 0; j < dataGridView4.Columns.Count; j++)
            {
                if (dataGridView4.Columns[j].Visible)
                {
                    worksheet.Cells[1, countColumn] = dataGridView4.Columns[j].HeaderText;
                    countColumn++;
                }
            }
            for (int i = 0; i < dataGridView4.Rows.Count; i++)
            {
                countColumn = 1;
                for (int j = 0; j < dataGridView4.Columns.Count; j++)
                {
                    if (dataGridView4.Columns[j].Visible)
                    {
                        worksheet.Cells[i + 2, countColumn] = Convert.ToString(dataGridView4.Rows[i].Cells[j].Value);
                        countColumn++;
                    }
                }
            }
            SaveFileDialog saveFileDialog1 = new SaveFileDialog();
            saveFileDialog1.Filter = "Excel File|*.xlsx";
            saveFileDialog1.Title = "Сохранить Excel файл";
            saveFileDialog1.ShowDialog();
            if (saveFileDialog1.FileName != "")
            {
                workbook.SaveAs(saveFileDialog1.FileName);
            }
            workbook.Close();
            excelApp.Quit();
        }
    }
}
