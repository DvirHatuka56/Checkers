using System.Windows.Controls;
using System.Windows.Input;

namespace Checkers
{
    public delegate void SquareClicked(int row, int column);
    
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow
    {
        private Square[,] Squares;
        private Board Board;
        public static event SquareClicked SquareClickedEvent;
        
        public MainWindow()
        {
            InitializeComponent();
            Squares = new Square[8, 8];
            Board = new Board();
            for (int i = 0; i < Squares.GetLength(0); i++)
            {
                for (int j = 0; j < Squares.GetLength(1); j++)
                {
                    Squares[i, j] = new Square(i, j);
                    Squares[i, j].MouseDown += OnMouseDown;
                    Grid.SetRow(Squares[i, j], i + 1); 
                    Grid.SetColumn(Squares[i, j], j + 1);
                    BoardGrid.Children.Add(Squares[i, j]);
                    Squares[i, j].Update(Board[i, j]);
                }
            }
        }

        private void OnMouseDown(object sender, MouseButtonEventArgs e)
        {
            Square square = (Square) sender;
            SquareClickedEvent?.Invoke(square.Row, square.Column);
            Update();
        }

        private void Update()
        {
            for (int i = 0; i < Squares.GetLength(0); i++)
            {
                for (int j = 0; j < Squares.GetLength(1); j++)
                {
                    Squares[i, j].IsEnabled = Board.IsBlack(i, j) && Board.BlackTurn ||
                                              Board.IsWhite(i, j) && !Board.BlackTurn ||
                                              Board[i, j] == State.Black;
                    Squares[i, j].Update(Board[i, j]);
                }
            }
        }
    }
}