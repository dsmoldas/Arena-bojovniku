using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace arenka
{
    internal class Program
    {
        static void Main(string[] args)
        {
            Random rnd = new Random();
            Bojovnik hrac;
            Bojovnik oponent;

            //Jméno hráče
            Console.WriteLine("Jak se chceš jmenovat?\n");
            string jmeno = Console.ReadLine();

            while (true)
            {
                Zbran mec = new Zbran(5, "Meč");
                Zbran kladivo = new Zbran(7, "Kladivo");
                Zbran luk = new Zbran(12, "Luk");

                string zbranVyber;

                //Výběr zbraně
                while (true)
                {
                    Console.Clear();

                    Console.WriteLine("Nyní si můžeš vybrat svoji zbraň, pro výběr napiš číslo...\n\n1. Meč\n2. Kladivo\n3. Luk\n");

                    zbranVyber = Console.ReadLine();

                    if (zbranVyber == "1" || zbranVyber == "2" || zbranVyber == "3")
                    {
                        break;
                    }
                    else
                    {
                        continue;
                    }
                }

                //Přiřazení zbraně
                if (zbranVyber == "1")
                {
                    hrac = new Bojovnik(jmeno, 100, 4, mec);
                }
                else if (zbranVyber == "2")
                {
                    hrac = new Bojovnik(jmeno, 110, 3, kladivo);
                }
                else
                {
                    hrac = new Bojovnik(jmeno, 80, 6, luk);
                }

                int nahodaOponent = rnd.Next(1, 4);

                //Naáhodné přiřazení zbraně "nehráči"
                if (nahodaOponent == 1)
                {
                    oponent = new Bojovnik("Oponent", 100, 4, mec);
                }
                else if (nahodaOponent == 2)
                {
                    oponent = new Bojovnik("Oponent", 110, 3, kladivo);
                }
                else
                {
                    oponent = new Bojovnik("Oponent", 80, 6, luk);
                }   

                //Přiřazení oponentů
                hrac.PrirazeniOponenta(oponent);
                oponent.PrirazeniOponenta(hrac);

                int kola = 1;

                Console.CursorVisible = false;
                Console.Clear();

                while (true)
                {
                    Console.Clear();

                    Bojovnik.Vypis(kola, oponent, hrac);

                    int nahoda = rnd.Next(0, 100);

                    if (nahoda <= 10) //Náhoda pro zvýšní síly
                    {
                        if (kola % 2 == 0)
                        {
                            hrac.Vylepseni(kola, oponent, hrac);
                        }
                        else
                        {
                            int x = rnd.Next(1, 3);

                            if (x == 1)
                            {
                                oponent.Sila += 2;
                                Console.SetCursorPosition(0, 12);
                                Console.WriteLine($"{oponent.Jmeno} nabral sílu ...");

                                Thread.Sleep(1000);
                                Console.Clear();
                            }
                            else
                            {
                                oponent.Utoc();
                            }
                        }
                    }
                    else if (nahoda <= 20) //Náhoda pro uzdravení
                    {
                        if (kola % 2 == 0)
                        {
                            hrac.UzdravSe(kola, oponent, hrac);
                        }
                        else
                        {
                            int x = rnd.Next(1, 3);

                            if (x == 1)
                            {
                                oponent.Hp += 10;
                                Console.SetCursorPosition(0, 12);
                                Console.WriteLine($"{oponent.Jmeno} se částečně uzdravil...");

                                Thread.Sleep(1000);
                                Console.Clear();
                            }
                            else 
                            { 
                                oponent.Utoc(); 
                            }
                        }
                    }
                    else //Normální boj
                    {
                        if (kola % 2 == 0)
                        {
                            hrac.Utoc();
                        }
                        else
                        {
                            oponent.Utoc();
                        }
                    }

                    if (hrac.Nazivu() == false || oponent.Nazivu() == false) //Kontrola "živosti"
                    {
                        break;
                    }

                    kola++; //Přidávání kol
                }

                //Výpis výhry
                if (hrac.Nazivu() == true)
                {
                    Console.ForegroundColor = ConsoleColor.Green;
                    hrac.VypisVyhry(kola);
                    Console.ResetColor();
                }
                else
                {
                    Console.ForegroundColor = ConsoleColor.Red;
                    Console.WriteLine($"Prohrál jsi po {kola} udatného boje...\n\nPro pokračování zmáčkni libovolnou klávesu...");
                    Console.ReadKey();
                    Console.ResetColor();
                }
            }
        }
    }
}