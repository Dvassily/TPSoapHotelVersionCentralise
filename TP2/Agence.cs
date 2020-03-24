using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace TP2
{
    class Agence
    {

        private Dictionary<int, Hotel> _Hotels = new Dictionary<int, Hotel>();
        private List<Reservation> Reservations = new List<Reservation>();

        public List<Hotel> Hotels
        {
            get
            {
                return new List<Hotel>(_Hotels.Values);
            }
        }
        public int CompteurHotels
        {
            get
            {
                return _CompteurHotels++;
            }
        }

        private int _CompteurHotels = 0;

        public void AjouterHotel(Hotel hotel) 
        {
            _Hotels.Add(CompteurHotels, hotel);
        } 

        public List<Chambre> Rechercher (string villeSejour, DateTime dateDepart, DateTime dateArrivee, double prixmin, double prixmax, int nbetoile, int nbpersonne)
        {
            List<Chambre> resultats = new List<Chambre>();

            foreach (Hotel hotel in Hotels)
            {
                if (HotelCorrespond(hotel, villeSejour, nbetoile))
                {
                    foreach (Chambre chambre in hotel.Chambres)
                    {
                        if (ChambreCorrespond(chambre, dateDepart, dateArrivee, prixmin, prixmax, nbpersonne))
                        {
                            resultats.Add(chambre);
                        }
                    }
                }
            }
                    

            return resultats;
        }

        public Reservation Reserver(string nom, string prenom, string numerocarte, int hotelId, int chambreId, DateTime dateDepart, DateTime dateArrivee)
        {
            Client client = new Client(nom, prenom, numerocarte);
            Chambre chambre = Hotels[hotelId].Chambres[chambreId];
            Reservation reservation = new Reservation(dateDepart, dateArrivee, Reservation.CompteurReservations, chambre, client);

            Reservations.Add(reservation);

            return reservation;
        }

        private bool HotelCorrespond(Hotel hotel, string villeSejour, int nbetoile)
        {
            if (villeSejour != null && hotel.Ville.IndexOf(villeSejour) == -1)
            {
                Console.WriteLine("villeSejour");
                return false;
            }

            if (nbetoile != -1 && hotel.Nbetoile != nbetoile)
            {
                Console.WriteLine("Nbetoile");
                return false;
            }

            return true;
        }

        private bool ChambreCorrespond(Chambre chambre, DateTime dateDepart, DateTime dateArrivee, double prixmin, double prixmax, int nbpersonne)
        {
            if (! chambre.estDisponible(dateDepart, dateArrivee))
            {
                Console.WriteLine("date");
                return false;
            }

            if (prixmin != -1 && chambre.Prix < prixmin)
            {
                Console.WriteLine("prixmin");
                return false;
            }

            if (prixmax != -1 && chambre.Prix > prixmax)
            {
                Console.WriteLine("prixmax");
                return false;
            }

            if (nbpersonne != -1 && chambre.Nblit < nbpersonne)
            {
                Console.WriteLine("lit");
                return false;
            }

            return true;
        }

    }
}
