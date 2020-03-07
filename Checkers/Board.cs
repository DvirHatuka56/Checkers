using System;

namespace Checkers
{
    public delegate void Winning(Win winner);
    
    internal struct SourceData
    {
        public State State { get; set; }
        public int Row { get; set; }
        public int Column { get; set; }
    }
    
    public class Board
    {
        private State[,] States { get; }
        private bool sourceMarked;
        private SourceData sourceData;
        public static event Winning OnWin;
        public bool BlackTurn { get; private set; }

        public Board()
        {
            sourceMarked = false;
            sourceData = new SourceData();
            BlackTurn = true;
            States = new State[8,8];

            for (int i = 0; i < States.GetLength(0); i++)
            {
                State state;
                if (i >= 0 && i <= 2)
                {
                    state = State.BlackPlayer;
                }
                else if (i >= 5 && i <= 7)
                {
                    state = State.WhitePlayer;
                }
                else
                {
                    state = State.Black;
                }
                
                for (int j = 0; j < States.GetLength(1); j++)
                {
                    States[i, j] = (i + j) % 2 != 0 ? State.White : state;
                }
            }

            MainWindow.SquareClickedEvent += OnSquareClicked;
        }

        public State this[int row, int column] => States[row, column];

        private void OnSquareClicked(int row, int column)
        {
            if (CheckValidSource(row, column))
            {
                if (sourceMarked)
                {
                    States[sourceData.Row, sourceData.Column] = sourceData.State;
                }
                sourceData = new SourceData
                {
                    State = States[row, column],
                    Row = row,
                    Column = column
                };
                States[row, column] = State.Source;
                sourceMarked = true;
                return;
            }

            Move move = CheckValidDestination(row, column);
            if (!sourceMarked || move == Move.Invalid) return;
            if (move == Move.Consume)
            {
                int middleRow = (sourceData.Row + row) / 2, middleColumn = (sourceData.Column + column) / 2;
                States[middleRow, middleColumn] = State.Black;
            }
            States[sourceData.Row, sourceData.Column] = State.Black;
            States[row, column] = sourceData.State;
            sourceMarked = false;
            BlackTurn = !BlackTurn;
            Win win = CheckWin();
            if (win != Win.None)
            {
                OnWin?.Invoke(win);
            }
        }

        private bool CheckValidSource(int row, int column)
        {
            return IsBlack(row, column) || IsWhite(row, column);
        }

        private Move CheckValidDestination(int row, int column)
        {
            bool simpleMove = States[row, column] == State.Black &&
                              CheckSimpleMove(row, column);
            bool consume = States[row, column] == State.Black &&
                           CheckConsume(row, column);
            
            if (consume)
            {
                return Move.Consume;
            }
            
            return simpleMove ? Move.Simple : Move.Invalid;
        }

        public bool IsWhite(int row, int column)
        {
            return States[row, column] == State.WhitePlayer || States[row, column] == State.WhiteQueen;
        }
        
        public bool IsBlack(int row, int column)
        {
            return States[row, column] == State.BlackPlayer || States[row, column] == State.BlackQueen;
        }

        private bool CheckConsume(int row, int column)
        {
            bool black = sourceData.State == State.BlackPlayer || sourceData.State == State.BlackQueen;
            int direction = black ? 1 : -1;
            int middleRow = (sourceData.Row + row) / 2, middleColumn = (sourceData.Column + column) / 2;
            return (row - sourceData.Row) * direction == 2 && Math.Abs(column - sourceData.Column) == 2 &&
                (black && IsWhite(middleRow, middleColumn) ||
                 !black && IsBlack(middleRow, middleColumn));
        }

        private bool CheckSimpleMove(int row, int column)
        {
            bool black = sourceData.State == State.BlackPlayer || sourceData.State == State.BlackQueen;
            int direction = black ? 1 : -1;
            return (row - sourceData.Row) * direction == 1 && Math.Abs(column - sourceData.Column) == 1;
        }

        private Win CheckWin()
        {
            int black = 0, white = 0;
            foreach (State state in States)
            {
                black = state == State.BlackPlayer || state == State.BlackQueen ? black + 1 : black;
                white = state == State.WhitePlayer || state == State.WhiteQueen ? white + 1 : white;
            }

            if (black == 0)
            {
                return Win.White;
            }

            if (white == 0)
            {
                return Win.Black;
            }

            return Win.None;
        }
    }
}