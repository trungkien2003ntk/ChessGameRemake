using System;

namespace ChessGameRemake.Pieces
{
    class Knight : ChessPiece
    {
        public Knight(ChessBoard parentBoard, PieceColor color) : base(parentBoard, color)
        {
            if (color == PieceColor.White)
                ImageLink = ChessPieceResources.IMAGE_KNIGHT_WHITE;
            else
                ImageLink = ChessPieceResources.IMAGE_KNIGHT_BLACK;

            type = PieceType.Knight;
        }

        public override void CalculateMove()
        {
            base.CalculateMove();

            CalculateKnightMove(Direction.UpLeft);
            CalculateKnightMove(Direction.UpRight);
            CalculateKnightMove(Direction.DownLeft);
            CalculateKnightMove(Direction.DownRight);
        }

        private void CalculateKnightMove(Direction direction)
        {
            int currX1 = position.X,
                currY1 = position.Y,
                currX2 = position.X,
                currY2 = position.Y;

            switch (direction)
            {
                case Direction.UpLeft:
                    currX1 -= 1;
                    currY1 -= 2;
                    currX2 -= 2;
                    currY2 -= 1;
                    break;
                case Direction.UpRight:
                    currX1 -= 1;
                    currY1 += 2;
                    currX2 -= 2;
                    currY2 += 1;
                    break;
                case Direction.DownLeft:
                    currX1 += 1;
                    currY1 -= 2;
                    currX2 += 2;
                    currY2 -= 1;
                    break;
                case Direction.DownRight:
                    currX1 += 1;
                    currY1 += 2;
                    currX2 += 2;
                    currY2 += 1;
                    break;
            }

            if (IsValidPosition(new Point(currX1, currY1)))
                CheckIfCanMoveOrAttackAtPosition(currX1, currY1);

            if (IsValidPosition(new Point(currX2, currY2)))
                CheckIfCanMoveOrAttackAtPosition(currX2, currY2);
        }
    }
}
