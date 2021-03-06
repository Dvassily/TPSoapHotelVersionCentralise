﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace TP2
{
    class Agence
    {
        private Dictionary<int, Reservation> _Reservations = new Dictionary<int, Reservation>();
        public List<Reservation> Reservations
        {
            get
            {
                return new List<Reservation>(_Reservations.Values);
            }
        }
        public int CompteurReservations
        {
            get
            {
                return _CompteurReservations++;
            }
        }

        private int _CompteurReservations = 0;

        private Dictionary<int, Hotel> _Hotels = new Dictionary<int, Hotel>();
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

        private Dictionary<int, Client> _Clients = new Dictionary<int, Client>();
        public List<Client> Clients
        {
            get
            {
                return new List<Client>(_Clients.Values);
            }
        }
        public int CompteurClients
        {
            get
            {
                return _CompteurClients++;
            }
        }

        private int _CompteurClients = 0;

        public int AjouterHotel(string nom, string pays, string ville, string rue, int numero, int nbetoile) 
        {
            int id = CompteurHotels;

            _Hotels.Add(id, new Hotel(id, nom, pays, ville, rue, numero, nbetoile));

            return id;
        }

        public void AjouterChambre(int hotelId, int nblit, double prix, double surface)
        {
            Hotel hotel = _Hotels[hotelId];

            hotel.AjouterChambre(nblit, prix, surface);
        }

        public List<Chambre> Rechercher (string villeSejour, DateTime dateArrivee, DateTime dateDepart,  double prixmin, double prixmax, int nbetoile, int nbpersonne)
        {
            List<Chambre> resultats = new List<Chambre>();

            foreach (Hotel hotel in Hotels)
            {
                if (HotelCorrespond(hotel, villeSejour, nbetoile))
                {
                    foreach (Chambre chambre in hotel.Chambres)
                    {
                        if (ChambreCorrespond(chambre, dateArrivee, dateDepart, prixmin, prixmax, nbpersonne))
                        {
                            resultats.Add(chambre);
                        }
                    }
                }
            }
                    

            return resultats;
        }

        public int Reserver(int clientId, int hotelId, int chambreId, DateTime dateArrivee, DateTime dateDepart)
        {
            int id = CompteurReservations;
            Client client = _Clients[clientId];
            Chambre chambre = Hotels[hotelId].Chambres[chambreId];

            if (! chambre.estDisponible(dateArrivee, dateDepart))
            {
                throw new ChambreNonDisponibleException(hotelId, chambreId, dateArrivee, dateDepart);
            }

            Reservation reservation = new Reservation(id, dateArrivee, dateDepart, chambre, client);
            _Reservations.Add(id, reservation);
            chambre.Reservations.Add(reservation);

            return id;
        }

        public Reservation ConsulterReservation(int numeroReservation)
        {
            return _Reservations[numeroReservation];         
        }


        public int EnregistrerClient(string nom, string prenom, string numeroCarte)
        {
            int Id = CompteurClients;

            _Clients[Id] = new Client(nom, prenom, numeroCarte);

            return Id;
        }


        private bool HotelCorrespond(Hotel hotel, string villeSejour, int nbetoile)
        {
            if (villeSejour != null && hotel.Ville.IndexOf(villeSejour) == -1)
            {
                return false;
            }

            if (nbetoile != -1 && hotel.Nbetoile != nbetoile)
            {
                return false;
            }

            return true;
        }

        private bool ChambreCorrespond(Chambre chambre, DateTime dateArrivee, DateTime dateDepart, double prixmin, double prixmax, int nbpersonne)
        {
            if (! chambre.estDisponible(dateArrivee, dateDepart))
            {
                return false;
            }

            if (prixmin != -1 && chambre.Prix < prixmin)
            {
                return false;
            }

            if (prixmax != -1 && chambre.Prix > prixmax)
            {
                return false;
            }

            if (nbpersonne != -1 && chambre.Nblit < nbpersonne)
            {
                return false;
            }

            return true;
        }
    }
}
