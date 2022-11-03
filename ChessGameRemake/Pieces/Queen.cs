using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ChessGameRemake.Pieces
{
    class Queen : ChessPiece
    {
        public Queen(ChessBoard parentBoard, PieceColor color) : base(parentBoard, color)
        {
            if (color == PieceColor.White)
                imageLink = ChessPieceResources.IMAGE_QUEEN_WHITE;
            else
                imageLink = ChessPieceResources.IMAGE_QUEEN_BLACK;

            type = PieceType.Queen;
        }

        public override void CalculateMove()
        {
            base.CalculateMove();

            CalculateStraightMove(MAX_DISTANCE, Direction.Up);
            CalculateStraightMove(MAX_DISTANCE, Direction.Down);
            CalculateStraightMove(MAX_DISTANCE, Direction.Left);
            CalculateStraightMove(MAX_DISTANCE, Direction.Right);

            CalculateDiagonalMove(MAX_DISTANCE, Direction.UpRight);
            CalculateDiagonalMove(MAX_DISTANCE, Direction.UpLeft);
            CalculateDiagonalMove(MAX_DISTANCE, Direction.DownRight);
            CalculateDiagonalMove(MAX_DISTANCE, Direction.DownLeft);
        }
    }
}
