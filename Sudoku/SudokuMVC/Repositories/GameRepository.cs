using SudokuMVC.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace SudokuMVC.Repositories
{
    public class GameRepository
    {
        private Context context;
        public GameRepository()
        {
            context = new Context();
        }
        public void create(Game game)
        {
            context.Game.Add(game);
            context.SaveChanges();
        }

        public int GetID(string username)
        {
            var id = (from u in context.Game where u.UserName == username select u).ToList();
            return id[0].ID;
        }

        public bool Unique(Game game)
        {
            string name = game.UserName;

            var uniek = (from u in context.Game where u.UserName == name select u).ToList();

            if (uniek.Count() == 0)
            {
                return true;
            }
            else
            {
                return false;
            }
        }

       
    }
}