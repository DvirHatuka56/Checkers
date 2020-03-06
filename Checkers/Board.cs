namespace Checkers
{
    struct SourceData
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
        private bool destinationMarked;

        public Board()
        {
            sourceMarked = false;
            sourceData = new SourceData();
            destinationMarked = false;
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
            if (!destinationMarked && CheckValidSource(row, column))
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

            if (!CheckValidDestination(row, column)) return;
            States[sourceData.Row, sourceData.Column] = State.Black;
            States[row, column] = sourceData.State;
            sourceMarked = false;
            destinationMarked = false;
        }

        private bool CheckValidSource(int row, int column)
        {
            return States[row, column] == State.BlackPlayer || States[row, column] == State.WhitePlayer;
        }

        private bool CheckValidDestination(int row, int column)
        {
            return States[row, column] == State.Black;
        }
    }
}