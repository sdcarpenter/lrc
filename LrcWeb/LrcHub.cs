using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using LrcData;
using Microsoft.AspNetCore.SignalR;
using Microsoft.EntityFrameworkCore;

namespace LrcWeb
{
    public class LrcHub : Hub
    {
        public const string HubUrl = "/lrc";

        public async Task Broadcast(string username, string message)
        {
            await Clients.All.SendAsync("Broadcast", username, message);
        }

        public async Task JoinOrCreateRoom(string roomName, string username)
        {
            await using var ctx = new LrcContextFactory().CreateDbContext(null);

            var room = await ctx.Rooms.SingleOrDefaultAsync(c => c.Name == roomName);

            if (room == null)
            {
                ctx.Add(new Room {Name = roomName});
                await ctx.SaveChangesAsync();
            }

            Console.WriteLine($"{username} wants to join room {roomName}");
        }

        public override Task OnConnectedAsync()
        {
            Console.WriteLine($"{Context.ConnectionId} connected");
            return base.OnConnectedAsync();
        }

        public override async Task OnDisconnectedAsync(Exception e)
        {
            Console.WriteLine($"Disconnected {e?.Message} {Context.ConnectionId}");
            await base.OnDisconnectedAsync(e);
        }
    }
}
