using System;
using System.Collections.Generic;

namespace AIGame
{
    public class GameManager
    {
        GameBoard gameBoard;
        Player player;
        AIPlayer aiPlayer;

        public void StartGame(int gameBoardSize)
        {
            gameBoard = new GameBoard(gameBoardSize);
            player = new Player(gameBoard);
            aiPlayer = new AIPlayer(gameBoard);

            do
            {
                PlayerMove();
                Console.WriteLine(gameBoard);
                if (gameBoard.CheckWinConditions() == GameBoard.WinCondition.PlayerVictory)
                {
                    Console.WriteLine("Player won the game");
                }
                AiMove();
                Console.WriteLine(gameBoard);
                if (gameBoard.CheckWinConditions() == GameBoard.WinCondition.AIVictory)
                {
                    Console.WriteLine("AI won the game");
                }
            } while (gameBoard.CheckWinConditions() == GameBoard.WinCondition.NoWinnerYet);

            if (gameBoard.CheckWinConditions() == GameBoard.WinCondition.Draw)
            {
                Console.WriteLine("Draw");
            }
        }

        public void PlayerMove()
        {
            Console.WriteLine("Where do you want to move your chess?");
            int rowNum;
            int colNum;
            do
            {
                Console.WriteLine("Enter the row number: ");
                rowNum = Convert.ToInt32(Console.ReadLine());
                Console.WriteLine("Enter the column number: ");
                colNum = Convert.ToInt32(Console.ReadLine());
            } while (!gameBoard.SetChess(rowNum, colNum, player.chess));
        }

        public void AiMove()
        {
            aiPlayer.FindBestMove();
            // List<Tuple<int, int>> emptySpaces = new List<Tuple<int, int>>();
            // for (int i = 0; i < gameBoard.board.GetLength(0); i++)
            // {
                // for (int j = 0; j < gameBoard.board.GetLength(1); j++)
                // {
                    // if (gameBoard.board[i, j].owner == Chess.OwnerType.NoBody)
                    // {
                        // emptySpaces.Add(new Tuple<int, int>(i,j));
                    // }
                // }
            // }

            // Random random = new Random();
            // var targetPosition = emptySpaces[random.Next(0, emptySpaces.Count)];
            // gameBoard.SetChess(targetPosition.Item1, targetPosition.Item2, new Chess(Chess.OwnerType.AI));
        }
    }
}