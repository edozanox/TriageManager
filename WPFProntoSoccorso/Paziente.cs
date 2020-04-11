using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WPFProntoSoccorso
{
    [Serializable]
    public class Paziente
    {
        /// <summary>
        /// Codici ordinati per priorità di accesso
        /// </summary>
        public enum CodiceAccesso { BIANCO, BLU, VERDE, GIALLO, ROSSO, NERO };
        public enum ModoAccesso { Ambulanza, Autonomo, Medico, Automedica, Autobus, Taxi, ElisoccorsoPegaso };

        public string Nome { get; private set; }
        public string Cognome { get; private set; }
        public DateTime DataNascita { get; private set; }
        public DateTime DataAccesso { get; private set; }
        public DateTime? DataDimissione { get; private set; }
        public ModoAccesso TipoAccesso { get; set; }
        public CodiceAccesso Codice { get; private set; }
        public string Esito { get; set; }

        public override string ToString()
        {
            return Nome + " " + Cognome;
        }

        public Paziente(string nome, string cognome, DateTime nascita, ModoAccesso accesso, CodiceAccesso cod)
        {

            Nome = nome;
            Cognome = cognome;
            DataNascita = nascita;
            DataAccesso = DateTime.Now;
            DataDimissione = null;
            TipoAccesso = accesso;
            Codice = cod;

        }

        public Paziente (string name, string surname, DateTime birth)
        {

            Nome = name;
            Cognome = surname;
            DataNascita = birth;
            DataAccesso = DateTime.Now;

        }

        
       
        
    }

}
