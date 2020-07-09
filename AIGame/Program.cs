using System;

namespace AIGame
{
  internal class Program
  {
    public static void Main(string[] args)
    {
      // Chess PC = new Chess(Chess.OwnerType.Player);
      // Chess AC = new Chess(Chess.OwnerType.AI);
      // Chess NB = new Chess(Chess.OwnerType.NoBody);
      

      // Chess[,] chessBoard =new Chess[3,3]
      // {
        // {NB, NB, AC},
        // {NB, NB, AC},
        // {NB, NB, AC}
      // };
      // GameBoard gameBoard = new GameBoard(chessBoard);
      // Console.WriteLine(gameBoard.Evaluate());

      GameManager gameManager = new GameManager();
      gameManager.StartGame(3);
    }
  }
}