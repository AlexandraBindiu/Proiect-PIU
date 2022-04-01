using System;
using System.Configuration;
using System.IO;
using System.Linq;
using LibrarieModele;
using NivelStocareDate;

namespace GestiuneFarmacie
{
    class Program
    {
        static void Main(string[] args)
        {
            //tema L4
            if (args.Length == 0)
                Console.Write("Linia de comanda nu contine argumente");
            else
            {
                // afisarea numarului de argumente
                Console.WriteLine("Numarul de argumente este: {0}", args.Length);
            }

            string numeFisier = ConfigurationManager.AppSettings["NumeFisier"];
            AdministrareMedicament_FisierText adminMedicamente = new AdministrareMedicament_FisierText(numeFisier);
            int nrMedicamente = 0;
            string denumireNoua = " ";
            double pretNou = 0; 
            string producatorNou = " ";
            int valabilitateNoua = 0;
            string Denumire = "";
            double Pret = 0;
            Medicament existingMedicament = new Medicament();

            string optiune;

            do
            {
                Console.WriteLine("~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~");
                Console.WriteLine("~ P. Afisare medicamente din fisier      ~");
                Console.WriteLine("~ S. Salvare medicament in fisier        ~");
                Console.WriteLine("~ A. Adauga medicament de la tastatura   ~");
                Console.WriteLine("~ C. Cauta medicament dupa denumire      ~");
                Console.WriteLine("~ V. Vizualizare produse vandute         ~");
                Console.WriteLine("~ X. Inchidere program                   ~");
                Console.WriteLine("~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~");
                Console.WriteLine("Alegeti o optiune: ");
                optiune = Console.ReadLine();
                switch (optiune.ToUpper())
                {
                    case "P":
                        Medicament[] medicamente = adminMedicamente.GetMedicamente(out nrMedicamente);
                        AfisareMedicamente(medicamente, nrMedicamente);
                        break;
                    case "S":
                        int idMedicament = adminMedicamente.GetLastMedicamentId() + 1;
                        nrMedicamente = nrMedicamente + 1;
                        Medicament medicament = new Medicament();
                        if (denumireNoua != "" || pretNou != 0 || producatorNou != "" || valabilitateNoua != 0)
                        {
                            //salvam medicamentul introdus
                            medicament = new Medicament(idMedicament, denumireNoua, pretNou, producatorNou, valabilitateNoua);
                        }
                        else
                        {
                            medicament = new Medicament(idMedicament, "Ibuprofen", 34.9, "Danem", 2024);
                        }

                        //adaugare medicament in fisier
                        adminMedicamente.AddMedicament(medicament);
                        break;

                    case "A":
                        Console.WriteLine("Introduceti denumirea medicamentului: ");
                        denumireNoua = Console.ReadLine();
                        Console.WriteLine("Introduceti pretul medicamentului: ");
                        pretNou = Convert.ToDouble(Console.ReadLine());
                        Console.WriteLine("Introduceti tara de provenienta: ");
                        producatorNou = Console.ReadLine();
                        Console.WriteLine("Introduceti anul de valabilitate a medicamentului: ");
                        valabilitateNoua = Convert.ToInt32(Console.ReadLine());
                        break;
                    case "C":
                        Console.WriteLine("Introduti denumirea medicamentului cautat:");
                        Denumire = Console.ReadLine();
                        existingMedicament = adminMedicamente.GetMedicament(Denumire);
                        if (existingMedicament == null)
                        {
                            Console.WriteLine("Medicamentul cautat nu exista!");
                        }
                        else
                        {
                            Console.WriteLine(string.Format("Medicamentul cautat are id-ul #{0} si pretul de {1} lei",
                                existingMedicament.GetIdMedicament(),
                                existingMedicament.GetPret()));
                        }
                        break;
                    /*case "P":
                        Console.WriteLine("Introduti denumirea medicamentului pe care doriti sa il stergeti:");
                        Denumire = Console.ReadLine();
                        
                        existingMedicament = adminMedicamente.GetMedicament(Denumire);
                        Console.WriteLine(existingMedicament.GetDenumire());
                        if (existingMedicament == null)
                        {
                            Console.WriteLine("Medicamentul cautat nu exista!");
                        }
                        else
                        {
                            var tempFile = Path.GetTempFileName();
                            var linesToKeep = File.ReadLines(numeFisier).Where(x => x != existingMedicament.GetDenumire());

                            File.WriteAllLines(tempFile, linesToKeep);

                            File.Delete(numeFisier);
                            File.Move(tempFile, numeFisier);
                        }
                        break;
                    */
                    case "V":
                        string[,] stocuri = new string[5, 2] { { "Paracetamol", "5 bucati" }, { "Ibusinus", "7 bucati" }, {"Faringo", "4 bucati"}, {"Strepsils","8 bucati"}, { "Ospen", "3 bucati" } };
                        Console.WriteLine("Astazi s-au vandut:");
                        //Console.WriteLine(stocuri[0, 0] + " - " + stocuri[0, 1]);
                        //Console.WriteLine(stocuri[1, 0] + " - " + stocuri[1, 1]);
                        //Console.WriteLine(stocuri[2, 0] + " - " + stocuri[2, 1]);
                        //Console.WriteLine(stocuri[3, 0] + " - " + stocuri[3, 1]);
                        //Console.WriteLine(stocuri[4, 0] + " - " + stocuri[4, 1]);
                        for (int i = 0; i < 5; i++)
                        {
                            for (int j = 0; j < 2; j++)
                                Console.Write(stocuri[i, j]+" ");
                            Console.Write("\n");
                        }
                        break;
                    case "X":
                        return;
                    default:
                        Console.WriteLine("Optiune inexistenta!");
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
                string infoMedicament = string.Format("Medicamentul cu id-ul #{0} are denumirea {1}, pretul de {2} lei, tara de origine {3} si este valabil pana in anul {4}",
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
