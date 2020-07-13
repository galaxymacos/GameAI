// C# program to find the  
// next optimal move for a player 

using System;

class GFG
{
    class Move
    {
        public int row, col;

        public Move(int row, int col)
        {
            this.row = row;
            this.col = col;
        }
    };

    static char player = 'x', opponent = 'o';

// This function returns true if there are moves 
// remaining on the board. It returns false if 
// there are no moves left to play. 
    static Boolean isMovesLeft(char[,] board)
    {
        for (int i = 0; i < board.GetLength(0); i++)
        for (int j = 0; j < board.GetLength(1); j++)
            if (board[i, j] == '_')
                return true;
        return false;
    }

    public enum GameState
    {
        PlayerWin,
        AIWin,
        InProgress,
        Draw
    }

    static GameState evaluate(char[,] b)
    {
        for (int i = 0; i < b.GetLength(0); i++)
        {
            char firstChess = b[i, 0];
            for (int j = 0; j < b.GetLength(1); j++)
            {
                if (b[i, j] != firstChess)
                {
                    break;
                }

                if (j == b.GetLength(1) - 1)
                {
                    if (firstChess == player)
                    {
                        return GameState.PlayerWin;
                    }

                    if (firstChess == opponent)
                    {
                        return GameState.AIWin;
                    }
                }
            }
        }

        for (int i = 0; i < b.GetLength(0); i++)
        {
            char firstChess = b[0, i];
            for (int j = 0; j < b.GetLength(1); j++)
            {
                if (b[j, i] != firstChess)
                {
                    break;
                }

                if (j == b.GetLength(1) - 1)
                {
                    if (firstChess == player)
                    {
                        return GameState.PlayerWin;
                    }

                    if (firstChess == opponent)
                    {
                        return GameState.AIWin;
                    }
                }
            }
        }

        char topLeftChess = b[0, 0];
        for (int i = 0; i < b.GetLength(0); i++)
        {
            if (b[i, i] != topLeftChess)
            {
                break;
            }

            if (i == b.GetLength(0) - 1)
            {
                if (topLeftChess == player)
                {
                    return GameState.PlayerWin;
                }

                if (topLeftChess == opponent)
                {
                    return GameState.AIWin;
                }
            }
        }

        char topRightChess = b[b.GetLength(0) - 1, 0];
        for (int i = 0; i < b.GetLength(0); i++)
        {
            if (b[i, b.GetLength(0) - 1 - i] != topRightChess)
            {
                break;
            }

            if (i == b.GetLength(0) - 1)
            {
                if (topRightChess == player)
                {
                    return GameState.PlayerWin;
                }

                if (topRightChess == opponent)
                {
                    return GameState.AIWin;
                }
            }
        }

        for (int i = 0; i < b.GetLength(0); i++)
        {
            for (int j = 0; j < b.GetLength(1); j++)
            {
                if (b[i, j] == '_')
                {
                    return GameState.InProgress;
                }
            }
        }

        return GameState.Draw;


        // Checking for Columns for X or O victory. 
        // for (int col = 0; col < 3; col++) 
        // { 
        //     if (b[0, col] == b[1, col] && 
        //         b[1, col] == b[2, col]) 
        //     { 
        //         if (b[0, col] == player) 
        //             return +10; 
        //
        //         else if (b[0, col] == opponent) 
        //             return -10; 
        //     } 
        // } 

        // // Checking for Diagonals for X or O victory. 
        // if (b[0, 0] == b[1, 1] && b[1, 1] == b[2, 2]) 
        // { 
        //     if (b[0, 0] == player) 
        //         return +10; 
        //     else if (b[0, 0] == opponent) 
        //         return -10; 
        // } 
        //
        // if (b[0, 2] == b[1, 1] && b[1, 1] == b[2, 0]) 
        // { 
        //     if (b[0, 2] == player) 
        //         return +10; 
        //     else if (b[0, 2] == opponent) 
        //         return -10; 
        // } 
        //
        // // Else if none of them have won then return 0 
        // return 0; 
    }

// This is the minimax function. It considers all 
// the possible ways the game can go and returns 
// the value of the board 

    private static int MAX = 1000;
    private static int MIN = -1000;

    static int minimax(char[,] board,
        int depth, Boolean isMax, int maxDepth, int alpha, int beta)
    {
        var gameState = evaluate(board);
        int score = 0;
        switch (gameState)
        {
            case GameState.PlayerWin:
                score = 1000;
                break;
            case GameState.AIWin:
                score = -1000;
                break;
        }


        // If Maximizer has won the game  
        // return his/her evaluated score 
        if (score == 1000)
            return score;

        // If Minimizer has won the game  
        // return his/her evaluated score 
        if (score == -1000)
            return score;


        // If there are no more moves and  
        // no winner then it is a tie 
        if (isMovesLeft(board) == false)
            return 0;


        // Limit the searching to increase performance (can delete later)
        if (depth > maxDepth)
            return 0;

        // If this maximizer's move 
        if (isMax)
        {
            int best = MIN;

            // Traverse all cells 
            for (int i = 0; i < board.GetLength(0); i++)
            {
                if (alpha >= beta)
                {
                    break;
                }

                for (int j = 0; j < board.GetLength(1); j++)
                {
                    // Check if cell is empty 
                    if (board[i, j] == '_')
                    {
                        // Make the move 
                        board[i, j] = player;

                        // Call minimax recursively and choose 
                        // the maximum value 
                        best = Math.Max(best + CalculateSurroundingUnit(board, i, j) * 5, minimax(board,
                            depth + 1, !isMax, maxDepth, alpha, beta));
                        alpha = Math.Max(alpha, best);
                        // Undo the move 
                        board[i, j] = '_';

                        if (alpha >= beta)
                        {
                            break;
                        }
                    }
                }
            }

            return best;
        }

        // If this minimizer's move 
        else
        {
            int best = MAX;

            // Traverse all cells 
            for (int i = 0; i < board.GetLength(0); i++)
            {
                if (alpha >= beta)
                    break;
                for (int j = 0; j < board.GetLength(1); j++)
                {
                    if (!HasSurroundingCell(board, i, j))
                        continue;
                    // Check if cell is empty 
                    if (board[i, j] == '_')
                    {
                        // Make the move 
                        board[i, j] = opponent;

                        // Call minimax recursively and choose 
                        // the minimum value 
                        best = Math.Min(best, minimax(board,
                            depth + 1, !isMax, maxDepth, alpha, beta));
                        beta = Math.Min(beta, best);

                        // Undo the move 
                        board[i, j] = '_';

                        if (alpha >= beta)
                        {
                            break;
                        }
                    }
                }
            }

            return best;
        }
    }

// This will return the best possible 
// move for the player 
    static Move findBestMove(char[,] board)
    {
        int bestVal = 1000;
        var bestMove = StopOpponentImmediately(board);
        if (bestMove != null)
        {
            return bestMove;
        }


        bestMove = new Move(-1, -1);
        

        // Traverse all cells, evaluate minimax function  
        // for all empty cells. And return the cell  
        // with optimal value. 
        for (int i = 0; i < board.GetLength(0); i++)
        {
            for (int j = 0; j < board.GetLength(1); j++)
            {
                if (!HasSurroundingCell(board, i, j))
                {
                    continue;
                }

                // Check if cell is empty 
                if (board[i, j] == '_')
                {
                    // Make the move 
                    board[i, j] = opponent;

                    // compute evaluation function for this 
                    // move. 
                    int moveVal = minimax(board, 0, true, 9, -1000, 1000);
                    // Undo the move 
                    board[i, j] = '_';

                    // If the value of the current move is 
                    // more than the best value, then update 
                    // best/ 
                    if (moveVal < bestVal)
                    {
                        bestMove.row = i;
                        bestMove.col = j;
                        bestVal = moveVal;
                    }
                }
            }
        }

        Console.Write("The value of the best Move " +
                      "is : {0}\n\n", bestVal);

        return bestMove;
    }

    public static void PrintBoard(char[,] b)
    {
        Console.WriteLine("--------------------------");
        for (int i = 0; i < b.GetLength(0); i++)
        {
            for (int j = 0; j < b.GetLength(1); j++)
            {
                Console.Write(b[i, j] + " ");
            }

            Console.WriteLine();
        }
    }

    public static bool IsValidMove(char[,] board, int row, int col)
    {
        if (row >= board.GetLength(0) || col >= board.GetLength(1))
            return false;
        return board[row, col] == '_';
    }

    /// <summary>
    /// How much score should a node gain because it is close to the other nodes
    /// </summary>
    /// <param name="board"></param>
    /// <param name="c"></param>
    /// <returns></returns>
    // public static int DistanceToScore(char[,] board,int row, int col)
    // {
    //     int scoreGain = 0;
    //     for (int i = 0; i < board.GetLength(0); i++)
    //     {
    //         for (int j = 0; j < board.GetLength(1); j++)
    //         {
    //             if (i != row && j != col && board[i,j] != '_')
    //             {
    //                 // Calculate distance
    //                 if (i < row && j < col)
    //                 {
    //                     int diagonalMovePoint = Math.Min(row - i, col - j);
    //                     int alongMovePoint = Math.Abs((row - i)-(col - j));
    //                     scoreGain += diagonalMovePoint;
    //                     scoreGain += alongMovePoint;
    //                 }
    //             }
    //         }
    //     }
    //
    //     return scoreGain;
    // }
    public static bool HasSurroundingCell(char[,] board, int row, int col)
    {
        if (!outOfBounds(board, row - 1, col - 1) && board[row - 1, col - 1] != '_')
        {
            return true;
        }

        if (!outOfBounds(board, row - 1, col + 1) && board[row - 1, col + 1] != '_')
        {
            return true;
        }

        if (!outOfBounds(board, row, col - 1) && board[row, col - 1] != '_')
        {
            return true;
        }

        if (!outOfBounds(board, row, col + 1) && board[row, col + 1] != '_')
        {
            return true;
        }

        if (!outOfBounds(board, row + 1, col - 1) && board[row + 1, col - 1] != '_')
        {
            return true;
        }

        if (!outOfBounds(board, row + 1, col + 1) && board[row + 1, col + 1] != '_')
        {
            return true;
        }

        if (!outOfBounds(board, row + 1, col) && board[row + 1, col] != '_')
        {
            return true;
        }

        if (!outOfBounds(board, row - 1, col) && board[row - 1, col] != '_')
        {
            return true;
        }


        return false;
    }

    public static int CalculateSurroundingUnit(char[,] board, int row, int col)
    {
        int unit = 0;
        if (!outOfBounds(board, row - 1, col - 1) && board[row - 1, col - 1] != '_')
        {
            unit++;
        }

        if (!outOfBounds(board, row - 1, col + 1) && board[row - 1, col + 1] != '_')
        {
            unit++;
        }

        if (!outOfBounds(board, row, col - 1) && board[row, col - 1] != '_')
        {
            unit++;
        }

        if (!outOfBounds(board, row, col + 1) && board[row, col + 1] != '_')
        {
            unit++;
        }

        if (!outOfBounds(board, row + 1, col - 1) && board[row + 1, col - 1] != '_')
        {
            unit++;
        }

        if (!outOfBounds(board, row + 1, col + 1) && board[row + 1, col + 1] != '_')
        {
            unit++;
        }

        if (!outOfBounds(board, row + 1, col) && board[row + 1, col] != '_')
        {
            unit++;
        }

        if (!outOfBounds(board, row - 1, col) && board[row - 1, col] != '_')
        {
            unit++;
        }


        return unit;
    }

    private static Move StopOpponentImmediately(char[,] board)
    {
        int boardLength = board.GetLength(0);
        int mostPlayerChessContinuously = 0;
        Move result = null;
        
        // check row
        for (int i = 0; i < boardLength; i++)
        {
            int count = 0;
            for (int j = 0; j < boardLength; j++)
            {
                if (board[i, j] == 'x')
                {
                    count++;
                }
            }

            if (count == boardLength - 1)
            {
                // dangerous
                for (int c = 0; c < boardLength; c++)
                {
                    if (board[i, c] == '_')
                    {
                        return new Move(i, c);
                    }
                }
            }

            if (count > mostPlayerChessContinuously)
            {
                mostPlayerChessContinuously = count;
                for (int j = 0; j < boardLength; j++)
                {
                    if (board[i, j] == '_')
                    {
                        result = new Move(i, j);
                        break;
                    }
                }
            }
            

            
        }
        

        for (int i = 0; i < boardLength; i++)
        {
            int count = 0;
            for (int j = 0; j < boardLength; j++)
            {
                if (board[j, i] == 'x')
                {
                    count++;
                }
            }

            if (count == boardLength - 1)
            {
                // dangerous
                for (int j = 0; j < boardLength; j++)
                {
                    if (board[j, i] == '_')
                    {
                        return new Move(j, i);
                    }
                }
            }
            else if (count > mostPlayerChessContinuously)
            {
                mostPlayerChessContinuously = count;
                for (int j = 0; j < boardLength; j++)
                {
                    if (board[j, i] == '_')
                    {
                        result = new Move(j, i);
                        break;
                    }
                }
            }
        }

        int countDiagonal = 0;
        for (int i = 0; i < boardLength; i++)
        {
            if (board[i, i] == 'x')
            {
                countDiagonal++;
            }

            
        }
        if (countDiagonal == boardLength - 1)
        {
            // dangerous
            for (int c = 0; c < boardLength; c++)
            {
                if (board[c, c] == '_')
                {
                    return new Move(c, c);
                }
            }
        }
        
        if (countDiagonal > mostPlayerChessContinuously)
        {
            mostPlayerChessContinuously = countDiagonal;
            for (int i = 0; i < boardLength; i++)
            {
                if (board[i, i] == '_')
                {
                    result = new Move(i, i);
                    break;
                }
            }
        }

        countDiagonal = 0;
        for (int i = 0; i < boardLength; i++)
        {
            if (board[i, boardLength-i-1] == 'x')
            {
                countDiagonal++;
            }
        }

        if (countDiagonal == boardLength - 1)
        {
            // dangerous
            for (int c = 0; c < boardLength; c++)
            {
                if (board[c, c] == '_')
                {
                    return new Move(c, c);
                }
            }
        }
        
        if (countDiagonal > mostPlayerChessContinuously)
        {
            mostPlayerChessContinuously = countDiagonal;
            for (int i = 0; i < boardLength; i++)
            {
                if (board[i, boardLength - i - 1] == '_')
                {
                    result = new Move(i, boardLength - i - 1);
                    break;
                }
            }
        }
        
        


        return result;
    }

    private static bool outOfBounds(char[,] board, int row, int col)
    {
        if (row < 0 || row >= board.GetLength(0) || col < 0 || col >= board.GetLength(1))
            return true;
        return false;
    }

// Driver code 
    public static void Main()
    {
        // var testBoard = new char[4, 4]
        // {
        //     {'X', '_', '_', '_'},
        //     {'_', 'X', '_', '_'},
        //     {'_', '_', 'X', '_'},
        //     {'_', '_', '_', '_'}
        // };
        //
        // var move = StopOpponentImmediately(testBoard);
        // if (move != null)
        // {
        //     Console.WriteLine($"{move.row}, {move.col}");
        // }

        RunGame();
    }

    private static void RunGame()
    {
        Console.WriteLine("Welcome to the game");
        Console.Write("Please enter the size of the board: ");
        int size = Convert.ToInt32(Console.ReadLine());
        char[,] board = new char[size, size];
        Console.WriteLine("size: " + size);
        for (int i = 0; i < board.GetLength(0); i++)
        {
            for (int j = 0; j < board.GetLength(1); j++)
            {
                board[i, j] = '_';
            }
        }


        PrintBoard(board);
        while (isMovesLeft(board) && evaluate(board) == GameState.InProgress)
        {
            // player move
            int row = -1;
            int col = -1;
            Console.WriteLine("Where do you want to move? Enter RowNum,ColNum (Separated by a comma)");

            bool hasTried = false;
            do
            {
                if (hasTried)
                {
                    Console.WriteLine("Please input valid row and col number, Ex: 1,2   ");
                }

                hasTried = true;
                string input = Console.ReadLine();
                var inputs = input.Split(',');
                row = Convert.ToInt32(inputs[0]);
                col = Convert.ToInt32(inputs[1]);
            } while (!IsValidMove(board, row, col));

            board[row, col] = 'x';


            PrintBoard(board);

            if (!isMovesLeft(board))
            {
                Console.WriteLine("Draw");
                break;
            }

            if (evaluate(board) == GameState.PlayerWin)
            {
                break;
            }

            // AI move
            Move bestMove = findBestMove(board);
            Console.WriteLine($"Best Move: {bestMove.row}, {bestMove.col}");
            board[bestMove.row, bestMove.col] = 'o';
            PrintBoard(board);
        }

        switch (evaluate(board))
        {
            case GameState.PlayerWin:
                Console.WriteLine("Congratulations, you won");
                break;
            case GameState.AIWin:
                Console.WriteLine("Your opponent won");
                break;
            case GameState.Draw:
                Console.WriteLine("There is no winner");
                break;
            default:
                throw new Exception("Game is still in progress but it shouldn't");
        }
    }
}