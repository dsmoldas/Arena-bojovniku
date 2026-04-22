using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace arenka
{
    internal class Bojovnik
    {
        Random rand = new Random();
        public string Jmeno {  get; set; }
        public decimal Hp {  get; set; }
        public int Sila { get; set; }
        Zbran Zbran { get; set; }
        Bojovnik Oponent { get; set; }

        /// <summary>
        /// Nastavení parametrového konstruktoru
        /// </summary>
        public Bojovnik(string jmeno, decimal Hp, int sila, Zbran zbran) 
        {
           this.Jmeno = jmeno;
           this.Hp = Hp;   
           this.Sila = sila;
           this.Zbran = zbran;
        }

        /// <summary>
        /// Přiřazení oponenta
        /// </summary>
        public void PrirazeniOponenta(Bojovnik oponent)
        {
            this.Oponent = oponent;
        }

        /// <summary>
        /// výpis všech hráčů
        /// </summary>
        public static void Vypis(int kola, Bojovnik nepřátel, Bojovnik hrac)
        {
            Console.WriteLine($"Kola: {kola}\n");

            Console.ForegroundColor = ConsoleColor.Yellow;

            Console.WriteLine(hrac.Jmeno);
            Console.WriteLine($"Životy: {hrac.Hp}");
            Console.WriteLine($"Síla: {hrac.Sila}");
            Console.WriteLine($"Zbraň: {hrac.Zbran.Nazev}\n");

            Console.ForegroundColor = ConsoleColor.Magenta;

            Console.WriteLine(nepřátel.Jmeno);
            Console.WriteLine($"Životy: {nepřátel.Hp}");
            Console.WriteLine($"Síla: {nepřátel.Sila}");
            Console.WriteLine($"Zbraň: {nepřátel.Zbran.Nazev}");

            Console.ResetColor();
        }

        /// <summary>
        /// výpis výhry
        /// </summary>
        public void VypisVyhry(int kola)
        {
            Console.Clear();
            Console.WriteLine($"Vyhrál {Jmeno} po {kola} kolech udatného boje\n\nPro pokračování zmáčkni libovolnou klávesu...");
            Console.ReadKey();
        }

        /// <summary>
        /// Útok
        /// </summary>
        public void Utoc()
        {
            decimal poskozeni;
            int nahoda = Random();

            Console.SetCursorPosition(0, 12);
            Console.WriteLine($"Útočí: {Jmeno}"); //Výpis kdo útočí (pro přehlednost)

            decimal prevod = (decimal)0.01; //Přetypání hodnoty double na decimal
            decimal x = Sila + Zbran.Poskozeni;

            if (nahoda == 0)
            {
                Oponent.Hp -= x;

                Console.SetCursorPosition(0, 14);
                Console.WriteLine($"Uděluje poškození {x}");

                Console.SetCursorPosition(0, 15);
                Console.WriteLine($"{Oponent.Jmeno} se neubránil a tak obrží plné poškození.");
            }
            else
            {
                if (x == 0)
                {
                    Console.SetCursorPosition(0, 14);
                    Console.WriteLine($"{Oponent.Jmeno} se ubránil a neobdrží žádné poškození.");
                }
                else
                {
                    x = x * (nahoda * prevod);
                    poskozeni = Math.Floor(x); //Zaokrouhlení
                    Oponent.Hp -= poskozeni;


                    Console.SetCursorPosition(0, 14);
                    Console.WriteLine($"Uděluje poškození {poskozeni}");

                    Console.SetCursorPosition(0, 15);
                    Console.WriteLine($"{Oponent.Jmeno} se částečně ubránil a obdrží pouze {nahoda}% poškození.");
                }
            }

            Thread.Sleep(1000);
            Console.Clear();
        }

        /// <summary>
        /// Zisk síly navíc pro jednu hru
        /// </summary>
        public void Vylepseni (int kola, Bojovnik nepřátel, Bojovnik hrac)
        {
            Console.CursorVisible = true;

            string znak;

            while (true)
            {
                Console.Clear();
                Console.WriteLine($"Kola: {kola}\n");
                Console.WriteLine($"{Jmeno} máš šanci získat sílu navíc.\nAle jedno kolo kvůli tomu vynecháš.\nJestli chceš zmáčkni klávesu A, pokud to nechceš zmáčkni klávesu N.\n");

                znak = Console.ReadLine().ToLower();

                if (znak == "a" || znak =="n")
                {
                    break;
                }
                else 
                {
                    continue;
                }
            }

            Console.CursorVisible = false;

            if (znak == "a")
            {
                Sila += 2;
            }
            else
            {
                Console.Clear();
                Vypis(kola, nepřátel, hrac);
                Utoc();
            }
        }

        /// <summary>
        /// Uzdravení se o 10 Hp
        /// </summary>
        public void UzdravSe (int kola, Bojovnik nepřátel, Bojovnik hrac)
        {
            Console.CursorVisible = true;

            string znak;

            while (true)
            {
                Console.Clear();
                Console.WriteLine($"Kola: {kola}\n");
                Console.WriteLine($"{Jmeno} máš šanci na uzdravení.\nAle jedno kolo kvůli tomu vynecháš.\nJestli chceš zmáčkni klávesu A, pokud to nechceš zmáčkni klávesu N.\n");

                znak = Console.ReadLine().ToLower();

                if (znak == "a" || znak == "n")
                {
                    break;
                }
                else
                {
                    continue;
                }
            }

            Console.CursorVisible = false;

            if (znak == "a")
            {
                Hp += 10;
            }
            else
            {
                Console.Clear();
                Vypis(kola, nepřátel, hrac);
                Utoc();
            }
        }

        /// <summary>
        /// Náhoda
        /// </summary>
        /// <returns>Náhodný číslo</returns>
        private int Random() 
        {
            return rand.Next(0, 101);
        }

        /// <summary>
        /// Kontrola "živosti"
        /// </summary>
        /// <returns> je naživu == true</returns>
        public bool Nazivu()
        {
            if (Hp <= 0)
            {
                return false;
            }
            else
            {
                return true;
            }     
        }
    }
}