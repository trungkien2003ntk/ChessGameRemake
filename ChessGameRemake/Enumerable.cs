using System;

namespace ChessGameRemake
{
    public struct Point
    {
        int x;
        int y;

        
        public int X { get => x; set => x = value; }
        public int Y { get => y; set => y = value; }


        public Point(int x, int y)
        {
            this.x = x;
            this.y = y;
        }

        public static bool operator ==(Point a, Point b)
        {
            return a.x == b.x && a.y == b.y;
        }

        public static bool operator !=(Point a, Point b)
        {
            return !(a == b);
        }
    }

    

    public enum PieceType
    {
        King,
        Queen,
        Pawn,
        Knight,
        Bishop,
        Rook
    }

    public enum PieceColor
    {
        White,
        Black
    }
    public enum SquareColor
    {
        White,
        Black
    }

    public enum Direction
    {
        Up,
        Down,
        Left,
        Right,
        UpRight,
        UpLeft,
        DownRight,
        DownLeft
    }


}
