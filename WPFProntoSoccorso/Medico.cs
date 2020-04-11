using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WPFProntoSoccorso
{
    [Serializable]
    class Medico
    {
        public enum Specializzazione { Geriatria, Pediatria, Cardiologia, Oncologia, MedicinaInterna, Otorinolaringoiatria, Radiologia }; 

         public string Nome { get; private set; }
        public string Cognome { get; private set; }
        public DateTime DataNascita { get; private set; }
        public string Matricola { get; private set; }
        public Specializzazione TipoMedico { get; private set; }
        public List<Paziente> Pazienti { get; private set; }
        public DateTime InizioServizio { get; set; }
        public DateTime FineServizio { get; set; }



        public Medico(string nome, string cognome, DateTime nascita, string matricola, Specializzazione spec)
        {
            Nome = nome;
            Cognome = cognome;
            DataNascita = nascita;
            Matricola = matricola;
            TipoMedico = spec;
            InizioServizio = DateTime.Now;
            Pazienti = new List<Paziente>();

        }

        public override string ToString()
        {
            return Nome + " " + Cognome;
        }


    }


}

