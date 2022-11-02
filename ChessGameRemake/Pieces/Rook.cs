using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ChessGameRemake.Pieces
{
    class Rook : ChessPiece
    {
        public Rook(ChessBoard parentBoard, PieceColor color) : base(parentBoard, color)
        {
            if (color == PieceColor.White)
                ImageLink = ChessPieceResources.IMAGE_ROOK_WHITE;
            else
                ImageLink = ChessPieceResources.IMAGE_ROOK_BLACK;

            type = PieceType.Rook;
        }

        public override void CalculateMove()
        {
            base.CalculateMove();

            CalculateStraightMove(MAX_DISTANCE, Direction.Up);
            CalculateStraightMove(MAX_DISTANCE, Direction.Down);
            CalculateStraightMove(MAX_DISTANCE, Direction.Left);
            CalculateStraightMove(MAX_DISTANCE, Direction.Right);
        }
    }
}
