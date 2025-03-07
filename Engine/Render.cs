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
        /// <summary>
        /// Константа размера ячейки
        /// </summary>
        private const int SizeCell = 50;                

        /// <summary>
        /// Отрисовка карты и объектов
        /// </summary>
        /// <param name="Reader"></param>
        /// <param name="Canvas"></param>
        public static void RenderMap(Reader Reader, Canvas Canvas) 
        {
            Canvas.Children.Clear();

            for (int i = 0; i < Reader.Size.y; i++)
            {
                for (int j = 0; j < Reader.Size.x; j++)
                {
                    Canvas.Children.Add(SquareGen((j + 1) * SizeCell, (i + 1) * SizeCell, Reader.Cells[i, j].Type));
                }
            }
            Canvas.Children.Add(SquareGen((Reader.Player.Coordinates.x + 1) * SizeCell, (Reader.Player.Coordinates.y + 1) * SizeCell, Reader.Player.Type));
            Reader.Boxes.ForEach(b => 
            {
                Canvas.Children.Add(SquareGen((b.Coordinates.x + 1) * SizeCell, (b.Coordinates.y + 1) * SizeCell, b.Type));
            });

        }
        /// <summary>
        /// Отрисовка единичной ячейки
        /// </summary>
        /// <param name="x"></param>
        /// <param name="y"></param>
        /// <param name="color"></param>
        /// <returns></returns>
        /// <exception cref="NotImplementedException"></exception>
        private static Polyline SquareGen(double x, double y, CellType color) => new()
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
                _ => throw new NotImplementedException()
            },
            Points = new PointCollection()
            {
                new Point(x, y),
                new Point(x, y + SizeCell),
                new Point(x + SizeCell, y + SizeCell),
                new Point(x + SizeCell, y),
                new Point(x, y)
            }
        };
    }
}
