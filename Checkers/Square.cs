using System;
using System.Windows.Controls;
using System.Windows.Media;
using System.Windows.Media.Imaging;
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
            BitmapImage image = new BitmapImage();
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
                    image.BeginInit();
                    image.UriSource = new Uri("Images/BlackPlayer.png", UriKind.Relative);
                    image.EndInit();
                    Content = new Image
                    {
                        Source = image,
                        Stretch = Stretch.UniformToFill
                    };
                    break;
                case State.WhitePlayer:
                    image.BeginInit();
                    image.UriSource = new Uri("Images/WhitePlayer.png", UriKind.Relative);
                    image.EndInit();
                    Content = new Image
                    {
                        Source = image,
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