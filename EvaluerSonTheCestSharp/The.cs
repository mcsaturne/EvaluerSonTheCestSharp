using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EvaluerSonTheCestSharp
{
    class The
    {
        public string nom { get; set; }
        public int note { get; set; }
        public string description { get; set; }
        public string magasin { get; set; }
        public List<string> saveurs = new List<string>();

        public override string ToString()
        {
            string retour = "\t Nom : " + nom;
            retour += ("\n\t Saveurs : ");
            foreach (var i in saveurs) {
                retour += i + " ";
            }
            retour += "\n\t Description : " + description;
            retour += "\n\t Magasin : " + magasin;
            retour += "\n\t Note : " + note;
            retour += "\n\t *****************";

            return retour;
        }

        public string objetVersFichier() {
            string retour = nom + "~";
            foreach (var i in saveurs) {
                retour += i + "=";
            }
            retour = retour + "~" + description + "~" + magasin + "~" + note;
            return retour;
        }
    }
}
