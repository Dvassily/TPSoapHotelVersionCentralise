using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace TP2
{
    class Chambre
    {
        public Hotel Hotel { get; set; }
        public int Id { get; set; }
        public int Nblit { get; set; }
        public double Prix { get; set; }
        public double Surface { get; set; }

        public List<Reservation> Reservations { get; } = new List<Reservation>();

        public Chambre (Hotel hotel, int id, int nblit, double prix, double surface)
        {
            Hotel = hotel;
            Id = id;
            Nblit = nblit;
            Prix = prix;
            Surface = surface;
        }


        public bool estDisponible(DateTime dateArrivee, DateTime dateDepart)
        {
            bool disponible = true;

            foreach (Reservation reservation in Reservations)
            {
                if (dateDepart >= reservation.DateArrivee && dateArrivee <= reservation.DateDepart)
                {
                    disponible = false;
                }
            }

            return disponible;
        }
    }
}
