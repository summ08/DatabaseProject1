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
    public partial class AnaSayfa : Form
    {
        public AnaSayfa()
        {
            InitializeComponent();
        }

        private void button2_Click(object sender, EventArgs e)
        {
            YolcuEkle yolcu = new YolcuEkle();
            yolcu.Show();
            this.Hide();
        }

        private void label2_Click(object sender, EventArgs e)
        {

        }

        private void button1_Click(object sender, EventArgs e)
        {
            FlightTbl ucus = new FlightTbl();
            ucus.Show();
            this.Hide();
        }

        private void button3_Click(object sender, EventArgs e)
        {
            Bilet bilet = new Bilet();
            bilet.Show();
            this.Hide();
        }

        private void button4_Click(object sender, EventArgs e)
        {
            Iptaller iptal = new Iptaller();
            iptal.Show();
            this.Hide();
        }

        private void label3_Click(object sender, EventArgs e)
        {
            Application.Exit();
        }
    }
}
