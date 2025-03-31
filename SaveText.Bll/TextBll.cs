using System;
// System.IO bevat allerlei classes en methodes
// om met mappen en bestanden te werken
using System.IO;

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
    }
}
