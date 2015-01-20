using SudokuMVC.Models;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Web;

namespace SudokuMVC.Repositories
{
    public class CellRepository
    {
        private Context context;

        public CellRepository()
        {
            context = new Context();
        }
        public List<List<short>> getBoard(int id)
        {
            List<List<short>> list = new List<List<short>>();
            for (int i = 0; i < 9; i++)
            {
                List<short> li = new List<short>();
                for (int x = 0; x < 9; x++)
                {
                    li.Add(0);
                }
                list.Add(li);
            }

            var cellen = (from u in context.Cell where u.GameID == id select u).ToList();
            for (int i = 0; i < cellen.Count(); i++)
            {
                list[cellen[i].X][cellen[i].Y] = cellen[i].Value;
            }
            return list;
        }

        public void saveBoard(List<List<short>> list, int id)
        {
            Cell cel;
            for (short y = 0; y < list.Count(); y++)
            {
                cel = new Cell();
                for (short x = 0; x < list.Count(); x++)
                {
                    cel.GameID = id;
                    cel.X = x;
                    cel.Y = y;
                    cel.Value = list[x][y];
                    var cels = (from u in context.Cell where u.GameID == cel.GameID & u.X == cel.X & u.Y == cel.Y select u).ToList();

                    if (cels.Count != 0)
                    {

                        cel.ID = cels[0].ID;
                        var oud = cels[0];
                        if (oud.ID == cel.ID && oud.GameID == cel.GameID && oud.Value == cel.Value && oud.X == cel.X && oud.Y == cel.Y)
                        {
                            context.Entry(oud).CurrentValues.SetValues(cel);
                            context.SaveChanges();
                        }

                    }
                    else
                    {
                        context.Cell.Add(cel);
                        context.SaveChanges();
                    }
                }
            }
        }

        public void Create(Cell cel)
        {
            context.Cell.Add(cel);
            context.SaveChanges();
        }

    }
}