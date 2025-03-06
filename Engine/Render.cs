using System.Windows;
using System.Windows.Controls;
using TryToG.Data;
using System.Windows.Media;
using System.Windows.Shapes;
using TryToG.Data.Cells;

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
                    Canvas.Children.Add(SquareGen((j + 1) * SizeCell, (i + 1) * SizeCell, Reader.Cell[i, j].Type));
                }
            }
            Canvas.Children.Add(SquareGen((Reader.Player.Coordinates.x + 1) * SizeCell, (Reader.Player.Coordinates.y + 1) * SizeCell, Reader.Player.Type));
            Reader.Box.ForEach(b => 
            {
                Canvas.Children.Add(SquareGen((b.Coordinates.x + 1) * SizeCell, (b.Coordinates.y + 1) * SizeCell, b.Type));
            });

        }

        private static Polyline SquareGen(double x, double y, CellType color)   // Генерация ячеек с заданными параметрами
        {
            Polyline Square = new()
            {
                Stroke = Brushes.Black,
                Fill = color switch
                {
                    CellType.None => Brushes.White,
                    CellType.Wall => Brushes.Black,
                    CellType.Lose => Brushes.Blue,
                    CellType.Win => Brushes.Green,
                    CellType.Box => Brushes.Orange,
                    CellType.Player => Brushes.Red,
                    _=> throw new NotImplementedException()
                }
            };
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
