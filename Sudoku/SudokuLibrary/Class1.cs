using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Sudoku;
using System.Diagnostics;

namespace SudokuLibrary
{
    public class Class1
    {


        private IGame game;

        public Class1()
        {
            game = new Sudoku.Game();
        }

        public bool CreateGameAndWrite()
        {
            int i;
            game.create();
            game.write(out i);
            Debug.WriteLine("baab");
            return i == 1;
        }


    }
}
