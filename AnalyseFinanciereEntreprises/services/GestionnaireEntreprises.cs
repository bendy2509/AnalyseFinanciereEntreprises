using AnalyseFinanciereEntreprises.models;

namespace AnalyseFinanciereEntreprises.services
{
    public class GestionnaireEntreprises
    {
        // Dictionnaires pour chaque secteur
        private Dictionary<int, EntrepriseTechnologie> entreprisesTech = new Dictionary<int, EntrepriseTechnologie>();
        private Dictionary<int, EntrepriseSante> entreprisesSante = new Dictionary<int, EntrepriseSante>();
        private Dictionary<int, EntrepriseFinance> entreprisesFinance = new Dictionary<int, EntrepriseFinance>();

        // Dictionnaire pour les elements supprimes (pour restauration)
        private Dictionary<string, List<Entreprise>> elementsSupprimes = new Dictionary<string, List<Entreprise>>();

        public GestionnaireEntreprises()
        {
            elementsSupprimes["Technologie"] = new List<Entreprise>();
            elementsSupprimes["Sante"] = new List<Entreprise>();
            elementsSupprimes["Finance"] = new List<Entreprise>();
        }

        // 1. Methode Enregistrer
        public void EnregistrerEntreprise(Entreprise entreprise, string secteur)
        {
            switch (secteur.ToLower())
            {
                case "technologie":
                    if (entreprise is EntrepriseTechnologie tech)
                        entreprisesTech[tech.ID] = tech;
                    break;
                case "sante":
                    if (entreprise is EntrepriseSante sante)
                        entreprisesSante[sante.ID] = sante;
                    break;
                case "finance":
                    if (entreprise is EntrepriseFinance finance)
                        entreprisesFinance[finance.ID] = finance;
                    break;
            }
        }

        // 2. Methode Afficher toutes les entreprises
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

        // 3. Methode AfficherParSecteur
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
            }
        }

        // 4. Methode Modifier (a implementer)
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
                Console.WriteLine($"{i + 1}. {supprimees[i].Nom} (ID: {supprimees[i].ID})");
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

            var entreprisesTriees = ordreCroissant ? 
                entreprises.OrderBy(e => e.Nom) : 
                entreprises.OrderByDescending(e => e.Nom);

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
        
                Console.WriteLine($"ID: {entreprise.ID} | Nom: {entreprise.Nom} | " +
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