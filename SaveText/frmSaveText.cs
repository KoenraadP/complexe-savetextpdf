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
using SaveText.Bll;

namespace SaveText
{
    public partial class frmSaveText : Form
    {

        // globale variabele om de map op te slaan
        string Directory = @"C:\stories\";

        public frmSaveText()
        {
            InitializeComponent();
        }

        private void BtnSave_Click(object sender, EventArgs e)
        {
            // data uit mijn textboxes opslaan in variabelen
            string title = txtTitle.Text;
            string story = txtStory.Text;

            // try catch om exceptions op te vangen
            try
            {
                // TextBll SaveText methode uitvoeren
                TextBll.SaveText(Directory, title, story); 
                // boodschap tonen dat het gelukt is
                MessageBox.Show(title + " opgeslagen.");
            }
            // probleem 'map bestaat niet' opvangen
            catch (DirectoryNotFoundException dirEx)
            {
                // DialogResult vangt op welke knop
                // we aangeklikt hebben bij de error message (yes or no)
                DialogResult result =  MessageBox.Show(dirEx.Message,
                    "Map bestaat niet", MessageBoxButtons.YesNo,
                    MessageBoxIcon.Error, MessageBoxDefaultButton.Button1);
                // als we op 'yes' klikken
                if (result == DialogResult.Yes) {
                    // opnieuw try catch omdat we opnieuw
                    // zullen proberen het bestand op te slaan
                    try
                    {
                        // map aanmaken
                        // Specifiek System.IO er voor plaatsen omdat
                        // er anders conflict is met mijn eigen Directory variabele
                        System.IO.Directory.CreateDirectory(Directory);
                        // opnieuw proberen om bestand op te slaan
                        TextBll.SaveText(Directory, title, story);
                        // boodschap tonen dat het gelukt is
                        MessageBox.Show(title + " opgeslagen.");
                    }
                    // algemene foutboodschap tonen
                    catch (Exception ex)
                    {
                        MessageBox.Show(ex.Message);
                    }
                }
            }
            // probleem 'niet goed ingevuld' opvangen
            catch (ArgumentNullException argEx)
            {
                MessageBox.Show(argEx.Message);
            }
            // algemene fouten opvangen
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        private void BtnOpen_Click(object sender, EventArgs e)
        {
           // dialoogvenster voor openen bestand tonen
           ofd.ShowDialog();
        }

        private void Ofd_FileOk(object sender, CancelEventArgs e)
        {
            // story invullen via inhoud van bestand
            // geselecteerd bestand = ofd.FileName
            // dit is bijvoorbeeld C:\stories\verhaal.txt
            txtStory.Text = File.ReadAllText(ofd.FileName);
            // title invullen via bestandsnaam
            // deze methode pakt het laatste stuk van het pad
            // bijvoorbeeld verhaal.txt en verwijdert dan ook nog
            // de extensie (.txt)
            txtTitle.Text = Path.GetFileNameWithoutExtension(ofd.FileName);
        }

        private void btnSavePDF_Click(object sender, EventArgs e)
        {
            // data uit mijn textboxes opslaan in variabelen
            string title = txtTitle.Text;
            string story = txtStory.Text;

            // TextBll SavePDF methode uitvoeren
            TextBll.SavePDF(Directory, title, story);
        }
    }
}
