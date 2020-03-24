using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace TP2
{
    class Reservation
    {
        public static string CompteurReservations
        {
            get
            {
                return (_CompteurReservations++).ToString();
            }
        }

        private static int _CompteurReservations = 0;
        public DateTime DateDepart { get; set; }
        public DateTime DateArrivee { get; set; }
        public string NumReservation { get; set; }
        public Chambre ChambreReservation { get; set; }
        public Client ClientReservation { get; set; }
        public Reservation (DateTime dateDepart, DateTime dateArrivee, string numreservation, Chambre chambre, Client client) 
        {
            DateDepart = dateDepart;
            DateArrivee = dateArrivee;
            NumReservation = numreservation;
            ChambreReservation = chambre;
            ClientReservation = client;
        }

    }
}
