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

        public static DateTime LireDateTime(string label)
        {
            DateTime dateTime;

            while (true)
            {
                Console.Write($"{label} :  ");
                string saisie = Console.ReadLine();

                if (DateTime.TryParse(saisie, out dateTime))
                {
                    if (dateTime <= DateTime.Now)
                    {
                       return dateTime; 
                    }
                    
                    Console.WriteLine("Date invalide ! Veuillez réessayer avec une date inférieure ou égale à aujourd'hui");  
                }
                Console.WriteLine("Date invalide ! Veuillez réessayer (ex: 2024-05-10).");
            }
        }
        
    }
}