using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;
using System.Speech.Synthesis;
using System.Speech.AudioFormat;
using System.Runtime.Serialization;
using System.Runtime.Serialization.Formatters.Binary;
using System.IO;

namespace WPFProntoSoccorso
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        private ProntoSoccorso ps = new ProntoSoccorso();
        SpeechSynthesizer syntVoice;

        public MainWindow()
        {
            InitializeComponent();

            ScrollViewer myScrollViewer = new ScrollViewer();
            myScrollViewer.HorizontalScrollBarVisibility = ScrollBarVisibility.Auto;

            syntVoice = new SpeechSynthesizer();

            boxAccesso.Items.Add("Modo Accesso");
            

            foreach (var value in Enum.GetValues(typeof(Paziente.ModoAccesso)))
            {
                boxAccesso.Items.Add(value);

            }

            lstPazienti.ItemsSource = ps.Pazienti;
            lstSalaUno.ItemsSource = ps.Sala1.PazientiInAttesa;
            lstSalaUno.ItemsSource = ps.Sala2.PazientiInAttesa;
            lstSalaUno.ItemsSource = ps.Sala3.PazientiInAttesa;

            lstMedici.ItemsSource = ps.Medici;

            boxSpecializzazione.Items.Add("Specializzazione");
            foreach (var value in Enum.GetValues(typeof(Medico.Specializzazione)))
            {
                boxSpecializzazione.Items.Add(value);
            }
            
            boxColore.SelectedIndex = 0;
            boxAccesso.SelectedIndex = 0;
            boxSpecializzazione.SelectedIndex = 0;

            CaricaMedici(ps.Medici);
            CaricaPazienti(ps.Pazienti);
         
        }

        
        
        public void BtnInserisciPaziente_Click(object sender, RoutedEventArgs e)
        {
            string NomePaziente = (txtNomePaziente.Text).ToUpper();
            string CognomePaziente = (txtCognomePaziente.Text).ToUpper();
            DateTime DataNascitaPaziente = (dpDataPaziente.DisplayDate);

            Paziente.ModoAccesso accesso = (Paziente.ModoAccesso)Enum.Parse(typeof(Paziente.ModoAccesso), boxAccesso.SelectedItem.ToString());
            Paziente.CodiceAccesso codice = (Paziente.CodiceAccesso)Enum.Parse(typeof(Paziente.CodiceAccesso), ((ComboBoxItem)(boxColore.SelectedItem)).Content.ToString());


            if (NomePaziente == String.Empty || NomePaziente == String.Empty || DataNascitaPaziente == null)
            {
                MessageBox.Show("Compilare tutti i campi richiesti!", "Dati incompleti", MessageBoxButton.OK, MessageBoxImage.Warning);

            }
            else
            {
                Paziente p = new Paziente(NomePaziente, CognomePaziente, DataNascitaPaziente, accesso, codice);
                ps.InserisciPaziente(p);
                lstPazienti.Items.Refresh();
                MessageBox.Show($"Paziente   {NomePaziente}  {CognomePaziente}  nato il {DataNascitaPaziente}  inserito correttamente", "Paziente inserito", MessageBoxButton.OK, MessageBoxImage.Information);
                SalvaPazienti(ps.Pazienti);
            }

            txtNomePaziente.Text = "Nome";
            txtCognomePaziente.Text = "Cognome";          
            boxAccesso.SelectedIndex = 0;
            boxColore.SelectedIndex = 0;

        }


        public void btnInserisciMedico_Click(object sender, RoutedEventArgs e)
        {
            string NomeMedico = (txtNomeMedico.Text).ToUpper();
            string CognomeMedico = (txtCognomeMedico.Text).ToUpper();
            string Matricola = (txtMatricola.Text);
            DateTime DataNascitaMedico = (txtDataMedico.DisplayDate);
            Medico.Specializzazione tipologia = (Medico.Specializzazione)Enum.Parse(typeof(Medico.Specializzazione), boxSpecializzazione.SelectedItem.ToString());


            if (NomeMedico == String.Empty || CognomeMedico == String.Empty || DataNascitaMedico == null)
            {
                MessageBox.Show("Compilare tutti i campi richiesti!", "Dati incompleti", MessageBoxButton.OK, MessageBoxImage.Warning);

            }
            else
            {
                Medico m = new Medico(NomeMedico, CognomeMedico, DataNascitaMedico, Matricola, tipologia);
                //lstMedici.Items.Clear();
                ps.InserisciMedico(m);
                lstMedici.Items.Refresh();
                MessageBox.Show($"Medico   {NomeMedico}  {CognomeMedico}  con matricola {Matricola}  inserito correttamente", "Medico inserito", MessageBoxButton.OK, MessageBoxImage.Information);
                SalvaMedico(ps.Medici);

            }

            SalvaMedico(ps.Medici);
            txtNomeMedico.Text = "Nome";
            txtCognomeMedico.Text = "Cognome";
            txtMatricola.Text = "Matricola";
            boxSpecializzazione.SelectedIndex = 0;
        }

        public void btnCancellaPaziente_Click(object sender, RoutedEventArgs e)
        {
            Paziente p = (Paziente)lstPazienti.SelectedItem;
            ps.Pazienti.Remove(p);
            lstPazienti.Items.Refresh();
            SalvaPazienti(ps.Pazienti);
            MessageBox.Show("Eliminazione avvenuta correttamente", "Eliminazione", MessageBoxButton.OK, MessageBoxImage.Information);

        }

        public void btnMandaInSala_Click(object sender, RoutedEventArgs e)
        {
            Paziente pz = (Paziente)lstPazienti.SelectedItem;
            Sala rtrn = ps.AggiungiPazienteSala(pz);

            if (pz == null)
            {
                MessageBox.Show("È necessario scegliere un paziente dalla lista", "Errore", MessageBoxButton.OK, MessageBoxImage.Warning);

            }
            else
            {


                if (rtrn == null)
                {
                    MessageBox.Show("Sala inesistente", "Errore", MessageBoxButton.OK, MessageBoxImage.Warning);

                }
                else if (rtrn == ps.Sala1)
                {

                    lstSalaUno.Items.Add(pz);
                    MessageBox.Show("PAZIENTE AGGIUNTO IN SALA 1", "Inserimento in sala", MessageBoxButton.OK, MessageBoxImage.Information);

                }
                else if (rtrn == ps.Sala2)
                {
                    lstSalaDue.Items.Add(pz);
                    MessageBox.Show("PAZIENTE AGGIUNTO IN SALA 2", "Inserimento in sala", MessageBoxButton.OK, MessageBoxImage.Information);

                }
                else if (rtrn == ps.Sala3)
                {
                    MessageBox.Show("PAZIENTE AGGIUNTO IN SALA 3", "Inserimento in sala", MessageBoxButton.OK, MessageBoxImage.Information);
                    lstSalaTre.Items.Add(pz);
                }

            }
        }

        public void btnProxPaziente_Click(object sender, RoutedEventArgs e)
        {
            //0. Selezionare medico disponibile
            //1. Ricerca del paziente più grave in assoluto all'interno delle tre sale metodo ProxPaziente di Sala       
            //2. Se vi è un paziente da gestire, rimuoverlo dalla sala ed aggiungerlo in lista pazienti medico
            //4. MessageBox con riepilogo dati paziente e medico interessati
            Sala s = null;
            Medico prox_medico = (Medico)lstMedici.SelectedItem;
            Paziente nxt_pz = ps.ProssimoPaziente(prox_medico, out s);
            if (prox_medico == null)
            {
                MessageBox.Show("Prima di procedere con la chiamata, è necessario selezionare un medico.", "Impossibile proseguire", MessageBoxButton.OK, MessageBoxImage.Warning);

            }
            else
            {
                //Aggiungere controllo   if (nxt_pz == null) {} else {}
                //MessageBox.Show("NESSUN PAZIENTE IN LISTA", "TriageManager", MessageBoxButton.OK, MessageBoxImage.Information);
                           
                if(s.Numero == 1)
                {
                    lstSalaUno.Items.Refresh();
                    

                } else if (s.Numero == 2)
                {
                    lstSalaDue.Items.Refresh();
                } else if (s.Numero == 3)
                {
                    lstSalaTre.Items.Refresh();
                }else
                
                txtNextPaziente.Text = " ";
                txtDottPerProxPaziente.Clear();
                txtNextPaziente.Text = (nxt_pz.Nome + " " + nxt_pz.Cognome);

                //  string str_nxt_dot = ("Dott. " + prox_medico.Nome + " " + prox_medico.Cognome);
                txtDottPerProxPaziente.Text = "Dott. " + prox_medico.Nome + " " + prox_medico.Cognome;

                Console.Beep(400, 150);
                Console.Beep(300, 150);
                Console.Beep(200, 150);
                syntVoice.SpeakAsync("Prossimo paziente: " + txtNextPaziente.Text);
                syntVoice.SpeakAsync("Medico incaricato: " + txtDottPerProxPaziente.Text);

                if (lstPazienti.Items.Equals(txtNextPaziente))
                {
                    lstPazienti.Items.Clear();
                    
                }


            }

        }



        private void SalvaPazienti(List<Paziente> saved_pazienti)
        {
            IFormatter fr = new BinaryFormatter();
            Stream file_pazienti = new FileStream("ListaPazienti.dat", FileMode.Create);
            fr.Serialize(file_pazienti, saved_pazienti);
            file_pazienti.Close();

        }

        private void SalvaMedico(List<Medico>saved_medici)
        {
            IFormatter f = new BinaryFormatter();
            Stream file_medici = new FileStream("ListaMedici.dat", FileMode.Create);
            f.Serialize(file_medici, saved_medici);
            file_medici.Close();
        }



        private void CaricaPazienti(List<Paziente> saved_pazienti)
        {
            if (File.Exists("ListaPazienti.dat") == true)
            {
                IFormatter f = new BinaryFormatter();
                Stream file_pazienti = new FileStream("ListaPazienti.dat", FileMode.Open);
                List<Paziente> lst_pz = (List<Paziente>)f.Deserialize(file_pazienti);
                saved_pazienti.AddRange(lst_pz);
                file_pazienti.Close();
                MessageBox.Show("FILE PAZIENTI CARICATO", "Inizializzazione...", MessageBoxButton.OK, MessageBoxImage.None);
            }
            else
            {
                MessageBox.Show("NESSUN FILE DI CARICAMENTO PAZIENTI TROVATO", "Inizializzazione - No data", MessageBoxButton.OK);
            }
        }

        private void CaricaMedici(List<Medico> saved_medici)
        {
            if (File.Exists("ListaMedici.dat") == true)
            {
                try
                {
                    IFormatter f = new BinaryFormatter();
                    Stream file_medici = new FileStream("ListaMedici.dat", FileMode.Open);
                    List<Medico> lst_med = (List<Medico>)f.Deserialize(file_medici);
                    saved_medici.AddRange(lst_med);
                    file_medici.Close();
                    MessageBox.Show("FILE MEDICI CARICATO", "Inizializzazione...", MessageBoxButton.OK, MessageBoxImage.None);
                }
                catch
                {

                }
            }
            else
            {
                MessageBox.Show("NESSUN FILE DI CARICAMENTO MEDICI TROVATO", "Inizializzazione - No data", MessageBoxButton.OK);

            }

        }
    }
}
    
