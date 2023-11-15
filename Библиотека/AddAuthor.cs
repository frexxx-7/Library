using MySql.Data.MySqlClient;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using static System.Windows.Forms.VisualStyles.VisualStyleElement;
using Библиотека.Classes;

namespace Библиотека
{
    public partial class AddAuthor : Form
    {
        public AddAuthor()
        {
            InitializeComponent();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            DB db = new DB();
            string query = "INSERT INTO `Автор книги`(`ФИО автора`) VALUES(@fio)";
            using (MySqlCommand command = new MySqlCommand(query, db.getConnection()))
            {
                db.openConnection();
                command.Parameters.AddWithValue("@fio", textBox1.Text);

                command.ExecuteNonQuery();
                db.closeConnection();
                this.DialogResult = DialogResult.OK;
                this.Close();
            }
        }

        private void button2_Click(object sender, EventArgs e)
        {
            this.Close();
        }
    }
}
