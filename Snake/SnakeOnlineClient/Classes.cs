using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SnakeOnlineClient
{
    public enum Direction
    {
        Left = 0,
        Top = 1,
        Right = 2,
        Bottom = 3
    }

    public sealed class NameResponse
    {
        public string Name { get; set; }
    }

    public sealed class DirectionRequest
    {
        public string Direction { get; set; }
        public string Token { get; set; }
    }

    public sealed class Point
    {
        public int X { get; set; }
        public int Y { get; set; }
    }

    public sealed class Size
    {
        public int Width { get; set; }
        public int Height { get; set; }
    }

    public class Rectangle
    {
        public int X { get; set; }
        public int Y { get; set; }
        public int Width { get; set; }
        public int Height { get; set; }
    }

    public sealed class PlayerState
    {
        public string Name { get; set; }
        public bool IsSpawnProtected { get; set; }
        public List<Point> Snake { get; set; }
    }

    public sealed class GameStateResponse
    {
        public bool IsStarted { get; set; }
        public bool IsPaused { get; set; }
        public int RoundNumber { get; set; }
        public int TurnNumber { get; set; }
        public int TurnTimeMilliseconds { get; set; }
        public int TimeUntilNextTurnMilliseconds { get; set; }
        public Size GameBoardSize { get; set; }
        public int MaxFood { get; set; }
        public List<PlayerState> Players { get; set; }
        public List<Point> Food { get; set; }
        public List<Rectangle> Walls { get; set; }
    }
}
