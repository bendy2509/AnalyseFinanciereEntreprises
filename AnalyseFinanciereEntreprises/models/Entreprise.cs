namespace AnalyseFinanciereEntreprises.models
{
    public abstract class Entreprise
    {
        // Champs prives
        private int _id;
        private string _nom;
        private string _adresse;
        private decimal _revenu;
        private decimal _depense;
        private string _pdg;
        private DateTime _dateCreation;

        // Proprietes avec encapsulation
        public int Id
        {
            get => _id;
            set => _id = value;
        }

        public string Nom
        {
            get => _nom;
            set => _nom = value;
        }

        public string Adresse
        {
            get => _adresse;
            set => _adresse = value;
        }

        public decimal Revenu
        {
            get => _revenu;
            set => _revenu = value;
        }

        public decimal Depense
        {
            get => _depense;
            set => _depense = value;
        }

        public string Pdg
        {
            get => _pdg;
            set => _pdg = value;
        }

        public DateTime DateCreation
        {
            get => _dateCreation;
            set => _dateCreation = value;
        }

        // Constructeur protege
        protected Entreprise(int id, string nom, string adresse, decimal revenu, 
                           decimal depense, string pdg, DateTime dateCreation)
        {
            Id = id;
            this._nom = nom;
            this._adresse = adresse;
            this._revenu = revenu;
            this._depense = depense;
            this._pdg = pdg;
            this._dateCreation = dateCreation;
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
            Console.WriteLine($"ID: {Id}");
            Console.WriteLine($"Nom: {Nom}");
            Console.WriteLine($"Adresse: {Adresse}");
            Console.WriteLine($"Revenu: {Revenu:C}");
            Console.WriteLine($"Depense: {Depense:C}");
            Console.WriteLine($"PDG: {Pdg}");
            Console.WriteLine($"Date de creation: {DateCreation:dd/MM/yyyy}");
            
            decimal benefice = CalculerBenefice();
            string type = benefice <= 0 ? "PERTE" : "BENEFICE";
            Console.WriteLine($"{type}: {Math.Abs(benefice):C}");
        }
    }
}