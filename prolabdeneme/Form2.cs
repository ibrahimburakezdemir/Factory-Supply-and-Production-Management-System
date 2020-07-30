using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using MySql.Data;
using MySql.Data.MySqlClient;

namespace prolabdeneme
{
    public partial class Form2 : Form
    {
        public MySqlConnection mysqlbaglan = new MySqlConnection("Server=localhost;Database=prolab;Uid=root;Pwd='1827';");

        public Form2()
        {
            InitializeComponent();

        }

        private void DataGridView1_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {


        }

        private void Form2_Load(object sender, EventArgs e)
        {
            string sql1 = "SELECT * FROM musteri";
            string sql2 = "SELECT * FROM m_siparis";
            string sql3 = "SELECT * FROM uretici";
            string sql4 = "SELECT * FROM u_bilesen";
            string sql5 = "SELECT * FROM u_kimyasal";
            string sql6 = "SELECT * FROM u_formul";
            string sql7 = "SELECT * FROM t_firma";
            string sql8 = "SELECT * FROM t_hammadde";
            string sql9 = "SELECT * FROM tedarikci";
            string sql10 = "SELECT * FROM k_urun";
            string sql11 = "SELECT * FROM k_fiyat";

            DataTable dt1 = new DataTable();
            DataTable dt2 = new DataTable();
            DataTable dt3 = new DataTable();
            DataTable dt4 = new DataTable();
            DataTable dt5 = new DataTable();
            DataTable dt6 = new DataTable();
            DataTable dt7 = new DataTable();
            DataTable dt8 = new DataTable();
            DataTable dt9 = new DataTable();
            DataTable dt10 = new DataTable();
            DataTable dt11 = new DataTable();

            MySqlDataAdapter adapter = new MySqlDataAdapter();
            MySqlCommand command = new MySqlCommand();

            command.CommandText = sql1;
            command.Connection = mysqlbaglan;
            adapter.SelectCommand = command;
            mysqlbaglan.Open();
            adapter.Fill(dt1);
            dataGridView1.DataSource = dt1;

            command.CommandText = sql2;
            command.Connection = mysqlbaglan;
            adapter.SelectCommand = command;
            adapter.Fill(dt2);
            dataGridView2.DataSource = dt2;

            command.CommandText = sql3;
            command.Connection = mysqlbaglan;
            adapter.SelectCommand = command;
            adapter.Fill(dt3);
            dataGridView3.DataSource = dt3;

            command.CommandText = sql4;
            command.Connection = mysqlbaglan;
            adapter.SelectCommand = command;
            adapter.Fill(dt4);
            dataGridView4.DataSource = dt4;

            command.CommandText = sql5;
            command.Connection = mysqlbaglan;
            adapter.SelectCommand = command;
            adapter.Fill(dt5);
            dataGridView5.DataSource = dt5;

            command.CommandText = sql6;
            command.Connection = mysqlbaglan;
            adapter.SelectCommand = command;
            adapter.Fill(dt6);
            dataGridView6.DataSource = dt6;

            command.CommandText = sql7;
            command.Connection = mysqlbaglan;
            adapter.SelectCommand = command;
            adapter.Fill(dt7);
            dataGridView7.DataSource = dt7;

            command.CommandText = sql8;
            command.Connection = mysqlbaglan;
            adapter.SelectCommand = command;
            adapter.Fill(dt8);
            dataGridView8.DataSource = dt8;

            command.CommandText = sql9;
            command.Connection = mysqlbaglan;
            adapter.SelectCommand = command;
            adapter.Fill(dt9);
            dataGridView9.DataSource = dt9;

            command.CommandText = sql10;
            command.Connection = mysqlbaglan;
            adapter.SelectCommand = command;
            adapter.Fill(dt10);
            dataGridView10.DataSource = dt10;

            command.CommandText = sql11;
            command.Connection = mysqlbaglan;
            adapter.SelectCommand = command;
            adapter.Fill(dt11);
            dataGridView11.DataSource = dt11;
        }
    }
}
