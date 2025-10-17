namespace AnalyseFinanciereEntreprises.models
{
    public class EntrepriseSante : Entreprise
    {
        // Champs prives specifiques
        private int nombreLaboratoires;
        private string certificationSanitaire;

        // Proprietes avec encapsulation
        public int NombreLaboratoires
        {
            get => nombreLaboratoires;
            set => nombreLaboratoires = value;
        }

        public string CertificationSanitaire
        {
            get => certificationSanitaire;
            set => certificationSanitaire = value;
        }

        // Constructeur
        public EntrepriseSante(int id, string nom, string adresse, decimal revenu,
            decimal depense, string pdg, DateTime dateCreation,
            int nombreLaboratoires, string certificationSanitaire)
            : base(id, nom, adresse, revenu, depense, pdg, dateCreation)
        {
            this.nombreLaboratoires = nombreLaboratoires;
            this.certificationSanitaire = certificationSanitaire;
        }

        // Implementation de la methode abstraite
        public override void AfficherInfosSpecifiques()
        {
            Console.WriteLine($"Nombre de laboratoires: {NombreLaboratoires}");
            Console.WriteLine($"Certification sanitaire: {CertificationSanitaire}");
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