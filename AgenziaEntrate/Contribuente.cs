using System;

namespace AgenziaEntrate
{
    internal class Contribuente
    {
        private string nome;
        private string cognome;
        private DateTime dataDiNascita;
        private string codiceFiscale;
        private string sesso;
        private string comuneResidenza;
        private double redditoAnnuale;
        private double imposta;

        public string GetNome() { return nome; }
        public string GetCognome() { return cognome; }
        public DateTime GetDataDiNascita() { return dataDiNascita; }
        public string GetCodiceFiscale() { return codiceFiscale; }
        public string GetSesso() { return sesso; }
        public string GetComuneResidenza() { return comuneResidenza; }
        public double GetRedditoAnnuale() { return redditoAnnuale; }
        public double GetImposta() { return imposta; }
        public void SetNome(string nome) { this.nome = nome; }
        public void SetCognome(string cognome) { this.cognome = cognome; }
        public void SetDataDiNascita(DateTime dataDiNascita) { this.dataDiNascita = dataDiNascita; }
        public void SetCodiceFiscale(string codiceFiscale) { this.codiceFiscale = codiceFiscale; }
        public void SetSesso(string sesso) { this.sesso = sesso; }
        public void SetComuneResidenza(string comuneResidenza) { this.comuneResidenza = comuneResidenza; }
        public void SetRedditoAnnuale(double redditoAnnuale) { this.redditoAnnuale = redditoAnnuale; }
        public void SetImposta(double imposta) { this.imposta = imposta; }


        //Metodo che calcola l'imposta a seconda del reddito annuale dichiarato
        public double CalcolaImposta()
        {
            switch (redditoAnnuale)
            {
                case var reddito when reddito >= 0 && reddito <= 15000:
                    imposta = redditoAnnuale * 23 / 100;
                    break;
                case var reddito when reddito <= 28000:
                    imposta = 3450 + ((redditoAnnuale - 15000) * 27 / 100);
                    break;
                case var reddito when reddito <= 55000:
                    imposta = 6960 + ((redditoAnnuale - 28000) * 38 / 100);
                    break;
                case var reddito when reddito <= 75000:
                    imposta = 17220 + ((redditoAnnuale - 55000) * 41 / 100);
                    break;
                case var reddito when reddito > 75000:
                    imposta = 25420 + ((redditoAnnuale - 75900) * 43 / 100);
                    break;
                default:
                    Console.WriteLine("Errore nel reperimento del reddito annuale.");
                    AgenziaDelleEntrate.Menu();
                    break;
            }
            return imposta;
        }
    }
}
