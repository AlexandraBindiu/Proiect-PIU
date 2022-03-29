using LibrarieModele;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;

namespace NivelStocareDate
{
    //tema L2
    public class AdministrareMedicament_FisierText
    {
        private const int NR_MAX_MEDICAMENTE = 80;
        private string numeFisier;

        public AdministrareMedicament_FisierText(string numeFisier)
        {
            this.numeFisier = numeFisier;
            Stream streamFisierText = File.Open(numeFisier, FileMode.OpenOrCreate);
            streamFisierText.Close();
        }

        public void AddMedicament(Medicament medicament)
        {

            using (StreamWriter streamWriterFisierText = new StreamWriter(numeFisier, true))
            {
                streamWriterFisierText.WriteLine(medicament.ConversieLaSir_PentruFisier());
            }
        }
        public List<Medicament> Get_Medicamente()
        {
            List<Medicament> medicamente = new List<Medicament>();
            using (StreamReader sr = new StreamReader(numeFisier))
            {
                string line;

                //citeste cate o linie si creaza un obiect de tip Medicament pe baza datelor din linia citita
                while ((line = sr.ReadLine()) != null)
                {
                    Medicament p = new Medicament(line);
                    medicamente.Add(p);
                }
            }
            return medicamente;
        }

        public Medicament[] GetMedicamente(out int nrMedicamente)
        {
            Medicament[] medicamente = new Medicament[NR_MAX_MEDICAMENTE];
            using (StreamReader streamReader = new StreamReader(numeFisier))
            {
                string linieFisier;
                nrMedicamente = 0;

                // citeste cate o linie si creaza un obiect de tip Medicament
                // pe baza datelor din linia citita
                while ((linieFisier = streamReader.ReadLine()) != null)
                {
                    medicamente[nrMedicamente++] = new Medicament(linieFisier);
                }
            }

            return medicamente;
        }

        //functie pentru a determina ultimul id 
        public int GetLastMedicamentId()
        {
            try
            {
                var lastLine = File.ReadLines(numeFisier).Last();
                Medicament medicament = new Medicament(lastLine);
                return medicament.GetIdMedicament();
            } catch (Exception ex) { return 0; }
        }
        //tema L3
        //functie pentru cautare medicament dupa denumire
        public Medicament GetMedicament(string denumire)
        {
            
            using (StreamReader streamReader = new StreamReader(numeFisier))
            {
                string linieFisier;

                while ((linieFisier = streamReader.ReadLine()) != null)
                {
                    Medicament medicament = new Medicament(linieFisier);
                    if (medicament.GetDenumire().Equals(denumire))
                    {
                        return medicament;
                    }
                }
                return null;
            }
        }

        //modifica pretul
        //functia inca nu este apelata
        public bool UpdateMedicament(Medicament MedicamentActualizat)
        {
            List<Medicament> medicament = Get_Medicamente();
            bool actualizareCuSucces = false;
            using (StreamWriter swFisierText = new StreamWriter(numeFisier, false))
            {
                foreach (Medicament med in medicament)
                {
                    //informatiile despre medicamentul actualizat vor fi preluate din parametrul "MedicamentActualizat"
                    if (med.GetPret() != MedicamentActualizat.GetPret())
                    {
                        swFisierText.WriteLine(med.ConversieLaSir_PentruFisier());
                    }
                    else
                    {
                        swFisierText.WriteLine(MedicamentActualizat.ConversieLaSir_PentruFisier());
                    }
                }
                actualizareCuSucces = true;
            }

            return actualizareCuSucces;
        }


        //functie stergere medicament
        //functia inca nu este apelata
        public bool StergereMedicament(Medicament MedicamentActualizat)
        {
            List<Medicament> medicament = Get_Medicamente();
            bool actualizareCuSucces = false;
            using (StreamWriter swFisierText = new StreamWriter(numeFisier, false))
            {
                foreach (Medicament med in medicament)
                {

                    if (med.GetDenumire() != MedicamentActualizat.GetDenumire())
                    {
                        swFisierText.WriteLine(med.ConversieLaSir_PentruFisier());
                    }
                    else
                    {
                        //swFisierText.WriteLineMedicamentActualizat.ConversieLaSir_PentruFisier());
                    }
                }
                actualizareCuSucces = true;
            }

            return actualizareCuSucces;
        }
    }
}