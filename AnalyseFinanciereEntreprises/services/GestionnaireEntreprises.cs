using AnalyseFinanciereEntreprises.models;
using AnalyseFinanciereEntreprises.services;

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
            elementsSupprimes["technologie"] = new List<Entreprise>();
            elementsSupprimes["sante"] = new List<Entreprise>();
            elementsSupprimes["finance"] = new List<Entreprise>();
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
            }

            ;
        }

        //Methode Modifier
        public void ModifierEntreprise(int id, string secteur)
        {
            try
            {
                if (string.IsNullOrWhiteSpace(secteur))
                {
                    Console.WriteLine("Erreur: Secteur vide.");
                    return;
                }

                bool succes = secteur.ToLower().Trim() switch
                {
                    "technologie" => ModifierAvecTryCatch(id, entreprisesTech, ModifierInfosTechnologie),
                    "sante" => ModifierAvecTryCatch(id, entreprisesSante, ModifierInfosSante),
                    "finance" => ModifierAvecTryCatch(id, entreprisesFinance, ModifierInfosFinance),
                    _ => false
                };

                Console.WriteLine(succes ? "Modification reussie." : "Modification echouee.");
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Erreur: {ex.Message}");
            }
        }

        private bool ModifierAvecTryCatch<T>(int id, Dictionary<int, T> dict, Action<T> modifierSpecifique)
            where T : Entreprise
        {
            if (!dict.TryGetValue(id, out T entreprise))
            {
                Console.WriteLine($"ID {id} non trouve.");
                return false;
            }

            Console.WriteLine($"\nModification: {entreprise.Nom} (ID: {id})");

            ModifierInfosBase(entreprise);
            modifierSpecifique(entreprise);

            return true;
        }

        private void ModifierInfosBase(Entreprise e)
        {
            Console.WriteLine("\nInfos base:");
            e.Nom = Utilitaires.LireValeur($"Nom ({e.Nom})", e.Nom);
            e.Adresse = Utilitaires.LireValeur($"Adresse ({e.Adresse})", e.Adresse);
            e.Revenu = Utilitaires.LireDecimal($"Revenu ({e.Revenu:C})", e.Revenu);
            e.Depense = Utilitaires.LireDecimal($"Depense ({e.Depense:C})", e.Depense);
            e.Pdg = Utilitaires.LireValeur($"PDG ({e.Pdg})", e.Pdg);
        }

        private void ModifierInfosTechnologie(EntrepriseTechnologie t)
        {
            Console.WriteLine("\nInfos tech:");
            t.NombreEmployesTech = Utilitaires.LireInt($"Employes tech ({t.NombreEmployesTech})", t.NombreEmployesTech);
            t.Budget = Utilitaires.LireDecimal($"Budget ({t.Budget:C})", t.Budget);
            t.NombreBrevets = Utilitaires.LireInt($"Brevets ({t.NombreBrevets})", t.NombreBrevets);
        }

        private void ModifierInfosSante(EntrepriseSante s)
        {
            Console.WriteLine("\nInfos sante:");
            s.NombreLaboratoires = Utilitaires.LireInt($"Laboratoires ({s.NombreLaboratoires})", s.NombreLaboratoires);
            s.CertificationSanitaire =
                Utilitaires.LireValeur($"Certification ({s.CertificationSanitaire})", s.CertificationSanitaire);
        }

        private void ModifierInfosFinance(EntrepriseFinance f)
        {
            Console.WriteLine("\nInfos finance:");
            f.CapitalSocial = Utilitaires.LireDecimal($"Capital ({f.CapitalSocial:C})", f.CapitalSocial);
            f.NombreClients = Utilitaires.LireInt($"Clients ({f.NombreClients})", f.NombreClients);
            f.RendementInvestissement =
                Utilitaires.LireDecimal($"Rendement ({f.RendementInvestissement:P2})", f.RendementInvestissement);
        }

        //Methode Supprimer
        public void SupprimerEntreprise(int id, string secteur)
        {
            Entreprise entrepriseASupprimer = null;

            try
            {
                if (string.IsNullOrWhiteSpace(secteur))
                {
                    Console.WriteLine($"Erreur: Le secteur ne peut pas etre vide.");
                    return;
                }

                switch (secteur.ToLower().Trim())
                {
                    case "technologie":
                        entrepriseASupprimer = GererSuppressionAvecTryCatch(
                            id,
                            entreprisesTech,
                            $"Entreprise avec ID {id} non trouvee dans le secteur Technologie.",
                            $"Erreur lors de la suppression dans le secteur Technologie: "
                        );
                        break;

                    case "sante":
                        entrepriseASupprimer = GererSuppressionAvecTryCatch(
                            id,
                            entreprisesSante,
                            $"Entreprise avec ID {id} non trouvee dans le secteur Sante.",
                            $"Erreur lors de la suppression dans le secteur Sante: "
                        );
                        break;

                    case "finance":
                        entrepriseASupprimer = GererSuppressionAvecTryCatch(
                            id,
                            entreprisesFinance,
                            $"Entreprise avec ID {id} non trouvee dans le secteur Finance.",
                            $"Erreur lors de la suppression dans le secteur Finance: "
                        );
                        break;

                    default:
                        Console.WriteLine($"Erreur: Le secteur '{secteur}' est inexistant.");
                        break;
                }

                if (entrepriseASupprimer != null)
                {
                    elementsSupprimes[secteur].Add(entrepriseASupprimer);
                    Console.WriteLine($"Entreprise '{entrepriseASupprimer.Nom}' (ID: {id}) supprimee avec succes.");
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Erreur inattendue lors de la suppression: {ex.Message}");
            }
        }

        private T GererSuppressionAvecTryCatch<T>(
            int id,
            Dictionary<int, T> dictionnaire,
            string messageNonTrouve,
            string messageErreur) where T : Entreprise
        {
            try
            {
                if (dictionnaire.TryGetValue(id, out T entreprise))
                {
                    dictionnaire.Remove(id);
                    return entreprise;
                }
                else
                {
                    Console.WriteLine(messageNonTrouve);
                    return null;
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine($"{messageErreur}{ex.Message}");
                return null;
            }
        }

        // Methode Restaurer
        public void RestaurerEntreprises(string secteur)
        {
            try
            {
                if (string.IsNullOrWhiteSpace(secteur) ||
                    !elementsSupprimes.TryGetValue(secteur.ToLower().Trim(), out var supprimees))
                {
                    Console.WriteLine($"Erreur: Secteur '{secteur}' invalide.");
                    return;
                }

                if (supprimees.Count == 0)
                {
                    Console.WriteLine($"Aucune entreprise a restaurer dans le secteur {secteur}.");
                    return;
                }

                bool continuer = true;

                while (continuer && supprimees.Count > 0)
                {
                    AfficherListeRestaurable(supprimees, secteur);

                    Console.Write("Entrez le numero a restaurer (0=stop, 'tous'=tout restaurer): ");
                    var input = Console.ReadLine()?.ToLower().Trim();

                    switch (input)
                    {
                        case "0":
                            continuer = false;
                            Console.WriteLine("Operation terminee.");
                            break;

                        case "tous":
                            RestaurerToutes(supprimees, secteur);
                            continuer = false;
                            break;

                        default:
                        {
                            if (int.TryParse(input, out int choix) && choix > 0 && choix <= supprimees.Count)
                            {
                                RestaurerUneEntreprise(supprimees, choix - 1, secteur);
                            }
                            else
                            {
                                Console.WriteLine("Choix invalide.");
                            }

                            break;
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Erreur lors de la restauration: {ex.Message}");
            }
        }

        private void AfficherListeRestaurable(List<Entreprise> supprimees, string secteur)
        {
            Console.WriteLine($"\nEntreprises supprimees dans le secteur {secteur} ({supprimees.Count} restantes):");
            for (int i = 0; i < supprimees.Count; i++)
            {
                var entreprise = supprimees[i];
                decimal benefice = entreprise.CalculerBenefice();
                string statut = benefice <= 0 ? "PERTE" : "BENEFICE";

                // Vérifier si l'ID est deja utilise
                string conflit = IdExisteDeja(entreprise.Id, secteur) ? " [CONFLIT ID]" : "";

                Console.WriteLine(
                    $"{i + 1}. {entreprise.Nom} (ID: {entreprise.Id}) - {statut}: {Math.Abs(benefice):C}{conflit}");
            }
        }

        private void RestaurerUneEntreprise(List<Entreprise> supprimees, int index, string secteur)
        {
            var entreprise = supprimees[index];

            if (IdExisteDeja(entreprise.Id, secteur))
            {
                Console.WriteLine($"Conflit: L'ID {entreprise.Id} est deja utilise dans le secteur {secteur}.");
                Console.WriteLine("Options:");
                Console.WriteLine("1. Generer un nouvel ID automatique");
                Console.WriteLine("2. Changer l'ID manuellement");
                Console.WriteLine("3. Annuler la restauration");
                Console.Write("Votre choix: ");

                var option = Console.ReadLine();
                switch (option)
                {
                    case "1":
                        int nouvelId = GenererNouvelId(secteur);
                        entreprise.Id = nouvelId;
                        EnregistrerEntreprise(entreprise, secteur);
                        supprimees.RemoveAt(index);
                        Console.WriteLine($"Entreprise '{entreprise.Nom}' restauree avec le nouvel ID: {nouvelId}");
                        break;

                    case "2":
                        Console.Write($"Entrez le nouvel ID pour '{entreprise.Nom}': ");
                        if (int.TryParse(Console.ReadLine(), out int idManuel) && idManuel > 0)
                        {
                            if (IdExisteDeja(idManuel, secteur))
                            {
                                Console.WriteLine("Erreur: Cet ID est deja utilise. Restauration annulee.");
                            }
                            else
                            {
                                entreprise.Id = idManuel;
                                EnregistrerEntreprise(entreprise, secteur);
                                supprimees.RemoveAt(index);
                                Console.WriteLine($"Entreprise '{entreprise.Nom}' restauree avec l'ID: {idManuel}");
                            }
                        }
                        else
                        {
                            Console.WriteLine("ID invalide. Restauration annulee.");
                        }

                        break;
                    default:
                        Console.WriteLine("Restauration annulee.");
                        break;
                }
            }
            else
            {
                EnregistrerEntreprise(entreprise, secteur);
                supprimees.RemoveAt(index);
                Console.WriteLine($"Entreprise '{entreprise.Nom}' restauree avec succes!");
            }
        }

        private void RestaurerToutes(List<Entreprise> supprimees, string secteur)
        {
            int compteur = 0;
            int conflits = 0;
            var aRestaurer = supprimees.ToList();

            foreach (var entreprise in aRestaurer)
            {
                if (!IdExisteDeja(entreprise.Id, secteur))
                {
                    EnregistrerEntreprise(entreprise, secteur);
                    supprimees.Remove(entreprise);
                    compteur++;
                    Console.WriteLine($"SUCCES: '{entreprise.Nom}' restauree (ID: {entreprise.Id})");
                }
                else
                {
                    // Pour la restauration multiple, on genere automatiquement un nouvel ID
                    int nouvelId = GenererNouvelId(secteur);
                    entreprise.Id = nouvelId;
                    EnregistrerEntreprise(entreprise, secteur);
                    supprimees.Remove(entreprise);
                    compteur++;
                    Console.WriteLine(
                        $"SUCCES: '{entreprise.Nom}' restauree avec nouvel ID: {nouvelId} (ancien ID en conflit)");
                }
            }

            Console.WriteLine(
                $"\nRestauration terminee: {compteur} entreprises restaurees, {conflits} conflits resolus.");
        }

        private int GenererNouvelId(string secteur)
        {
            // Trouver le prochain ID disponible dans le secteur
            int maxId = GetMaxIdDansSecteur(secteur);
            int maxIdSupprime = elementsSupprimes[secteur].Count > 0 ? elementsSupprimes[secteur].Max(e => e.Id) : 0;

            return Math.Max(maxId, maxIdSupprime) + 1;
        }

        private int GetMaxIdDansSecteur(string secteur)
        {
            return secteur.ToLower() switch
            {
                "technologie" => entreprisesTech.Keys.Count > 0 ? entreprisesTech.Keys.Max() : 0,
                "sante" => entreprisesSante.Keys.Count > 0 ? entreprisesSante.Keys.Max() : 0,
                "finance" => entreprisesFinance.Keys.Count > 0 ? entreprisesFinance.Keys.Max() : 0,
                _ => 0
            };
        }

        private bool IdExisteDeja(int id, string secteur)
        {
            return secteur.ToLower() switch
            {
                "technologie" => entreprisesTech.ContainsKey(id),
                "sante" => entreprisesSante.ContainsKey(id),
                "finance" => entreprisesFinance.ContainsKey(id),
                _ => false
            };
        }

        //Methode Trier
        public void TrierEntreprises(string secteur, bool ordreCroissant = true)
        {
            var entreprises = secteur.ToLower() switch
            {
                "technologie" => entreprisesTech.Values,
                "sante" => entreprisesSante.Values,
                "finance" => entreprisesFinance.Values,
                "tous" => entreprisesTech.Values
                    .Concat(entreprisesSante.Values.Cast<Entreprise>())
                    .Concat(entreprisesFinance.Values),
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

        // Rapport global
        public void GenererRapportGlobal()
        {
            var toutesEntreprises = entreprisesTech.Values
                .Concat(entreprisesSante.Values.Cast<Entreprise>())
                .Concat(entreprisesFinance.Values);

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

        // Analyse financiere 1: Plus haut revenu
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

        // Analyse financiere 2: Plus bas revenu
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