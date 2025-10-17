namespace AnalyseFinanciereEntreprises.models
{
    public class EntrepriseTechnologie : Entreprise
    {
        // Champs prives specifiques
        private int nombreEmployesTech;
        private decimal budget;
        private int nombreBrevets;

        // Proprietes avec encapsulation
        public int NombreEmployesTech
        {
            get => nombreEmployesTech;
            set => nombreEmployesTech = value;
        }

        public decimal Budget
        {
            get => budget;
            set => budget = value;
        }

        public int NombreBrevets
        {
            get => nombreBrevets;
            set => nombreBrevets = value;
        }

        // Constructeur
        public EntrepriseTechnologie(int id, string nom, string adresse, decimal revenu,
            decimal depense, string pdg, DateTime dateCreation,
            int nombreEmployesTech, decimal budget, int nombreBrevets)
            : base(id, nom, adresse, revenu, depense, pdg, dateCreation)
        {
            this.nombreEmployesTech = nombreEmployesTech;
            this.budget = budget;
            this.nombreBrevets = nombreBrevets;
        }

        // Implementation de la methode abstraite
        public override void AfficherInfosSpecifiques()
        {
            Console.WriteLine($"Nombre d'employes tech: {NombreEmployesTech}");
            Console.WriteLine($"Budget: {Budget:C}");
            Console.WriteLine($"Nombre de brevets: {NombreBrevets}");
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