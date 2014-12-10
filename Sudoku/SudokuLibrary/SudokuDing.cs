using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Sudoku;
using System.Diagnostics;

namespace SudokuLibrary
{
    public class SudokuDing
    {


        private IGame game;

        public SudokuDing()
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

        public bool hint(out short x, out short y, out short value)
        {
            int result;
            game.hint(out x, out y, out value, out result);
            return result == 1;
        }

        public bool isValid()
        {
            int result;
            game.isValid(out result);
            return result == 1;
        }

        public bool read()
        {
            int result;
            game.read(out result);
            return result == 1;
        }

        public bool set(short x, short y, short value)
        {
            int result;
            game.set(x, y, value, out result);
            return result == 1;
        }




    }
}
