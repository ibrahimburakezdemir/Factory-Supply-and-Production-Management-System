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
    public partial class Form1 : Form
    {
        public MySqlConnection mysqlbaglan = new MySqlConnection("Server=localhost;Database=prolab;Uid=root;Pwd='1827';");
        public Form1()
        {
            InitializeComponent();
        }

        private void Button1_Click(object sender, EventArgs e)
        {
            Form2 form2 = new Form2();
           // form2.MdiParent = this;
            form2.Show();
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            try
            {
                mysqlbaglan.Open(); //oluşturtuğumuz tanımı çalıştırarak açılmasını sağlıyoruz
                if (mysqlbaglan.State != ConnectionState.Closed) // tanımın durumunu kontrol ediyoruz bağlı mı değil mi
                {
                    MessageBox.Show("Bağlantı Başarılı Bir Şekilde Gerçekleşti"); // bağlı ise buradaki işlemler gerçekleşiyor
                }
                else
                {
                    MessageBox.Show("Maalesef Bağlantı Yapılamadı...!"); // bağlı değilse buradaki işlemler gerçekleşiyor
                }
            }
            catch (Exception err)
            {
                MessageBox.Show("Hata! " + err.Message, "Hata Oluştu", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }

        }

        private void Button2_Click(object sender, EventArgs e)
        {
            

        }

        private void Button3_Click(object sender, EventArgs e)
        {
            Form3 form3 = new Form3();
            // form2.MdiParent = this;
            form3.Show();
        }

        private void Button4_Click(object sender, EventArgs e)
        {
            Form4 form4 = new Form4();
            // form2.MdiParent = this;
            form4.Show();
        }

        private void Button5_Click(object sender, EventArgs e)
        {
            Form5 form5 = new Form5();
            // form2.MdiParent = this;
            form5.Show();
        }

        private void Button6_Click(object sender, EventArgs e)
        {
            Form6 form6 = new Form6();
            // form2.MdiParent = this;
            form6.Show();
        }

        private void Label2_Click(object sender, EventArgs e)
        {

        }
    }
}
