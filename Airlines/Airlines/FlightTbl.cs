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
    public partial class FlightTbl : Form
    {
        public FlightTbl()
        {
            InitializeComponent();
        }
        NpgsqlConnection baglanti = new(" server=PostgreSQL 13; port=5432 ; Database = Postgre ;username = postgres ;password = 123456 ");

        private void label3_Click(object sender, EventArgs e)
        {

        }

        private void FlightTbl_Load(object sender, EventArgs e)
        {

        }

        private void label5_Click(object sender, EventArgs e)
        {

        }

        private void comboBox1_SelectedIndexChanged(object sender, EventArgs e)
        {

        }

        private void button1_Click(object sender, EventArgs e)
        {
            if (FcodeTb.Text == "" || FNere.Text == "" || FNerd.Text == "" || koltuknum.Text == ""|| FDate.Text=="")
            {
                MessageBox.Show("Bilgileri Doldurun!");
            }
            else
            {
                try
                {

                    baglanti.Open();
                    string sorgu = "insert into FlightTbl values(" + FcodeTb.Text + "','" + FNerd.SelectedItem.ToString() + "','" + FNere.SelectedItem.ToString() + "','" + FDate.Value.ToString() + "','" + koltuknum.ToString()+ ")";
                    NpgsqlDataAdapter cmd = new NpgsqlDataAdapter(sorgu, baglanti);
                    cmd.ExecuteNonQuery();
                    MessageBox.Show("Uçuş Kaydedildi!");
                    baglanti.Close();
                }
                catch (Exception ex)
                {
                    MessageBox.Show(ex.Message);
                }


            }
        }

        private void button2_Click(object sender, EventArgs e)
        {
            FcodeTb.Text = "";
            koltuknum.Text = "";
        }

        private void button3_Click(object sender, EventArgs e)
        {
            UcusGor ucusgor = new UcusGor();
            ucusgor.Show();
            this.Hide();

        }

        private void button4_Click(object sender, EventArgs e)
        {
            AnaSayfa home = new AnaSayfa();
            home.Show();
            this.Hide();
        }
    }
}
