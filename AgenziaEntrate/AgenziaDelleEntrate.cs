using System;

namespace AgenziaEntrate
{
    static class AgenziaDelleEntrate
    {
        //Menu iniziale con scelta per continuare o per uscire
        public static void Menu()
        {
            Console.WriteLine("***************************************************************");
            Console.WriteLine("*********** A G E N Z I A  D E L L E  E N T R A T E ***********");
            Console.WriteLine("***************************************************************");
            Console.WriteLine("\nBenvenuto nel programma per il calcolo dell'imposta da versare.");
            Console.WriteLine("Avremo bisogno di alcuni dati personali.");
            Console.WriteLine("Vuoi procedere? (y/n)");
            string resp = Console.ReadLine().ToLower();
            switch (resp)
            {
                case "y":
                    Register();
                    break;
                case "n":
                    Exit();
                    break;
                default:
                    Console.Clear();
                    Console.WriteLine("\nInput non valido\n");
                    Menu();
                    break;
            }
        }

        //Metodo per chiudere l'applicazione
        public static void Exit()
        {
            Console.WriteLine("A presto!");
            Environment.Exit(0);
        }

        //Metodo principale che prende in input da tastiera i vari dati
        public static void Register()
        {
            Contribuente contribuente = new Contribuente();
            Console.Write("\nInserisci il tuo nome: ");
            string nome = Console.ReadLine();
            contribuente.SetNome(nome);

            Console.Write("Inserisci il tuo cognome: ");
            string cognome = Console.ReadLine();
            contribuente.SetCognome(cognome);

            DateTime dataDiNascita = InsertDate();        //Per alcuni valori che richiedevano maggiori controlli, uso funzioni custom create in basso
            contribuente.SetDataDiNascita(dataDiNascita);

            Console.Write("Inserisci il tuo codice fiscale: ");
            string codiceFiscale = Console.ReadLine();
            contribuente.SetCodiceFiscale(codiceFiscale);

            string sesso = InsertSex();
            contribuente.SetSesso(sesso);

            Console.Write("Inserisci il tuo comune di residenza: ");
            string comuneResidenza = Console.ReadLine();
            contribuente.SetComuneResidenza(comuneResidenza);

            double redditoAnnuale = InsertReddito();
            contribuente.SetRedditoAnnuale(redditoAnnuale);

            double imposta = contribuente.CalcolaImposta();

            Console.Clear();
            Console.WriteLine("***************************************************************" +
                "\n*********** A G E N Z I A  D E L L E  E N T R A T E ***********" +
                "\n***************************************************************" +
                $"\nContribuente: {contribuente.GetNome()} {contribuente.GetCognome()} ({contribuente.GetSesso()})," +
                $"\nnato il {contribuente.GetDataDiNascita().ToString().Split(' ')[0]}," +
                $"\nresidente in {contribuente.GetComuneResidenza()}," +
                $"\ncodice fiscale: {contribuente.GetCodiceFiscale()}," +
                $"\nreddito dichiarato: {contribuente.GetRedditoAnnuale()} euro," +
                $"\nIMPOSTA DA VERSARE: {imposta} euro");
        }

        //Funzione che prende in input da tastiera una stringa e la trasforma in DateTime e poi la ritorna
        public static DateTime InsertDate()
        {
            Console.Write("Inserisci la tua data di nascita (DD/MM/YYYY): ");
            string dataDiNascitaString = Console.ReadLine();
            DateTime dataDiNascita;
            //Se non si riesce a trasformare dataDiNascitaString in DateTime, o se non rientra nel range [1920, data odierna - 18 anni], dà errore e richiama la funzione stessa
            if (!DateTime.TryParse(dataDiNascitaString, out dataDiNascita) || int.Parse(dataDiNascitaString.Split('/')[2]) < 1920 || dataDiNascita > DateTime.Now.AddYears(-18))
            {
                Console.WriteLine("Data inserita non valida. Riprovare.");
                //Si richiama la funzione dentro dataDiNascita così da non perdere il valore mentre si risale tra gli stack in memoria dovuti alla ricorsione
                dataDiNascita = InsertDate();
            }
            return dataDiNascita;
        }
        //Funzione che prende in input da tastiera una stringa e controlla se è uguale a M o F. Ritorna poi la stringa risultante
        public static string InsertSex()
        {
            Console.Write("Inserisci il tuo sesso (M/F): ");
            string sesso = Console.ReadLine().ToUpper();
            //Se non coincide con nessuna delle due opzioni, la funzione richiama sé stessa
            if (sesso != "M" && sesso != "F")
            {
                Console.WriteLine("Sesso non valido. Inserire M o F");
                sesso = InsertSex();
            }
            return sesso;
        }

        //Funzione che prende in input da tastiera il reddito, controlla che il formato sia giusto e lo ritorna
        public static double InsertReddito()
        {
            Console.Write("Inserisci il tuo reddito annuale: ");
            string redditoAnnualeString = Console.ReadLine();
            double redditoAnnuale;
            //Controlla che sia possibile trasformare la stringa in input in un double e che non sia negativa
            if (!double.TryParse(redditoAnnualeString, out redditoAnnuale) || double.Parse(redditoAnnualeString) < 0)
            {
                Console.WriteLine("Valore inserito non valido. Riprovare.");
                redditoAnnuale = InsertReddito();
            }
            return redditoAnnuale;
        }
    }
}
