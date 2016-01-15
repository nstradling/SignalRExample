using System.Linq;
using System.Collections.Generic;
using Microsoft.AspNet.SignalR;
using Microsoft.Owin;
using System;

[assembly: OwinStartup(typeof(SignalRMazeGame.Startup))]
namespace SignalRMazeGame
{
    public class GameHub : Hub
    {
        private const int MAX_PLAYERS_PER_GAME = 2;
        private static readonly IList<Game> _games = new List<Game>();

        /// <summary>
        /// Handles when a player joins a game. Will create a game if there isn't a free on available.
        /// </summary>
        /// <param name="name"></param>
        public void Join(string name)
        {
            // Remove a player from a game if they were already in one.
            //var existingGame = _games.FirstOrDefault(game => game.Players.Any(player => player.PlayerId == Context.ConnectionId));
            //if(existingGame != null)
            //{
            //    existingGame.Players.;
            //}

            // Find a game. For now just find one that isn't full.
            var ourGame = _games.FirstOrDefault(game => game.Players.Count < MAX_PLAYERS_PER_GAME);

            // If no game exists, create one and add the player to it.
            if(ourGame == null)
            {
                ourGame = new Game
                {
                    GameId = Guid.NewGuid().ToString(),
                    Players = new List<Player>
                    {
                        new Player { PlayerId = Context.ConnectionId, Name = name }
                    },
                    Status = GameStatus.WAITING_FOR_PLAYERS,
                };
                _games.Add(ourGame);                
            }
            else
            {
                ourGame.Players.Add(new Player { PlayerId = Context.ConnectionId, Name = name });
            }

            // Add the player to the game group.
            Groups.Add(Context.ConnectionId, ourGame.GameId);

            // Inform client of game ID.
            Clients.Caller.send(ourGame.GameId);

            // Inform other clients in game of new player.
            Clients.AllExcept(Context.ConnectionId).joined(Context.ConnectionId, name + " has joined the game!");

            // TODO: If two players have joined, start the game.
        }

        /// <summary>
        /// Notifies other players when a player moves.
        /// </summary>
        /// <param name="clientId"></param>
        /// <param name="location"></param>
        public void Move(string location)
        {
            // Get game ID using client ID.
            var ourGame = _games.FirstOrDefault(game => game.Players.Any(player => player.PlayerId == Context.ConnectionId));

            // Player is not in a game yet.
            if(ourGame == null)
            {
                return;
            }

            // Alert other players of move.
            Clients.AllExcept(Context.ConnectionId).moved(Context.ConnectionId, location);
        }
    }
}