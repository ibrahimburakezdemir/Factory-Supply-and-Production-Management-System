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
    public partial class Form4 : Form
    {
        public MySqlConnection mysqlbaglan = new MySqlConnection("Server=localhost;Database=prolab;Uid=root;Pwd='1827';");
        MySqlCommand cmd = new MySqlCommand();
        public Form4()
        {
            InitializeComponent();
        }

        private void Form4_Load(object sender, EventArgs e)
        {
            mysqlbaglan.Open();
        }


        /////////////T_FİRMA TABLOSUNA VERİ EKLEDİK
        private void Button1_Click(object sender, EventArgs e)
        {
            if (textBox2.Text != "" || textBox3.Text != "" || textBox4.Text != "" || textBox5.Text != "")
            {
                cmd.Connection = mysqlbaglan;
                cmd.CommandText = "Insert into t_firma(  firma_adi, ulke, sehir, uzaklik ) Values('" + textBox2.Text + "','" + textBox3.Text + "','" + textBox4.Text + "','" + textBox5.Text + "')";
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


        ////////////T_FİRMA TABLOSUNUN VERİLERİNİ GÖRÜNTÜLEDİK
        private void Button2_Click(object sender, EventArgs e)
        {
            string sql1 = "SELECT * FROM t_firma";
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

        ////////////T_FİRMA TABLOSUNda idye göre silme
        private void Button3_Click(object sender, EventArgs e)
        {
            cmd.Connection = mysqlbaglan;
            cmd.CommandText = "Delete From t_firma Where f_id='" + dataGridView1.CurrentRow.Cells[0].Value.ToString() + "'";
            cmd.ExecuteNonQuery();
        }

        ////////////T_HAMMADDE VERİ EKLEDİK
        private void Button4_Click(object sender, EventArgs e)
        {
            if ( textBox7.Text != "" || textBox8.Text != "" )
            {
                cmd.Connection = mysqlbaglan;
                cmd.CommandText = "Insert into t_hammadde(  hammadde, raf_omru ) Values('" + textBox7.Text + "','" + textBox8.Text + "')";
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

        ////////////T_HAMMADDE görüntüle
        private void Button6_Click(object sender, EventArgs e)
        {
            string sql1 = "SELECT * FROM t_hammadde";
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


        //////////// T_HAMMADDE VERİ SİLDİK
        private void Button5_Click(object sender, EventArgs e)
        {
            cmd.Connection = mysqlbaglan;
            cmd.CommandText = "Delete From t_hammadde Where h_id='" + dataGridView2.CurrentRow.Cells[0].Value.ToString() + "'";
            cmd.ExecuteNonQuery();
        }

        ///////////TEDARİK TABLOSUNA EKLE
        private void Button7_Click(object sender, EventArgs e)
        {
           // mysqlbaglan.Open();
            if (textBox9.Text != "" || textBox10.Text != "" || textBox11.Text != "" || textBox12.Text != "" || textBox13.Text != "")
            {
                cmd.Connection = mysqlbaglan;
                cmd.CommandText = "Insert into tedarikci(fi_id, h_id, miktar, uretim_tarihi, satis_fiyati) Values('" + textBox9.Text + "','" + textBox10.Text + "','" + textBox11.Text + "','" + textBox12.Text + "','" + textBox13.Text + "')";
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


        ///////////TEDARİK TABLOSU GÖRÜNTÜLE
        private void Button9_Click(object sender, EventArgs e)
        {
            string sql1 = "SELECT * FROM tedarikci";
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

        ///////////TEDARİK SİL
        private void Button8_Click(object sender, EventArgs e)
        {
            cmd.Connection = mysqlbaglan;
            cmd.CommandText = "Delete From tedarikci Where fi_id='" + dataGridView3.CurrentRow.Cells[0].Value.ToString() + "'";
            cmd.ExecuteNonQuery();
        }
    }
}
