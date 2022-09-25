//Matteo Rigolin
//5F
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ProvaIngresso
{
    class Program
    {

        static void Main(string[] args)
        {
            Menù();
            Console.ReadKey();
        }
        static void Menù()
        {
            //inizializzo le quantità di elettricità e gas consumati dall'utente
            double kWh=0, SMC=0;
            //lista con le bollette dei 5 sistemi + quella del sistema attuale dell'utente
            List<Bolletta> bollette = new List<Bolletta>();
            //i 5 sistemi di riscaldamento disponibili
            Caldaia_condensazione caldcond = new Caldaia_condensazione("Caldaia a condensazione", "gas", 1, 1375, 1.05, 0, 0);
            Caldaia_tradizionale caldtrad = new Caldaia_tradizionale("Caldaia tradizionale", "gas", 0.9, 1375, 1.05, 0, 0);
            Stufa_elettrica stufa = new Stufa_elettrica("Stufa elettrica", "elettricità", 1, 1550, 0.276, 0, 0);
            Pompa_economica pompaec = new Pompa_economica("Pompa economica", "elettricità", 2.8, 1000, 0.276, 0, 0);
            Pompa_buon_livello pompablv = new Pompa_buon_livello("Pompa di buon livello", "elettricità", 3.6, 3000, 0.276, 0, 0);
            //funzioni per fare inserire all'utente qunanto consuma
            Set_SMC(SMC);
            Set_kWh(kWh);
            //calcolo i costi totali in un anno dei 5 sistemi in base ai consumi dell'utente
            caldcond.consumo = SMC + (kWh / (10.7 * caldcond.Get_rendimento()));
            caldcond.costo_totale = caldcond.consumo * caldcond.Get_costo_annuo();

            caldtrad.consumo = SMC + (kWh / (10.7 * caldtrad.Get_rendimento()));
            caldtrad.costo_totale = caldtrad.consumo * caldtrad.Get_costo_annuo();

            stufa.consumo = SMC + ((kWh * 10.7) / stufa.Get_rendimento());
            stufa.costo_totale = stufa.consumo * stufa.Get_costo_annuo();

            pompaec.consumo = SMC + ((kWh * 10.7) / pompaec.Get_rendimento());
            pompaec.costo_totale = pompaec.consumo * pompaec.Get_costo_annuo();

            pompablv.consumo = SMC + ((kWh * 10.7) / pompablv.Get_rendimento());
            pompablv.costo_totale = pompablv.consumo * pompablv.Get_costo_annuo();
            //creo le 5 bollette che includono spese di installazione aggiuntive e l'utilizzo per un anno
            bollette.Add(new Bolletta(caldcond.Get_nome(), caldcond.costo_totale, 96, 47, 70, caldcond.Get_costo_installazione()));
            bollette.Add(new Bolletta(caldtrad.Get_nome(), caldtrad.costo_totale, 96, 47, 70, caldtrad.Get_costo_installazione()));
            bollette.Add(new Bolletta(stufa.Get_nome(), stufa.costo_totale, 96, 47, 70, stufa.Get_costo_installazione()));
            bollette.Add(new Bolletta(pompaec.Get_nome(), pompaec.costo_totale, 96, 47, 70, pompaec.Get_costo_installazione()));
            bollette.Add(new Bolletta(pompablv.Get_nome(), pompablv.costo_totale, 96, 47, 70, pompablv.Get_costo_installazione()));
            //l'utente sceglie il sistema di riscaldamento che usa attualmente
            string scelta;

            do
            {
                Console.WriteLine("\nInserisci il sistema di riscaldamento che utilizzi:" +
                "\n1 per la caldaia  a condensazione;" +
                "\n2 per la caldaia tradizionale;" +
                "\n3 per la stufa elettrica;" +
                "\n4 per la pompa di calore economica;" +
                "\n5 per la pompa di calore di buon livello;" +
                "\n6 per uscire.\n");
                scelta = Console.ReadLine();
            } while (scelta != "1" && scelta != "2" && scelta != "3" && scelta != "4" && scelta != "5" && scelta != "6");
            //in base alla scelta si passa a funzioni diverse apposite
            switch (scelta)
            {
                case "1": Scelta_caldaia_condensazione(caldcond, caldtrad, stufa, pompaec, pompablv, bollette); break;
                case "2": Scelta_caldaia_tradizionale(caldcond, caldtrad, stufa, pompaec, pompablv, bollette); break;
                case "3": Scelta_stufa_elettrica(caldcond, caldtrad, stufa, pompaec, pompablv, bollette); break;
                case "4": Scelta_pompa_economica(caldcond, caldtrad, stufa, pompaec, pompablv, bollette); break;
                case "5": Scelta_pompa_buon_livello(caldcond, caldtrad, stufa, pompaec, pompablv, bollette); break;
                case "6": Environment.Exit(0); break;
            }

        }

        static double Set_SMC(double SMC)
        {
            string consumato;
            bool controllo;
            do
            {
                Console.WriteLine("\nInserisci quanti SMC consumi all'anno: ");
                consumato = Console.ReadLine();
                controllo = double.TryParse(consumato, out SMC);
            } while (SMC < 0 || controllo == false);
            return SMC;
        }
        static double Set_kWh(double kWh)
        {
            string consumato;
            bool controllo;
            do
            {
                Console.WriteLine("\nInserisci quanti kWh consumi all'anno: ");
                consumato = Console.ReadLine();
                controllo = double.TryParse(consumato, out kWh);
            } while (kWh < 0 || controllo == false);
            return kWh;
        }

        static void Scelta_caldaia_condensazione(Caldaia_condensazione caldcond, Caldaia_tradizionale caldtrad, Stufa_elettrica stufa, Pompa_economica pompaec, Pompa_buon_livello pompablv, List<Bolletta> bollette)
        {
            //creo la bolletta con l'attuale sistema di riscaldamento dell'utente e la aggiungo in lista
            //i costi aggiuntivi sono a 0 perché il macchinario è già installato
            Bolletta bolletta_attuale = new Bolletta(caldcond.Get_nome(), caldcond.costo_totale, 0, 0, 0, 0);
            bollette.Add(bolletta_attuale);
            //ordino la lista in base alla spesa totale della bolletta
            bollette = bollette.OrderBy(b => b.Calcolo_totale()).ToList();  
            //il primo elemento della lista è la bolletta meno cara
            if (bolletta_attuale.Calcolo_totale() == bollette[0].Calcolo_totale())
            {
                //se la prima bolletta è attuale allora si consiglia di mantenere il riscaldamento già installato
                Console.WriteLine("\nIl sistema di riscaldamento attuale è il più conveniente");
            }
            else
            {
                //se la prima non è quella attuale allora si consiglia il sistema meno caro
                Console.WriteLine("\nIl sistema di riscaldamento più conveniente è: " + bollette[0].Get_nome_riscaldamento());
            }
        }
        static void Scelta_caldaia_tradizionale(Caldaia_condensazione caldcond, Caldaia_tradizionale caldtrad, Stufa_elettrica stufa, Pompa_economica pompaec, Pompa_buon_livello pompablv, List<Bolletta> bollette)
        {
            Bolletta bolletta_attuale = new Bolletta(caldtrad.Get_nome(), caldtrad.costo_totale, 0, 0, 0, 0);
            bollette.Add(bolletta_attuale);
            bollette = bollette.OrderBy(b => b.Calcolo_totale()).ToList();
            if (bolletta_attuale.Calcolo_totale() == bollette[1].Calcolo_totale())
            {
                Console.WriteLine("\nIl sistema di riscaldamento attuale è il più conveniente");
            }
            else
            {
                Console.WriteLine("\nIl sistema di riscaldamento più conveniente è: " + bollette[1].Get_nome_riscaldamento());
            }
        }
        static void Scelta_stufa_elettrica(Caldaia_condensazione caldcond, Caldaia_tradizionale caldtrad, Stufa_elettrica stufa, Pompa_economica pompaec, Pompa_buon_livello pompablv, List<Bolletta> bollette)
        {
            Bolletta bolletta_attuale = new Bolletta(stufa.Get_nome(), stufa.costo_totale, 0, 0, 0, 0);
            bollette.Add(bolletta_attuale);
            bollette = bollette.OrderBy(b => b.Calcolo_totale()).ToList();
            if (bolletta_attuale.Calcolo_totale() == bollette[2].Calcolo_totale())
            {
                Console.WriteLine("\nIl sistema di riscaldamento attuale è il più conveniente");
            }
            else
            {
                Console.WriteLine("\nIl sistema di riscaldamento più conveniente è: " + bollette[2].Get_nome_riscaldamento());
            }
        }
        static void Scelta_pompa_economica(Caldaia_condensazione caldcond, Caldaia_tradizionale caldtrad, Stufa_elettrica stufa, Pompa_economica pompaec, Pompa_buon_livello pompablv, List<Bolletta> bollette)
        {
            Bolletta bolletta_attuale = new Bolletta(pompaec.Get_nome(), pompaec.costo_totale, 0, 0, 0, 0);
            bollette.Add(bolletta_attuale);
            bollette = bollette.OrderBy(b => b.Calcolo_totale()).ToList();
            if (bolletta_attuale.Calcolo_totale() == bollette[3].Calcolo_totale())
            {
                Console.WriteLine("\nIl sistema di riscaldamento attuale è il più conveniente");
            }
            else
            {
                Console.WriteLine("\nIl sistema di riscaldamento più conveniente è: " + bollette[3].Get_nome_riscaldamento());
            }
        }
        static void Scelta_pompa_buon_livello(Caldaia_condensazione caldcond, Caldaia_tradizionale caldtrad, Stufa_elettrica stufa, Pompa_economica pompaec, Pompa_buon_livello pompablv, List<Bolletta> bollette)
        {
            Bolletta bolletta_attuale = new Bolletta(pompablv.Get_nome(), pompablv.costo_totale, 0, 0, 0, 0);
            bollette.Add(bolletta_attuale);
            bollette = bollette.OrderBy(b => b.Calcolo_totale()).ToList();
            if (bolletta_attuale.Calcolo_totale() == bollette[4].Calcolo_totale())
            {
                Console.WriteLine("\nIl sistema di riscaldamento attuale è il più conveniente");
            }
            else
            {
                Console.WriteLine("\nIl sistema di riscaldamento più conveniente è: " + bollette[4].Get_nome_riscaldamento());
            }
        }
    }
}
