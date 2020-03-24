using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

// string villeSejour, DateTime dateDepart, DateTime dateArrivee, double prixmin, double prixmax, int nbetoile, int nbpersonne
namespace TP2
{
    class Program
    {
        private const string CHAINE_IGNORER = "*";
        private const string CHAINE_VILLE_SEJOUR = "Ville de séjour";
        private const string CHAINE_VILLE_DEPART = "Date de départ";
        private const string CHAINE_VILLE_ARRIVEE = "Date d'arrivée";
        private const string CHAINE_PRIX_MINIMAL = "Prix minimal";
        private const string CHAINE_PRIX_MAXIMAL = "Prix maximal";
        private const string CHAINE_NOMBRE_ETOILE = "Nombre d'etoile";
        private const string CHAINE_NOMBRE_PERSONNE = "Nombre de personnes";
        private const string CHAINE_ANNEE = "Année";
        private const string CHAINE_MOIS = "Mois";
        private const string CHAINE_JOUR = "Jour";
        private const string CHAINE_RESULTAT_HOTEL = "Hotel : ";
        private const string CHAINE_RESULTAT_VILLE = "Ville : ";
        private const string CHAINE_RESULTAT_RUE = "Rue : ";
        private const string CHAINE_RESULTAT_PRIX = "Prix : ";
        private const string CHAINE_RESULTAT_NOMBRE_LIT = "Nombre de lit : ";
        private const string CHAINE_CHOIX_INVALIDE = "Choix invalide";
        private const string CHAINE_POUR_IGNORER = "(* pour ignorer) : ";
        private const string CHAINE_MENU = "Choix : \n" +
                                           "1) Rechercher\n" +
                                           "2) Réserver\n" +
                                           "3) Quitter\n" +
                                           "> ";

        private static Agence Agence { get; } = new Agence();

        private static bool verifierIgnorer(string entree)
        {
            if (string.Compare(entree, CHAINE_IGNORER) == 0)
            {
                return true;
            }

            return false;
        }

        private static string SaisirChaine(string libelle)
        {
            Console.WriteLine(libelle + CHAINE_POUR_IGNORER);

            string chaine = Console.ReadLine();

            if (verifierIgnorer(chaine))
            {
                return null;
            }

            return chaine;
        }

        private static int SaisirEntierPositif(string libelle)
        {
            Console.WriteLine(libelle + CHAINE_POUR_IGNORER);

            string entier = Console.ReadLine();

            if (verifierIgnorer(entier))
            {
                return -1;
            }

            return Int32.Parse(entier);
        }

        private static DateTime SaisirDate(string libelle)
        {
            Console.WriteLine(libelle + " : ");
            int year = SaisirEntierPositif(CHAINE_ANNEE);
            if (year == -1)
            {
                return default;
            }
            int month = SaisirEntierPositif(CHAINE_MOIS);
            int day = SaisirEntierPositif(CHAINE_JOUR);

            return new DateTime(year, month, day);
        }

        private static void MenuRechercher()
        {
            string villeSejour = SaisirChaine(CHAINE_VILLE_SEJOUR);
            DateTime dateDepart = SaisirDate(CHAINE_VILLE_DEPART);
            DateTime dateArrivee = SaisirDate(CHAINE_VILLE_ARRIVEE);
            double prixMin = SaisirEntierPositif(CHAINE_PRIX_MINIMAL);
            double prixMax = SaisirEntierPositif(CHAINE_PRIX_MAXIMAL);
            int nbEtoile = SaisirEntierPositif(CHAINE_NOMBRE_ETOILE);
            int nbPersonne = SaisirEntierPositif(CHAINE_NOMBRE_PERSONNE);

            List<Chambre> chambres = Agence.Rechercher(villeSejour, dateDepart, dateArrivee, prixMin, prixMax, nbEtoile, nbPersonne);

            for (int i = 0; i < chambres.Count; ++i)
            {
                Console.WriteLine(i + ")");
                AfficherResultat(chambres[i]);
                Console.WriteLine();
            }
        }

        private static void AfficherResultat(Chambre chambre)
        {
            Console.WriteLine(CHAINE_RESULTAT_HOTEL + chambre.Hotel.Nom);
            Console.WriteLine(CHAINE_RESULTAT_VILLE + chambre.Hotel.Ville);
            Console.WriteLine(CHAINE_RESULTAT_RUE + chambre.Hotel.Rue);
            Console.WriteLine(CHAINE_RESULTAT_PRIX + chambre.Prix);
            Console.WriteLine(CHAINE_RESULTAT_NOMBRE_LIT + chambre.Nblit);
        }

        private static void MenuReserver()
        {
            throw new NotImplementedException();
        }

        public static bool AfficherMenu()
        {  
            Console.WriteLine(CHAINE_MENU);
            
            int choix = Int32.Parse(Console.ReadLine());

            if (choix == 1)
            {
                MenuRechercher();
            } else if (choix == 2)
            {
                MenuReserver();
            } else if (choix == 3)
            {
                return false;
            } else
            {
                Console.WriteLine(CHAINE_CHOIX_INVALIDE);
            }

            return true;
        }

        static void Main(string[] args)
        {
            Hotel hotel1 = new Hotel("hilton", "france", "montpellier", "av", 75, 3);
            Hotel hotel2 = new Hotel("george", "usa", "losangeles", "street", 2, 5);
            hotel1.AjouterChambre(3, 80, 20);
            hotel1.AjouterChambre(5, 100, 50);
            hotel1.AjouterChambre(2, 60, 40);
            hotel2.AjouterChambre(3, 100, 40);
            hotel2.AjouterChambre(1, 35, 100);

            Agence.AjouterHotel(hotel1);
            Agence.AjouterHotel(hotel2);

            bool continuer = true;

            do
            {
                continuer = AfficherMenu();
            } while (continuer);
        }
    }
}
