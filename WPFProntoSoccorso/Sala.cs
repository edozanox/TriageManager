using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WPFProntoSoccorso
{
     class Sala
    {
        public int Numero { get; private set; }
        public List<Paziente> PazientiInAttesa { get; private set; }
        /// <summary>
        /// Questa è la lista dei soli codici ammessi in sala.
        /// </summary>
        public List<Paziente.CodiceAccesso> CodicePriorita { get; private set; }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="num"> Numero identificativo della sala</param>
        /// <param name="CodiciAmmessi">Codici ammessi in sala</param>
        /// 
        public Sala(int num, List<Paziente.CodiceAccesso> CodiciAmmessi)
        {
            Numero = num;
            PazientiInAttesa = new List<Paziente>();
            CodicePriorita = CodiciAmmessi;

        }



        public Paziente ProxPaziente()
        {
            //0. Trovare il codice di accesso più grave
            List<Paziente> pGravi = new List<Paziente>();
            Paziente.CodiceAccesso max = 0;
            foreach (Paziente.CodiceAccesso cod in CodicePriorita)
            {
                foreach (Paziente p in PazientiInAttesa)
                {
                    if (p.Codice == max)
                    {
                        pGravi.Add(p);
                    }

                    if (cod > max)
                    {
                        max = cod;
                    }
                }
            }
            //1. Cerco i pazienti con maggior priorità (CodiceAccesso)
            
           
              


            //2. Tra pazienti individuati al pt. 1 scelgo quelli con DataIngresso minore

            DateTime min = DateTime.Now;
            Paziente pArr = null;          //indica il paziente arrivato prima

            if (pGravi != null)             
            {
                foreach (Paziente p in pGravi)      //analisi elementi lista pazienti gravi
                {
                    if (p.DataAccesso < min)        
                    {
                        min = p.DataAccesso;       
                        pArr = p;        //viene selezionato il paziente arrivato prima
                    }

                }
            }
          
                    

            return pArr;        

        }

    }
}

