namespace AnalyseFinanciereEntreprises.models
{
    public abstract class Entreprise
    {
        // Champs prives
        private int id;
        private string nom;
        private string adresse;
        private decimal revenu;
        private decimal depense;
        private string pdg;
        private DateTime dateCreation;

        // Proprietes avec encapsulation
        public int ID
        {
            get => id;
            set => id = value;
        }

        public string Nom
        {
            get => nom;
            set => nom = value;
        }

        public string Adresse
        {
            get => adresse;
            set => adresse = value;
        }

        public decimal Revenu
        {
            get => revenu;
            set => revenu = value;
        }

        public decimal Depense
        {
            get => depense;
            set => depense = value;
        }

        public string PDG
        {
            get => pdg;
            set => pdg = value;
        }

        public DateTime DateCreation
        {
            get => dateCreation;
            set => dateCreation = value;
        }

        // Constructeur protege
        protected Entreprise(int id, string nom, string adresse, decimal revenu, 
                           decimal depense, string pdg, DateTime dateCreation)
        {
            ID = id;
            this.nom = nom;
            this.adresse = adresse;
            Nom = nom;
            Adresse = adresse;
            Revenu = revenu;
            Depense = depense;
            this.pdg = pdg;
            PDG = pdg;
            DateCreation = dateCreation;
        }

        // Methode pour calculer le benefice/perte
        public decimal CalculerBenefice()
        {
            return Revenu - Depense;
        }

        // Methode abstraite pour afficher les informations specifiques
        public abstract void AfficherInfosSpecifiques();

        // Methode virtuelle pour afficher toutes les informations
        public virtual void AfficherInfos()
        {
            Console.WriteLine($"ID: {ID}");
            Console.WriteLine($"Nom: {Nom}");
            Console.WriteLine($"Adresse: {Adresse}");
            Console.WriteLine($"Revenu: {Revenu:C}");
            Console.WriteLine($"Depense: {Depense:C}");
            Console.WriteLine($"PDG: {PDG}");
            Console.WriteLine($"Date de creation: {DateCreation:dd/MM/yyyy}");
            
            decimal benefice = CalculerBenefice();
            string type = benefice <= 0 ? "PERTE" : "BENEFICE";
            Console.WriteLine($"{type}: {Math.Abs(benefice):C}");
        }
    }
}