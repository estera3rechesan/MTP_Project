using System;
using System.IO;
using System.Windows.Forms;
using System.Drawing;
using static System.Windows.Forms.VisualStyles.VisualStyleElement.Window;

namespace Proiect_MTP
{
    public partial class Form4 : Form
    {
        public Form4()
        {
            InitializeComponent();
            this.listBox1.DragDrop += new DragEventHandler(this.listBox1_DragDrop);
            this.listBox1.DragEnter += new DragEventHandler(this.listBox1_DragEnter);
        }

        private void listBox1_DragDrop(object sender, DragEventArgs e)
        {
            string[] s = (string[])e.Data.GetData(DataFormats.FileDrop, false);
            int i;
            for (i = 0; i < s.Length; i++)
                listBox1.Items.Add(s[i]);
        }

        private void listBox1_DragEnter(object sender, DragEventArgs e)
        {
            if (e.Data.GetDataPresent(DataFormats.FileDrop))
                e.Effect = DragDropEffects.All;
            else
                e.Effect = DragDropEffects.None;
        }

        private void button2_Click(object sender, EventArgs e)
        {
            using (SaveFileDialog dialog = new SaveFileDialog())
            {
                // Setează filtre pentru tipurile de fișiere acceptate (de exemplu, imagini)
                dialog.Filter = "Imagini (*.jpg, *.jpeg, *.png, *.bmp)|*.jpg;*.jpeg;*.png;*.bmp|Toate fișierele (*.*)|*.*";
                dialog.FilterIndex = 1; // Setează filtrul implicit

                // Arată dialogul de salvare
                if (dialog.ShowDialog() == DialogResult.OK)
                {
                    // Obține calea de salvare a directorului selectat de utilizator
                    string saveDirectory = Path.GetDirectoryName(dialog.FileName);

                    // Verifică dacă lista de imagini nu este goală
                    if (listBox1.Items.Count > 0)
                    {
                        // Iterează prin fiecare element din lista de imagini
                        foreach (string imagePath in listBox1.Items)
                        {
                            try
                            {
                                // Copiază fișierele selectate în directorul de salvare
                                string fileName = Path.GetFileName(imagePath);
                                File.Copy(imagePath, Path.Combine(saveDirectory, fileName), true);
                            }
                            catch (Exception ex)
                            {
                                MessageBox.Show($"Eroare la salvarea imaginii {Path.GetFileName(imagePath)}: {ex.Message}");
                            }
                        }

                        MessageBox.Show("Imaginile au fost salvate cu succes.", "Succes", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    }
                    else
                    {
                        MessageBox.Show("Lista de imagini este goală. Nu există imagini de salvat.", "Avertisment", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    }
                }
            }
        }

    }
}
