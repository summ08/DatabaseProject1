using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using Npgsql;

namespace Airlines
{
    public partial class YolcuEkle : Form
    {
        public YolcuEkle()
        {
            InitializeComponent();
        }

        NpgsqlConnection baglanti = new(" server=PostgreSQL 13; port=5432 ; Database = Postgre ;username = postgres ;password = 123456 ");

        private void label10_Click(object sender, EventArgs e)
        {
            Application.Exit();
        }

        private void YolcuEkle_Load(object sender, EventArgs e)
        {

        }

        private void button5_Click(object sender, EventArgs e)
        {
            YolcuGoruntule yolcugor = new YolcuGoruntule();
            yolcugor.Show();
            this.Hide();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            if (PassId.Text == "" || PassAd.Text == "" || PassportTb.Text == "" || GSMCb.Text == "")
            {
                MessageBox.Show("Bilgileri Doldurun!");
            }
            else
            {
                try
                {

                    baglanti.Open();
                    string sorgu = "insert into PassengerTbl values(" + PassId.Text + "','" + PassAd.Text + "','" + PassAdr.Text + "','" + PassportTb.Text + "','" + UyrukCb.SelectedItem.ToString() + "','" + GenderCb.SelectedItem.ToString() + "','" + GSMCb.Text + "')";
                    NpgsqlDataAdapter cmd = new NpgsqlDataAdapter(sorgu, baglanti);
                    cmd.ExecuteNonQuery();
                    MessageBox.Show("Yolcu Kaydedildi!");
                    baglanti.Close();
                }
                catch(Exception ex)
                {
                    MessageBox.Show(ex.Message);
                }

                
            }
        }

        private void button4_Click(object sender, EventArgs e)
        {
            AnaSayfa home = new AnaSayfa(); 
            home.Show();
            this.Hide();
        }

        private void button3_Click(object sender, EventArgs e)
        {

            PassId.Text = "";
            PassportTb.Text = "";
            PassAd.Text = "";
            PassAdr.Text = "";
            UyrukCb.SelectedItem = "";
            GenderCb.SelectedItem = "";
            GSMCb.Text = "";

        }

    }
}
