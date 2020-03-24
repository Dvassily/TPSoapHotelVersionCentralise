using System;
using System.Runtime.Serialization;

namespace TP2
{
    [Serializable]
    internal class ChambreNonDisponibleException : Exception
    {
        private int hotelId;
        private int chambreId;
        private DateTime dateDepart;
        private DateTime dateArrivee;

        public ChambreNonDisponibleException()
        {
        }

        public ChambreNonDisponibleException(string message) : base(message)
        {
        }

        public ChambreNonDisponibleException(string message, Exception innerException) : base(message, innerException)
        {
        }

        public ChambreNonDisponibleException(int hotelId, int chambreId, DateTime dateArrivee, DateTime dateDepart)
            : this("La chambre " + chambreId + "de l'hotel " + hotelId + " n'est pas disponible pour le créneau : " + dateArrivee.ToString() + "-" + dateDepart.ToString())
        {
            this.hotelId = hotelId;
            this.chambreId = chambreId;
            this.dateDepart = dateDepart;
            this.dateArrivee = dateArrivee;
        }

        protected ChambreNonDisponibleException(SerializationInfo info, StreamingContext context) : base(info, context)
        {
        }
    }
}