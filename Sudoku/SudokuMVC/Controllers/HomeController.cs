using SudokuLibrary;
using SudokuMVC.Models;
using SudokuMVC.Repositories;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace SudokuMVC.Controllers
{
    public class HomeController : Controller
    {
        public int EmptySpots { get; set; }
        public List<List<short>> SudTable { get; set; }
        public SudokuDing sudWrapper { get; set; }
        public CellRepository cellRepo { get; set; }
        public GameRepository gameRepo { get; set; }


        public HomeController()
        {
            sudWrapper = new SudokuDing();
            cellRepo = new CellRepository();
            gameRepo = new GameRepository();

        }

        public ActionResult Index()
        {

            return View(new Game());
        }

        [HttpPost]
        public ActionResult Index(Game model)
        {
            int id;
            if (gameRepo.Unique(model))
            {
                sudWrapper.CreateGameAndWrite();
                SudTable = GetStartBoard();
                gameRepo.create(model);
                id = gameRepo.GetID(model.UserName);
            }
            else
            {
                id = gameRepo.GetID(model.UserName);
                sudWrapper.create();
                LoadBoard(id);
                SudTable = GetStartBoard();
            }

            MySudokuTableModel game = new MySudokuTableModel();
            game.ID = id;
            game.Name = model.UserName;
            game.list = SudTable;
            game.EmptySpots = EmptySpots;
            game.Wrapper = sudWrapper;
            

            TempData["model"] = game;

            return RedirectToAction("Game");
        }

        public ActionResult Game()
        {
            MySudokuTableModel model = (MySudokuTableModel)TempData["model"];
            if (TempData["Error"] != null)
            {
                bool error = (bool)TempData["Error"];
                if (error)
                {
                    ViewData["error"] = "Ongeldige zet";
                }
            }
            ViewData["model"] = model;
            TempData["model"] = model;
            
            return View(new Turn());
        }
        [HttpPost]
        public ActionResult Game(Turn turn)
        {
            MySudokuTableModel game = (MySudokuTableModel)TempData["model"];
            cellRepo.saveBoard(game.list, game.ID);

            if (turn.GameMode.Equals("normal"))
            {
                short waarde = game.list[turn.y - 1][turn.x - 1];

                bool gelukt = game.Wrapper.set(turn.y, turn.x, turn.Value);
                if (!gelukt || waarde != 0)
                {
                    TempData["Error"] = true;
                }
                else
                {
                    int x = turn.x - 1;
                    int y = turn.y - 1;
                    game.list[y][x] = turn.Value;
                    game.EmptySpots--;
                }
            }
            else
            {
                //this.LoadBoard(game.ID);
                game = Cheatmode(game);


            }
            if (game.EmptySpots == 0)
            {
                //game ended
                return RedirectToAction("Index");
            }
            else
            {
                sudWrapper.write();
                TempData["model"] = game;
                return RedirectToAction("Game");
            }           
        }







        public List<List<short>> GetStartBoard()
        {
            EmptySpots = 0;
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

            for (Int16 y = 1; y < 10; y++)
            {
                for (Int16 x = 1; x < 10; x++)
                {

                    short value;
                    sudWrapper.get(y, x, out value);

                    if (value != 0)
                    {
                        list[(y - 1)][(x - 1)] = value;
                    }
                    else
                    {
                        list[(y - 1)][(x - 1)] = value;
                        EmptySpots++;
                    }
                }
            }
            return list;
        }

        public void LoadBoard(int id)
        {
            List<List<short>> bord = cellRepo.getBoard(id);
            for (Int16 y = 1; y < 9; y++)
            {
                for (Int16 x = 1; x < 9; x++)
                {
                    short value = bord[x][y];                    
                    Boolean gelukt = sudWrapper.set(x, y, value);
                }
            }
            List<List<short>> bord2 = this.GetStartBoard();
        }

        public MySudokuTableModel Cheatmode(MySudokuTableModel game)
        {
            MySudokuTableModel temp = game;
            int amount = game.EmptySpots - 2;
            for (int i = 0; i < amount; i++)
            {
                short x;
                short y;
                short value;
                
                if (temp.Wrapper.hint(out x, out y, out value))
                {
                    temp.Wrapper.set(x, y, value);
                    temp.list[(x - 1)][(y - 1)] = value;
                    temp.EmptySpots--;
                    temp.Wrapper.write();
                }
                else
                {
                    break;
                }
            }
            return temp;
        }
    }
}