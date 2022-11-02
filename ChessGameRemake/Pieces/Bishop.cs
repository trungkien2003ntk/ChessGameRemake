using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ChessGameRemake.Pieces
{
    class Bishop : ChessPiece
    {
        public Bishop(ChessBoard parentBoard, PieceColor color) : base(parentBoard, color)
        {
            if (color == PieceColor.White)
                ImageLink = ChessPieceResources.IMAGE_BISHOP_WHITE;
            else
                ImageLink = ChessPieceResources.IMAGE_BISHOP_BLACK;

            type = PieceType.Bishop;
        }

        public override void CalculateMove()
        {
            base.CalculateMove();

            CalculateDiagonalMove(MAX_DISTANCE, Direction.UpRight);
            CalculateDiagonalMove(MAX_DISTANCE, Direction.UpLeft);
            CalculateDiagonalMove(MAX_DISTANCE, Direction.DownRight);
            CalculateDiagonalMove(MAX_DISTANCE, Direction.DownLeft);
        }
    }
}
