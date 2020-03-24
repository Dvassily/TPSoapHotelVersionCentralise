using System;
using System.Runtime.Serialization;

namespace TP2
{
    [Serializable]
    internal class PeriodeInvalideException : Exception
    {
        private DateTime dateArrivee;
        private DateTime dateDepart;

        public PeriodeInvalideException()
        {
        }

        public PeriodeInvalideException(string message) : base(message)
        {
        }

        public PeriodeInvalideException(DateTime dateArrivee, DateTime dateDepart)
            : this("La période [" + dateArrivee.ToString() + "; " + dateDepart.ToString() + "] est invalide")
        {
            this.dateArrivee = dateArrivee;
            this.dateDepart = dateDepart;
        }

        public PeriodeInvalideException(string message, Exception innerException) : base(message, innerException)
        {
        }

        protected PeriodeInvalideException(SerializationInfo info, StreamingContext context) : base(info, context)
        {
        }
    }
}