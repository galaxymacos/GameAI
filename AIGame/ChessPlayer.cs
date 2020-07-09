namespace AIGame
{
    public abstract class ChessPlayer
    {
        public Chess chess;
        public GameBoard gameBoard;

        public ChessPlayer(GameBoard gameBoard)
        {
            this.gameBoard = gameBoard;
        }

        public abstract void PlaceChess(int row, int col);
    }
}