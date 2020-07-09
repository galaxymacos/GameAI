using System;
using System.Diagnostics;

namespace AIGame
{
    public class AIPlayer : ChessPlayer
    {
    
        public override void PlaceChess(int row, int col)
        {
            if (gameBoard != null) gameBoard.board[row, col] = new Chess(Chess.OwnerType.AI);
        }

        public AIPlayer(GameBoard gameBoard) : base(gameBoard)
        {
            chess = new Chess(Chess.OwnerType.AI);
        }
        
        public int Minimax(Chess[,] board, int depth, bool isMax)
        {
            int score = gameBoard.Evaluate();
            if (score == 10)
                return score;
            if (score == -10)
            {
                return score;
            }

            if (!gameBoard.IsMoveLeft())
                return 0;

            if (isMax)
            {
                int best = -1000;
                for (int i = 0; i < gameBoard.board.GetLength(0); i++)
                {
                    for (int j = 0; j < gameBoard.board.GetLength(1); j++)
                    {
                        if (gameBoard.board[i, j].owner == Chess.OwnerType.NoBody)
                        {
                            gameBoard.board[i, j].owner = Chess.OwnerType.Player;

                            best = Math.Max(best, Minimax(board, depth + 1, false));
                            
                            gameBoard.board[i, j].owner = Chess.OwnerType.NoBody;
                        }
                    }
                }
                return best;
            }
            else
            {
                int best = 1000;
                for (int i = 0; i < gameBoard.board.GetLength(0); i++)
                {
                    for (int j = 0; j < gameBoard.board.GetLength(1); j++)
                    {
                        if (gameBoard.board[i, j].owner == Chess.OwnerType.NoBody)
                        {
                            gameBoard.board[i, j].owner = Chess.OwnerType.AI;

                            best = Math.Min(best, Minimax(board, depth + 1, true));
                            
                            gameBoard.board[i, j].owner = Chess.OwnerType.NoBody;
                        }

                    }
                }

                return best;
            }
            
        }
        
        public void FindBestMove()
        {
            int bestVal = 1000;
            int bestMoveRow = -1;
            int bestMoveCol = -1;

            for (int i = 0; i < gameBoard.board.GetLength(0); i++)
            {
                for (int j = 0; j < gameBoard.board.GetLength(1); j++)
                {
                    if (gameBoard.board[i, j].owner == Chess.OwnerType.NoBody)
                    {
                        gameBoard.board[i, j].owner = Chess.OwnerType.AI;
                        int moveVal = Minimax(gameBoard.board, 0, true);
                        
                        gameBoard.board[i, j].owner = Chess.OwnerType.NoBody;
                        if (moveVal < bestVal)
                        {
                            bestMoveRow = i;
                            bestMoveCol = j;
                            bestVal = moveVal;
                        }
                    }
                }
            }
            
            PlaceChess(bestMoveRow, bestMoveCol);
        }
    }
    
    
}