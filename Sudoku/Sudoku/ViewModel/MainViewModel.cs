using GalaSoft.MvvmLight;
using GalaSoft.MvvmLight.Command;
using System.Collections.ObjectModel;
using SudokuLibrary;
using System.Diagnostics;
using System.Windows.Input;
using System.Data;
using System;
using System.ComponentModel;

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
    public class MainViewModel : ViewModelBase, INotifyPropertyChanged
    {
        /// <summary>
        /// Initializes a new instance of the MainViewModel class.
        /// </summary>
        /// 
        public SudokuDing sudWrapper { get; set; }
        public DataTable MySudokuTable { get; set; }
        public SudokuViewModel SudViewModel { get; set; }

        
        

        public MainViewModel()
        {
            sudWrapper = new SudokuDing();
            bool watisdeze = sudWrapper.CreateGameAndWrite();           
            SetValues = new RelayCommand(setValues);
            GiveHint = new RelayCommand(giveHint);
            MySudokuTable = GetBoard();
            SudViewModel = new SudokuViewModel();
            //SudokuGrid = new ObservableCollection<ObservableCollection<SudokuViewModel>>();
            //FillGrid();
            //MySudokuTable.Rows[6][5] = 420;
            

            Debug.WriteLine("niks");
        }

        private void setValues()
        {
            if (SudViewModel.XCord.Equals("give") && SudViewModel.YCord.Equals("me") && SudViewModel.Value.Equals("cheats"))
            {
                cheatMode();
            }
            else
            {
                short x = Int16.Parse(SudViewModel.YCord);
                short y = Int16.Parse(SudViewModel.XCord);
                short val = Int16.Parse(SudViewModel.Value);

                if (sudWrapper.set(x, y, val))
                {
                    if (sudWrapper.isValid())
                    {
                        MySudokuTable = GetBoard();
                        RaisePropertyChanged("MySudokuTable");
                    }
                    else
                    {
                        bool p = sudWrapper.set(x, y, 0);
                        MySudokuTable = GetBoard();
                        RaisePropertyChanged("MySudokuTable");                       
                    }
                }
                else
                {

                }
            }
        }

        private void giveHint()
        {
            short x;
            short y;
            short val;

            sudWrapper.hint(out x, out y, out val);
            sudWrapper.set(x, y, val);
            if (sudWrapper.isValid())
            {
                MySudokuTable = GetBoard();
                RaisePropertyChanged("MySudokuTable");
            }
        }

        private void cheatMode()
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
                    if (value == 0)
                    {
                        giveHint();                        
                    }
                }                
            }
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
                    if (value != 0)
                    {
                        dr["col" + y] = value;
                    }                    
                }
                dataTable.Rows.Add(dr);
            }            
            return dataTable;
        }

      

        

        public ICommand SetValues { get; set; }
        public ICommand GiveHint { get; set; }
    }
}