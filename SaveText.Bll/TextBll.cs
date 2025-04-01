using System;
using System.Diagnostics;
// System.IO bevat allerlei classes en methodes
// om met mappen en bestanden te werken
using System.IO;
using PdfSharp;
using PdfSharp.Drawing;
using PdfSharp.Drawing.Layout;
using PdfSharp.Pdf;

namespace SaveText.Bll
{
    // public want nodig in ander project
    // static want ik wil gewoon werken met TextBll.SaveText()
    // en niet met new TextBll()
    public static class TextBll
    {
        // directory: in welke map moet het bestand opgeslagen worden
        // title: onder welke naam moet het bestand opgeslagen worden
        // story: welke tekst komt effectief in het bestand
        public static void SaveText(string directory,
            string title, string story)
        {
            // als de directory (map) nog niet bestaatn
            // dan error boodschap aanmaken en 'terug gooien'
            if (!Directory.Exists(directory))
            {
                throw new DirectoryNotFoundException("Map bestaat niet." +
                                                     " Wil je deze aanmaken?");
            }

            // controleren of title en story
            // niet leeg of null zijn
            if (string.IsNullOrEmpty(title) ||
               string.IsNullOrEmpty(story))
            {
                throw new ArgumentNullException("Title of story is leeg.");
            }

            // als alles OK is, pad samenstellen
            // bijvoorbeeld: C:\stories\cinderella.txt
            string path = directory + title + ".txt";

            // bestand aanmaken en tekst wegschrijven
            File.WriteAllText(path, story);
        }

        // methode om tekst in PDF op te slaan
        // zelfde parameters als bij de andere methode
        public static void SavePDF(string directory,
            string title, string story)
        {
            // PDF document aanmaken
            // nodig: using PdfSharp.Pdf;
            PdfDocument document = new PdfDocument();

            // lege pagina toevoegen
            PdfPage page = document.AddPage();

            // grootte pagina instellen op A4
            // nodig: using PdfSharp;
            page.Size = PageSize.A4;

            // XGraphics object aanmaken om de tekst te kunnen
            // 'tekenen' op de pagina
            // nodig: using PdfSharp.Drawing;
            XGraphics gfx = XGraphics.FromPdfPage(page);

            // lettertype en grootte instellen
            XFont font = new XFont("Verdana", 12);

            // een 'textformatter' aanmaken
            // zorgt er voor dat de tekst niet gewoon in één lijn doorloopt
            // en verwerkt ook 'new lines'
            // nodig: using PdfSharp.Drawing.Layout;
            XTextFormatter tf = new XTextFormatter(gfx);

            // tekst op de pagina 'tekenen'
            // XBrushes.Black --> zwarte tekst
            // XRect --> rechthoek waarin de tekst geplaatst wordt
            // eerste twee parameters bij XRect: marge links en boven
            // laatste twee parameters bij XRect: marge rechts en onderaan
            tf.DrawString(story, font, XBrushes.Black,
                new XRect(50, 50, page.Width.Point-100, 
                    page.Height.Point-100));

            // pad samenstellen om op te slaan
            // map + bestandsnaam + extensie
            string path = directory + title + ".pdf";

            // PDF document effectief opslaan
            document.Save(path);

            // PDF bestand onmiddellijk eens openen
            // nodig: using System.Diagnostics;
            Process.Start(path);
        }
    }
}
