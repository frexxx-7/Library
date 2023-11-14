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
    public partial class Profile : Form
    {
        private string idAdditionalInfo;
        public Profile()
        {
            InitializeComponent();
        }

        private void Profile_Load(object sender, EventArgs e)
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
                label12.Text = dataTable.Rows[0][1].ToString();
                label13.Text = dataTable.Rows[0][2].ToString();
                idAdditionalInfo = dataTable.Rows[0][3].ToString();
                label6.Text = dataTable.Rows[0][5].ToString();
                label7.Text = dataTable.Rows[0][6].ToString();
                label8.Text = dataTable.Rows[0][7].ToString();
                label9.Text = dataTable.Rows[0][9].ToString();
            }
            db.closeConnection();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            new EditProfile(idAdditionalInfo).Show();
            this.Hide();
        }

        private void Profile_FormClosed(object sender, FormClosedEventArgs e)
        {
            Application.Exit();
        }
    }
}
