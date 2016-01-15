using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace SignalRMazeGame
{
    /// <summary>
    /// Generates a maze for the game.
    /// 
    /// 0 = empty
    /// 1 = wall
    /// 2 = start
    /// 3 = end
    /// </summary>
    public class StaticMazeGenerator : IMazeGenerator
    {
        public string Generate()
        {
            return "[[1,1,0,1,1,1,1,1,0,1]," + 
                    "[1,1,0,0,1,0,1,1,0,0]," +
                    "[1,1,1,0,0,0,0,1,1,0]," +
                    "[1,1,1,0,1,1,0,1,1,0]," + 
                    "[1,0,0,0,1,3,1,0,1,0]," +
                    "[1,0,1,1,1,0,1,0,1,0]," +
                    "[1,0,0,1,0,0,1,0,0,0]," +
                    "[1,1,0,1,0,1,1,0,1,1]," +
                    "[1,0,0,0,0,0,0,0,1,1]," + 
                    "[1,2,1,1,1,1,1,1,1,1]]";
        }
    }
}