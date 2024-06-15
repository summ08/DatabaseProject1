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
    public partial class Iptaller : Form
    {
        public Iptaller()
        {
            InitializeComponent();
        }
        NpgsqlConnection baglanti = new(" server=PostgreSQL 13; port=5432 ; Database = Postgre ;username = postgres ;password = 123456 ");

        private void fillTicketId()
        {
            baglanti.Open();
            NpgsqlCommand cmd = new NpgsqlCommand("select Fcode from TicketTbl", baglanti);

            NpgsqlDataReader rdr = cmd.ExecuteReader();
            DataTable dt = new DataTable();
            dt.Columns.Add("TId", typeof(int));
            dt.Load(rdr);
            TIdCb.ValueMember = "TId";
            TIdCb.DataSource = dt;
            baglanti.Close();

        }

        private void getirfcode()
        {
            baglanti.Open();
            string sorgu = "select* from TicketTbl where TIdId=" + TIdCb.SelectedValue.ToString() + "";
            NpgsqlCommand cmd = new NpgsqlCommand(sorgu, baglanti);
            DataTable dt = new DataTable();
            NpgsqlDataAdapter da = new NpgsqlDataAdapter();
            da.Fill(dt);
            foreach (DataRow dr in dt.Rows)
            {
                FcodeTb.Text = dr["Fcode"].ToString();


                //  page = Convert.ToInt32(dr["PassNat"].ToString());

            }
            baglanti.Close();
        }


        private void populate()
        {
            baglanti.Open();
            string sorgu = "select * from  IptalTbl ";
            NpgsqlDataAdapter da = new NpgsqlDataAdapter(sorgu, baglanti);
            DataSet ds = new DataSet();
            da.Fill(ds);
            IptalDGV.DataSource = ds.Tables[0];
            baglanti.Close();

        }

        private void Iptaller_Load(object sender, EventArgs e)
        {
            fillTicketId();
            populate();
        }

        private void TIdCb_SelectedIndexChanged(object sender, EventArgs e)
        {
            getirfcode();
        }

        private void button2_Click(object sender, EventArgs e)
        {
           AnaSayfa home= new AnaSayfa();
            home.Show();
            this.Hide();
        }

        private void silBilet()
        {
                try
                {
                    baglanti.Open();
                    string sorgu = "delete from TicketTbl where TId='" + TIdCb.SelectedValue.ToString()  + ";";
                    NpgsqlDataAdapter cmd = new NpgsqlDataAdapter(sorgu, baglanti);
                    cmd.ExecuteNonQuery();
                    MessageBox.Show("Uçuş Silindi!");
                    baglanti.Close();
                    populate();

                }
                catch (Exception ex)
                { MessageBox.Show(ex.Message); }
            

        }

        private void button1_Click(object sender, EventArgs e)
        {
            if (CanId.Text == "" || FcodeTb.Text == "")
            {
                MessageBox.Show("Bilgileri Doldurun!");
            }
            else
            {
                try
                {

                    baglanti.Open();
                    string sorgu = "insert into IptalTbl values(" + CanId.Text + "," + TIdCb.SelectedValue.ToString() + ",'" + FcodeTb.Text + ",'" + CancDate.Value.Date  + "')";
                    NpgsqlDataAdapter cmd = new NpgsqlDataAdapter(sorgu, baglanti);
                    cmd.ExecuteNonQuery();
                    MessageBox.Show("Bilet Rezerve Edildi!");
                    baglanti.Close();
                   populate();
                    silBilet();
                }
                catch (Exception ex)
                {
                    MessageBox.Show(ex.Message);
                }


            }
        }

        private void button3_Click(object sender, EventArgs e)
        {

            CanId.Text = "";
            FcodeTb.Text = "";
        }

        private void label10_Click(object sender, EventArgs e)
        {
            Application.Exit();
        }
    }
}
