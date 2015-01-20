using SudokuLibrary;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace SudokuMVC.Models
{
    public class MySudokuTableModel
    {

        public List<List<short>> list { get; set; }
        public String Name { get; set; }
        public int ID { get; set; }
        public int EmptySpots { get; set; }
        public SudokuDing Wrapper { get; set; }
    }
}