using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MovieApp
{
     public class MovieBase
    {
        public int MovieID { get; set; }
        public string MovieName { get; set; }
        public string MovieCategory { get; set; }
        public double MovieIMDB { get; set; }
        public string MovieIMG { get; set; }
        public string MovieURL { get; set; }

        // Burada genel film özellikleri tanımlanabilir.
    }
}
