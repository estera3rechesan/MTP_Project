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
    public partial class Form5 : Form
    {
        public Form5()
        {
            InitializeComponent();
        }

        private void Form5_Load(object sender, EventArgs e)
        {

        }

        private void button1_Click(object sender, EventArgs e)
        {
            if (!string.IsNullOrWhiteSpace(textBox2.Text) &&
                !string.IsNullOrWhiteSpace(textBox3.Text) &&
                !string.IsNullOrWhiteSpace(textBox4.Text) &&
                !string.IsNullOrWhiteSpace(textBox5.Text) &&
                !string.IsNullOrWhiteSpace(textBox1.Text))
            {
                string connect = @"Data Source=Esty\SQLEXPRESS;Initial Catalog=Ferma;Integrated Security=True";
                using (SqlConnection con = new SqlConnection(connect))
                {
                    con.Open();

                    string stmt = "INSERT INTO Animale ([specie], [nume], [varsta], [nr_picioare], [personalitate]) VALUES (@Specie, @NumeAnimal, @Varsta, @NrPicioare, @Personalitate)";

                    using (SqlCommand command = new SqlCommand(stmt, con))
                    {
                        command.Parameters.AddWithValue("@NumeAnimal", textBox1.Text);
                        command.Parameters.AddWithValue("@Specie", textBox2.Text);
                        command.Parameters.AddWithValue("@Varsta", textBox3.Text);
                        command.Parameters.AddWithValue("@NrPicioare", textBox4.Text);
                        command.Parameters.AddWithValue("@Personalitate", textBox5.Text);

                        int rowsAffected = command.ExecuteNonQuery();
                        if (rowsAffected > 0)
                        {
                            MessageBox.Show("Inserare reușită!");
                        }
                        else
                        {
                            MessageBox.Show("Inserare nereușită!");
                        }
                    }
                }
                this.Close();
            }
            else
            {
                MessageBox.Show("Vă rugăm să completați toate câmpurile pentru a efectua modificările.");
            }
        }


        //SPECIE TEXTBOX (incepe cu litera mare)
        private void textBox2_TextChanged_1(object sender, EventArgs e)
        {
            string specie = textBox2.Text;
            if (!char.IsUpper(specie[0]))
            {
                MessageBox.Show("Specia trebuie sa inceapa cu litera mare");
                textBox2.BackColor = Color.Red;
            }
            else
            {
                textBox2.BackColor = SystemColors.Window;
            }
        }

        //NUME TEXTBOX (incepe cu litera mare si contine doar litere)
        private void textBox1_TextChanged_1(object sender, EventArgs e)
        {
            string nume = textBox1.Text;
            if (!char.IsUpper(nume[0]))
            {
                MessageBox.Show("Numele trebuie sa inceapa cu litera mare");
                textBox1.BackColor = Color.Red;
            }
            else if (!nume.All(char.IsLetter))
            {
                MessageBox.Show("Numele trebuie sa contina doar litere");
                textBox1.BackColor = Color.Red;
            }
            else
            {
                textBox1.BackColor = SystemColors.Window;
            }
        }

        //VARSTA TEXTBOX (doar cifre si nu mai mare de 30 ani)
        private void textBox3_TextChanged_1(object sender, EventArgs e)
        {
            string varsta = textBox3.Text;
            if (!varsta.All(char.IsDigit))
            {
                MessageBox.Show("Varsta trebuie să conțină doar cifre.");
                textBox3.BackColor = Color.Red;
            }
            else if (!string.IsNullOrWhiteSpace(varsta) && int.TryParse(varsta, out int v))
            {
                if (v > 30)
                {
                    MessageBox.Show("Varsta nu poate fi mai mare de 30 de ani.");
                    textBox3.BackColor = Color.Red;
                }
                else
                {
                    textBox4.BackColor = SystemColors.Window;
                }
            }
            else
            {
                textBox3.BackColor = SystemColors.Window;
            }
        }

        //NR PICIOARE TEXTBOX (doar cifre si nu mai mult de 4 picioare)
        private void textBox4_TextChanged_1(object sender, EventArgs e)
        {
            string pic = textBox4.Text;

            if (!string.IsNullOrWhiteSpace(pic) && int.TryParse(pic, out int picioare))
            {
                if (picioare > 4)
                {
                    MessageBox.Show("Numărul de picioare nu poate fi mai mare de 4.");
                    textBox4.BackColor = Color.Red;
                }
                else
                {
                    textBox4.BackColor = SystemColors.Window;
                }
            }
            else
                textBox4.BackColor = SystemColors.Window;
            if (!pic.All(char.IsDigit))
            {
                MessageBox.Show("Numarul de picioare trebuie să conțină doar cifre.");
                textBox4.BackColor = Color.Red;
            }
            else
            {
                textBox4.BackColor = SystemColors.Window;
            }
        }

        //PERSONALITATE TEXTBOX (doar litere si nu mai mult de 20 caractere)
        private void textBox5_TextChanged_1(object sender, EventArgs e)
        {
            //sa contina doar litere
            string pers = textBox5.Text;
            if (!pers.All(char.IsLetter))
            {
                MessageBox.Show("Personalitatea trebuie să conțină doar litere.");
                textBox5.BackColor = Color.Red;
            }
            if (pers.Length > 20)
            {
                MessageBox.Show("Personalitatea nu poate fi mai lungă de 20 caractere.");
                textBox5.BackColor = Color.Red;
            }
            else
            {
                textBox5.BackColor = SystemColors.Window;
            }
        }
    }
    
}
