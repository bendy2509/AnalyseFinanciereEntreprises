using System;
    
namespace AnalyseFinanciereEntreprises.services
{
    public static class Utilitaires
    {
        public static void AffichageConsole(string label ="", string champ = "")
        {
            Console.ForegroundColor = ConsoleColor.Cyan;
            Console.WriteLine("\n──────────────────────────────────────────────");
            Console.ForegroundColor = ConsoleColor.Yellow;
            Console.Write($" ➤ {champ}");
            Console.ForegroundColor = ConsoleColor.White;
            Console.Write(label);
            Console.ResetColor();

        }
        
        public static void Avertissement(string label)
        {
            Console.ForegroundColor = ConsoleColor.Cyan;
            Console.WriteLine("\n──────────────────────────────────────────────");
            Console.ForegroundColor = ConsoleColor.DarkRed;
            Console.Write($"{label}");
            Console.ResetColor();

        }

        public static void Success(string label)
        {
            Console.ForegroundColor = ConsoleColor.Cyan;
            Console.WriteLine("\n──────────────────────────────────────────────");
            Console.ForegroundColor = ConsoleColor.DarkGreen;
            Console.Write($"{label}");
            Console.ResetColor();
        }
        
        public static string LireSecteurValide()
        {
            string secteur;

            while (true)
            {
                AffichageConsole("(technologie / sante / finance) : ", "secteur");

                secteur = Console.ReadLine()?.Trim().ToLower();

                if (secteur == "technologie" || secteur == "sante" || secteur == "finance")
                {
                    return secteur;
                }

                Avertissement(" Choix invalide ! Veuillez saisir 'technologie', 'sante' ou 'finance'.");
            }
        }
        
        public static int LireEntier(string label)
        {
            int valeur;
            AffichageConsole(champ: label + ": ");
            while (!int.TryParse(Console.ReadLine(), out valeur))
            {
                Avertissement("Entrée invalide, réessayez : ");
                AffichageConsole(label + ": ");
            }
            return valeur;
        }
    
        public static string LireTexteNonVide(string label)
        {
            string saisie;
            do
            {
                AffichageConsole(champ:label + " : ");
                saisie = Console.ReadLine();

                if (string.IsNullOrWhiteSpace(saisie))
                {
                    Avertissement(" Ce champ est obligatoire !");
                }

            } while (string.IsNullOrWhiteSpace(saisie));

            return saisie;
        }

        public static decimal LireDecimal(string label)
        {
            decimal valeur;
            AffichageConsole(champ:label + ": ");
            while (!decimal.TryParse(Console.ReadLine(),out valeur))
            {
                Avertissement("Entrée invalide, réessayez avec un decimal/nombre : ");
                AffichageConsole(label + ": ");
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
        

        public static DateTime LireDateTime(string label)
        {
            DateTime dateTime;

            while (true)
            {
                AffichageConsole(champ:label +":  ");
                string saisie = Console.ReadLine();

                if (DateTime.TryParse(saisie, out dateTime))
                {
                    if (dateTime <= DateTime.Now)
                    {
                       return dateTime; 
                    }
                    Avertissement("Date invalide ! Veuillez réessayer avec une date inférieure ou égale à aujourd'hui");  
                }
                Avertissement("Date invalide ! Veuillez réessayer (ex: 2024-05-10).");
            }
        }
        
    }
}