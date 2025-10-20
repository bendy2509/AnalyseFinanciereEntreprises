using System;
using System.Threading;
using AnalyseFinanciereEntreprises.models;
using AnalyseFinanciereEntreprises.services;

namespace AnalyseFinanciereEntreprises
{
    class Program
    {
        static void Main(string[] args)
        {
            // === Écran de bienvenue avant tout ===
            AfficherEcranBienvenue();

            GestionnaireEntreprises gestionnaire = new GestionnaireEntreprises();
            bool continuer = true;

            // Données d'exemple
            InitialiserDonneesExemple(gestionnaire);

            while (continuer)
            {
                AfficherMenu();
                var choix = Console.ReadLine();

                switch (choix)
                {
                    case "1":
                        EnregistrerEntreprise(gestionnaire);
                        break;
                    case "2":
                        gestionnaire.AfficherToutesEntreprises();
                        break;
                    case "3":
                        AfficherParSecteur(gestionnaire);
                        break;
                    case "4":
                        ModifierEntreprise(gestionnaire);
                        break;
                    case "5":
                        SupprimerEntreprise(gestionnaire);
                        break;
                    case "6":
                        RestaurerEntreprise(gestionnaire);
                        break;
                    case "7":
                        TrierEntreprises(gestionnaire);
                        break;
                    case "8":
                        gestionnaire.GenererRapportGlobal();
                        break;
                    case "9":
                        gestionnaire.AfficherPlusHautRevenu();
                        break;
                    case "10":
                        gestionnaire.AfficherPlusBasRevenu();
                        break;
                    case "11":
                        gestionnaire.AfficherBeneficesSectoriels();
                        break;
                    case "0":
                        continuer = false;
                        break;
                    default:
                        Console.WriteLine("Choix invalide.");
                        break;
                }

                if (continuer)
                {
                    Console.WriteLine("\nAppuyez sur une touche pour continuer...");
                    Console.ReadKey();
                    Console.Clear();
                }
            }
        }

        // === ÉCRAN DE BIENVENUE ===
        static void AfficherEcranBienvenue()
        {
            Console.Clear();
            Console.ForegroundColor = ConsoleColor.Cyan;

            // === En-tête institution ===
            CentrerTexte("═══════════════════════════════════════════════════════════════════════════════════════════════");
            CentrerTexte("     CAMPUS HENRY CHRISTOPHE DE LIMONADE (CHC-L)");
            CentrerTexte("     Faculté des Sciences et de Génie (FSG)");
            CentrerTexte("═══════════════════════════════════════════════════════════════════════════════════════════════\n");

            // === Titre principal ===
            Console.ForegroundColor = ConsoleColor.Yellow;
            CentrerTexte("SYSTÈME D'ANALYSE FINANCIÈRE");
            CentrerTexte("DES ENTREPRISES HAÏTIENNES");
            CentrerTexte("Année fiscale 2024 - 2025\n");

            Console.ForegroundColor = ConsoleColor.Gray;

            // === Préparé par ===
            Console.WriteLine();
            CentrerTexte("Préparé par :");
            CentrerTexte(" ALBIKENDY JEAN | Bendy SERVILUS | Blemy JOSEPH\n");

            // === Professeur ===
            CentrerTexte("Soumis au professeur : Jaures PIERRE\n");

            // === Date ===
            CentrerTexte("Date de remise : 19 octobre 2025\n");

            // === Attente utilisateur ===
            Console.ForegroundColor = ConsoleColor.Green;
            CentrerTexte("Appuyez sur une touche pour continuer...");
            Console.ReadKey();

            // === Animation de chargement ===
            AnimationChargement();
        }

        static void AnimationChargement()
        {
            Console.Clear();
            Console.ForegroundColor = ConsoleColor.Yellow;
            CentrerTexte("Chargement du système...\n");
            Console.ResetColor();

            int leftMargin = (Console.WindowWidth - 22) / 2;

            Console.CursorLeft = leftMargin;
            for (int j = 0; j < 22; j++)
            {
                Console.ForegroundColor = (j % 2 == 0) ? ConsoleColor.Green : ConsoleColor.Cyan;
                Console.Write("");
                Thread.Sleep(60);
            }

            Console.ResetColor();
            Thread.Sleep(400);
            Console.Clear();
        }

        // === FONCTION POUR CENTRER UN TEXTE ===
        static void CentrerTexte(string texte, ConsoleColor couleur = ConsoleColor.Cyan)
        {
            int largeurFixe = 100;
            int espaces = (largeurFixe - texte.Length) / 2;
            Console.ForegroundColor = couleur;
            Console.WriteLine(new string(' ', Math.Max(espaces, 0)) + texte);
            Console.ResetColor();
        }

        // === MENU PRINCIPAL ===
        static void AfficherMenu()
        {
            Console.Clear();
            Console.ForegroundColor = ConsoleColor.Cyan;

            string titre = "SYSTÈME D'ANALYSE FINANCIÈRE DES ENTREPRISES HAÏTIENNES";
            int largeur = 85;
            string ligneHaut = "╔" + new string('═', largeur) + "╗";
            string ligneMilieu = "╠" + new string('═', largeur) + "╣";
            string ligneBas = "╚" + new string('═', largeur) + "╝";

            Console.WriteLine();
            Console.WriteLine("  " + ligneHaut);
            Console.WriteLine("  ║" + titre.PadLeft((largeur + titre.Length) / 2).PadRight(largeur) + "║");
            Console.WriteLine("  " + ligneMilieu);

            Console.ForegroundColor = ConsoleColor.White;

            string[] options =
            {
                "1. Enregistrer une entreprise",
                "2. Afficher toutes les entreprises",
                "3. Afficher par secteur",
                "4. Modifier une entreprise",
                "5. Supprimer une entreprise",
                "6. Restaurer une entreprise",
                "7. Trier les entreprises",
                "8. Rapport global",
                "9. Analyse : Plus haut revenu",
                "10. Analyse : Plus bas revenu",
                "11. Bénéfices sectoriels et globaux",
                "0. Quitter"
            };

            foreach (string option in options)
            {
                Console.WriteLine($"  ║  {option.PadRight(largeur - 2)}║");
            }

            Console.ForegroundColor = ConsoleColor.Cyan;
            Console.WriteLine("  " + ligneBas);

            Console.ResetColor();
            Console.Write("\n  Votre choix : ");
        }

        // === AUTRES MÉTHODES  ===
        static void InitialiserDonneesExemple(GestionnaireEntreprises gestionnaire)
        {
            var tech1 = new EntrepriseTechnologie(1, "TechHaiti", "Port-au-Prince", 5000000, 3000000,
                "Jean Pierre", new DateTime(2020, 1, 15), 50, 1000000, 5);
            
            var tech2 = new EntrepriseTechnologie(2, "Innovation Caraibes", "Petion-Ville", 3000000, 2500000,
                "Marie Laurent", new DateTime(2019, 5, 20), 30, 800000, 3);

            var sante1 = new EntrepriseSante(3, "Hopital Universel", "Delmas", 8000000, 6000000,
                "Dr. Marc Antoine", new DateTime(2018, 3, 10), 5, "ISO 13485");
            
            var sante2 = new EntrepriseSante(4, "Laboratoires Modernes", "Tabarre", 4000000, 3500000,
                "Dr. Sophie Joseph", new DateTime(2021, 7, 5), 3, "FDA Approved");

            var finance1 = new EntrepriseFinance(5, "Banque Capital", "Bourdon", 12000000, 9000000,
                "Paul Desrosiers", new DateTime(2015, 1, 1), 50000000, 15000, 0.15m);
            
            var finance2 = new EntrepriseFinance(6, "Investissements Caraibes", "Petion-Ville", 6000000, 4500000,
                "Lucie Fontaine", new DateTime(2020, 9, 12), 20000000, 8000, 0.12m); 

            gestionnaire.EnregistrerEntreprise(tech1, "technologie");
            gestionnaire.EnregistrerEntreprise(tech2, "technologie");
            gestionnaire.EnregistrerEntreprise(sante1, "sante");
            gestionnaire.EnregistrerEntreprise(sante2, "sante");
            gestionnaire.EnregistrerEntreprise(finance1, "finance");
            gestionnaire.EnregistrerEntreprise(finance2, "finance");
        }

        static void EnregistrerEntreprise(GestionnaireEntreprises gestionnaire)
        {
            Console.Clear();
            Console.WriteLine("=== ENREGISTRER UNE ENTREPRISE ===");
            
            // La saisie du Secteur (technologie/sante/finance)
            string secteur = Utilitaires.LireSecteurValide();
            
            // La Saisie de l'id
            int id = gestionnaire.GenererNouvelId(secteur);
            Console.WriteLine($"ID attribue automatiquement: {id}");
            
            // la saisie du nom
            string nom = Utilitaires.LireTexteNonVide("Nom");

            // La saisie de l'Adresse
            string adresse = Utilitaires.LireTexteNonVide("Adresse");

            // La saisie du Revenu
            decimal revenu = Utilitaires.LireDecimal("Revenu");

            // La saisie du Depense
            decimal depense = Utilitaires.LireDecimal("Depense");

            // La saisie du PDG
            string pdg = Utilitaires.LireTexteNonVide("PDG");

            // La saisie de la Date de creation (yyyy-mm-dd)
            DateTime dateCreation = Utilitaires.LireDateTime("Date de creation (format: yyyy-MM-dd)");

            Entreprise nouvelleEntreprise;

            switch (secteur.ToLower())
            {
                case "technologie":
                    // La saisie du Nombre d'employes tech
                    int nbEmployesTech = Utilitaires.LireEntier("Nombre d'employes tech");

                    // La saisie du Budget
                    decimal budget = Utilitaires.LireDecimal("Budget");

                    // La saisie du Nombre de brevets
                    int nbBrevets =Utilitaires.LireEntier("Nombre de brevets");

                    nouvelleEntreprise = new EntrepriseTechnologie(id, nom, adresse, revenu, depense, pdg, dateCreation, nbEmployesTech, budget, nbBrevets);
                    break;

                case "sante":
                    // La saisie du Nombre de laboratoire
                    int nbLabos = Utilitaires.LireEntier("Nombre de laboratoires");

                    // La saisie de laCertification sanitaire 
                    string certification = Utilitaires.LireTexteNonVide("Certification sanitaire");

                    nouvelleEntreprise = new EntrepriseSante(id, nom, adresse, revenu, depense, pdg, dateCreation, nbLabos, certification);
                    break;

                case "finance":
                    // La saisie du Capital social
                    decimal capital = Utilitaires.LireDecimal("Capital social");

                    // La saisie du Nombre de clients
                    int nbClients = Utilitaires.LireEntier("Nombre de clients");

                    // La saisie du Rendement d'investissement
                    decimal rendement = Utilitaires.LireDecimal("Rendement d'investissement");

                    nouvelleEntreprise = new EntrepriseFinance(id, nom, adresse, revenu, depense, pdg, dateCreation, capital, nbClients, rendement);
                    break;

                default:
                    Console.WriteLine("Secteur invalide.");
                    return;
            }

            gestionnaire.EnregistrerEntreprise(nouvelleEntreprise, secteur);
            Utilitaires.Success("Entreprise enregistrée avec succès!");
        }

        static void AfficherParSecteur(GestionnaireEntreprises gestionnaire)
        {
            Console.Write("Secteur (technologie/sante/finance): ");
            string secteur = Console.ReadLine();
            gestionnaire.AfficherParSecteur(secteur);
        }

        static void ModifierEntreprise(GestionnaireEntreprises gestionnaire)
        {
            Console.WriteLine("=== MODIFIER UNE ENTREPRISE ===");
    
            string secteur = Utilitaires.LireSecteurValide();
    
            Console.Write("ID de l'entreprise a modifier: ");
            if (int.TryParse(Console.ReadLine(), out int id) && id > 0)
            {
                gestionnaire.ModifierEntreprise(id, secteur);
            }
            else
            {
                Console.WriteLine("ID invalide.");
            }
        }
        
        static void SupprimerEntreprise(GestionnaireEntreprises gestionnaire)
        {
            Console.Write("Secteur (technologie/sante/finance): ");
            string secteur = Console.ReadLine();

            Console.Write("ID de l'entreprise à supprimer: ");
            int id = int.Parse(Console.ReadLine());

            gestionnaire.SupprimerEntreprise(id, secteur);
        }

        static void RestaurerEntreprise(GestionnaireEntreprises gestionnaire)
        {
            Console.Write("Secteur (technologie/sante/finance): ");
            var secteur = Console.ReadLine();
            gestionnaire.RestaurerEntreprises(secteur);
        }

        static void TrierEntreprises(GestionnaireEntreprises gestionnaire)
        {
            Console.Write("Secteur (technologie/sante/finance/tous): ");
            string secteur = Console.ReadLine();

            Console.Write("Ordre (croissant/decroissant): ");
            string ordre = Console.ReadLine();

            bool ordreCroissant = ordre.ToLower() == "croissant";
            gestionnaire.TrierEntreprises(secteur, ordreCroissant);
        }
    }
}
