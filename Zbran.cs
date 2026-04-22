using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace arenka
{
    internal class Zbran
    {
        public int Poskozeni { get; set; }
        public string Nazev { get; set; }
        
        /// <summary>
        /// Nastavení parametrového konstruktoru
        /// </summary>
        public Zbran(int poskozeni, string nazev)
        {
            this.Poskozeni = poskozeni;
            this.Nazev = nazev;
        }
    }
}