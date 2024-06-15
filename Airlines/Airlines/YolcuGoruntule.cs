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
    public partial class YolcuGoruntule : Form
    {
        public YolcuGoruntule()
        {
            InitializeComponent();
        }

        NpgsqlConnection baglanti = new(" server=PostgreSQL 13; port=5432 ; Database = Postgre ;username = postgres ;password = 123456 ");

        private void populate()
        {
           baglanti.Open();
            string sorgu = "select * from  PassengerTbl ";
            NpgsqlDataAdapter da = new NpgsqlDataAdapter(sorgu, baglanti);
            DataSet ds = new DataSet();
            da.Fill(ds);
            PassDGV.DataSource = ds.Tables[0];
            baglanti.Close();

        }
        private void YolcuGoruntule_Load(object sender, EventArgs e)
        {
            populate();
        }
        private void button4_Click (object sender, EventArgs e)
        {
            YolcuEkle yolcuekle=new YolcuEkle();     
            yolcuekle.Show();
            this.Hide();
        }
        private void button2_Click(object sender, EventArgs e)
        {
            if (PidTb.Text == "")
            {
                MessageBox.Show("Silinecek Yolcu Bilgilerini Giriniz! ");
            }
            else
            {
                try
                {
                    baglanti.Open();
                    string sorgu = "delete from PassengerTbl where PassId=" + PidTb.Text + ";";
                    NpgsqlDataAdapter cmd = new NpgsqlDataAdapter(sorgu, baglanti);
                    cmd.ExecuteNonQuery();
                    MessageBox.Show("Yolcu Silindi!");
                    baglanti.Close();
                    populate();

                }
                catch (Exception ex)
                { MessageBox.Show(ex.Message); }
            }
        }

        private void PassDGV_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {
            PidTb.Text = PassDGV.SelectedRows[0].Cells[0].Value.ToString();
            PnameTb.Text = PassDGV.SelectedRows[0].Cells[1].Value.ToString();
            PpassTb.Text = PassDGV.SelectedRows[0].Cells[2].Value.ToString();
            PaddTb.Text = PassDGV.SelectedRows[0].Cells[3].Value.ToString();
            natcb.SelectedItem = PassDGV.SelectedRows[0].Cells[4].Value.ToString();
            GendCb.SelectedItem = PassDGV.SelectedRows[0].Cells[5].Value.ToString();
        }

        private void button3_Click(object sender, EventArgs e)
        {
            PidTb.Text = "";
            PnameTb.Text = "";
            PpassTb.Text = "";
            PaddTb.Text = "";
            natcb.SelectedItem = "";
            GendCb.SelectedItem = "";

        }

        private void button1_Click(object sender, EventArgs e)
        {
            if (PidTb.Text == "" || PaddTb.Text == "" || PnameTb.Text == "" || PpassTb.Text == "")
            {

                MessageBox.Show("Bilgileri Doldurun!");
            }
            else
            {
                try
                {
                    
                    baglanti.Open();
                    string sorgu = "update PassengerTbl set PassName='" + PnameTb.Text + "',Passport='" + PpassTb.Text + "',PassAd'" + PaddTb.Text + "',PassNat='" + natcb.SelectedItem.ToString() + "',PassNat='" + GendCb.SelectedItem.ToString() + "',PassPhone='" + GSMCb.Text + "' where PassId=" + PidTb.Text + ";)";
                    NpgsqlDataAdapter cmd = new NpgsqlDataAdapter(sorgu, baglanti);
                    cmd.ExecuteNonQuery();
                    MessageBox.Show("Yolcu Kaydedildi!");
                    baglanti.Close();
                    populate();
                }
                catch (Exception ex)
                {
                    MessageBox.Show(ex.Message);
                }
            }
        }

        private void button4_Click_1(object sender, EventArgs e)
        {

            AnaSayfa home = new AnaSayfa();
            home.Show();
            this.Hide();
        }
    }
}
