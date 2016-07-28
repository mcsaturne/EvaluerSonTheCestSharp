using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;

namespace EvaluerSonTheCestSharp
{
    class Program
    {
        public static int fact(int i) {
            if (i > 1) return i * fact(i-1);
            else return 1;
        }

        static void Main(string[] args)
        {
            The the = new The();
            int choix;
            List<The> listeDesThes = new List<The>();
            lireDansLaBD(ref listeDesThes);
            Console.WriteLine("Debut du programme");

            do
            {
                choix = afficherMenu();
                switch (choix)
                {
                    case 1:
                        the = ajouterUnThe(ref listeDesThes);
                        if (the != null)
                        {
                            listeDesThes.Add(the);
                            Console.WriteLine("\t Creation du the reussi\n" + the.ToString());
                        }
                        break;
                    case 2:
                        Console.Write("Entrez le nom du the a modifier : ");
                        string nomDuTheAModifier = Console.ReadLine();
                        The resThe = listeDesThes.Find(e => e.nom == nomDuTheAModifier);
                        modifierTheExistant(listeDesThes, ref resThe);
                        break;
                    case 3:
                        supprimerThe(ref listeDesThes);
                        break;
                    case 4:
                        trierLesThes(ref listeDesThes);
                        break;
                    case 5:
                        ecrireDansLaBD(ref listeDesThes);
                        Console.WriteLine("Fin du programme");
                        break;
                }
            } while (choix != 5);
        }

        private static void trierLesThes(ref List<The> listeDesThes) {
            Console.WriteLine("Trier les thes par...");
            Console.WriteLine("1 - Note");
            Console.WriteLine("2 - Nom");
            Console.WriteLine("3 - Magasin");
            Console.Write("Votre choix : ");
            string choix = Console.ReadLine();
            List<The> listeTriee = new List<The>();
            switch (choix) {
                case "1":
                    listeTriee = listeDesThes.OrderBy(o => o.nom).ToList<The>();
                    listeTriee.ForEach(o => Console.WriteLine(o.ToString()));
                    break;
                case "2":
                    listeTriee = listeDesThes.OrderBy(o => o.nom).ToList<The>();
                    listeTriee.ForEach(o => Console.WriteLine(o.ToString()));
                    break;
                case "3":
                    listeTriee = listeDesThes.OrderBy(o => o.magasin).ToList<The>();
                    listeTriee.ForEach(o => Console.WriteLine(o.ToString()));
                    break;

            }

        }

        private static void supprimerThe(ref List<The> listeDesThes) {
            Console.Write("Entrez le nom du the a supprimer : ");
            string nomDuTheASupprimer = Console.ReadLine();
            The resThe = listeDesThes.Find(e => e.nom == nomDuTheASupprimer);
            if (resThe != null)
                listeDesThes.Remove(resThe);
            else
                Console.WriteLine("Le the n'existe pas");

        }

        static void modifierTheExistant(List<The> listeDesThes, ref The resThe) {
            
            if (resThe != null)
            {
                int choix = AfficherMenuModifier();
                
                switch (choix){
                    case 1: changerNomThe(ref resThe);
                              break;
                    case 2: ajouterUneSaveur(ref resThe);
                              break;
                    case 3: changerLaDescription(ref resThe);
                              break;
                    case 4: changerLeMagasin(ref resThe);
                              break;
                    case 5: changerLaNote(ref resThe);
                              break;
                }

                Console.WriteLine("\t Modification du the reussi\n" + resThe.ToString());
            }
            else {
                Console.WriteLine("Le the n'existe pas");
            }
        }

        private static void changerLaNote(ref The resThe) {
            Console.Write("Entrez une nouvelle note : ");
            string note = Console.ReadLine();
            resThe.note = Int32.Parse(note);
        }

        private static void changerLeMagasin(ref The resThe) {
            Console.Write("Entrez un nouveau magasin : ");
            string magasin = Console.ReadLine();
            resThe.magasin = magasin;
        }

        private static void changerLaDescription(ref The resThe) {
            Console.Write("Entrez une nouvelle description : ");
            string description = Console.ReadLine();
            resThe.description = description;
        }

        private static void changerNomThe(ref The resThe) {
            Console.Write("Entrez le nouveau nom du the : ");
            string nom = Console.ReadLine();
            resThe.nom = nom;
        }

        private static void ajouterUneSaveur(ref The resThe) {
            Console.Write("Entrez des saveurs au the : ");
            string saveurs = Console.ReadLine();
            string[] tabSaveurs = saveurs.Split(' ');
            foreach (var i in tabSaveurs) {
                resThe.saveurs.Add(i);
            }
        }

        private static int AfficherMenuModifier()
        {
            string choix;
            int nombre;
            Console.WriteLine("*************Modification de the*************");
            Console.WriteLine("1 - Changer le nom");
            Console.WriteLine("2 - Ajouter une saveur");
            Console.WriteLine("3 - Changer la description");
            Console.WriteLine("4 - Changer la magasin");
            Console.WriteLine("5 - Changer la note");
            Console.Write("Votre choix : ");
            choix = Console.ReadLine();
            Int32.TryParse(choix, out nombre);
            return nombre;
        }

        public static The ajouterUnThe(ref List<The> listeDesThes) {
            The theCree = new The();
            theCree = obtenirInfoThe(ref listeDesThes);
            return theCree;
        }

        static The obtenirInfoThe(ref List<The> listeDesThes) {
            The theACreer = new The();
            Console.Write("Entrez le nom du thé : ");
            theACreer.nom = Console.ReadLine();
            The resThe = listeDesThes.Find(e => e.nom == theACreer.nom);
            if (resThe == null)
            {
                Console.Write("Entrez des saveurs : ");
                string saveurs = Console.ReadLine();
                string[] tabSaveurs = saveurs.Split(' ');
                for (int i = 0; i < tabSaveurs.Length; i++)
                {
                    theACreer.saveurs.Add(tabSaveurs[i]);
                }
                Console.Write("Entrez une description : ");
                theACreer.description = Console.ReadLine();
                Console.Write("Entrez un magasin d'achat : ");
                theACreer.magasin = Console.ReadLine();
                Console.Write("Entrez une note : ");
                theACreer.note = Int32.Parse(Console.ReadLine());
            }
            else {
                theACreer = null;
                Console.WriteLine("Le the existe deja. Voici les informations");
                Console.WriteLine(resThe.ToString());
                Console.Write("Souhaitez-vous le modifier ? (O/N) : ");
                string choix = Console.ReadLine();
                if (choix == "O") {
                    modifierTheExistant(listeDesThes, ref resThe);
                }
            }
            return theACreer;

        }
        public static int afficherMenu()
        {
            string choix;
            int nombre;
            do
            {
                Console.WriteLine("*************Systeme de gestion de the*************");
                Console.WriteLine("1 - Entrez un nouveau the");
                Console.WriteLine("2 - Modifier un the existant");
                Console.WriteLine("3 - Supprimer un the existant");
                Console.WriteLine("4 - Trier les thes");
                Console.WriteLine("5 - Quitter le logiciel");
                Console.Write("Votre choix : ");
                choix = Console.ReadLine();
                Int32.TryParse(choix, out nombre);
            } while (nombre < 0 || nombre > 6 );
            return nombre;
        }

        private static void lireDansLaBD(ref List<The> listeDesThes) {
            string ligne;
            The the = null;
            StreamReader file = null;
            try
            {
                file = new StreamReader(@"C:\Users\Maxime Grenier\Documents\petiteBD.txt");
                while ((ligne = file.ReadLine()) != null)
                {
                    the = new The();
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
            catch (IOException e)
            {
                Console.WriteLine("Le fichier n'existe pas");
                Console.WriteLine(e.ToString());
            }
            finally {
                if (file != null)
                    file.Dispose();
            }
            
        }

        private static void ecrireDansLaBD(ref List<The> listeDesThes) {
            string chemin = "C:\\Users\\Maxime Grenier\\Documents\\petiteBD.txt";
            FileStream fs = null;
            try
            {
                fs = new FileStream(chemin, FileMode.Create);
                using (StreamWriter writer = new StreamWriter(fs, Encoding.Default))
                {
                    foreach (var i in listeDesThes)
                    {
                        writer.WriteLine(i.objetVersFichier());
                    }
                }

            }
            catch (Exception e) {
                Console.WriteLine("Erreur dans l'ecriture du fichier");
                Console.WriteLine(e.ToString());
            }
            finally
            {
                if (fs != null)
                    fs.Dispose();
            }
        }
    }
}
