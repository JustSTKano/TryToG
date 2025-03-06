using System.Windows;
using System.Windows.Controls;
using TryToG.Data;
using System.Windows.Media;
using System.Windows.Shapes;

namespace TryToG.Engine
{
    internal static class Render
    {
        private const int SizeCell = 50;                // Размер ячейки

        public static void RenderMap(Reader Reader, Canvas Canvas)  // Отрисовка карты и игрока
        {
            Canvas.Children.Clear();

            for (int i = 0; i < Reader.Size.y; i++)
            {
                for (int j = 0; j < Reader.Size.x; j++)
                {
                    Canvas.Children.Add(SquareGen((j + 1) * SizeCell, (i + 1) * SizeCell, Reader.Cell[i, j].Type)); //Map[i][j]

                }
            }
            //Reader.Box;
            Canvas.Children.Add(SquareGen((Reader.Player.Coordinates.x + 1) * SizeCell, (Reader.Player.Coordinates.y + 1) * SizeCell, Reader.Player.Type));
            Reader.Box.ForEach(b => {
                Canvas.Children.Add(SquareGen((b.Coordinates.x + 1) * SizeCell, (b.Coordinates.y + 1) * SizeCell, b.Type));
            });

        }

        private static Polyline SquareGen(double x, double y, int color)   // Генерация ячеек с заданными параметрами
        {
            Polyline Square = new Polyline();

            Square.Stroke = Brushes.Black;
            switch (color)
            {
                case 0: Square.Fill = Brushes.White; break;
                case 1: Square.Fill = Brushes.Black; break;
                case 2: Square.Fill = Brushes.Blue; break;
                case 3: Square.Fill = Brushes.Green; break;
                case 4: Square.Fill = Brushes.Orange; break;
                case 5: Square.Fill = Brushes.Red; break;
                default: throw new NotImplementedException();

            }

            Square.Points = new PointCollection()
            {
                new Point(x, y),
                new Point(x, y + SizeCell),
                new Point(x + SizeCell, y + SizeCell),
                new Point(x + SizeCell, y),
                new Point(x, y)
            };

            return Square;
        }
    }
}
