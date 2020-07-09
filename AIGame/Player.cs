namespace AIGame
{
    public class Player: ChessPlayer
    {

    
    
        public override void PlaceChess(int row, int col)
        {
            if (gameBoard != null) gameBoard.board[row, col] = new Chess(Chess.OwnerType.Player);
        }

        public Player(GameBoard gameBoard) : base(gameBoard)
        {
            chess = new Chess(Chess.OwnerType.Player);
        }
    }
}