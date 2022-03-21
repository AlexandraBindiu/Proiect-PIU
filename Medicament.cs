using System;

namespace LibrarieModele
{
    public class Medicament
    {
        //constante
        private const char SEPARATOR_PRINCIPAL_FISIER = ';';

        private const int ID = 0;
        private const int DENUMIRE = 1;
        private const int PRET = 2;
        private const int PRODUCATOR = 3;
        private const int VALABILITATE = 4;

        //proprietati auto-implemented
        private int idMedicament; //identificator unic medicament
        private string denumire;
        private string pret;
        private string producator;
        private string valabilitate;  //anul in care medicamentul expira

        //contructor implicit
        public Medicament()
        {
            denumire = producator = pret =valabilitate= string.Empty;
            //pret = 0;
            //valabilitate = 0;
        }

        //constructor cu parametri
        public Medicament(int idMedicament, string denumire, string pret, string producator, string valabilitate)
        {
            this.idMedicament = idMedicament;
            this.denumire = denumire;
            this.pret = pret;
            this.producator = producator;
            this.valabilitate = valabilitate;
        }

        //constructor cu un singur parametru de tip string care reprezinta o linie dintr-un fisier text
        public Medicament(string linieFisier)
        {
            var dateFisier = linieFisier.Split(SEPARATOR_PRINCIPAL_FISIER);

            //ordinea de preluare a campurilor este data de ordinea in care au fost scrise in fisier prin apelul implicit al metodei ConversieLaSir_PentruFisier()
            idMedicament = Convert.ToInt32(dateFisier[ID]);
            denumire = dateFisier[DENUMIRE];
            pret = dateFisier[PRET];
            producator = dateFisier[PRODUCATOR];
            valabilitate = dateFisier[VALABILITATE];
        }

        public string ConversieLaSir_PentruFisier()
        {
            string obiectMedicamentPentruFisier = string.Format("{1}{0}{2}{0}{3}{0}{4}{0}{5}{0}",
                   SEPARATOR_PRINCIPAL_FISIER,
                   idMedicament.ToString(),
                   denumire.ToString(),
                   pret.ToString(),
                   producator.ToString(),
                   valabilitate.ToString());

            return obiectMedicamentPentruFisier;
        }
        public int GetIdMedicament()
        {
            return idMedicament;
        }

        public string GetDenumire()
        {
            return denumire;
        }
        public string GetPret()
        {
            return pret;
        }
        public string GetProducator()
        {
            return producator;
        }
        public string GetValabilitate()
        {
            return valabilitate;
        }
    }
}
