using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace TP2
{
    
    
    class Client
    {
        public string Nom { get; set; }
        public string Prenom { get; set; }
        public string NumeroCarte { get; set; }

        public Client (string nom, string prenom, string numerocarte)
        {
            Nom = nom;
            Prenom = prenom;
            NumeroCarte = numerocarte;
        }
    }
}
