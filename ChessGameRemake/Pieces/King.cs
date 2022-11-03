using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ChessGameRemake.Pieces
{
    class King : ChessPiece
    {
        public King(ChessBoard parentBoard, PieceColor color) : base(parentBoard, color)
        {
            if (color == PieceColor.White)
                imageLink = ChessPieceResources.IMAGE_KING_WHITE;
            else
                imageLink = ChessPieceResources.IMAGE_KING_BLACK;

            type = PieceType.King;
        }

        public override void CalculateMove()
        {
            base.CalculateMove();

            CalculateStraightMove(1, Direction.Up);
            CalculateStraightMove(1, Direction.Down);
            CalculateStraightMove(1, Direction.Left);
            CalculateStraightMove(1, Direction.Right);

            CalculateDiagonalMove(1, Direction.UpRight);
            CalculateDiagonalMove(1, Direction.UpLeft);
            CalculateDiagonalMove(1, Direction.DownRight);
            CalculateDiagonalMove(1, Direction.DownLeft);
        }
    }
}
