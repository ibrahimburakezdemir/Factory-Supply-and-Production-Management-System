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
    public partial class Form6 : Form
    {
        public MySqlConnection mysqlbaglan = new MySqlConnection("Server=localhost;Database=prolab;Uid=root;Pwd='1827';");
        MySqlCommand cmd = new MySqlCommand();
        public Form6()
        {
            InitializeComponent();
        }

        private void Form6_Load(object sender, EventArgs e)
        {
            mysqlbaglan.Open();
        }

        /////////////K_URUN TABLOSUNA VERİ EKLEDİK
        private void Button1_Click(object sender, EventArgs e)
        {
            if (textBox1.Text != "" || textBox2.Text != "" || textBox3.Text != "" )
            {
                cmd.Connection = mysqlbaglan;
                cmd.CommandText = "Insert into k_urun(urun_adi, raf_omru, iscilik) Values('" + textBox1.Text + "','" + textBox2.Text + "','" + textBox3.Text + "')";
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


        /////////////K_URUN TABLOSU VERİ GÖSTER
        private void Button2_Click(object sender, EventArgs e)
        {
            string sql1 = "SELECT * FROM k_urun";
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


        /////////////K_URUN TABLOSU SİL
        private void Button3_Click(object sender, EventArgs e)
        {
            MySqlCommand cmd1 = new MySqlCommand();
            cmd1.Connection = mysqlbaglan;
            cmd1.CommandText = "Delete From k_urun Where urun_id='" + dataGridView1.CurrentRow.Cells[0].Value.ToString() + "'";
            cmd1.ExecuteNonQuery();
            
        }


        /////////////K_FIYAT TABLOSUNA VERİ EKLEDİK
        private void Button4_Click(object sender, EventArgs e)
        {
            if (textBox4.Text != "" || textBox5.Text != "" || textBox6.Text != "" || textBox7.Text != "")
            {
                MySqlCommand cmd2 = new MySqlCommand();
                cmd2.Connection = mysqlbaglan;
                
                cmd2.CommandText = "Insert into k_fiyat(uretim_tarihi, urun_id, toplam_maliyet, satis_fiyati) Values('" + textBox4.Text + "','" + textBox5.Text + "','" + textBox6.Text + "','" + textBox7.Text + "')";
                cmd2.ExecuteNonQuery();
                
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


        /////////////K_FIYAT TABLOSU VERİ GÖSTER
        private void Button5_Click(object sender, EventArgs e)
        {
            string sql1 = "SELECT * FROM k_fiyat";
            DataTable dt = new DataTable();

            MySqlDataAdapter adapter1 = new MySqlDataAdapter();
            MySqlCommand command1 = new MySqlCommand();

            command1.CommandText = sql1;
            command1.Connection = mysqlbaglan;
            adapter1.SelectCommand = command1;

            //mysqlbaglan.Open();
            adapter1.Fill(dt);
            dataGridView2.DataSource = dt;
            
        }
        /////////////K_FİYAT TABLOSU SİL
        private void Button6_Click(object sender, EventArgs e)
        {
            MySqlCommand cmd3 = new MySqlCommand();
            
            cmd3.Connection = mysqlbaglan;
            cmd3.CommandText = "Delete From k_fiyat Where uretim_tarihi='" + dataGridView2.CurrentRow.Cells[0].Value.ToString() + "' && urun_id='" + dataGridView2.CurrentRow.Cells[1].Value.ToString() + "'";
            cmd3.ExecuteNonQuery();
            
        }
    }
}
