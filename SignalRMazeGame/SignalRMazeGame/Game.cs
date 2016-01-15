using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace SignalRMazeGame
{
    public class Game
    {
        public string GameId { get; set; }

        public GameStatus Status { get; set; }

        public IList<Player> Players { get; set; }
    }

    public enum GameStatus
    {
        WAITING_FOR_PLAYERS, STARTED
    }

    public class Player
    {
        public string PlayerId { get; set; }

        public string Name { get; set; }

        public string Orientation { get; set; }
    }

    public class Maze
    {
        /// <summary>
        /// JSON grid of the map
        /// </summary>
        public string Grid { get; set; }
    }
}