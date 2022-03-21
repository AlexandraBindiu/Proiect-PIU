using System;
using System.Configuration;
using LibrarieModele;
using NivelStocareDate;

namespace GestiuneFarmacie
{
    class Program
    {
        static void Main()
        {
            string numeFisier = ConfigurationManager.AppSettings["NumeFisier"];
            AdministrareMedicament_FisierText adminMedicamente = new AdministrareMedicament_FisierText(numeFisier);
            int nrMedicamente = 0;
            string denumireNoua = " ";
            string pretNou = "";
            string producatorNou = " ";
            string valabilitateNoua = "";
            string Denumire = "";
            Medicament existingMedicament = new Medicament();


            string optiune;
            do
            {
                Console.WriteLine("F. Afisare medicamente din fisier");
                Console.WriteLine("S. Salvare medicament in fisier");
                Console.WriteLine("C. Adauga medicament de la tastatura");
                Console.WriteLine("L. cauta medicament dupa denumire");
                Console.WriteLine("X. Inchidere program");
                Console.WriteLine("Alegeti o optiune");
                optiune = Console.ReadLine();
                switch (optiune.ToUpper())
                {
                    case "F":

                        Medicament[] medicamente = adminMedicamente.GetMedicamente(out nrMedicamente);
                        AfisareMedicamente(medicamente, nrMedicamente);
                        break;
                    case "S":
                        int idMedicament = adminMedicamente.GetLastMedicamentId() + 1;
                        //int idMedicament = nrMedicamente + 1;
                        nrMedicamente = nrMedicamente + 1;

                        //Medicament medicament = new Medicament(idMedicament, "Paracetamol", "15.7", "Andronescu", "2023");
                        
                        Medicament medicament = new Medicament();
                        if (denumireNoua != "" || pretNou!= "" || producatorNou != "" || valabilitateNoua != "")
                        {
                            //salvam medicamentul introdus
                            medicament = new Medicament(idMedicament, denumireNoua, pretNou, producatorNou,valabilitateNoua);
                        }
                        else
                        {
                            medicament = new Medicament(idMedicament, "Ibuprofen", "34.9", "Danem", "2024");
                        }

                        //adaugare student in fisier
                        adminMedicamente.AddMedicament(medicament);
                        break;
                    case "C":
                        Console.WriteLine("Introduceti denumirea medicamentului: ");
                        denumireNoua = Console.ReadLine();
                        Console.WriteLine("Introduceti pretul medicamentului: ");
                        pretNou = Console.ReadLine();
                        // pretNou = Convert.ToInt32(pretNou);
                        Console.WriteLine("Introduceti producatorul medicamentului: ");
                        producatorNou = Console.ReadLine();
                        Console.WriteLine("Introduceti anul de valabilitate a medicamentului: ");
                        valabilitateNoua = Console.ReadLine();
                        break;
                    case "L":
                        Console.WriteLine("Introduti denumirea medicamentului cautat:");
                        Denumire = Console.ReadLine();
                        existingMedicament = adminMedicamente.GetMedicament(Denumire);
                        if(existingMedicament == null)
                        {
                            Console.WriteLine("Medicamentul cautat nu exista!");
                        }
                        else
                        {
                            Console.WriteLine(string.Format("Medicamentul cautat are id-ul #{0} si pretul {1}",
                                existingMedicament.GetIdMedicament(),
                                existingMedicament.GetPret() ?? "NECUNOSCUT"));
                        }
                        
                        break;
                    case "X":
                        return;
                    default:
                        Console.WriteLine("Optiune inexistenta");
                        break;
                }
            } while (optiune.ToUpper() != "X");

            Console.ReadKey();
        }

        public static void AfisareMedicamente(Medicament[] medicamente, int nrMedicamente)
        {
            Console.WriteLine("Medicamentele sunt:");
            for (int contor = 0; contor < nrMedicamente; contor++)
            {
                string infoMedicament = string.Format("Medicamentul cu id-ul #{0} are denumirea: {1}, pretul {2}, producatorul {3} si este valabil pana in anul {4}",
                   medicamente[contor].GetIdMedicament(),
                   medicamente[contor].GetDenumire() ?? " NECUNOSCUT ",
                   medicamente[contor].GetPret(),
                   medicamente[contor].GetProducator() ?? " NECUNOSCUT ",
                   medicamente[contor].GetValabilitate());

                Console.WriteLine(infoMedicament);
            }
        }
    }
}
