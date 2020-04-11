using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WPFProntoSoccorso
{
    class ProntoSoccorso
    {
        public List<Medico> Medici { get; } = new List<Medico>();
        public List<Paziente> Pazienti { get; } = new List<Paziente>();
        public Sala Sala1 { get; private set; }
        public Sala Sala2 { get; private set; }
        public Sala Sala3 { get; private set; }

        public ProntoSoccorso()
        {
            Sala1 = new Sala(1, new List<Paziente.CodiceAccesso> { Paziente.CodiceAccesso.BIANCO, Paziente.CodiceAccesso.BLU });
            Sala2 = new Sala(2, new List<Paziente.CodiceAccesso> { Paziente.CodiceAccesso.VERDE, Paziente.CodiceAccesso.GIALLO });
            Sala3 = new Sala(3, new List<Paziente.CodiceAccesso> { Paziente.CodiceAccesso.ROSSO, Paziente.CodiceAccesso.NERO });
        }

        public Sala AggiungiPazienteSala(Paziente p)
        {
            if (Sala1.CodicePriorita.Contains(p.Codice))
            {
                Sala1.PazientiInAttesa.Add(p);
                return Sala1;
            }
            else if (Sala2.CodicePriorita.Contains(p.Codice))
            {
                Sala2.PazientiInAttesa.Add(p);
                return Sala2;
            }
            else if (Sala3.CodicePriorita.Contains(p.Codice))
            {
                Sala3.PazientiInAttesa.Add(p);
                return Sala3;

            }
            else
            {
                return null;
            }
        }

        public void InserisciMedico(Medico m)
        {
            Medici.Add(m);
        }

        public void InserisciPaziente(Paziente p)
        {
            Pazienti.Add(p);
        }

        public Paziente ProssimoPaziente(Medico m, out Sala s)
        {
            Paziente p = null;
            p = Sala3.ProxPaziente();

            if (p != null)
            {
                m.Pazienti.Add(p);
                Sala3.PazientiInAttesa.Remove(p);
                s = Sala3;
                return p;
            }
            p = Sala2.ProxPaziente();

            if (p != null)
            {
                m.Pazienti.Add(p);
                Sala2.PazientiInAttesa.Remove(p);
                s = Sala2;
                return p;

            }

            p = Sala1.ProxPaziente();

            if (p != null)
            {
                m.Pazienti.Add(p);
                Sala1.PazientiInAttesa.Remove(p);
                s = Sala1;
                return p;

            }

            s = null;
            return null;
        }
    }
}
