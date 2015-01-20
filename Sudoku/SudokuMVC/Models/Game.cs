using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;

namespace SudokuMVC.Models
{
    public class Game
    {
        [Key]
        public int ID { get; set; }
        public string UserName { get; set; }
    }
}
