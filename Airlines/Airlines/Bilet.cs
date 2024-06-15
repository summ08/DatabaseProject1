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
    public partial class Bilet : Form
    {
        public Bilet()
        {
            InitializeComponent();
        }


        NpgsqlConnection baglanti = new(" server=PostgreSQL 13; port=5432 ; Database = Postgre ;username = postgres ;password = 123456 ");


        private void populate()
        {
            baglanti.Open();
            string sorgu = "select * from  TicketTbl ";
            NpgsqlDataAdapter da = new NpgsqlDataAdapter(sorgu, baglanti);
            DataSet ds = new DataSet();
            da.Fill(ds);
            TicketDGV.DataSource = ds.Tables[0];
            baglanti.Close();

        }


        private void fillYolcu()
        {
            baglanti.Open();
            NpgsqlCommand cmd = new NpgsqlCommand("select PassId from PassengerTbl", baglanti);
         
            NpgsqlDataReader rdr = cmd.ExecuteReader();
            DataTable dt = new DataTable();
            dt.Columns.Add("PassId", typeof(int));
            dt.Load(rdr);
            PIdCb.ValueMember = "PassId";
            PIdCb.DataSource= dt;
            baglanti.Close();

        }


        private void fillucuskodu()
        {
            baglanti.Open();
            NpgsqlCommand cmd = new NpgsqlCommand("select Fcode from FlightTbl", baglanti);

            NpgsqlDataReader rdr = cmd.ExecuteReader();
            DataTable dt = new DataTable();
            dt.Columns.Add("Fcode", typeof(int));
            dt.Load(rdr);
            FCodeCb.ValueMember = "Fcode";
            FCodeCb.DataSource = dt;
            baglanti.Close();

        }

        private void label7_Click(object sender, EventArgs e)
        {

        }

        private void textBox5_TextChanged(object sender, EventArgs e)
        {

        }

        string pname, ppass, pnat;

        private void button3_Click(object sender, EventArgs e)
        {
            PNameTb.Text = "";
            PPassTb.Text = "";
            PNatTb.Text = "";
            PAmtTb.Text = "";
            TId.Text = "";

        }

        private void button2_Click(object sender, EventArgs e)
        {
            AnaSayfa home = new AnaSayfa();
            home.Show();
            this.Hide();
        }

        private void button1_Click(object sender, EventArgs e)
        {

            if (TId.Text == "" || PNameTb.Text == "" )
            {
                MessageBox.Show("Bilgileri Doldurun!");
            }
            else
            {
                try
                {

                    baglanti.Open();
                    string sorgu = "insert into TicketTbl values(" + TId.Text + "','" + FCodeCb.SelectedValue.ToString() + "'," + PIdCb.SelectedValue.ToString() + ",'" + PNameTb.Text + "','" + PPassTb.Text + "','" +PNameTb.Text  + "','" + PAmtTb.Text + "')";
                    NpgsqlDataAdapter cmd = new NpgsqlDataAdapter(sorgu, baglanti);
                    cmd.ExecuteNonQuery();
                    MessageBox.Show("Bilet Rezerve Edildi!");
                    baglanti.Close();
                    populate();
                }
                catch (Exception ex)
                {
                    MessageBox.Show(ex.Message);
                }


            }
        }

        private void yolcugetir()
        {
            baglanti.Open();
            string sorgu = "select* from PassengerTbl where PassId="+PIdCb.SelectedValue.ToString() + "";
            NpgsqlCommand cmd =new NpgsqlCommand(sorgu, baglanti);
            DataTable dt = new DataTable();
            NpgsqlDataAdapter da = new NpgsqlDataAdapter();
                da.Fill(dt);
            foreach(DataRow dr in dt.Rows)
            {
                pname = dr["PassName"].ToString();
                ppass = dr["Passport"].ToString();
                pnat = dr["PassNat"].ToString();
                PNameTb.Text=pname;
                PPassTb.Text=ppass;
                PNatTb.Text=pnat;  

              //  page = Convert.ToInt32(dr["PassNat"].ToString());

            }
            baglanti.Close();
        }



        private void Bilet_Load(object sender, EventArgs e)
        {
            fillYolcu();
         fillucuskodu();
            populate();
        }

        private void PIdCb_SelectionChangeCommitted(object sender, EventArgs e)
        {
            yolcugetir();
        }
    }
}
