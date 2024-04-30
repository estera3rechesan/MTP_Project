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
        // Definim o structură pentru a stoca perechile de nume utilizator și parolă
        private Dictionary<string, string> new_obj = new Dictionary<string, string>();

        public Form1()
        {
            InitializeComponent();
            // Încărcăm datele utilizatorilor și parolelor la pornirea formularului
            IncarcaUtilizatoriSiParole("C:\\Documente\\ProiectMTP.csv");
        }

        private void button1_Click(object sender, EventArgs e)
        {
            string nume = comboBox1.Text;
            string parola = textBox1.Text;

            // Verificăm dacă utilizatorul introdus există în colectie și parola corespunde
            if (new_obj.ContainsKey(nume) && new_obj[nume] == parola)
            {
                MessageBox.Show("Autentificare reușită!");
                Form2 f = new Form2(nume, parola);
                f.ShowDialog();
                this.Hide();
            }
            else
            {
                MessageBox.Show("Autentificare eșuată. Numele de utilizator sau parola incorecte.");
            }
        }

        // Metodă pentru încărcarea datelor utilizatorilor și parolelor din fișierul specificat
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

                        // Verificăm dacă parola aflata in fisier respectă cerințele: minim 8 caractere, o literă mare, o literă mică, o cifră și un caracter special
                        bool parolaValida = parola.Length >= 8 &&
                                            parola.Any(char.IsUpper) &&
                                            parola.Any(char.IsLower) &&
                                            parola.Any(char.IsDigit) &&
                                            parola.Intersect("!@#$%^&*.,'").Any();

                        if (parolaValida)
                        {
                            // Adăugăm numele de utilizator și parola asociată în dicționar
                            new_obj[numeUtilizator] = parola;
                        }
                        else
                        {
                            MessageBox.Show($"Parola pentru utilizatorul {numeUtilizator} nu este validă și nu a fost adăugată.");
                        }
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
    }
}
