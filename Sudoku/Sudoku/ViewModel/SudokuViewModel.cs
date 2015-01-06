using Sudoku.Model;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;

namespace Sudoku.ViewModel
{
    public class SudokuViewModel : INotifyPropertyChanged
    {

        public string Value
        {
            get { return _unit.value; }
            set { _unit.value = value; OnPropertyChanged(); }
        }
        public string XCord
        {
            get { return _unit.xCord; }
            set { _unit.xCord = value; OnPropertyChanged(); }
        }
        public string YCord
        {
            get { return _unit.yCord; }
            set { _unit.yCord = value; OnPropertyChanged(); }
        }

        private SudokuUnit _unit;

        public SudokuViewModel()
        {
            this._unit = new SudokuUnit();
        }

        public SudokuViewModel(SudokuUnit unit)
        {
            this._unit = unit;
        }

        public event PropertyChangedEventHandler PropertyChanged;



        protected void OnPropertyChanged([CallerMemberName] string propertyName = "")
        {
            if (PropertyChanged != null)
            {
                PropertyChanged(this, new PropertyChangedEventArgs(propertyName));
            }
        }
    }
}
