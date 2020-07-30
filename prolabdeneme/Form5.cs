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
    public partial class Form5 : Form
    {
        public MySqlConnection mysqlbaglan = new MySqlConnection("Server=localhost;Database=prolab;Uid=root;Pwd='1827';");
        MySqlCommand cmd = new MySqlCommand();

        public Form5()
        {
            InitializeComponent();
        }

        private void Button1_Click(object sender, EventArgs e)
        {
            string sql1 = "SELECT * FROM uretici";
            DataTable dt = new DataTable();

            MySqlDataAdapter adapter = new MySqlDataAdapter();
            MySqlCommand command = new MySqlCommand();

            command.CommandText = sql1;
            command.Connection = mysqlbaglan;
            adapter.SelectCommand = command;

            //mysqlbaglan.Open();
            adapter.Fill(dt);
            dataGridView1.DataSource = dt;
        }

        private void Form5_Load(object sender, EventArgs e)
        {
            mysqlbaglan.Open();
        }

        ////// U_KİMYASAL_URUN TABLOSUNA EKLEME
        private void Button2_Click(object sender, EventArgs e)
        {
            if ( textBox2.Text != "" || textBox3.Text != "")
            {
                cmd.Connection = mysqlbaglan;
                cmd.CommandText = "Insert into u_kimyasal(kimyasal_urun, stok) Values('" + textBox2.Text + "','" + textBox3.Text + "')";
                cmd.ExecuteNonQuery();
                mysqlbaglan.Close();
                for (int i = 0; i < this.Controls.Count; i++)
                {
                    if (Controls[i] is TextBox) Controls[i].Text = "";
                }
            }
            else
            {
                MessageBox.Show("Lutfen bilgileri eksiksiz giriniz");
            }
        }

        ////// U_KİMYASAL_URUN eleman silme
        private void Button3_Click(object sender, EventArgs e)
        {
            string sql1 = "SELECT * FROM u_kimyasal";
            DataTable dt = new DataTable();

            MySqlDataAdapter adapter = new MySqlDataAdapter();
            MySqlCommand command = new MySqlCommand();

            command.CommandText = sql1;
            command.Connection = mysqlbaglan;
            adapter.SelectCommand = command;

            //mysqlbaglan.Open();
            adapter.Fill(dt);
            dataGridView2.DataSource = dt;
        }

        private void Button4_Click(object sender, EventArgs e)
        {
            cmd.Connection = mysqlbaglan;
            cmd.CommandText = "Delete From u_kimyasal Where k_id='" + dataGridView2.CurrentRow.Cells[0].Value.ToString() + "'";
            cmd.ExecuteNonQuery();
        }

        /////////////U_BİLESEN VERİ EKLEME
        private void Button5_Click(object sender, EventArgs e)
        {
            if (textBox5.Text != "" )
            {
                cmd.Connection = mysqlbaglan;
                cmd.CommandText = "Insert into u_bilesen(hammadde) Values('" + textBox5.Text + "')";
                cmd.ExecuteNonQuery();
                mysqlbaglan.Close();
                for (int i = 0; i < this.Controls.Count; i++)
                {
                    if (Controls[i] is TextBox) Controls[i].Text = "";
                }
            }
            else
            {
                MessageBox.Show("Lutfen bilgileri eksiksiz giriniz");
            }

        }


        ///////////////U_BİLESEN GÖSTERME
        private void Button6_Click(object sender, EventArgs e)
        {
            string sql1 = "SELECT * FROM u_bilesen";
            DataTable dt = new DataTable();

            MySqlDataAdapter adapter = new MySqlDataAdapter();
            MySqlCommand command = new MySqlCommand();

            command.CommandText = sql1;
            command.Connection = mysqlbaglan;
            adapter.SelectCommand = command;

            //mysqlbaglan.Open();
            adapter.Fill(dt);
            dataGridView3.DataSource = dt;
        }


        ///////////////U_BİLESEN SİLME
        private void Button7_Click(object sender, EventArgs e)
        {
            cmd.Connection = mysqlbaglan;
            cmd.CommandText = "Delete From u_bilesen Where b_id='" + dataGridView3.CurrentRow.Cells[0].Value.ToString() + "'";
            cmd.ExecuteNonQuery();
        }

        private void Label6_Click(object sender, EventArgs e)
        {

        }

        ///////////////U_FORMUL VERİ EKLEME
        private void Button8_Click(object sender, EventArgs e)
        {
            if (textBox6.Text != "" || textBox7.Text != "" || textBox8.Text != "")
            {
                cmd.Connection = mysqlbaglan;
                cmd.CommandText = "Insert into u_formul(u_id, k_id, b_id) Values('" + textBox6.Text + "','" + textBox7.Text + "','" + textBox8.Text + "')";
                cmd.ExecuteNonQuery();
                mysqlbaglan.Close();
                for (int i = 0; i < this.Controls.Count; i++)
                {
                    if (Controls[i] is TextBox) Controls[i].Text = "";
                }
            }
            else
            {
                MessageBox.Show("Lutfen bilgileri eksiksiz giriniz");
            }
        }


        ///////////////U_FORMUL GÖSTERME
        private void Button10_Click(object sender, EventArgs e)
        {
            string sql1 = "SELECT * FROM u_formul";
            DataTable dt = new DataTable();

            MySqlDataAdapter adapter = new MySqlDataAdapter();
            MySqlCommand command = new MySqlCommand();

            command.CommandText = sql1;
            command.Connection = mysqlbaglan;
            adapter.SelectCommand = command;

            //mysqlbaglan.Open();
            adapter.Fill(dt);
            dataGridView4.DataSource = dt;
        }


        ///////////////U_FORMUL SİLME
        private void Button9_Click(object sender, EventArgs e)
        {
            cmd.Connection = mysqlbaglan;
            cmd.CommandText = "Delete From u_formul Where u_id='" + dataGridView4.CurrentRow.Cells[0].Value.ToString() + "'";
            cmd.ExecuteNonQuery();
        }
    }
}
