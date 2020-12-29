using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Lab5.Models
{
    public class Game
    {
        public int Id { get; set; }     
        public string Name { get; set; }

        public string currentPicker { get; set; }
        public string Owner { get; set; }
        public string Opponent { get; set; }
        public bool isFree { get; set; }
       
    }
}
