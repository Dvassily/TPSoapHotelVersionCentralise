using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace TP2
{
    class Reservation
    {
        public DateTime DateArrivee { get; set; }
        public DateTime DateDepart { get; set; }
        public int NumReservation { get; set; }
        public Chambre ChambreReservation { get; set; }
        public Client ClientReservation { get; set; }
        public Reservation (int numreservation, DateTime dateArrivee, DateTime dateDepart,Chambre chambre, Client client)
        {
            NumReservation = numreservation;
            DateArrivee = dateArrivee;
            DateDepart = dateDepart;
            ChambreReservation = chambre;
            ClientReservation = client;
        }

    }
}
