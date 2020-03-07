using System;
using System.Windows.Controls;
using System.Windows.Media;
using System.Windows.Shapes;

namespace Checkers
{
    public class Square : Label
    {
        public int Row { get; }
        public int Column { get; }

        public Square(int row, int column)
        {
            Row = row;
            Column = column;
        }

        public void Update(State state)
        {
            Background = new SolidColorBrush(Colors.Black);
            switch (state)
            {
                case State.Black:
                    Content = null;
                    break;
                case State.White:
                    Background = new SolidColorBrush(Colors.White);
                    Content = null;
                    break;
                case State.BlackPlayer:
                    Content = new Ellipse
                    {
                        Fill = new SolidColorBrush(Colors.DimGray),
                        Stretch = Stretch.UniformToFill
                    };
                    break;
                case State.WhitePlayer:
                    Content = new Ellipse
                    {
                        Fill = new SolidColorBrush(Colors.Wheat),
                        Stretch = Stretch.UniformToFill
                    };
                    break;
                case State.BlackQueen:
                    Content = new Ellipse
                    {
                        Fill = new SolidColorBrush(Colors.DarkGoldenrod),
                        Stretch = Stretch.UniformToFill
                    };
                    break;
                case State.WhiteQueen:
                    Content = new Ellipse
                    {
                        Fill = new SolidColorBrush(Colors.Goldenrod),
                        Stretch = Stretch.UniformToFill
                    };
                    break;
                case State.Source:
                    Background = new SolidColorBrush(Colors.Cyan);
                    break;
                default:
                    throw new ArgumentOutOfRangeException(nameof(state), state, null);
            }
        }
    }
}