using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace SudokuMVC.Models
{
    public class Turn
    {
        [Display(Name = "X positie")]
        public short x { get; set; }
        [Display(Name = "Y positie")]
        public short y { get; set; }
        [Required]
        public string GameMode { get; set; }
        public short Value { get; set; }
    }
}