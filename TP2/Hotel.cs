using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace TP2
{
    class Hotel
	{
		public int Id { get; set; }
		public string Nom { get; set; }
		public string Pays { get; set; }
		public string Ville { get; set; }
		public string Rue { get; set; }
		public int Numero { get; set; }
		public int Nbetoile { get; set; }
		private Dictionary<int, Chambre> _Chambres = new Dictionary<int, Chambre>();

		public List<Chambre> Chambres { get
			{
				return new List<Chambre>(_Chambres.Values);
			}
		}

		public int CompteurChambres { get {
				return _CompteurChambres++;
			} 
		}

		private int _CompteurChambres = 0;

		public Hotel(int id, string nom, string pays, string ville, string rue, int numero, int nbetoile)
		{
			Id = id;
			Nom = nom;
			Pays = pays;
			Ville = ville;
			Rue = rue;
			Numero = numero;
			Nbetoile = nbetoile;
		}

		public void AjouterChambre(int nblit, double prix, double surface)
		{
			int _Id = CompteurChambres;

			_Chambres.Add(_Id, new Chambre(this, _Id, nblit, prix, surface));
		}
	}
}
