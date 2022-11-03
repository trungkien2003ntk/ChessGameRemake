using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ChessGameRemake.Pieces
{
    class Pawn : ChessPiece
    {
        public Pawn(ChessBoard parentBoard, PieceColor color) : base(parentBoard, color)
        {
            if (color == PieceColor.White)
                imageLink = ChessPieceResources.IMAGE_PAWN_WHITE;
            else
                imageLink = ChessPieceResources.IMAGE_PAWN_BLACK;

            type = PieceType.Pawn;
        }

        public override void CalculateMove()
        {
            base.CalculateMove();

            if (this.color == PieceColor.White)
            {
                CalculatePawnStraightMove(Direction.Down);
                CalculatePawnDiagonalMove(Direction.DownRight);
                CalculatePawnDiagonalMove(Direction.DownLeft);
            }
            else
            {
                CalculatePawnStraightMove(Direction.Up);
                CalculatePawnDiagonalMove(Direction.UpRight);
                CalculatePawnDiagonalMove(Direction.UpLeft);
            }
        }

        private void CalculatePawnStraightMove(Direction direction)
        {
            int currX = position.X,
                currY = position.Y;

            switch (direction)
            {
                case Direction.Up:
                    currX--;
                    break;
                case Direction.Down:
                    currX++;
                    break;
            }

            if (IsValidPosition(new Point(currX, currY)))
            {
                CheckIfCanMoveAtPosition(currX, currY);
            }

            if (CanDoubleJump)
            {
                switch (direction)
                {
                    case Direction.Up:
                        currX--;
                        break;
                    case Direction.Down:
                        currX++;
                        break;
                }
                CheckIfCanMoveAtPosition(currX, currY);
            }
        }

        

        private void CalculatePawnDiagonalMove(Direction direction)
        {
            int currX = position.X,
                currY = position.Y;

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
                CheckIfCanAttackAtPosition(currX, currY);
            }
        }
        private void CheckIfCanMoveAtPosition(int x, int y)
        {
            ChessSquare currSquare = parentBoard.Board[x, y];

            if (!currSquare.HasPiece)
            {
                CanMoves[x, y] = true;
            }
        }

        private void CheckIfCanAttackAtPosition(int x, int y)
        {
            ChessSquare currSquare = parentBoard.Board[x, y];

            if(currSquare.HasPiece)
            {
                if (IsEnemy(currSquare.Piece))
                {
                    CanAttacks[x, y] = true;
                }
            }
        }
    }
}
