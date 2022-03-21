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
            try
            { 
                using (StreamWriter streamWriterFisierText = new StreamWriter(numeFisier, true))
                {
                     streamWriterFisierText.WriteLine(medicament.ConversieLaSir_PentruFisier());
                }
            }
            catch (IOException eIO)
            {
                throw new Exception("Eroare la deschiderea fisierului. Mesaj: " + eIO.Message);
            }
            catch (Exception eGen)
            {
                throw new Exception("Eroare generica. Mesaj: " + eGen.Message);
            }
        }


        public List<Medicament> Get_Medicamente()
        {
            List<Medicament> medicamente = new List<Medicament>();

            try
            {
                // instructiunea 'using' va apela sr.Close()
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
            }
            catch (IOException eIO)
            {
                throw new Exception("Eroare la deschiderea fisierului. Mesaj: " + eIO.Message);
            }
            catch (Exception eGen)
            {
                throw new Exception("Eroare generica. Mesaj: " + eGen.Message);
            }

            return medicamente;
        }




        public Medicament[] GetMedicamente(out int nrMedicamente)
        {
            Medicament[] medicamente = new Medicament[NR_MAX_MEDICAMENTE];

            try
            {
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
            }
            catch (IOException eIO)
            {
                throw new Exception("Eroare la deschiderea fisierului. Mesaj: " + eIO.Message);
            }
            catch (Exception eGen)
            {
                throw new Exception("Eroare generica. Mesaj: " + eGen.Message);
            }
            return medicamente;
        }

        //functie pentru a determina ultimul id 
        public int GetLastMedicamentId()
        {
            var lastLine = File.ReadLines(numeFisier).Last();
            Medicament medicament = new Medicament(lastLine);
            return medicament.GetIdMedicament();

        }


        //tema L3
        //functie pentru cautare medicament dupa denumire
        public Medicament GetMedicament(string denumire)
         {
            try
            {
                // instructiunea 'using' va apela sr.Close()
                using (StreamReader sr = new StreamReader(numeFisier))
                {
                    string lineFisier;

                    //citeste cate o linie si creaza un obiect de tip Persoana pe baza datelor din linia citita
                    while ((lineFisier = sr.ReadLine()) != null)
                    {
                        Medicament medicament = new Medicament(lineFisier);
                        if (medicament.GetDenumire().Equals(denumire))
                            return medicament;
                    }
                }
            }
            catch (IOException eIO)
            {
                throw new Exception("Eroare la deschiderea fisierului. Mesaj: " + eIO.Message);
            }
            catch (Exception eGen)
            {
                throw new Exception("Eroare generica. Mesaj: " + eGen.Message);
            }
            return null;

        }

        //modifica pretul
        //functia inca nu este apelata
        public bool UpdateMedicament(Medicament MedicamentActualizat)
        {
            List<Medicament> medicament = Get_Medicamente();
            bool actualizareCuSucces = false;
            try
            {
                //instructiunea 'using' va apela la final swFisierText.Close();
                //al doilea parametru setat la 'false' al constructorului StreamWriter indica modul 'overwrite' de deschidere al fisierului
                using (StreamWriter swFisierText = new StreamWriter(numeFisier, false))
                {
                    foreach (Medicament med in medicament) 
                    {
                        //informatiile despre persoana actualizat vor fi preluate din parametrul "persoanaActualizata"
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
            }
            catch (IOException eIO)
            {
                throw new Exception("Eroare la deschiderea fisierului. Mesaj: " + eIO.Message);
            }
            catch (Exception eGen)
            {
                throw new Exception("Eroare generica. Mesaj: " + eGen.Message);
            }

            return actualizareCuSucces;
        }


        //functie stergere medicament
        //functia inca nu este apelata
        public bool StergereMedicament(Medicament MedicamentActualizat)
        {
            List<Medicament> persoana = Get_Medicamente();
            bool actualizareCuSucces = false;
            try
            {

                using (StreamWriter swFisierText = new StreamWriter(numeFisier, false))
                {
                    foreach (Medicament pers in persoana)
                    {

                        if (pers.GetDenumire()!= MedicamentActualizat.GetDenumire())
                        {
                            swFisierText.WriteLine(pers.ConversieLaSir_PentruFisier());
                        }
                        else
                        {
                            //swFisierText.WriteLine(persoanaActualizata.ConversieLaSir_PentruFisier());
                        }
                    }
                    actualizareCuSucces = true;
                }
            }
            catch (IOException eIO)
            {
                throw new Exception("Eroare la deschiderea fisierului. Mesaj: " + eIO.Message);
            }
            catch (Exception eGen)
            {
                throw new Exception("Eroare generica. Mesaj: " + eGen.Message);
            }

            return actualizareCuSucces;
        }
    }
}