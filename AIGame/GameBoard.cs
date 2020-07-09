using System;
using System.Text;

namespace AIGame
{
    public class GameBoard
    {
        public enum WinCondition
        {
            AIVictory, PlayerVictory, Draw, NoWinnerYet
        }
    
        public Chess[,] board;

        public GameBoard(Chess[,] testBoard)
        {
            board = testBoard;
        }

        public GameBoard(int size)
        {
            board = new Chess[size, size];
            for (int i = 0; i < board.GetLength(0); i++)
            {
                for (int j = 0; j < board.GetLength(1); j++)
                {
                    board[i,j] = new Chess(Chess.OwnerType.NoBody);
                }
            }
        }

        public bool SetChess(int row, int col, Chess chess)
        {
            if (row >= board.GetLength(0) || col >= board.GetLength(1))
            {
                return false;
            }
            if (board[row, col].owner == Chess.OwnerType.NoBody)
            {
                board[row, col] = chess;
                return true;
            }

            return false;
        }
        public WinCondition CheckWinConditions()
        {
            Chess firstElement = null;
            // Check row
            for (int i = 0; i < board.GetLength(0); i++)
            {
                firstElement = board[i,0];
                for (int j = 0; j < board.GetLength(1); j++)
                {
                    if (board[i, j] != firstElement)
                    {
                        break;
                    }

                    if (j == board.GetLength(1)-1)
                    {
                        if (firstElement.owner == Chess.OwnerType.Player)
                        {
                            return WinCondition.PlayerVictory;
                        }

                        if (firstElement.owner == Chess.OwnerType.AI)
                        {
                            return WinCondition.AIVictory;
                        }
                    }
                }
            }
            // Check Col
            for (int i = 0; i < board.GetLength(0); i++)
            {
                firstElement = board[0, i];
                for (int j = 0; j < board.GetLength(1); j++)
                {
                    if (board[j, i] != firstElement)
                    {
                        break;
                    }

                    if (j == board.GetLength(1) - 1)
                    {
                        if (firstElement.owner == Chess.OwnerType.Player)
                        {
                            return WinCondition.PlayerVictory;
                        }

                        if (firstElement.owner == Chess.OwnerType.AI)
                        {
                            return WinCondition.AIVictory;
                        }
            
            
                    }
                }
            }

            firstElement = board[0, 0];
            // Check diagonal
            for (int i = 0; i < board.GetLength(0); i++)
            {
                if (board[i, i] != firstElement)
                {
                    break;
                }

                if (i == board.GetLength(0)-1)
                {
                    if (firstElement.owner == Chess.OwnerType.Player)
                    {
                        Console.WriteLine("Player wins");
                        return WinCondition.PlayerVictory;
                    }

                    if (firstElement.owner == Chess.OwnerType.AI)
                    {
                        return WinCondition.AIVictory;
                    }
                }
            }
      
            firstElement = board[board.GetLength(0)-1, 0];

            for (int i = 0; i < board.GetLength(0); i++)
            {
                if (board[board.GetLength(0)-1-i, i] != firstElement)
                {
                    break;
                }

                if (i == board.GetLength(0)-1)
                {
                    if (firstElement.owner == Chess.OwnerType.Player)
                    {
                        Console.WriteLine("Player wins 2");
                        return WinCondition.PlayerVictory;
                    }

                    if (firstElement.owner == Chess.OwnerType.AI)
                    {
                        Console.WriteLine("ai wins 2");
                        return WinCondition.AIVictory;
                    }
                }
            }

            for (int i = 0; i < board.GetLength(0); i++)
            {
                for (int j = 0; j < board.GetLength(1); j++)
                {
                    if (board[i, j].owner == Chess.OwnerType.NoBody)
                    {
                        return WinCondition.NoWinnerYet;
                    }
                }
            }

            return WinCondition.Draw;


        }

        public int Evaluate()
        {
            WinCondition condition = CheckWinConditions();
            switch (condition)
            {
                case WinCondition.AIVictory:
                    return -10;
                    break;
                case WinCondition.PlayerVictory:
                    return 10;
                    break;
                case WinCondition.Draw:
                    return 0;
                    break;
                case WinCondition.NoWinnerYet:
                    return 0;
                    break;
                default:
                    return 0;
            }
        }

        public bool IsMoveLeft()
        {
            foreach (Chess chess in board)
            {
                if (chess.owner == Chess.OwnerType.NoBody)
                    return true;
            }

            return false;

        }

        public override string ToString()
        {
            StringBuilder sb = new StringBuilder();
            for (int i = 0; i < board.GetLength(0); i++)
            {
                for (int j = 0; j < board.GetLength(1); j++)
                {
                    sb.Append(board[i,j]+" ");
                }

                sb.Append("\n");
            }

            return sb.ToString();
        }

        
    }

    
}