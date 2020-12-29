using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.SignalR;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Lab5.Models
{
    public class ChatHub : Hub
    {
        private readonly UserManager<User> _userManager;
        ApplicationContext _db;

        public ChatHub(UserManager<User> userManager, ApplicationContext db)
        {
            _userManager = userManager;
            _db = db;
        }
        

        [Authorize]
        public async Task Send(string id,string gameId,int i)
        {
           
            var userName = Context.User.Identity.Name;         
            var currentGame = await _db.Games.FirstOrDefaultAsync(x => x.Id == Convert.ToInt32(gameId));
            var player1 = currentGame.Owner;
            var player2 = currentGame.Opponent;
            var picker = currentGame.currentPicker;
            if(userName == picker && picker != player2)
            {                
                char message = 'X';
                await Clients.User(player2).SendAsync("Receive", message, id,userName,i);
                await Clients.Caller.SendAsync("Receive", message, id,userName,i);
                currentGame.currentPicker = player2;
                _db.Games.UpdateRange(currentGame);
                await _db.SaveChangesAsync();
            }
            if(userName == picker && picker != player1)
            {               
                char message = 'O';
                await Clients.User(player1).SendAsync("Receive", message, id,userName,i);
                await Clients.Caller.SendAsync("Receive", message, id,userName,i);
                currentGame.currentPicker = player1;
                _db.Games.UpdateRange(currentGame);
                await _db.SaveChangesAsync();
            }             

        }

        public override async Task OnConnectedAsync()
        {
            await Clients.All.SendAsync("Notify", $"{Context.User.Identity.Name} присоединяется");
            await base.OnConnectedAsync();
        }
        public override async Task OnDisconnectedAsync(Exception exception)
        {
            await Clients.All.SendAsync("Notify", $"{Context.User.Identity.Name} покинул в чат");
            await base.OnDisconnectedAsync(exception);
        }
    }
}
