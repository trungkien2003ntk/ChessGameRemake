using System;
using System.Windows.Forms;
using System.Drawing;

namespace ChessGameRemake 
{
    class ChessSquareResources
    {
        public static Color SQUARE_COLOR_WHITE = ColorTranslator.FromHtml("#f5e6bf");
        public static Color SQUARE_COLOR_BLACK = ColorTranslator.FromHtml("#664439");
        public static Color CAN_MOVE_COLOR_WHITE = ColorTranslator.FromHtml("#7EE1DC");
        public static Color CAN_MOVE_COLOR_BLACK = ColorTranslator.FromHtml("#369099");
        public static Color CAN_ATTACK_COLOR_WHITE = ColorTranslator.FromHtml("#F89A62");
        public static Color CAN_ATTACK_COLOR_BLACK = ColorTranslator.FromHtml("#B1491F");
    }

    class ChessSquare : PictureBox  
    {
        #region field
        private SquareColor color;
        private Point position;
        private ChessBoard parentBoard;
        private ChessPiece piece;
        #endregion


        #region properties
        public SquareColor Color 
        { 
            get => color;
            set
            {
                color = value;
                if (color == SquareColor.White)
                    this.BackColor = ChessSquareResources.SQUARE_COLOR_WHITE;
                else
                    this.BackColor = ChessSquareResources.SQUARE_COLOR_BLACK;
            }
        }
        public Point Position { get => position; }
        public ChessPiece Piece 
        { 
            get => piece; 
            set
            {
                piece = value;
                if (value != null)
                {
                    value.Position = this.Position;
                }
                else
                {
                    this.Image = null;
                }
            }
        }
        public bool HasPiece { get => this.Piece != null; }
        #endregion


        #region constructor
        public ChessSquare(ChessBoard parentBoard, Point position, int squareEdge)
        {
            this.parentBoard = parentBoard;
            this.position = position;
            this.Width = squareEdge;
            this.Height = squareEdge;

            this.SizeMode = PictureBoxSizeMode.StretchImage;

            this.MouseClick += new MouseEventHandler(Square_MouseClick);
        }
        #endregion

        private void Square_MouseClick(object sender, MouseEventArgs e)
        {
            if (e.Button == MouseButtons.Left)
            {
                if (!parentBoard.HasPieceSelecting)
                {
                    if (this.HasPiece)
                    {
                        parentBoard.SelectingPiece = this.piece;
                        parentBoard.ShowAllPossibleMovesAndAttacks(parentBoard.SelectingPiece);
                    }
                }
                else
                {
                    if (parentBoard.SelectingPiece.CanMoves[position.X, position.Y] ||
                        parentBoard.SelectingPiece.CanAttacks[position.X, position.Y])
                    {
                        parentBoard.UnShowAllPossibleMovesAndAttacks(parentBoard.SelectingPiece);

                        int selectingX = parentBoard.SelectingPiece.Position.X,
                            selectingY = parentBoard.SelectingPiece.Position.Y;

                        parentBoard.Board[selectingX, selectingY].Piece.Position = this.position;
                        this.piece = parentBoard.Board[selectingX, selectingY].Piece;
                        parentBoard.Board[selectingX, selectingY].Piece = null;

                        parentBoard.UpdateMoves();
                        parentBoard.SelectingPiece = null;
                    }
                    else
                    {
                        if (this.HasPiece)
                        {
                            if (this.Position != parentBoard.SelectingPiece.Position)
                            {
                                parentBoard.UnShowAllPossibleMovesAndAttacks(parentBoard.SelectingPiece);
                                parentBoard.SelectingPiece = this.piece;
                                parentBoard.ShowAllPossibleMovesAndAttacks(parentBoard.SelectingPiece);
                            }
                            else
                            {
                                parentBoard.UnShowAllPossibleMovesAndAttacks(parentBoard.SelectingPiece);
                                parentBoard.SelectingPiece = null;
                            }
                        }
                    }
                }
            }
        }

    }
}
