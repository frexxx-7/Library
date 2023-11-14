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
using Библиотека.Classes;
using static System.Windows.Forms.VisualStyles.VisualStyleElement;

namespace Библиотека
{
    public partial class AddAddress : Form
    {
        public AddAddress()
        {
            InitializeComponent();
        }

        private void button2_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            DB db = new DB();
            string query = "INSERT INTO Адрес(`Дом`,`Улица`,`Город`, `Страна`) VALUES(@house,@street,@city,@country)";
            using (MySqlCommand command = new MySqlCommand(query, db.getConnection()))
            {
                db.openConnection();
                command.Parameters.AddWithValue("@house", textBox1.Text);
                command.Parameters.AddWithValue("@street", textBox1.Text);
                command.Parameters.AddWithValue("@city", textBox1.Text);
                command.Parameters.AddWithValue("@country", textBox5.Text);

                command.ExecuteNonQuery();
                db.closeConnection();
                this.DialogResult = DialogResult.OK;
                this.Close();
            }
        }
    }
}
