using System;
using System.Windows.Forms;
using ChessGameRemake.Pieces;

namespace ChessGameRemake
{
    class ChessBoard
    {
        private const int SQUARE_EDGE = 60;
        private const int INIT_BOARD_LEFT = 60;
        public const int MAXIMUM_N_BOARD_ROWS = 8;
        public const int MAXIMUM_N_BOARD_COLUMNS = 8;

        private ChessSquare[,] board = new ChessSquare[MAXIMUM_N_BOARD_ROWS, MAXIMUM_N_BOARD_COLUMNS];
        private SquareColor currColor = SquareColor.Black;
        private ChessPiece selectingPiece;
        
        public ChessSquare[,] Board { get => board;}
        public ChessPiece SelectingPiece 
        { 
            get => selectingPiece;
            set
            {
                selectingPiece = value;
            }
        }
        public bool HasPieceSelecting { get => selectingPiece != null; }

        public ChessBoard(
            Form f,
            int squareEdge = SQUARE_EDGE,
            int maxNBoardRows = MAXIMUM_N_BOARD_ROWS,
            int maxNBoardCols = MAXIMUM_N_BOARD_COLUMNS
            )
        {
            int left, 
                top = (f.ClientRectangle.Height - squareEdge * maxNBoardRows) / 2 ;

            for (int i = 0; i < maxNBoardRows; i++)
            {
                left = INIT_BOARD_LEFT;
                ChangeCurrentSquareColor();

                for (int j = 0; j < maxNBoardCols; j++)
                {
                    Board[i, j] = new ChessSquare(this, new Point(i, j), squareEdge)
                    {
                        Top = top,
                        Left = left,
                        Color = currColor,
                    };

                    ChangeCurrentSquareColor();

                    f.Controls.Add(Board[i, j]);
                    left += squareEdge;
                }

                top += squareEdge;
            }

            ShowPieces();
            UpdateMoves();
        }

        private void ChangeCurrentSquareColor()
        {
            if (currColor == SquareColor.White)
                currColor = SquareColor.Black;
            else
                currColor = SquareColor.White;
        }
        
        private void ShowPieces()
        {
            ShowKing();
            ShowQueen();
            ShowBishop();
            ShowKnight();
            ShowRook();
            ShowPawn();
        }

        public void ShowAllPossibleMovesAndAttacks(ChessPiece piece)
        {
            for (int i = 0; i < MAXIMUM_N_BOARD_ROWS; i++)
            {
                for (int j = 0; j < MAXIMUM_N_BOARD_COLUMNS; j++)
                {
                    if (piece.CanMoves[i, j])
                    {
                        SetCanMoveColorAtPosition(i, j);
                    }

                    if (piece.CanAttacks[i, j])
                    {
                        SetCanAttackColorAtPosition(i, j);
                    }
                }
            }
        }

        public void UnShowAllPossibleMovesAndAttacks(ChessPiece piece)
        {
            for (int i = 0; i < MAXIMUM_N_BOARD_ROWS; i++)
            {
                for (int j = 0; j < MAXIMUM_N_BOARD_COLUMNS; j++)
                {
                    if (piece.CanMoves[i, j] || piece.CanAttacks[i, j])
                    {
                        SetDefaultBackColorAtPosition(i, j);
                    }
                }
            }
        }

        

        public void UpdateMoves()
        {
            for (int i = 0; i < MAXIMUM_N_BOARD_ROWS; i++)
            {
                for (int j = 0; j < MAXIMUM_N_BOARD_COLUMNS; j++)
                {
                    if (board[i,j].HasPiece)
                        board[i, j].Piece.CalculateMove();
                }
            }
        }

        private void ShowKing()
        {
            Board[0, 3].Piece = new King(this, PieceColor.White);
            Board[7, 3].Piece = new King(this, PieceColor.Black);
        }

        private void ShowQueen()
        {
            Board[0, 4].Piece = new Queen(this, PieceColor.White);
            Board[7, 4].Piece = new Queen(this, PieceColor.Black);
        }

        private void ShowBishop()
        {
            Board[0, 2].Piece = new Bishop(this, PieceColor.White);
            Board[7, 2].Piece = new Bishop(this, PieceColor.Black);
            Board[0, 5].Piece = new Bishop(this, PieceColor.White);
            Board[7, 5].Piece = new Bishop(this, PieceColor.Black);
        }

        private void ShowKnight()
        {
            Board[0, 1].Piece = new Knight(this, PieceColor.White);
            Board[7, 1].Piece = new Knight(this, PieceColor.Black);
            Board[0, 6].Piece = new Knight(this, PieceColor.White);
            Board[7, 6].Piece = new Knight(this, PieceColor.Black);
        }

        private void ShowRook()
        {
            Board[0, 0].Piece = new Rook(this, PieceColor.White);
            Board[7, 0].Piece = new Rook(this, PieceColor.Black);
            Board[0, 7].Piece = new Rook(this, PieceColor.White);
            Board[7, 7].Piece = new Rook(this, PieceColor.Black);
        }

        private void ShowPawn()
        {
            for (int i = 0; i < MAXIMUM_N_BOARD_COLUMNS; i++)
            {
                Board[1, i].Piece = new Pawn(this, PieceColor.White);
                Board[6, i].Piece = new Pawn(this, PieceColor.Black);
            }
        }

        private void SetCanAttackColorAtPosition(int i, int j)
        {
            if (board[i, j].Color == SquareColor.White)
                board[i, j].BackColor = ChessSquareResources.CAN_ATTACK_COLOR_WHITE;
            else
                board[i, j].BackColor = ChessSquareResources.CAN_ATTACK_COLOR_BLACK;
        }

        private void SetCanMoveColorAtPosition(int i, int j)
        {
            if (board[i, j].Color == SquareColor.White)
                board[i, j].BackColor = ChessSquareResources.CAN_MOVE_COLOR_WHITE;
            else
                board[i, j].BackColor = ChessSquareResources.CAN_MOVE_COLOR_BLACK;
        }

        private void SetDefaultBackColorAtPosition(int i, int j)
        {
            if (board[i, j].Color == SquareColor.White)
                board[i, j].BackColor = ChessSquareResources.SQUARE_COLOR_WHITE;
            else
                board[i, j].BackColor = ChessSquareResources.SQUARE_COLOR_BLACK;
        }
    }
}