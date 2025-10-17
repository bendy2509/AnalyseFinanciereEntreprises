namespace AnalyseFinanciereEntreprises.models
{
    public class EntrepriseFinance : Entreprise
    {
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
        
        // Champs prives specifiques
        private decimal _capitalSocial;
        private int _nombreClients;
        private decimal _rendementInvestissement;

        // Proprietes avec encapsulation
        public decimal CapitalSocial
        {
            get => _capitalSocial;
            set => _capitalSocial = value;
        }

        public int NombreClients
        {
            get => _nombreClients;
            set => _nombreClients = value;
        }

        public decimal RendementInvestissement
        {
            get => _rendementInvestissement;
            set => _rendementInvestissement = value;
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