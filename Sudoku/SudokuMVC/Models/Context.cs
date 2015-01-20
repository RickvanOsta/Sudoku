using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Web;

namespace SudokuMVC.Models
{
    public class Context : DbContext
    {
        public DbSet<Cell> Cell { get; set; }
        public DbSet<Game> Game { get; set; }
    }
}