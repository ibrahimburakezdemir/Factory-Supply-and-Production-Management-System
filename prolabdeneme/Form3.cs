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
    public partial class Form3 : Form
    {
        public MySqlConnection mysqlbaglan = new MySqlConnection("Server=localhost;Database=prolab;Uid=root;Pwd='1827';");
        MySqlCommand cmd = new MySqlCommand();
        public Form3()
        {
            InitializeComponent();
        }


        private void Label1_Click(object sender, EventArgs e)
        {

        }

        private void Label2_Click(object sender, EventArgs e)
        {

        }

        private void Label4_Click(object sender, EventArgs e)
        {

        }

        private void TextBox4_TextChanged(object sender, EventArgs e)
        {

        }

        private void Form3_Load(object sender, EventArgs e)
        {
            mysqlbaglan.Open();

        }

        //////////MUSTERİ TABLOSUNA VERİ EKLEDİK
        private void Button1_Click(object sender, EventArgs e)
        {
            if (textBox2.Text != "" || textBox3.Text != "" || textBox4.Text != "")
            {
                cmd.Connection = mysqlbaglan;
                cmd.CommandText = "Insert into musteri( ad, soyad, adres) Values('" + textBox2.Text + "','" + textBox3.Text + "','" + textBox4.Text + "')";
                cmd.ExecuteNonQuery();
                //mysqlbaglan.Close();
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

        //////////MUSTERİ TABLOSUNUN VERİLERİNİ GÖSTERDİK
        private void Button2_Click(object sender, EventArgs e)
        {
            //mysqlbaglan.Open();
            string sql1 = "SELECT * FROM musteri";
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

        private void Label5_Click(object sender, EventArgs e)
        {

        }

        //////////M_SİPARİS TABLOSUNA VERİ EKLEDİK
        private void Button3_Click(object sender, EventArgs e)
        {
            if (textBox5.Text != "" || textBox6.Text != "" || textBox7.Text != "" || textBox8.Text != ""|| textBox11.Text!="")
            {
                // mysqlbaglan.Open();
                cmd.Connection = mysqlbaglan;
                cmd.CommandText = "Insert into m_siparis(m_id, siparis, miktar, kar) Values('" + textBox5.Text + "','" + textBox6.Text + "','" + textBox7.Text + "','" + textBox8.Text + "')";

                string siparis = "", urunid = "";
                int miktar = Convert.ToInt32(textBox7.Text), kid = 0, sayac_2 = 0;
                double stok = 0, maliyet = 0, toplam = 0, fiyat = 0, tedarik = 0, element = 0;
                double oran = 0;
                double kar = Convert.ToDouble(textBox8.Text.ToString());
                string tarih= textBox11.Text;
                kar = kar / 100;


                cmd.ExecuteNonQuery();
                // mysqlbaglan.Close();
                /* for (int i = 0; i < this.Controls.Count; i++)
                 {
                     if (Controls[i] is TextBox) Controls[i].Text = "";
                 }*/

                MySqlCommand cmd1 = new MySqlCommand("Select * From u_kimyasal,u_bilesen Where kimyasal_urun= @deger", mysqlbaglan);
                cmd1.Parameters.AddWithValue("@deger", textBox6.Text);//&& b_id=1
                MySqlDataReader dr = cmd1.ExecuteReader();


                while (dr.Read())
                {
                    kid = Convert.ToInt32(dr["k_id"].ToString());
                    siparis = dr["kimyasal_urun"].ToString();
                    stok = Convert.ToDouble(dr["stok"].ToString());
                    maliyet = Convert.ToDouble(dr["maliyet"].ToString());
                    element = Convert.ToDouble(dr["element"].ToString());
                }
                dr.Close();
                cmd1.Dispose();

                MySqlCommand cmd2 = new MySqlCommand("Select * From k_urun, k_fiyat Where urun_adi = @deger2", mysqlbaglan);
                cmd2.Parameters.AddWithValue("@deger2", textBox6.Text);
                MySqlDataReader dr2 = cmd2.ExecuteReader();
                while (dr2.Read())
                {
                    urunid = dr2["urun_id"].ToString();
                    toplam = Convert.ToDouble(dr2["toplam_maliyet"].ToString());
                    fiyat = Convert.ToDouble(dr2["satis_fiyati"].ToString());

                }
                dr2.Close();
                cmd2.Dispose();

                //bileşen sayısı
                MySqlCommand cmd5 = new MySqlCommand("", mysqlbaglan);
                cmd5.CommandText = "Select Count(b_id) From u_bilesen";
                //Int64 hammadde = (Int64)cmd5.ExecuteScalar(); ;
                //MySqlDataReader dr5 = cmd5.ExecuteReader();
                /*while (dr5.Read())
                {
                    hammadde = Convert.ToInt32(dr5["Count b_id"].ToString());
                }
                dr5.Close();*/
                int hammadde = 3;
                cmd5.Dispose();
                int[] bid = new int[hammadde];
                int[] mol = new int[hammadde];

                MySqlCommand cmd6 = new MySqlCommand("", mysqlbaglan);
                cmd6.CommandText = "Select Count(f_id) From t_firma ";
                //Int64 firma = (Int64)cmd6.ExecuteScalar(); ;
                /*MySqlDataReader dr6 = cmd6.ExecuteReader();
                while (dr6.Read())
                {
                    firma = Convert.ToInt32(dr6["Count (f_id)"].ToString());
                }
                dr6.Close();*/
                cmd6.Dispose();
                int firma = 10;
                int[] fid = new int[firma];
                int[] t_stok = new int[firma];
                int[] t_sfiyat = new int[firma];
                string[] t_ulke = new string[firma];
                int[] uzaklik = new int[firma];
                int[] carpan = new int[firma];

                int[] fid2 = new int[firma];
                int[] t_stok2 = new int[firma];
                int[] t_sfiyat2 = new int[firma];
                string[] t_ulke2 = new string[firma];
                double[] uzaklik2 = new double[firma];
                double[] carpan2 = new double[firma];

                if (stok >= miktar)
                {
                    oran = (stok - miktar) / stok;
                    stok = stok * oran;
                    maliyet = maliyet * oran;
                    //textBox5.Text = oran.ToString();
                    /*toplam = toplam * oran;
                    fiyat = fiyat * oran;*/


                }
                else if (miktar > stok)
                {
                    tedarik = miktar - stok;//miktar=50 stok=30 tedarik=20
                    double tedarix = tedarik;
                    if (element == 2)
                    {
                        
                        int sayac = 0, sayac3 = 0, toplamstoksayisi1 = 0;
                        for (int i = 0; i <= hammadde; i++)
                        {
                            MySqlCommand komut = new MySqlCommand("Select * From u_formul Where k_id='" + @kid + "'&& b_id='" + @i + "'", mysqlbaglan);
                            MySqlDataReader veri = komut.ExecuteReader();
                            while (veri.Read())
                            {
                                bid[sayac] = Convert.ToInt32(veri["b_id"].ToString());
                                mol[sayac] = Convert.ToInt32(veri["bilesen"].ToString());
                                sayac++;
                            }
                            veri.Close();
                            komut.Dispose();
                        }
                        double[] result = new double[2];
                        double son_fiyat = 0;
                        for (int k = 0; k < 2; k++)
                        {
                            tedarik = tedarix;
                            //tedarikçi bilgilerini çekme işlemi
                            sayac_2 = 0;
                            for (int i = 1; i < firma + 1; i++)
                            {
                                MySqlCommand komut1 = new MySqlCommand("Select * From tedarikci, t_firma Where h_id='" + bid[k] + "'&& fi_id='" + @i + "'&&f_id='" + @i + "'", mysqlbaglan);
                                MySqlDataReader veri1 = komut1.ExecuteReader();
                                while (veri1.Read())
                                {
                                    fid[i - 1] = Convert.ToInt32(veri1["fi_id"].ToString());
                                    t_stok[i - 1] = Convert.ToInt32(veri1["miktar"].ToString());
                                    t_sfiyat[i - 1] = Convert.ToInt32(veri1["satis_fiyati"].ToString());
                                    uzaklik[i - 1] = Convert.ToInt32(veri1["uzaklik"].ToString());
                                    t_ulke[i - 1] = veri1["ulke"].ToString();
                                    sayac_2++;
                                }
                                veri1.Close();
                                komut1.Dispose();
                            }

                            double[] sonuc = new double[firma];
                            int yli = 0, ysiz = 0;
                            double tedarik1 = tedarik;

                            for (int i = 0; i < firma; i++)
                            {
                                if (t_ulke[i] != "" && t_ulke[i] == "Turkiye")
                                {
                                    carpan[i] = 2;
                                }
                                else if (t_ulke[i] != "" && t_ulke[i] != "Turkiye")
                                {
                                    carpan[i] = 1;
                                }
                            }
                            for (int i = 0; i < firma; i++)
                            {
                                if (t_stok[i] != 0 && tedarik * mol[k] <= t_stok[i])
                                {
                                    sonuc[i] = tedarik * mol[k] * t_sfiyat[i] + uzaklik[i] / carpan[i];
                                    yli++;
                                }
                                else if (t_stok[i] != 0 && tedarik * mol[k] > t_stok[i])
                                {
                                    sonuc[i] = t_stok[i] * t_sfiyat[i] + uzaklik[i] / carpan[i];
                                    //tedarik1 -= t_stok[i];//100-5=95
                                    ysiz++;
                                }
                            }
                            double[] yeterli = new double[yli];
                            double[] yetersiz = new double[ysiz];
                            int[] ysizstok = new int[ysiz];
                            int[] ylistok = new int[yli];
                            int a = 0, b = 0;
                            for (int i = 0; i < firma; i++)
                            {
                                if (t_stok[i] != 0 && tedarik * mol[k] <= t_stok[i])
                                {
                                    yeterli[a] = sonuc[i];
                                    ylistok[a] = t_stok[i];
                                    a++;
                                }
                                else if (t_stok[i] != 0 && tedarik * mol[k] > t_stok[i])
                                {
                                    yetersiz[b] = sonuc[i];
                                    ysizstok[b] = t_stok[i];
                                    b++;
                                }
                            }
                            //minliye göre yeterli stoklu olanların en küçüğü
                            double minli = yeterli[0];
                            int yeterli_en_kucuk = 0;
                            for (int i = 0; i < yli; i++)
                            {
                                if (yeterli[i] < minli)
                                {
                                    minli = yeterli[i];
                                    yeterli_en_kucuk = i;
                                }
                            }

                            double[] yli_birfi = new double[yli];
                            for (int j = 0; j < yli; j++)
                            {
                                yli_birfi[j] = yeterli[j] / tedarik;
                            }
                            double yli_minbirfi = yli_birfi[0];
                            int yli_en_kucuk_birfi = 0;
                            for (int j = 0; j < yli; j++)
                            {
                                if (yli_birfi[j] != 0 && yli_birfi[j] < yli_minbirfi)
                                {
                                    yli_minbirfi = yli_birfi[j];
                                    yli_en_kucuk_birfi = j;
                                }
                            }
                            //sonuç dizisine göre en küçük stok sayısına sahip tedarikçi maliyeti
                            for (int j = 0; j < firma; j++)
                            {
                                if (sonuc[j] == yeterli[yli_en_kucuk_birfi])
                                {
                                    yeterli_en_kucuk = j;
                                }
                            }

                            //minsize göre yetersiz stoklu olanların en küçüğü
                            double minsiz = yetersiz[0];
                            int yetersiz_en_kucuk = 0;
                            for (int i = 0; i < ysiz; i++)
                            {
                                if (yetersiz[i] < minsiz)
                                {
                                    minsiz = yetersiz[i];
                                    yetersiz_en_kucuk = i;
                                }
                            }


                            double[] ysiz_birfi = new double[ysiz];

                            for (int j = 0; j < ysiz; j++)
                            {
                                ysiz_birfi[j] = yetersiz[j] / ysizstok[j];
                            }
                            double ysiz_minbirfi = ysiz_birfi[0];
                            int en_kucuk_birfi = 0;
                            for (int j = 0; j < ysiz; j++)
                            {
                                if (ysiz_birfi[j] != 0 && ysiz_birfi[j] < ysiz_minbirfi)
                                {
                                    ysiz_minbirfi = ysiz_birfi[j];
                                    en_kucuk_birfi = j;
                                }
                            }
                            //sonuc dizisine göre en küçük tedarik maliyeti indisi
                            for (int j = 0; j < firma; j++)
                            {
                                if (sonuc[j] == yetersiz[en_kucuk_birfi])
                                {
                                    yetersiz_en_kucuk = j;
                                }
                            }

                            
                            double yenimiktar = 0;
                            int c = 0;
                            int[] ysizfirma = new int[ysiz];
                            int[] ylifirma = new int[yli];
                            int firma_id = 0;


                            if (yli_minbirfi <= ysiz_minbirfi)
                            {
                                result[k] = sonuc[yeterli_en_kucuk];
                                
                                //fid[yeterli_en_kucuk];
                                yenimiktar = t_stok[yeterli_en_kucuk] - tedarik * mol[k];
                                firma_id = fid[yeterli_en_kucuk];
                                mysqlbaglan.Close();
                                String guncelle5 = "Update tedarikci Set miktar='" + @yenimiktar + "' Where h_id='" + @bid[k] + "'&&fi_id='" + @firma_id + "'";
                                MySqlCommand komut5 = new MySqlCommand(guncelle5, mysqlbaglan);
                                MySqlDataReader MyReader5;
                                mysqlbaglan.Open();
                                MyReader5 = komut5.ExecuteReader();
                                //MessageBox.Show("değişim oldu!!!");
                                komut5.Dispose();
                                
                            }
                            else if (yli_minbirfi > ysiz_minbirfi)
                            {
                                tedarik = tedarix;
                                sonuc[yetersiz_en_kucuk] = t_stok[yetersiz_en_kucuk] * t_sfiyat[yetersiz_en_kucuk] + uzaklik[yetersiz_en_kucuk] / carpan[yetersiz_en_kucuk];

                                yenimiktar = 0;
                                firma_id = fid[yetersiz_en_kucuk];
                                mysqlbaglan.Close();
                                String guncelle5 = "Update tedarikci Set miktar='" + @yenimiktar + "' Where h_id='" + @bid[k] + "'&&fi_id='" + @firma_id + "'";
                                MySqlCommand komut5 = new MySqlCommand(guncelle5, mysqlbaglan);
                                MySqlDataReader MyReader5;
                                mysqlbaglan.Open();
                                MyReader5 = komut5.ExecuteReader();
                                //MessageBox.Show("değişim oldu!!!");
                                komut5.Dispose();

                                tedarik = tedarik * mol[k] - t_stok[yetersiz_en_kucuk];
                                sonuc[yeterli_en_kucuk] = tedarik * t_sfiyat[yeterli_en_kucuk] + uzaklik[yeterli_en_kucuk] / carpan[yeterli_en_kucuk];
                                result[k] = sonuc[yetersiz_en_kucuk] + sonuc[yeterli_en_kucuk];

                                yenimiktar = t_stok[yeterli_en_kucuk] - tedarik ;
                                firma_id = fid[yeterli_en_kucuk];
                                mysqlbaglan.Close();
                                String guncelle6 = "Update tedarikci Set miktar='" + @yenimiktar + "' Where h_id='" + @bid[k] + "'&&fi_id='" + @firma_id + "'";
                                MySqlCommand komut6 = new MySqlCommand(guncelle6, mysqlbaglan);
                                MySqlDataReader MyReader6;
                                mysqlbaglan.Open();
                                MyReader6 = komut6.ExecuteReader();
                                //MessageBox.Show("değişim oldu!!!");
                                komut6.Dispose();
                            }
                        }
                        //yeterli olan indisteki firmaya ulaş ve stok bilgisini güncelle
                        double sonmaliyet = result[0] + result[1]+tedarix;
                        son_fiyat = sonmaliyet + kar * sonmaliyet;
                        MySqlCommand ekle1 = new MySqlCommand();
                        ekle1.Connection = mysqlbaglan;
                        ekle1.CommandText = "Insert into k_fiyat(uretim_tarihi, urun_id, toplam_maliyet, satis_fiyati) Values('" + @tarih + "','" + @urunid + "','" + @sonmaliyet+ "','" + @son_fiyat + "')";
                        ekle1.ExecuteNonQuery();
                        mysqlbaglan.Close();
                        stok = 0;
                        maliyet = 0;
                        toplam = result[0]+ result[1]+tedarik;
                        fiyat = toplam * kar;


                    }
                    //Element sayısı 3 ise
                    else if (element == 3)
                    {
                        int sayac = 0;
                        for (int i = 0; i <= hammadde; i++)
                        {
                            MySqlCommand komut = new MySqlCommand("Select * From u_formul Where k_id='" + @kid + "'&& b_id='" + @i + "'", mysqlbaglan);
                            MySqlDataReader veri = komut.ExecuteReader();
                            while (veri.Read())
                            {
                                bid[sayac] = Convert.ToInt32(veri["b_id"].ToString());
                                mol[sayac] = Convert.ToInt32(veri["bilesen"].ToString());
                                sayac++;
                            }
                            veri.Close();
                            komut.Dispose();
                        }
                        double[] result = new double[3];
                        double son_fiyat = 0;
                        for (int k = 0; k < 3; k++)
                        {
                            tedarik = tedarix;
                            //tedarikçi bilgilerini çekme işlemi
                            sayac_2 = 0;
                            for (int i = 1; i < firma + 1; i++)
                            {
                                MySqlCommand komut1 = new MySqlCommand("Select * From tedarikci, t_firma Where h_id='" + bid[k] + "'&& fi_id='" + @i + "'&&f_id='" + @i + "'", mysqlbaglan);
                                MySqlDataReader veri1 = komut1.ExecuteReader();
                                while (veri1.Read())
                                {
                                    fid[i - 1] = Convert.ToInt32(veri1["fi_id"].ToString());
                                    t_stok[i - 1] = Convert.ToInt32(veri1["miktar"].ToString());
                                    t_sfiyat[i - 1] = Convert.ToInt32(veri1["satis_fiyati"].ToString());
                                    uzaklik[i - 1] = Convert.ToInt32(veri1["uzaklik"].ToString());
                                    t_ulke[i - 1] = veri1["ulke"].ToString();
                                    sayac_2++;
                                }
                                veri1.Close();
                                komut1.Dispose();
                            }

                            double[] sonuc = new double[firma];
                            int yli = 0, ysiz = 0;
                            double tedarik1 = tedarik;

                            for (int i = 0; i < firma; i++)
                            {
                                if (t_ulke[i] != "" && t_ulke[i] == "Turkiye")
                                {
                                    carpan[i] = 2;
                                }
                                else if (t_ulke[i] != "" && t_ulke[i] != "Turkiye")
                                {
                                    carpan[i] = 1;
                                }
                            }
                            for (int i = 0; i < firma; i++)
                            {
                                if (t_stok[i] != 0 && tedarik * mol[k] <= t_stok[i])
                                {
                                    sonuc[i] = tedarik * mol[k] * t_sfiyat[i] + uzaklik[i] / carpan[i];
                                    yli++;
                                }
                                else if (t_stok[i] != 0 && tedarik * mol[k] > t_stok[i])
                                {
                                    sonuc[i] = t_stok[i] * t_sfiyat[i] + uzaklik[i] / carpan[i];
                                    //tedarik1 -= t_stok[i];//100-5=95
                                    ysiz++;
                                }
                            }
                            double[] yeterli = new double[yli];
                            double[] yetersiz = new double[ysiz];
                            int[] ysizstok = new int[ysiz];
                            int[] ylistok = new int[yli];
                            int a = 0, b = 0;
                            for (int i = 0; i < firma; i++)
                            {
                                if (t_stok[i] != 0 && tedarik * mol[k] <= t_stok[i])
                                {
                                    yeterli[a] = sonuc[i];
                                    ylistok[a] = t_stok[i];
                                    a++;
                                }
                                else if (t_stok[i] != 0 && tedarik * mol[k] > t_stok[i])
                                {
                                    yetersiz[b] = sonuc[i];
                                    ysizstok[b] = t_stok[i];
                                    b++;
                                }
                            }
                            //minliye göre yeterli stoklu olanların en küçüğü
                            double minli = yeterli[0];
                            int yeterli_en_kucuk = 0;
                            for (int i = 0; i < yli; i++)
                            {
                                if (yeterli[i] < minli)
                                {
                                    minli = yeterli[i];
                                    yeterli_en_kucuk = i;
                                }
                            }

                            double[] yli_birfi = new double[yli];
                            for (int j = 0; j < yli; j++)
                            {
                                yli_birfi[j] = yeterli[j] / tedarik;
                            }
                            double yli_minbirfi = yli_birfi[0];
                            int yli_en_kucuk_birfi = 0;
                            for (int j = 0; j < yli; j++)
                            {
                                if (yli_birfi[j] != 0 && yli_birfi[j] < yli_minbirfi)
                                {
                                    yli_minbirfi = yli_birfi[j];
                                    yli_en_kucuk_birfi = j;
                                }
                            }
                            //sonuç dizisine göre en küçük stok sayısına sahip tedarikçi maliyeti
                            for (int j = 0; j < firma; j++)
                            {
                                if (sonuc[j] == yeterli[yli_en_kucuk_birfi])
                                {
                                    yeterli_en_kucuk = j;
                                }
                            }

                            //minsize göre yetersiz stoklu olanların en küçüğü
                            double minsiz = yetersiz[0];
                            int yetersiz_en_kucuk = 0;
                            for (int i = 0; i < ysiz; i++)
                            {
                                if (yetersiz[i] < minsiz)
                                {
                                    minsiz = yetersiz[i];
                                    yetersiz_en_kucuk = i;
                                }
                            }


                            double[] ysiz_birfi = new double[ysiz];

                            for (int j = 0; j < ysiz; j++)
                            {
                                ysiz_birfi[j] = yetersiz[j] / ysizstok[j];
                            }
                            double ysiz_minbirfi = ysiz_birfi[0];
                            int en_kucuk_birfi = 0;
                            for (int j = 0; j < ysiz; j++)
                            {
                                if (ysiz_birfi[j] != 0 && ysiz_birfi[j] < ysiz_minbirfi)
                                {
                                    ysiz_minbirfi = ysiz_birfi[j];
                                    en_kucuk_birfi = j;
                                }
                            }
                            //sonuc dizisine göre en küçük tedarik maliyeti indisi
                            for (int j = 0; j < firma; j++)
                            {
                                if (sonuc[j] == yetersiz[en_kucuk_birfi])
                                {
                                    yetersiz_en_kucuk = j;
                                }
                            }


                            double yenimiktar = 0;
                            int c = 0;
                            int[] ysizfirma = new int[ysiz];
                            int[] ylifirma = new int[yli];
                            int firma_id = 0;


                            if (yli_minbirfi <= ysiz_minbirfi)
                            {
                                result[k] = sonuc[yeterli_en_kucuk];

                                //fid[yeterli_en_kucuk];
                                yenimiktar = t_stok[yeterli_en_kucuk] - tedarik * mol[k];
                                firma_id = fid[yeterli_en_kucuk];
                                mysqlbaglan.Close();
                                String guncelle5 = "Update tedarikci Set miktar='" + @yenimiktar + "' Where h_id='" + @bid[k] + "'&&fi_id='" + @firma_id + "'";
                                MySqlCommand komut5 = new MySqlCommand(guncelle5, mysqlbaglan);
                                MySqlDataReader MyReader5;
                                mysqlbaglan.Open();
                                MyReader5 = komut5.ExecuteReader();
                                //MessageBox.Show("değişim oldu!!!");
                                komut5.Dispose();

                            }
                            else
                            {
                                tedarik = tedarix;
                                sonuc[yetersiz_en_kucuk] = t_stok[yetersiz_en_kucuk] * t_sfiyat[yetersiz_en_kucuk] + uzaklik[yetersiz_en_kucuk] / carpan[yetersiz_en_kucuk];

                                yenimiktar = 0;
                                firma_id = fid[yetersiz_en_kucuk];
                                mysqlbaglan.Close();
                                String guncelle5 = "Update tedarikci Set miktar='" + @yenimiktar + "' Where h_id='" + @bid[k] + "'&&fi_id='" + @firma_id + "'";
                                MySqlCommand komut5 = new MySqlCommand(guncelle5, mysqlbaglan);
                                MySqlDataReader MyReader5;
                                mysqlbaglan.Open();
                                MyReader5 = komut5.ExecuteReader();
                                //MessageBox.Show("değişim oldu!!!");
                                komut5.Dispose();

                                tedarik = tedarik * mol[k] - t_stok[yetersiz_en_kucuk];
                                sonuc[yeterli_en_kucuk] = tedarik * t_sfiyat[yeterli_en_kucuk] + uzaklik[yeterli_en_kucuk] / carpan[yeterli_en_kucuk];
                                result[k] = sonuc[yetersiz_en_kucuk] + sonuc[yeterli_en_kucuk];

                                yenimiktar = t_stok[yeterli_en_kucuk] - tedarik;
                                firma_id = fid[yeterli_en_kucuk];
                                mysqlbaglan.Close();
                                String guncelle6 = "Update tedarikci Set miktar='" + @yenimiktar + "' Where h_id='" + @bid[k] + "'&&fi_id='" + @firma_id + "'";
                                MySqlCommand komut6 = new MySqlCommand(guncelle6, mysqlbaglan);
                                MySqlDataReader MyReader6;
                                mysqlbaglan.Open();
                                MyReader6 = komut6.ExecuteReader();
                                //MessageBox.Show("değişim oldu!!!");
                                komut6.Dispose();

                            }
                        }
                        //yeterli olan indisteki firmaya ulaş ve stok bilgisini güncelle
                        double sonmaliyet = result[0] + result[1] + result[2] + tedarix;
                        son_fiyat = sonmaliyet + kar * sonmaliyet;
                        MySqlCommand ekle1 = new MySqlCommand();
                        ekle1.Connection = mysqlbaglan;
                        ekle1.CommandText = "Insert into k_fiyat(uretim_tarihi, urun_id, toplam_maliyet, satis_fiyati) Values('" + @tarih + "','" + @urunid + "','" + @sonmaliyet + "','" + @son_fiyat + "')";
                        ekle1.ExecuteNonQuery();
                        mysqlbaglan.Close();
                        stok = 0;
                        maliyet = 0;
                    }
                }

                mysqlbaglan.Close();
                String guncelle = "Update u_kimyasal Set stok='" + @stok + "', maliyet='" + @maliyet + "' Where kimyasal_urun= @deger";
                MySqlCommand cmd3 = new MySqlCommand(guncelle, mysqlbaglan);
                cmd3.Parameters.AddWithValue("@deger", textBox6.Text);
                MySqlDataReader MyReader2;
                mysqlbaglan.Open();
                MyReader2 = cmd3.ExecuteReader();
                MessageBox.Show("Data Updated");
                cmd3.Dispose();


                /*mysqlbaglan.Close();
                String guncelle1 = "Update k_fiyat Set toplam_maliyet='" + @toplam + "', satis_fiyati='" + @fiyat + "' Where urun_id= '"+@urunid+"'";
                MySqlCommand cmd4 = new MySqlCommand(guncelle1, mysqlbaglan);
                //cmd4.Parameters.AddWithValue("@deger", textBox6.Text);
                MySqlDataReader MyReader3;
                mysqlbaglan.Open();
                MyReader3 = cmd4.ExecuteReader();
                MessageBox.Show("Data Updated");
                cmd4.Dispose();*/
            }

            else
            {
                MessageBox.Show("Lutfen bilgileri eksiksiz giriniz");
            }




        }

        //////////M_SİPARİS TABLOSUNUN VERİLERİNİ GÖSTERDİK
        private void Button4_Click(object sender, EventArgs e)
        {
            string sql1 = "SELECT * FROM m_siparis";
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

        ///////////////MUSTERİ TABLOSUNDAN İD SEÇİP SİLMEK
        private void Button5_Click(object sender, EventArgs e)
        {
            cmd.Connection = mysqlbaglan;
            cmd.CommandText = "Delete From musteri Where m_id='" + dataGridView1.CurrentRow.Cells[0].Value.ToString() + "'";
            cmd.ExecuteNonQuery();
            //mysqlbaglan.Close();
        }

        ///////////////M_SİPARİS TABLOSUNDAN İD SEÇİP SİLMEK
        private void Button6_Click(object sender, EventArgs e)
        {
            cmd.Connection = mysqlbaglan;
            cmd.CommandText = "Delete From m_siparis Where m_id='" + dataGridView2.CurrentRow.Cells[0].Value.ToString() + "' And siparis='" + dataGridView2.CurrentRow.Cells[1].Value.ToString() + "'";
            cmd.ExecuteNonQuery();
        }
    }
}
