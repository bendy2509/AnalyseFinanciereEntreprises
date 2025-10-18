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
            //InitialiserDonneesExemple(gestionnaire);

            while (continuer)
            {
                AfficherMenu();
                string choix = Console.ReadLine();

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
                        gestionnaire.ModifierEntreprise(0, "");
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
            
            // En-tête institution
            CentrerTexte("CAMPUS HENRY CHRISTOPHE DE LIMONADE (CHC-L)");
            CentrerTexte("Faculté des Sciences et de Génie (FSG)");
            Console.WriteLine(new string('-', 120));  

            // Titre principal
            Console.WriteLine();
            CentrerTexte("SYSTÈME D'ANALYSE FINANCIÈRE DES ENTREPRISES HAÏTIENNES\n\n\n");
            Console.WriteLine();

            // Préparé par
            CentrerTexte("\n\nPréparé par : ALBIKENDY JEAN | Bendy SERVILUS | Blemy JOSEPH ");
            Console.WriteLine();

            // Professeur
            CentrerTexte("\nSoumis au professeur : Jaures PIERRE");
            Console.WriteLine();

            // Date
            Console.Write(new string(' ', 50));
            CentrerTexte("\n\n\t\t\t\t\t\t\t\t Date de remise : 19 octobre 2025\n");

            CentrerTexte("\n\t\t\t\tAppuyez sur une touche pour continuer...");
            Console.ReadKey();

            // Animation
            AnimationChargement();
        }

        // === ANIMATION DE CHARGEMENT ===
        static void AnimationChargement()
        {
            Console.Clear();
            CentrerTexte("Chargement du système...", ConsoleColor.Yellow);
            Console.WriteLine();

            for (int i = 0; i < 1; i++)
            {
                Console.Write(new string(' ', 40));
                for (int j = 0; j < 20; j++)
                {
                    Console.ForegroundColor = (j % 2 == 0) ? ConsoleColor.Red : ConsoleColor.Yellow;
                    Console.Write("»");
                    Thread.Sleep(50);
                }
                Console.WriteLine();
            }

            Console.ResetColor();
            Thread.Sleep(300);
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
            Console.WriteLine("\n  ╔══════════════════════════════════════════════════════════════════════════════╗");
            Console.WriteLine("  ║      SYSTÈME D'ANALYSE FINANCIÈRE DES ENTREPRISES HAÏTIENNES                 ║");
            Console.WriteLine("  ╠══════════════════════════════════════════════════════════════════════════════╣");
            Console.WriteLine("  ║  1. Enregistrer une entreprise                                               ║");
            Console.WriteLine("  ║  2. Afficher toutes les entreprises                                          ║");
            Console.WriteLine("  ║  3. Afficher par secteur                                                     ║");
            Console.WriteLine("  ║  4. Modifier une entreprise                                                  ║");
            Console.WriteLine("  ║  5. Supprimer une entreprise                                                 ║");
            Console.WriteLine("  ║  6. Restaurer une entreprise                                                 ║");
            Console.WriteLine("  ║  7. Trier les entreprises                                                    ║");
            Console.WriteLine("  ║  8. Rapport global                                                           ║");
            Console.WriteLine("  ║  9. Analyse : Plus haut revenu                                               ║");
            Console.WriteLine("  ║ 10. Analyse : Plus bas revenu                                                ║");
            Console.WriteLine("  ║ 11. Bénéfices sectoriels et globaux                                          ║");
            Console.WriteLine("  ║  0. Quitter                                                                  ║");
            Console.WriteLine("  ╚══════════════════════════════════════════════════════════════════════════════╝");

            Console.Write("\n  Votre choix : ");
        }

        // === AUTRES MÉTHODES DE TON PROGRAMME ===
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
            Console.WriteLine("=== ENREGISTRER UNE ENTREPRISE ===");
            Console.Write("Secteur (technologie/sante/finance): ");
            string secteur = Console.ReadLine();

            Console.Write("ID: ");
            int id = int.Parse(Console.ReadLine());

            Console.Write("Nom: ");
            string nom = Console.ReadLine();

            Console.Write("Adresse: ");
            string adresse = Console.ReadLine();

            Console.Write("Revenu: ");
            decimal revenu = decimal.Parse(Console.ReadLine());

            Console.Write("Depense: ");
            decimal depense = decimal.Parse(Console.ReadLine());

            Console.Write("PDG: ");
            string pdg = Console.ReadLine();

            Console.Write("Date de creation (yyyy-mm-dd): ");
            DateTime dateCreation = DateTime.Parse(Console.ReadLine());

            Entreprise nouvelleEntreprise;

            switch (secteur.ToLower())
            {
                case "technologie":
                    Console.Write("Nombre d'employes tech: ");
                    int nbEmployesTech = int.Parse(Console.ReadLine());

                    Console.Write("Budget: ");
                    decimal budget = decimal.Parse(Console.ReadLine());

                    Console.Write("Nombre de brevets: ");
                    int nbBrevets = int.Parse(Console.ReadLine());

                    nouvelleEntreprise = new EntrepriseTechnologie(id, nom, adresse, revenu, depense, pdg, dateCreation, nbEmployesTech, budget, nbBrevets);
                    break;

                case "sante":
                    Console.Write("Nombre de laboratoires: ");
                    int nbLabos = int.Parse(Console.ReadLine());

                    Console.Write("Certification sanitaire: ");
                    string certification = Console.ReadLine();

                    nouvelleEntreprise = new EntrepriseSante(id, nom, adresse, revenu, depense, pdg, dateCreation, nbLabos, certification);
                    break;

                case "finance":
                    Console.Write("Capital social: ");
                    decimal capital = decimal.Parse(Console.ReadLine());

                    Console.Write("Nombre de clients: ");
                    int nbClients = int.Parse(Console.ReadLine());

                    Console.Write("Rendement d'investissement (decimal): ");
                    decimal rendement = decimal.Parse(Console.ReadLine());

                    nouvelleEntreprise = new EntrepriseFinance(id, nom, adresse, revenu, depense, pdg, dateCreation, capital, nbClients, rendement);
                    break;

                default:
                    Console.WriteLine("Secteur invalide.");
                    return;
            }

            gestionnaire.EnregistrerEntreprise(nouvelleEntreprise, secteur);
            Console.WriteLine("Entreprise enregistrée avec succès!");
        }

        static void AfficherParSecteur(GestionnaireEntreprises gestionnaire)
        {
            Console.Write("Secteur (technologie/sante/finance): ");
            string secteur = Console.ReadLine();
            gestionnaire.AfficherParSecteur(secteur);
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
            string secteur = Console.ReadLine();
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
