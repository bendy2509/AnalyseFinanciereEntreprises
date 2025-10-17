namespace AnalyseFinanciereEntreprises.models
{
    public class EntrepriseFinance : Entreprise
    {
        // Champs prives specifiques
        private decimal capitalSocial;
        private int nombreClients;
        private decimal rendementInvestissement;

        // Proprietes avec encapsulation
        public decimal CapitalSocial
        {
            get => capitalSocial;
            set => capitalSocial = value;
        }

        public int NombreClients
        {
            get => nombreClients;
            set => nombreClients = value;
        }

        public decimal RendementInvestissement
        {
            get => rendementInvestissement;
            set => rendementInvestissement = value;
        }

        // Constructeur
        public EntrepriseFinance(int id, string nom, string adresse, decimal revenu,
            decimal depense, string pdg, DateTime dateCreation,
            decimal capitalSocial, int nombreClients, decimal rendementInvestissement)
            : base(id, nom, adresse, revenu, depense, pdg, dateCreation)
        {
            CapitalSocial = capitalSocial;
            NombreClients = nombreClients;
            RendementInvestissement = rendementInvestissement;
        }

        // Implementation de la methode abstraite
        public override void AfficherInfosSpecifiques()
        {
            Console.WriteLine($"Capital social: {CapitalSocial:C}");
            Console.WriteLine($"Nombre de clients: {NombreClients}");
            Console.WriteLine($"Rendement d'investissement: {RendementInvestissement:P}");
        }

        // Redefinition de la methode AfficherInfos
        public override void AfficherInfos()
        {
            base.AfficherInfos();
            AfficherInfosSpecifiques();
            Console.WriteLine("---");
        }
    }
}