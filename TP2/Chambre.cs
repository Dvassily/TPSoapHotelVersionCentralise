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

        private List<Reservation> reservations = new List<Reservation>();

        public Chambre (Hotel hotel, int id, int nblit, double prix, double surface)
        {
            Hotel = hotel;
            Id = id;
            Nblit = nblit;
            Prix = prix;
            Surface = surface;
        }


        public bool estDisponible(DateTime dateDepart, DateTime dateArrivee)
        {
            bool disponible = true;

            foreach (Reservation reservation in reservations)
            {
                bool dateDepartOk = true;
                bool dateArriveeOk = true;

                if (dateDepart != default && dateDepart <= reservation.DateDepart)
                {
                    dateDepartOk = false;
                }

                if (dateArrivee != default && dateArrivee >= reservation.DateArrivee)
                {
                    dateArriveeOk = false;
                }

                if (! dateDepartOk || ! dateArriveeOk)
                {
                    disponible = false;
                }
            }

            return disponible;
        }
    }
}
