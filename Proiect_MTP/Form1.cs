using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Windows.Forms;
using static System.Net.Mime.MediaTypeNames;

namespace Proiect_MTP
{
    public partial class Form1 : Form
    {
        private Dictionary<string, string> new_obj = new Dictionary<string, string>(); //structura cu perechile user-parola

        public Form1()
        {
            InitializeComponent();
            IncarcaUtilizatoriSiParole("users.csv");
        }

        private void Form1_Load(object sender, EventArgs e)
        {

        }

        private void button1_Click(object sender, EventArgs e)
        {
            string nume = textBox.Text;
            string parola = textBox1.Text;

            if (new_obj.ContainsKey(nume) && new_obj[nume] == parola)
            {
                //MessageBox.Show("Autentificare reușită!");
                Form2 f = new Form2(nume, parola);
                f.ShowDialog();
                this.Hide();
            }
            else
            {
                MessageBox.Show("Autentificare eșuată. Numele de utilizator sau parola incorecte.");
            }
        }

        private void IncarcaUtilizatoriSiParole(string caleFisier)
        {
            try
            {
                string[] linii = File.ReadAllLines(caleFisier);

                foreach (string linie in linii)
                {
                    string[] elemente = linie.Split(',');
                    if (elemente.Length == 2)
                    {
                        string numeUtilizator = elemente[0];
                        string parola = elemente[1];

                        new_obj[numeUtilizator] = parola;
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Eroare la încărcarea datelor din fișier: " + ex.Message);
            }
        }

        private void button2_Click_1(object sender, EventArgs e)
        {
            this.Close();
        }


        private void button3_Click(object sender, EventArgs e)
        {
            this.Hide();
            Form6 signUp = new Form6();
            signUp.FormClosed += (s, args) => this.Close();
            signUp.Show();
        }
    }
}
