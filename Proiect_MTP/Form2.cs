using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Data.SqlClient;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Proiect_MTP
{
    public partial class Form2 : Form
    {
        string num, par;
        public Form2(string nume, string parola)
        {
            num = nume;
            par = parola;
            InitializeComponent();
        }

        private void dataGridView1_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {

        }

        private void button4_Click(object sender, EventArgs e)
        {
            string connect = @"Data Source=Esty\SQLEXPRESS;Initial Catalog=Ferma;Integrated Security=True";
            SqlConnection con = new SqlConnection(connect);
            con.Open();
            string stmt = "select * from animale where nume='" + textBox1.Text + "'";
            SqlDataAdapter da = new SqlDataAdapter(stmt, con);
            DataSet ds = new DataSet();
            da.Fill(ds, "Animale");
            dataGridView1.DataSource = ds.Tables["Animale"].DefaultView;
            con.Close();
            da.Dispose();
            ds.Dispose();
        }

        private void textBox1_TextChanged(object sender, EventArgs e)
        { 
            string nume = textBox1.Text;
            if (!char.IsUpper(nume[0]))
            {
                MessageBox.Show("Numele trebuie sa inceapa cu litera mare");
            }
            if (!nume.All(char.IsLetter))
            {
                MessageBox.Show("Numele trebuie sa contina doar litere");
            }

        }

        private void button3_Click(object sender, EventArgs e)
        {
            // Asigurați-vă că utilizatorul confirmă ștergerea înainte de a continua
            DialogResult result = MessageBox.Show("Sigur doriți să ștergeți numele introdus din baza de date?", "Confirmare ștergere", MessageBoxButtons.YesNo);
            if (result == DialogResult.Yes)
            {
                string connect = @"Data Source=Esty\SQLEXPRESS;Initial Catalog=Ferma;Integrated Security=True";
                using (SqlConnection con = new SqlConnection(connect))
                {
                    con.Open();
                    string stmt = "DELETE FROM animale WHERE nume = @Nume"; //comanda pt stergere

                    using (SqlCommand command = new SqlCommand(stmt, con)) //executare
                    {
                        command.Parameters.AddWithValue("@Nume", textBox1.Text);
                        int rowsAffected = command.ExecuteNonQuery();
                        if (rowsAffected > 0)
                        {
                            MessageBox.Show("Numele a fost șters cu succes din baza de date.");
                        }
                        else
                        {
                            MessageBox.Show("Nu s-au găsit înregistrări cu numele specificat în baza de date.");
                        }
                    }
                }
            }
        }


        private void textBox2_TextChanged(object sender, EventArgs e)
        {
            string nume = textBox1.Text;
            if (!char.IsUpper(nume[0]))
            {
                MessageBox.Show("Numele trebuie sa inceapa cu litera mare");
            }
            else if (!nume.All(char.IsLetter))
            {
                MessageBox.Show("Numele trebuie sa contina doar litere");
            }
        }

        private void button2_Click(object sender, EventArgs e)
        {
            string numeAnimal = textBox1.Text;
            if (!string.IsNullOrWhiteSpace(numeAnimal))
            {
                // Deschideți Form3 și transmiteți numele animalului către acesta
                Form3 form3 = new Form3(numeAnimal);
                form3.ShowDialog();
            }
            else
            {
                MessageBox.Show("Vă rugăm să introduceți numele animalului pentru modificare.");
            }
        }

        private void button1_Click(object sender, EventArgs e)
        {
            // Redeschideți Form2 cu aceleași informații nume și parolă
            Form2 form2 = new Form2(num, par);
            form2.Show();

            // Închideți formularul actual (Form2)
            this.Close();
        }

        private void button5_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void button5_Click_1(object sender, EventArgs e)
        {
           Form4 form4 = new Form4();
           form4.ShowDialog();
        }

        private void Form2_Load(object sender, EventArgs e)
        {
            string connect = @"Data Source=Esty\SQLEXPRESS;Initial Catalog=Ferma;Integrated Security=True";
            SqlConnection cnn = new SqlConnection(connect);
            cnn.Open();
            string tabel_date = "select * from Animale";
            SqlDataAdapter da = new SqlDataAdapter(tabel_date, connect);
            DataSet ds = new DataSet();
            da.Fill(ds, "Animale");
            dataGridView1.DataSource = ds.Tables["Animale"].DefaultView;
            cnn.Close();
        }
    }
}
