using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WPFProntoSoccorso
{
    interface IProntoSoccorso
    {
        List<Paziente> Pazienti { get; }
        List<Medico> Medici { get; }
        Sala Sala1 { get; }
        Sala Sala2 { get; }
        Sala Sala3 { get; }

        void InserisciPaziente(Paziente p);
        void InserisciMedico(Medico m);
        /// <summary>
        /// Invio del paziente alla sala relativa in base al codice d'accesso
        /// </summary>
        /// <param name="p">Paziente da aggiungere alla sala</param>
        /// <returns> Sala in cui il paziente è stato aggiunto</returns>
        Sala AggiungiPazienteSala(Paziente p);
        /// <summary>
        /// Individua il successivo paziente da gestire in base a codice accesso e DateTime di arrivo
        /// </summary>
        /// <param name="m"></param>
        /// <returns> Il paziente da gestire </returns>
        Paziente ProssimoPaziente(Medico m, out Sala s);

    }
}
