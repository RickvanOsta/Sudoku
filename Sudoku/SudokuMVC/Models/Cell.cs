using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;

namespace SudokuMVC.Models
{
    public class Cell
    {
        [Key]
        public int ID { get; set; }
        public int GameID { get; set; }
        public short X { get; set; }
        public short Y { get; set; }
        public short Value { get; set; }
    }
}
