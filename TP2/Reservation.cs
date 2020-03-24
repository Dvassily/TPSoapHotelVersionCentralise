using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace TP2
{
    class Reservation
    {
        public DateTime DateDepart { get; set; }
        public DateTime DateArrivee { get; set; }
        public int NumReservation { get; set; }
        public Chambre ChambreReservation { get; set; }
        public Client ClientReservation { get; set; }
        public Reservation (int numreservation, DateTime dateDepart, DateTime dateArrivee, Chambre chambre, Client client)
        {
            NumReservation = numreservation;
            DateDepart = dateDepart;
            DateArrivee = dateArrivee;
            ChambreReservation = chambre;
            ClientReservation = client;
        }

    }
}
