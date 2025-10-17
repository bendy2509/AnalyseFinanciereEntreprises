using AnalyseFinanciereEntreprises.models;

namespace AnalyseFinanciereEntreprises.services
{
    public class GestionnaireEntreprises
    {
        // Dictionnaires pour chaque secteur
        private Dictionary<int, EntrepriseTechnologie> entreprisesTech = new();
        private Dictionary<int, EntrepriseSante> entreprisesSante = new();
        private Dictionary<int, EntrepriseFinance> entreprisesFinance = new();

        // Dictionnaire pour les elements supprimes (pour restauration)
        private Dictionary<string, List<Entreprise>> elementsSupprimes = new();

        public GestionnaireEntreprises()
        {
            elementsSupprimes["Technologie"] = new List<Entreprise>();
            elementsSupprimes["Sante"] = new List<Entreprise>();
            elementsSupprimes["Finance"] = new List<Entreprise>();
        }

        // Methode Enregistrer
        public void EnregistrerEntreprise(Entreprise entreprise, string secteur)
        {
            try
            {
                // Verification secteur
                if (string.IsNullOrWhiteSpace(secteur))
                {
                    Console.WriteLine("Erreur: Le secteur ne peut pas etre vide.");
                    return;
                }

                string secteurNormalise = secteur.ToLower().Trim();

                switch (secteurNormalise)
                {
                    case "technologie":
                        EnregistrerEntrepriseTechnologie(entreprise);
                        break;

                    case "sante":
                        EnregistrerEntrepriseSante(entreprise);
                        break;

                    case "finance":
                        EnregistrerEntrepriseFinance(entreprise);
                        break;

                    default:
                        Console.WriteLine(
                            $"Erreur: Secteur '{secteur}' non reconnu. Utilisez: technologie, sante ou finance.");
                        break;
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Erreur inattendue lors de l'enregistrement: {ex.Message}");
            }
        }

        //Methode pour enregiatrer une entreprise tech
        private void EnregistrerEntrepriseTechnologie(Entreprise entreprise)
        {
            if (entreprise is EntrepriseTechnologie tech)
            {
                if (entreprisesTech.ContainsKey(tech.Id))
                {
                    Console.WriteLine(
                        $"Avertissement: L'ID {tech.Id} existe deja dans le secteur Technologie. Remplacement en cours...");
                }

                entreprisesTech[tech.Id] = tech;
                Console.WriteLine($"Entreprise technologique '{tech.Nom}' enregistree (ID: {tech.Id})");
            }
            else
            {
                Console.WriteLine("Erreur: Type d'entreprise incompatible avec le secteur Technologie.");
            }
        }

        //Methode pour enregiatrer une entreprise Sante
        private void EnregistrerEntrepriseSante(Entreprise entreprise)
        {
            if (entreprise is EntrepriseSante sante)
            {
                if (entreprisesSante.ContainsKey(sante.Id))
                {
                    Console.WriteLine(
                        $"Avertissement: L'ID {sante.Id} existe deja dans le secteur Sante. Remplacement...");
                }

                entreprisesSante[sante.Id] = sante;
                Console.WriteLine($"Entreprise sante '{sante.Nom}' enregistree (ID: {sante.Id})");
            }
            else
            {
                Console.WriteLine("Erreur: Type d'entreprise incompatible avec le secteur Sante.");
            }
        }

        //Methode pour enregiatrer une entreprise Finance
        private void EnregistrerEntrepriseFinance(Entreprise entreprise)
        {
            if (entreprise is EntrepriseFinance finance)
            {
                if (entreprisesFinance.ContainsKey(finance.Id))
                {
                    Console.WriteLine(
                        $"Avertissement: L'ID {finance.Id} existe deja dans le secteur Finance. Remplacement...");
                }

                entreprisesFinance[finance.Id] = finance;
                Console.WriteLine($"Entreprise finance '{finance.Nom}' enregistree (ID: {finance.Id})");
            }
            else
            {
                Console.WriteLine("Erreur: Type d'entreprise incompatible avec le secteur Finance.");
            }
        }

        // Methode Afficher toutes les entreprises
        public void AfficherToutesEntreprises()
        {
            Console.WriteLine("=== TOUTES LES ENTREPRISES ===");

            Console.WriteLine("\n--- Secteur Technologie ---");
            foreach (var entreprise in entreprisesTech.Values)
                entreprise.AfficherInfos();

            Console.WriteLine("\n--- Secteur Sante ---");
            foreach (var entreprise in entreprisesSante.Values)
                entreprise.AfficherInfos();

            Console.WriteLine("\n--- Secteur Finance ---");
            foreach (var entreprise in entreprisesFinance.Values)
                entreprise.AfficherInfos();
        }

        // Methode AfficherParSecteur
        public void AfficherParSecteur(string secteur)
        {
            Console.WriteLine($"=== ENTREPRISES DU SECTEUR {secteur.ToUpper()} ===");

            switch (secteur.ToLower())
            {
                case "technologie":
                    foreach (var entreprise in entreprisesTech.Values)
                        entreprise.AfficherInfos();
                    break;
                case "sante":
                    foreach (var entreprise in entreprisesSante.Values)
                        entreprise.AfficherInfos();
                    break;
                case "finance":
                    foreach (var entreprise in entreprisesFinance.Values)
                        entreprise.AfficherInfos();
                    break;
                default:
                    Console.WriteLine($"L'entreprise {secteur} est inconnu.");
                    break;
            };
        }

        //Methode Modifier (a implementer)
        public void ModifierEntreprise(int id, string secteur)
        {
            // Implementation de la modification
            Console.WriteLine("Fonctionnalite de modification a implementer");
        }

        // 5. Methode Supprimer
        public void SupprimerEntreprise(int id, string secteur)
        {
            Entreprise entrepriseASupprimer = null;

            switch (secteur.ToLower())
            {
                case "technologie":
                    if (entreprisesTech.TryGetValue(id, out var tech))
                    {
                        entrepriseASupprimer = tech;
                        entreprisesTech.Remove(id);
                    }

                    break;
                case "sante":
                    if (entreprisesSante.TryGetValue(id, out var sante))
                    {
                        entrepriseASupprimer = sante;
                        entreprisesSante.Remove(id);
                    }

                    break;
                case "finance":
                    if (entreprisesFinance.TryGetValue(id, out var finance))
                    {
                        entrepriseASupprimer = finance;
                        entreprisesFinance.Remove(id);
                    }

                    break;
            }

            if (entrepriseASupprimer != null)
            {
                elementsSupprimes[secteur].Add(entrepriseASupprimer);
                Console.WriteLine($"Entreprise {id} supprimee avec succes.");
            }
            else
            {
                Console.WriteLine("Entreprise non trouvee.");
            }
        }

        // 6. Methode Restaurer
        public void RestaurerEntreprises(string secteur)
        {
            var supprimees = elementsSupprimes[secteur];
            if (supprimees.Count == 0)
            {
                Console.WriteLine($"Aucune entreprise a restaurer dans le secteur {secteur}.");
                return;
            }

            Console.WriteLine($"Entreprises supprimees dans le secteur {secteur}:");
            for (int i = 0; i < supprimees.Count; i++)
            {
                Console.WriteLine($"{i + 1}. {supprimees[i].Nom} (ID: {supprimees[i].Id})");
            }

            Console.Write("Entrez le numero de l'entreprise a restaurer: ");
            if (int.TryParse(Console.ReadLine(), out int choix) && choix > 0 && choix <= supprimees.Count)
            {
                var entreprise = supprimees[choix - 1];
                EnregistrerEntreprise(entreprise, secteur);
                supprimees.RemoveAt(choix - 1);
                Console.WriteLine("Entreprise restauree avec succes.");
            }
        }

        // 7. Methode Trier
        public void TrierEntreprises(string secteur, bool ordreCroissant = true)
        {
            IEnumerable<Entreprise> entreprises = secteur.ToLower() switch
            {
                "technologie" => entreprisesTech.Values.Cast<Entreprise>(),
                "sante" => entreprisesSante.Values.Cast<Entreprise>(),
                "finance" => entreprisesFinance.Values.Cast<Entreprise>(),
                "tous" => entreprisesTech.Values.Cast<Entreprise>()
                    .Concat(entreprisesSante.Values.Cast<Entreprise>())
                    .Concat(entreprisesFinance.Values.Cast<Entreprise>()),
                _ => Enumerable.Empty<Entreprise>()
            };

            var entreprisesTriees =
                ordreCroissant ? entreprises.OrderBy(e => e.Nom) : entreprises.OrderByDescending(e => e.Nom);

            Console.WriteLine($"=== ENTREPRISES TRIEES PAR NOM ({(ordreCroissant ? "CROISSANT" : "DECROISSANT")}) ===");
            foreach (var entreprise in entreprisesTriees)
            {
                entreprise.AfficherInfos();
            }
        }

        // Fonctionnalites supplementaires avec LINQ

        // 1. Rapport global
        public void GenererRapportGlobal()
        {
            var toutesEntreprises = entreprisesTech.Values.Cast<Entreprise>()
                .Concat(entreprisesSante.Values.Cast<Entreprise>())
                .Concat(entreprisesFinance.Values.Cast<Entreprise>());

            Console.WriteLine("=== RAPPORT GLOBAL ===");
            foreach (var entreprise in toutesEntreprises)
            {
                decimal benefice = entreprise.CalculerBenefice();
                string statut = benefice <= 0 ? "PERTE" : "BENEFICE";

                Console.WriteLine($"ID: {entreprise.Id} | Nom: {entreprise.Nom} | " +
                                  $"Adresse: {entreprise.Adresse} | Revenu: {entreprise.Revenu:C} | " +
                                  $"Depense: {entreprise.Depense:C} | {statut}: {Math.Abs(benefice):C}");
            }
        }

        // 2. Analyse financiere 1: Plus haut revenu
        public void AfficherPlusHautRevenu()
        {
            var toutesEntreprises = entreprisesTech.Values.Cast<Entreprise>()
                .Concat(entreprisesSante.Values.Cast<Entreprise>())
                .Concat(entreprisesFinance.Values.Cast<Entreprise>());

            var entreprisesParRevenu = toutesEntreprises.OrderByDescending(e => e.Revenu);

            Console.WriteLine("=== CLASSEMENT PAR PLUS HAUT REVENU ===");
            foreach (var entreprise in entreprisesParRevenu)
            {
                Console.WriteLine($"{entreprise.Nom}: {entreprise.Revenu:C}");
            }
        }

        // 3. Analyse financiere 2: Plus bas revenu
        public void AfficherPlusBasRevenu()
        {
            var toutesEntreprises = entreprisesTech.Values.Cast<Entreprise>()
                .Concat(entreprisesSante.Values.Cast<Entreprise>())
                .Concat(entreprisesFinance.Values.Cast<Entreprise>());

            var entreprisesParRevenu = toutesEntreprises.OrderBy(e => e.Revenu);

            Console.WriteLine("=== CLASSEMENT PAR PLUS BAS REVENU ===");
            foreach (var entreprise in entreprisesParRevenu)
            {
                Console.WriteLine($"{entreprise.Nom}: {entreprise.Revenu:C}");
            }
        }

        // Methode pour calculer les benefices sectoriels
        public void AfficherBeneficesSectoriels()
        {
            decimal beneficeTech = entreprisesTech.Values.Sum(e => e.CalculerBenefice());
            decimal beneficeSante = entreprisesSante.Values.Sum(e => e.CalculerBenefice());
            decimal beneficeFinance = entreprisesFinance.Values.Sum(e => e.CalculerBenefice());
            decimal beneficeGlobal = beneficeTech + beneficeSante + beneficeFinance;

            Console.WriteLine("=== BENEFICES SECTORIELS ET GLOBAUX ===");
            Console.WriteLine($"Secteur Technologie: {beneficeTech:C}");
            Console.WriteLine($"Secteur Sante: {beneficeSante:C}");
            Console.WriteLine($"Secteur Finance: {beneficeFinance:C}");
            Console.WriteLine($"BENEFICE GLOBAL: {beneficeGlobal:C}");
        }
    }
}