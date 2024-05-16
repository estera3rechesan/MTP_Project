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
            //evenimentele pentru drag and drop
            this.listBox1.DragDrop += new DragEventHandler(this.listBox1_DragDrop);
            this.listBox1.DragEnter += new DragEventHandler(this.listBox1_DragEnter);
        }

        //extragere
        private void listBox1_DragDrop(object sender, DragEventArgs e)
        {
            string[] s = (string[])e.Data.GetData(DataFormats.FileDrop, false);
            int i;
            for (i = 0; i < s.Length; i++)
                listBox1.Items.Add(s[i]);
        }

        //verificare date extrase
        private void listBox1_DragEnter(object sender, DragEventArgs e)
        {
            if (e.Data.GetDataPresent(DataFormats.FileDrop))
                e.Effect = DragDropEffects.All;
            else
                e.Effect = DragDropEffects.None;
        }

        //SALVARE
        private void button2_Click(object sender, EventArgs e)
        {
            using (SaveFileDialog dialog = new SaveFileDialog())
            {
                dialog.Filter = "Imagini (*.jpg, *.jpeg, *.png, *.bmp)|*.jpg;*.jpeg;*.png;*.bmp|Toate fișierele (*.*)|*.*";
                dialog.FilterIndex = 1; //primul filtu e implicit

                if (dialog.ShowDialog() == DialogResult.OK)
                {
                    string saveDirectory = Path.GetDirectoryName(dialog.FileName);
                    if (listBox1.Items.Count > 0)
                    {
                        foreach (string imagePath in listBox1.Items)
                        {
                            try
                            {
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


        //SALVARE IN LOCATIE IMPLICITA
        private void button3_Click(object sender, EventArgs e)
        {
            string saveDirectory = @"C:\Documente\Facultate an 2 sem 2\Lab MTP\PROIECT\Proiect_MTP\assets";
            if (listBox1.Items.Count > 0)
            {
                foreach (string imagePath in listBox1.Items)
                {
                    try
                    {
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
                MessageBox.Show("Lista de imagini este goală. Nu există imagini de salvat.", "Avertisment", MessageBoxButtons.OK, MessageBoxIcon.Warning);
        }

        private void Form4_Load(object sender, EventArgs e)
        {

        }
    }

}
