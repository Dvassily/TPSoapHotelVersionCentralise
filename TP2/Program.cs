using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace TP2
{
    class Program
    {
        private const string CHAINE_IGNORER = "*";
        private const string CHAINE_VILLE_SEJOUR = "Ville de séjour";
        private const string CHAINE_DATE_DEPART = "Date de départ";
        private const string CHAINE_DATE_ARRIVEE = "Date d'arrivée";
        private const string CHAINE_PRIX_MINIMAL = "Prix minimal";
        private const string CHAINE_PRIX_MAXIMAL = "Prix maximal";
        private const string CHAINE_NOMBRE_ETOILE = "Nombre d'etoile";
        private const string CHAINE_NOMBRE_PERSONNE = "Nombre de personnes";
        private const string CHAINE_ANNEE = "Année format YYYY";
        private const string CHAINE_MOIS = "Mois format MM";
        private const string CHAINE_JOUR = "Jour format DD";
        private const string CHAINE_RESULTAT_HOTEL = "Hotel : ";
        private const string CHAINE_RESULTAT_VILLE = "Ville : ";
        private const string CHAINE_RESULTAT_RUE = "Rue : ";
        private const string CHAINE_RESULTAT_PRIX = "Prix : ";
        private const string CHAINE_RESULTAT_NOMBRE_LIT = "Nombre de lit : ";
        private const string CHAINE_CHOIX_INVALIDE = "Choix invalide";
        private const string CHAINE_POUR_IGNORER = " (* pour ignorer) : ";
        private const string CHAINE_MENU = "Choix : \n" +
                                           "1) Rechercher\n" +
                                           "2) Réserver\n" +
                                           "3) Consulter une réservation\n" +
                                           "4) Quitter\n" +
                                           "> ";
        private const string CHAINE_IDENTIFIANT_HOTEL = "Identifiant hotel";
        private const string CHAINE_IDENTIFIANT_CHAMBRE = "Identifiant chambre";
        private const string CHAINE_NOM_CLIENT = "Nom";
        private const string CHAINE_PRENOM_CLIENT = "Prenom";
        private const string CHAINE_NUMERO_CARTE_BANCAIRE = "Numero de carte bancaire";
        private const string CHAINE_NUMERO_RESERVATION = "Numéro de reservation";

        private static Agence Agence { get; } = new Agence();

        private static bool verifierIgnorer(string entree)
        {
            if (string.Compare(entree, CHAINE_IGNORER) == 0)
            {
                return true;
            }

            return false;
        }

        private static string SaisirChaine(string libelle, bool demanderIgnorer)
        {
            string affichage = libelle;

            if (demanderIgnorer)
            {
                affichage += CHAINE_POUR_IGNORER;
            }

            affichage += " : ";

            Console.WriteLine(affichage);

            string chaine = Console.ReadLine();

            if (demanderIgnorer && verifierIgnorer(chaine))
            {
                return null;
            }

            return chaine;
        }

        private static int SaisirEntierPositif(string libelle, bool demanderIgnorer)
        {
            string affichage = libelle;

            if (demanderIgnorer)
            {
                affichage += CHAINE_POUR_IGNORER;
            }

            affichage += " : ";

            Console.WriteLine(affichage);

            string entier = Console.ReadLine();

            if (verifierIgnorer(entier))
            {
                return -1;
            }

            return Int32.Parse(entier);
        }

        private static DateTime SaisirDate(string libelle, bool demanderIgnorer)
        {
            Console.WriteLine(libelle + " : ");
            int year = SaisirEntierPositif(CHAINE_ANNEE, demanderIgnorer);
            if (year == -1)
            {
                return default;
            }
            int month = SaisirEntierPositif(CHAINE_MOIS, demanderIgnorer);
            int day = SaisirEntierPositif(CHAINE_JOUR, demanderIgnorer);

            return new DateTime(year, month, day);
        }

        private static void MenuRechercher()
        {
            string villeSejour = SaisirChaine(CHAINE_VILLE_SEJOUR, true);
            DateTime dateArrivee = SaisirDate(CHAINE_DATE_ARRIVEE, true);
            DateTime dateDepart = DateTime.MaxValue;

            if (dateArrivee != default)
            {
                dateDepart = SaisirDate(CHAINE_DATE_DEPART, true);
            } else
            {
                dateArrivee = DateTime.MinValue;
            }

            if (dateArrivee > dateDepart)
            {
                throw new PeriodeInvalideException(dateArrivee, dateDepart);
            }

            double prixMin = SaisirEntierPositif(CHAINE_PRIX_MINIMAL, true);
            double prixMax = SaisirEntierPositif(CHAINE_PRIX_MAXIMAL, true);
            int nbEtoile = SaisirEntierPositif(CHAINE_NOMBRE_ETOILE, true);
            int nbPersonne = SaisirEntierPositif(CHAINE_NOMBRE_PERSONNE, true);

            List<Chambre> chambres = Agence.Rechercher(villeSejour, dateArrivee, dateDepart, prixMin, prixMax, nbEtoile, nbPersonne);

            for (int i = 0; i < chambres.Count; ++i)
            {
                AfficherResultat(chambres[i]);
            }
        }

        private static void AfficherResultat(Chambre chambre)
        {
            Console.WriteLine(CHAINE_IDENTIFIANT_HOTEL + " : " + chambre.Hotel.Id);
            Console.WriteLine(CHAINE_IDENTIFIANT_CHAMBRE + " : " + chambre.Id);
            Console.WriteLine(CHAINE_RESULTAT_HOTEL + chambre.Hotel.Nom);
            Console.WriteLine(CHAINE_RESULTAT_VILLE + chambre.Hotel.Ville);
            Console.WriteLine(CHAINE_RESULTAT_RUE + chambre.Hotel.Rue);
            Console.WriteLine(CHAINE_RESULTAT_PRIX + chambre.Prix);
            Console.WriteLine(CHAINE_RESULTAT_NOMBRE_LIT + chambre.Nblit);
            Console.WriteLine();
        }

        private static void MenuReserver()
        {
            int hotelId = SaisirEntierPositif(CHAINE_IDENTIFIANT_HOTEL, false);
            int chambreId = SaisirEntierPositif(CHAINE_IDENTIFIANT_CHAMBRE, false);
            string nom = SaisirChaine(CHAINE_NOM_CLIENT, false);
            string prenom = SaisirChaine(CHAINE_PRENOM_CLIENT, false);
            string numeroCarte = SaisirChaine(CHAINE_NUMERO_CARTE_BANCAIRE, false);
            DateTime dateArrivee = SaisirDate(CHAINE_DATE_ARRIVEE, false);
            DateTime dateDepart = SaisirDate(CHAINE_DATE_DEPART, false);

            if (dateArrivee > dateDepart)
            {
                throw new PeriodeInvalideException(dateArrivee, dateDepart);
            }

            int clientId = Agence.EnregistrerClient(nom, prenom, numeroCarte);
            int numeroReservation = Agence.Reserver(clientId, hotelId, chambreId, dateArrivee, dateDepart);

            Console.WriteLine(CHAINE_NUMERO_RESERVATION + " : " + numeroReservation);

        }

        private static void MenuConsulterReservation()
        {
            int numeroReservation = SaisirEntierPositif(CHAINE_NUMERO_RESERVATION, false);

            Reservation reservation = Agence.ConsulterReservation(numeroReservation);

            string chaineReservation = "Réservation " + numeroReservation + " : \n";
            chaineReservation += "* Client : " + reservation.ClientReservation.Nom + " " + reservation.ClientReservation.Prenom + "\n";
            chaineReservation += "* Hotel : " + reservation.ChambreReservation.Hotel.Nom + "\n";
            chaineReservation += "* Chambre : " + reservation.ChambreReservation.Nblit + " lits\n";
            chaineReservation += "* Prix : " + reservation.ChambreReservation.Prix + "€\n";

            Console.WriteLine(chaineReservation);
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
                MenuConsulterReservation();
            } else if (choix == 4)
            {
                return false;
            }
            else
            {
                Console.WriteLine(CHAINE_CHOIX_INVALIDE);
            }

            return true;
        }

        static void Main(string[] args)
        {
            int hotel1Id = Agence.AjouterHotel("hilton", "france", "montpellier", "av", 75, 3);
            int hotel2Id = Agence.AjouterHotel("gheorge", "usa", "losangeles", "street", 2, 5);
            Agence.AjouterChambre(hotel1Id, 3, 80, 20);
            Agence.AjouterChambre(hotel1Id, 5, 100, 50);
            Agence.AjouterChambre(hotel1Id, 2, 60, 40);
            Agence.AjouterChambre(hotel2Id, 3, 100, 40);
            Agence.AjouterChambre(hotel2Id, 1, 35, 100);

            bool continuer = true;

            do
            {
                try {
                    continuer = AfficherMenu();
                } catch (ChambreNonDisponibleException e)
                {
                    Console.WriteLine("Erreur : " + e.Message);
                } catch (PeriodeInvalideException e)
                {
                    Console.WriteLine("Erreur : " + e.Message);
                }
            } while (continuer);
        }
    }
}
