using Lab5.Models;
using Lab5.ViewModels;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Lab5.Controllers
{
    [Authorize]
    public class GameController : Controller
    {
        ApplicationContext _db;
        UserManager<User> _userManager;
    

        public GameController(UserManager<User> userManager,ApplicationContext db)
        {
            _userManager = userManager;
            _db = db;           
        }

        [HttpPost]
        public async Task<IActionResult> Connect(int? id)
        {
            var currentUser = await _userManager.GetUserAsync(User);
            if (id == null)
                return NotFound();
            var game = await _db.Games.FirstOrDefaultAsync(x => x.Id == id);
            if (game == null)
                return NotFound();
            else
            {
                if(game.Owner != currentUser.Email)
                {
                    game.Opponent = currentUser.Email;
                    game.isFree = false;
                    _db.Games.UpdateRange(game);
                    await _db.SaveChangesAsync();
                    return RedirectToAction("Game", "Game", new { id = id});                    
                }
                else
                {
                    return NotFound();
                }
            }
                
        }

        
        public async  Task<IActionResult> Game(int? id)
        {
            var result = await _db.Games.FirstOrDefaultAsync(x => x.Id == id);
            
            if (result != null)
            {
                return View(result);
            }
            else
                return NotFound();
        }

        [HttpGet]
        public IActionResult Create()
        {
            return View();
        }


        [HttpPost]
        public async Task<IActionResult> Create(GameViewModel model)
        {
            if (ModelState.IsValid)
            {
                var currentUser = await _userManager.GetUserAsync(User);
                Game game = new Game { Name = model.Name, Owner = currentUser.Email, currentPicker=currentUser.Email, isFree=true};
                await _db.Games.AddAsync(game);
                await _db.SaveChangesAsync();
                return RedirectToAction("Game", "Game",new {id = game.Id});

            }
            return View(model);
        }

        public IActionResult AllGames()
        {
            return View (_db.Games.ToList());
        }

        
    }
}
