using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.IO;


namespace EvaluerSonTheCestSharp.Helpers
{
    class FileHelper
    {
        public static void lireDansLaBD(List<The> listeDesThes, string path)
        {
            try
            {
                using (var file = new StreamReader(path))
                {
                    string ligne;
                    while ((ligne = file.ReadLine()) != null)
                    {
                        var the = new The();
                        IEnumerable<string> ligneThe = new List<string>();
                        ligneThe = ligne.Split('~');
                        the.nom = ligneThe.ElementAt(0);
                        the.saveurs = ligneThe.ElementAt(1).Split('=').ToList<string>();
                        the.description = ligneThe.ElementAt(2);
                        the.magasin = ligneThe.ElementAt(3);
                        the.note = Int32.Parse(ligneThe.ElementAt(4));
                        listeDesThes.Add(the);
                    }
                }
            }
            catch (IOException e)
            {
                Console.WriteLine("Le fichier n'existe pas");
                Console.WriteLine(e.ToString());
            }
        }

        public static void ecrireDansLaBD(List<The> listeDesThes, string path)
        {
            try
            {
                using (var fs = new FileStream(path, FileMode.Create))
                {
                    using (var writer = new StreamWriter(fs, Encoding.Default))
                    {
                        foreach (var i in listeDesThes)
                        {
                            writer.WriteLine(i.objetVersFichier());
                        }
                    }
                }
            }
            catch (Exception e)
            {
                Console.WriteLine("Erreur dans l'ecriture du fichier");
                Console.WriteLine(e.ToString());
            }
        }
    }
}
