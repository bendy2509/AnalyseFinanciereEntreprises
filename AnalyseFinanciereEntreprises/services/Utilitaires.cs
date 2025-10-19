using System;
    
namespace AnalyseFinanciereEntreprises.services
{
    public static class Utilitaires
    {
        public static string LireSecteurValide()
        {
            string secteur;

            while (true)
            {
                Console.Write("Secteur (technologie/sante/finance): ");
                secteur = Console.ReadLine()?.Trim().ToLower();

                if (secteur == "technologie" || secteur == "sante" || secteur == "finance")
                {
                    return secteur;
                }

                Console.WriteLine(" Choix invalide ! Veuillez saisir 'technologie', 'sante' ou 'finance'.");
            }
        }
        
        public static int LireEntier(string label)
        {
            int valeur;
            Console.Write(label + ": ");
            while (!int.TryParse(Console.ReadLine(), out valeur))
            {
                Console.WriteLine("Entrée invalide, réessayez : ");
                Console.Write(label + ": ");
            }
            return valeur;
        }
    
        public static string LireTexteNonVide(string label)
        {
            string saisie;
            do
            {
                Console.Write(label + " : ");
                saisie = Console.ReadLine();

                if (string.IsNullOrWhiteSpace(saisie))
                {
                    Console.WriteLine("Ce champ est obligatoire !");
                }

            } while (string.IsNullOrWhiteSpace(saisie));

            return saisie;
        }

        public static decimal LireDecimal(string label)
        {
            decimal valeur;
            Console.Write(label + ": ");
            while (!decimal.TryParse(Console.ReadLine(),out valeur))
            {
                Console.WriteLine("Entrée invalide, réessayez avec un decimal/nombre : ");
                Console.Write(label + ": ");
            }
            return valeur;
        }


        #region Modification
        public static string LireValeur(string prompt, string valeurActuelle)
        {
            Console.Write($"{prompt}: ");
            var input = Console.ReadLine();
            return string.IsNullOrWhiteSpace(input) ? valeurActuelle : input.Trim();
        }

        public static decimal LireDecimal(string prompt, decimal valeurActuelle)
        {
            Console.Write($"{prompt}: ");
            string input = Console.ReadLine();
            return string.IsNullOrWhiteSpace(input) || !decimal.TryParse(input, out decimal result) || result < 0
                ? valeurActuelle
                : result;
        }

        public static int LireInt(string prompt, int valeurActuelle)
        {
            Console.Write($"{prompt}: ");
            string input = Console.ReadLine();
            return string.IsNullOrWhiteSpace(input) || !int.TryParse(input, out int result) || result < 0
                ? valeurActuelle
                : result;
        }
        #endregion
        

    }
}