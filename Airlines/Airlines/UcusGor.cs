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
    public partial class UcusGor : Form
    {
        public UcusGor()
        {
            InitializeComponent();
        }
        NpgsqlConnection baglanti = new(" server=PostgreSQL 13; port=5432 ; Database = Postgre ;username = postgres ;password = 123456 ");

        private void populate()
        {
            baglanti.Open();
            string sorgu = "select * from  FlightTbl ";
            NpgsqlDataAdapter da = new NpgsqlDataAdapter(sorgu, baglanti);
            DataSet ds = new DataSet();
            da.Fill(ds);
            FlightDGV.DataSource = ds.Tables[0];
            baglanti.Close();

        }
        private void UcusGor_Load(object sender, EventArgs e)
        {
            populate();
        }

        private void button4_Click(object sender, EventArgs e)
        {
            FlightTbl ucusekle = new FlightTbl();
            ucusekle.Show();
            this.Hide();
        }

        private void button3_Click(object sender, EventArgs e)
        {
            FcodeTb.Text = "";
            koltuknum.Text = "";

        }

        private void FlightDGV_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {
            FcodeTb.Text = FlightDGV.SelectedRows[0].Cells[0].Value.ToString();
            FNerdCb.SelectedItem = FlightDGV.SelectedRows[0].Cells[1].Value.ToString();
            FNereCb.SelectedItem = FlightDGV.SelectedRows[0].Cells[2].Value.ToString();
            koltuknum.Text = FlightDGV.SelectedRows[0].Cells[3].Value.ToString();
           
        }

        private void button2_Click(object sender, EventArgs e)
        {

            if (FcodeTb.Text == "")
            {
                MessageBox.Show("Silinecek Uçuş Bilgilerini Giriniz! ");
            }
            else
            {
                try
                {
                    baglanti.Open();
                    string sorgu = "delete from FlightTbl where Fcode='" + FcodeTb.Text + "';";
                    NpgsqlDataAdapter cmd = new NpgsqlDataAdapter(sorgu, baglanti);
                    cmd.ExecuteNonQuery();
                    MessageBox.Show("Uçuş Silindi!");
                    baglanti.Close();
                    populate();

                }
                catch (Exception ex)
                { MessageBox.Show(ex.Message); }
            }
        }

        private void button1_Click(object sender, EventArgs e)
        {

            if (FcodeTb.Text == "" || koltuknum.Text == "" )
            {

                MessageBox.Show("Bilgileri Doldurun!");
            }
            else
            {
                try
                {

                    baglanti.Open();
                    string sorgu = "update FlightTbl set Fcode='" + FcodeTb.Text + "',FNere='" + FNereCb.SelectedItem.ToString() + "',FNerd='" + FNerdCb.SelectedItem.ToString() + "',FDate='"+FDate.Value.Date.ToString() + "',FCap='"+koltuknum.Text+ "where Fcode='"+FcodeTb.Text+"'; ";
                    NpgsqlDataAdapter cmd = new NpgsqlDataAdapter(sorgu, baglanti);
                    cmd.ExecuteNonQuery();
                    MessageBox.Show("Uçuş Güncellendi!");
                    baglanti.Close();
                    populate();
                }
                catch (Exception ex)
                {
                    MessageBox.Show(ex.Message);
                }
            }
        }

        private void label5_Click(object sender, EventArgs e)
        {

        }
    }
    }


