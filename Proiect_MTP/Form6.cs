using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Proiect_MTP
{
    public partial class Form6 : Form
    {
        private Dictionary<string, string> new_obj = new Dictionary<string, string>();
        public Form6()
        {
            InitializeComponent();
            InitializeCustomers("users.csv");
        }

        //adaugam in structura toate numele deja existente in fisier
        private void InitializeCustomers(string caleFisier)
        {
            try
            {
                string[] linii = File.ReadAllLines(caleFisier);

                foreach (string linie in linii)
                {
                    string[] date = linie.Split(',');
                    new_obj.Add(date[0], date[1]);
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Eroare la citirea datelor din fișier: " + ex.Message);
            }
        }

        private void Form6_Load(object sender, EventArgs e)
        {

        }

        //functie care adauga in structura un nou utilizator si parola
        private void IncarcaUtilizatoriSiParole(string caleFisier, Dictionary<string, string> obj)
        {
            string user = textBox1.Text;
            string parola = textBox2.Text;

            try
            {
                /*  - are cel putin 8 caractere
                         - contine cel putin o litera mare
                         - contine cel putin o litera mica
                         - contine cel putin o cifra
                         - contine cel putin un caracter special
               */
                bool parolaValida = parola.Length >= 8 &&
                                    parola.Any(char.IsUpper) &&
                                    parola.Any(char.IsLower) &&
                                    parola.Any(char.IsDigit) &&
                                    parola.Intersect("!@#$%^&*.,'").Any();

                /*  - inceape cu litera mare
                */
                bool numeValid = user.Length > 0 &&
                                 char.IsUpper(user[0]);

                if (parolaValida && numeValid)
                {
                    if (new_obj.ContainsKey(user))
                    {
                        MessageBox.Show("Acest nume de utilizator este deja folosit !");
                    }
                    else
                    {
                        obj.Add(user, parola);
                    }
                }
                else
                {
                    MessageBox.Show("Utilizatorul trebuie sa inceapa cu litera mare si parola sa contina minim 8 caractere, o literă mare, o literă mică, o cifră și un caracter special!");
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Eroare la încărcarea datelor în fișier: " + ex.Message);
            }
        }

        private void button2_Click(object sender, EventArgs e)
        {
            Form1 form1 = new Form1();
            this.Hide();
            form1.ShowDialog();
            this.Show();
        }

        //rescrie intreaga lista inapoi in fisier
        private void button1_Click(object sender, EventArgs e)
        {
            IncarcaUtilizatoriSiParole("users.csv", new_obj);

            try
            {
                using (StreamWriter sw = new StreamWriter("users.csv"))
                {
                    foreach (var utilizator in new_obj)
                    {
                        sw.WriteLine(utilizator.Key + "," + utilizator.Value);
                    }
                }

                //la log in
                this.Hide();
                Form1 signIn = new Form1();
                signIn.Closed += (s, args) => this.Close();
                signIn.Show();
                

            }
            catch (Exception ex)
            {
                MessageBox.Show("Eroare la scrierea datelor în fișier: " + ex.Message);
            }
        }
    }
}
