using GalaSoft.MvvmLight;
using GalaSoft.MvvmLight.Command;
using System.Collections.ObjectModel;
using SudokuLibrary;
using System.Diagnostics;
using System.Windows.Input;
using System.Data;
using System;

namespace Sudoku.ViewModel
{
    /// <summary>
    /// This class contains properties that the main View can data bind to.
    /// <para>
    /// Use the <strong>mvvminpc</strong> snippet to add bindable properties to this ViewModel.
    /// </para>
    /// <para>
    /// You can also use Blend to data bind with the tool's support.
    /// </para>
    /// <para>
    /// See http://www.galasoft.ch/mvvm
    /// </para>
    /// </summary>
    public class MainViewModel : ViewModelBase
    {
        /// <summary>
        /// Initializes a new instance of the MainViewModel class.
        /// </summary>
        /// 
        public SudokuDing sudWrapper { get; set; }
        public DataTable MySudokuTable { get; set; }
        

        public MainViewModel()
        {
            sudWrapper = new SudokuDing();
            bool watisdeze = sudWrapper.CreateGameAndWrite();           
            SetValues = new RelayCommand(test);
            bool aids = sudWrapper.set(6, 6, 2);
            MySudokuTable = GetBoard();
            //SudokuGrid = new ObservableCollection<ObservableCollection<SudokuViewModel>>();
            //FillGrid();
            //MySudokuTable.Rows[6][6] = 420;

            Debug.WriteLine("niks");
        }

        private void test()
        {
            short x = 3;
            short y = 4;
            short val = 9;
            bool werktdit = sudWrapper.set(x, y, val);
            
            //bool endezedan = sudWrapper.hint(out x, out y, out val);
            //bool baab = sudWrapper.read();
            Debug.WriteLine("nik2s");
        }

        //public ObservableCollection<SudokuViewModel> SudokuGrid;

       public ObservableCollection<ObservableCollection<SudokuViewModel>> SudokuGrid { get; set; }

       public DataTable GetBoard()
        {
            DataTable dataTable = new DataTable();

            for (int i = 1; i <= 9; i++)
            {
                DataColumn dc = new DataColumn("col" + i);
                dataTable.Columns.Add(dc);
            }

            for (Int16 x = 1; x <= 9; x++)
            {
                DataRow dr = dataTable.NewRow();
                for (Int16 y = 1; y <= 9; y++)
                {
                    short value = 0;
                    sudWrapper.get((short)x, (short)y, out value);
                    dr["col" + y] = value;
                }
                dataTable.Rows.Add(dr);
            }
            
            return dataTable;
        }

       

        private void FillGrid()
        {
            DataTable temp = GetBoard();

            for (int x = 0; x < 9; x++)
            {
                SudokuGrid.Add(new ObservableCollection<SudokuViewModel>());
                for (int y = 0; y < 9; y++)
                {
                    var obj = new SudokuViewModel()
                    {
                       Value = Int16.Parse((string)temp.Rows[y][x])
                    };
                    SudokuGrid[x].Add(obj);
                    
                }
            }
        }

        

        public ICommand SetValues { get; set; }
    }
}