using System;
using System.Drawing;

namespace ChessGameRemake
{
    class ChessPieceResources
    {
        public static string IMAGE_KING_WHITE = @"images/king_white.png";
        public static string IMAGE_KING_BLACK = @"images/king_black.png";
        public static string IMAGE_QUEEN_WHITE = @"images/queen_white.png";
        public static string IMAGE_QUEEN_BLACK = @"images/queen_black.png";
        public static string IMAGE_KNIGHT_WHITE = @"images/knight_white.png";
        public static string IMAGE_KNIGHT_BLACK = @"images/knight_black.png";
        public static string IMAGE_ROOK_WHITE = @"images/rook_white.png";
        public static string IMAGE_ROOK_BLACK = @"images/rook_black.png";
        public static string IMAGE_PAWN_WHITE = @"images/pawn_white.png";
        public static string IMAGE_PAWN_BLACK = @"images/pawn_black.png";
        public static string IMAGE_BISHOP_WHITE = @"images/bishop_white.png";
        public static string IMAGE_BISHOP_BLACK = @"images/bishop_black.png";
    }

    abstract class ChessPiece
    {
        protected int MAX_DISTANCE = 7;

        #region field

        // for all other
        private string imageLink;
        protected PieceColor color;
        protected PieceType type;
        protected Point position;
        protected ChessBoard parentBoard;

        private bool[,] canMoves = new bool[8, 8];
        private bool[,] canAttacks = new bool[8, 8];
        #endregion


        #region properties
        // for pawn
        protected bool CanDoubleJump { get => (color == PieceColor.White && position.X == 1) || (color == PieceColor.Black && position.X == 6); }
        
        // genearl
        public PieceColor Color { get => color; set => color = value; }
        public Point Position 
        { 
            get => position;
            set
            {
                position = value;
                parentBoard.Board[value.X, value.Y].Image = Image.FromFile(this.imageLink);
            }
        }

        public string ImageLink { get => imageLink; set => imageLink = value; }
        public bool[,] CanMoves { get => canMoves; set => canMoves = value; }
        public bool[,] CanAttacks { get => canAttacks; set => canAttacks = value; }
        #endregion


        protected ChessPiece(ChessBoard parentBoard, PieceColor color)
        {
            this.parentBoard = parentBoard;
            this.color = color;
        }


        public virtual void CalculateMove()
        {
            // 64 = maxBoardRow * maxBoardCols = 8 * 8
            Array.Clear(CanAttacks, 0, 64);
            Array.Clear(CanMoves, 0, 64);
        }

        protected void CalculateStraightMove(int maxMoves, Direction direction)
        {
            int i = 0,
                currX = position.X,
                currY = position.Y;

            while (i < maxMoves)
            {
                switch (direction)
                {
                    case Direction.Up:
                        currX--;
                        break;
                    case Direction.Down:
                        currX++;
                        break;
                    case Direction.Left:
                        currY--;
                        break;
                    case Direction.Right:
                        currY++;
                        break;
                }

                if (IsValidPosition(new Point(currX, currY)))
                {
                    ChessSquare currSquare = parentBoard.Board[currX, currY];

                    CheckIfCanMoveOrAttackAtPosition(currX, currY);

                    if (currSquare.HasPiece)
                        break;
                }
                else
                    break;

                i++;
            }
        }

        protected void CalculateDiagonalMove(int maxMoves, Direction direction)
        {
            int i = 0,
                currX = position.X,
                currY = position.Y;

            while (i < maxMoves)
            {
                switch (direction)
                {
                    case Direction.UpLeft:
                        currX--;
                        currY--;
                        break;
                    case Direction.UpRight:
                        currX--;
                        currY++;
                        break;
                    case Direction.DownLeft:
                        currX++;
                        currY--;
                        break;
                    case Direction.DownRight:
                        currX++;
                        currY++;
                        break;
                }

                if (IsValidPosition(new Point(currX, currY)))
                {
                    ChessSquare currSquare = parentBoard.Board[currX, currY];

                    CheckIfCanMoveOrAttackAtPosition(currX, currY);

                    if (currSquare.HasPiece)
                        break;
                }
                else
                    break;
                
                i++;
            }
        }

        protected void CheckIfCanMoveOrAttackAtPosition(int x, int y)
        {
            ChessSquare currSquare = parentBoard.Board[x, y];

            if (!currSquare.HasPiece)
            {
                CanMoves[x, y] = true;
            }
            else
            {
                if (IsEnemy(currSquare.Piece))
                {
                    CanAttacks[x, y] = true;
                }
            }
        }

        public bool IsEnemy(ChessPiece piece)
        {
            return this.color != piece.Color;
        }

        protected bool IsValidPosition(Point position)
        {
            return IsValidCoord(position.X) && IsValidCoord(position.Y);
        }

        protected bool IsValidCoord(int coord)
        {
            return coord >= 0 && coord <= 7;
        }
    }

}