using EvaluerSonTheCestSharp.Helpers;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;

namespace EvaluerSonTheCestSharp
{
    class Program
    {
        static void Main(string[] args)
        {
            The the = new The();
            int choix;
            string path = $@"{AppDomain.CurrentDomain.BaseDirectory}\\petiteBD.txt";
            List<The> listeDesThes = new List<The>();
            FileHelper.lireDansLaBD(listeDesThes, path);
            Console.WriteLine("Debut du programme");

            do
            {
                choix = afficherMenu();
                switch (choix)
                {
                    case 1:
                        the = ajouterUnThe(listeDesThes);
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
                        modifierTheExistant(listeDesThes, resThe);
                        break;
                    case 3:
                        supprimerThe(listeDesThes);
                        break;
                    case 4:
                        trierLesThes(listeDesThes);
                        break;
                    case 5:
                        FileHelper.ecrireDansLaBD(listeDesThes, path);
                        Console.WriteLine("Fin du programme");
                        break;
                }
            } while (choix != 5);
        }

        private static void trierLesThes(List<The> listeDesThes)
        {
            Console.WriteLine("Trier les thes par...");
            Console.WriteLine("1 - Note");
            Console.WriteLine("2 - Nom");
            Console.WriteLine("3 - Magasin");
            Console.Write("Votre choix : ");
            string choix = Console.ReadLine();
            List<The> listeTriee = new List<The>();
            switch (choix)
            {
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

        private static void supprimerThe(List<The> listeDesThes)
        {
            Console.Write("Entrez le nom du the a supprimer : ");
            string nomDuTheASupprimer = Console.ReadLine();
            The resThe = listeDesThes.Find(e => e.nom == nomDuTheASupprimer);
            if (resThe != null)
                listeDesThes.Remove(resThe);
            else
                Console.WriteLine("Le the n'existe pas");

        }

        static void modifierTheExistant(List<The> listeDesThes, The resThe)
        {

            if (resThe != null)
            {
                int choix = AfficherMenuModifier();

                switch (choix)
                {
                    case 1:
                        changerNomThe(resThe);
                        break;
                    case 2:
                        ajouterUneSaveur(resThe);
                        break;
                    case 3:
                        changerLaDescription(resThe);
                        break;
                    case 4:
                        changerLeMagasin(resThe);
                        break;
                    case 5:
                        changerLaNote(resThe);
                        break;
                }

                Console.WriteLine("\t Modification du the reussi\n" + resThe.ToString());
            }
            else
            {
                Console.WriteLine("Le the n'existe pas");
            }
        }

        private static void changerLaNote(The resThe)
        {
            Console.Write("Entrez une nouvelle note : ");
            string note = Console.ReadLine();
            resThe.note = Int32.Parse(note);
        }

        private static void changerLeMagasin(The resThe)
        {
            Console.Write("Entrez un nouveau magasin : ");
            string magasin = Console.ReadLine();
            resThe.magasin = magasin;
        }

        private static void changerLaDescription(The resThe)
        {
            Console.Write("Entrez une nouvelle description : ");
            string description = Console.ReadLine();
            resThe.description = description;
        }

        private static void changerNomThe(The resThe)
        {
            Console.Write("Entrez le nouveau nom du the : ");
            string nom = Console.ReadLine();
            resThe.nom = nom;
        }

        private static void ajouterUneSaveur(The resThe)
        {
            Console.Write("Entrez des saveurs au the : ");
            string saveurs = Console.ReadLine();
            string[] tabSaveurs = saveurs.Split(' ');
            foreach (var i in tabSaveurs)
            {
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

        public static The ajouterUnThe(List<The> listeDesThes)
        {
            The theCree = new The();
            theCree = obtenirInfoThe(listeDesThes);
            return theCree;
        }

        static The obtenirInfoThe(List<The> listeDesThes)
        {
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
            else
            {
                theACreer = null;
                Console.WriteLine("Le the existe deja. Voici les informations");
                Console.WriteLine(resThe.ToString());
                Console.Write("Souhaitez-vous le modifier ? (O/N) : ");
                string choix = Console.ReadLine();
                if (choix == "O")
                {
                    modifierTheExistant(listeDesThes, resThe);
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
            } while (nombre < 0 || nombre > 6);
            return nombre;
        }
    }
}
