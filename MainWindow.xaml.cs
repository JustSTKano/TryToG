using System;
using System.IO;
using System.Numerics;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;
using TryToG.Cells;

namespace TryToG;

/// <summary>
/// Interaction logic for MainWindow.xaml
/// </summary>
public partial class MainWindow : Window
 
{
    private const int SizeCell = 50;                // Размер ячейки
    private const string Pathlevel = "Levels\\";
    private int lvl = 1;

    public (int x, int y) Size { get; set; }

    private DefCell[,] Cell { get; set; } = new StaticCell[0, 0];
    public BoxCell Box = new();
    public PlayerCell Players = new();

    private static (int x, int y) GetMaxPositions(string[] pos) => (pos.Select(s => s.Split().Length).ToArray().Max(), pos.Length);

    public MainWindow()
    {
        InitializeComponent();

        ReadMap();
    }
   
    public void ReadMap()   //Чтение файла с картой, заполнение матрицы
    {

        var str = File.ReadAllLines($"{Pathlevel}level{lvl}.txt");
        Size = GetMaxPositions(str);

        Cell = new StaticCell[Size.y, Size.x];
        for (int i = 0; i<Size.y; i++)
        {
            var linestr = str[i].Split().Select(int.Parse).ToArray();
            for (int j = 0; j < Size.x; j++)
            {
                Cell[i,j] = new StaticCell();
                switch (linestr[j])
                {
                    case 0: { Cell[i, j].Type = linestr[j]; Cell[i, j].Coordinates = (i, j); break; }  //empty
                    case 1: { Cell[i, j].Type = linestr[j]; Cell[i, j].Coordinates = (i, j); break; }  //wall
                    case 2: { Cell[i, j].Type = linestr[j]; Cell[i, j].Coordinates = (i, j); break; }  //lose
                    case 3: { Cell[i, j].Type = linestr[j]; Cell[i, j].Coordinates = (i, j); break; }  //win
                    case 4: //box
                        { 
                            Cell[i, j].Type = 0;
                            Cell[i, j].Coordinates = (i, j);
                            /*Box = new BoxCell()
                            {
                                Type = linestr[j],
                                Coordinates = (i, j),
                                StartPoint = (i, j)
                            };*/

                            Box.Type = linestr[j];
                            Box.Coordinates = (j, i);
                            Box.StartPoint = (j, i);
                            break; 
                        }  
                    case 5: //Player
                        { 
                            Cell[i, j].Type = 0;
                            Cell[i, j].Coordinates = (i, j);
                            Players.Type = linestr[j];
                            Players.Coordinates = (j, i);
                            Players.StartPoint = (j, i);
                            break; 
                        }  
                    default: break;
                }
            }
        }
        RenderMap();
    }

    public void RenderMap()  // Отрисовка карты и игрока
    {
        Canvas.Children.Clear();

        for (int i = 0; i < Size.y; i++)
        {
            for (int j = 0; j < Size.x; j++)
            {
                Canvas.Children.Add(SquareGen((j+1) * SizeCell, (i+1) * SizeCell, Cell[i,j].Type)); //Map[i][j]

            }
        }
        Canvas.Children.Add(SquareGen((Players.Coordinates.x+1) * SizeCell, (Players.Coordinates.y+1) * SizeCell, Players.Type));
        if (Box.Coordinates != (0,0)) Canvas.Children.Add(SquareGen((Box.Coordinates.x+1) * SizeCell, (Box.Coordinates.y+1) * SizeCell, Box.Type));

    }
    private void Window_KeyDown(object sender, KeyEventArgs e)    //Управление
    {
        //Players.Coordinates = (Players.Coordinates.x + 2, Players.Coordinates.y + 2);

        switch (e.Key)
         {
             case Key.Right:
                if (Cell[Players.Coordinates.y, Players.Coordinates.x + 1].Type != 1)
                {
                    Players.Coordinates = (Players.Coordinates.x + 1, Players.Coordinates.y);
                    if (Players.Coordinates != Box.Coordinates)
                    {
                        Players.Coordinates = (Players.Coordinates.x + 1, Players.Coordinates.y);
                    }
                    else if (Cell[Players.Coordinates.y, Players.Coordinates.x + 1].Type != 1)
                    {
                        Players.Coordinates = (Players.Coordinates.x + 1, Players.Coordinates.y);
                        Box.Coordinates = (Box.Coordinates.x + 1, Box.Coordinates.y);
                    }
                    Players.Coordinates = (Players.Coordinates.x - 1, Players.Coordinates.y);
                }; break;

            case Key.Left:
                if (Cell[Players.Coordinates.y, Players.Coordinates.x - 1].Type != 1)
                {
                    Players.Coordinates = (Players.Coordinates.x - 1, Players.Coordinates.y);
                    if (Players.Coordinates != Box.Coordinates)
                    {
                        Players.Coordinates = (Players.Coordinates.x - 1, Players.Coordinates.y);
                    }
                    else if (Cell[Players.Coordinates.y, Players.Coordinates.x - 1].Type != 1)
                    {
                        Players.Coordinates = (Players.Coordinates.x - 1, Players.Coordinates.y);
                        Box.Coordinates = (Box.Coordinates.x - 1, Box.Coordinates.y);
                    }
                    Players.Coordinates = (Players.Coordinates.x + 1, Players.Coordinates.y);
                }; break;

            case Key.Up:
                if (Cell[Players.Coordinates.y - 1, Players.Coordinates.x].Type != 1)
                {
                    Players.Coordinates = (Players.Coordinates.x, Players.Coordinates.y-1);
                    if (Players.Coordinates != Box.Coordinates)
                    {
                        Players.Coordinates = (Players.Coordinates.x, Players.Coordinates.y - 1);
                    }
                    else if (Cell[Players.Coordinates.y-1, Players.Coordinates.x].Type != 1)
                    {
                        Players.Coordinates = (Players.Coordinates.x, Players.Coordinates.y-1);
                        Box.Coordinates = (Box.Coordinates.x, Box.Coordinates.y-1);
                    }
                    Players.Coordinates = (Players.Coordinates.x, Players.Coordinates.y + 1);
                }; break;

             case Key.Down:
                if (Cell[Players.Coordinates.y + 1, Players.Coordinates.x].Type != 1)
                {
                    Players.Coordinates = (Players.Coordinates.x, Players.Coordinates.y + 1);
                    if (Players.Coordinates != Box.Coordinates)
                    {
                        Players.Coordinates = (Players.Coordinates.x, Players.Coordinates.y + 1);
                    }
                    else if (Cell[Players.Coordinates.y + 1, Players.Coordinates.x].Type != 1)
                    {
                        Players.Coordinates = (Players.Coordinates.x, Players.Coordinates.y + 1);
                        Box.Coordinates = (Box.Coordinates.x, Box.Coordinates.y + 1);
                    }
                    Players.Coordinates = (Players.Coordinates.x, Players.Coordinates.y - 1);
                }
                ; break;
            default: break;
         }
        RenderMap();
        WinLose(Cell[Players.Coordinates.y, Players.Coordinates.x].Type);
        
    }



    private void WinLose(int check) //Проверка ячейки ()
    {
        if (check == 3)
        {
            //MessageBox.Show("Вы победили!");
            //Players.Coordinates = Players.StartPoint;
            lvl++;
            FileInfo what = new FileInfo($"{Pathlevel}level{lvl}.txt");
            if (what.Exists)
            {
                Canvas.Children.Clear();
                ReadMap();
                //RenderMap();
            }
            else
            {
                MessageBox.Show("Сорян, уровней нет больше. Давай по новой!");
                lvl = 1;
                Canvas.Children.Clear();
                Box.Coordinates = (0, 0);
                ReadMap();
                //RenderMap();
            }

        }
        if (check == 2)
        {
            //MessageBox.Show("Вы проиграли!");
           Players.Coordinates = Players.StartPoint;
            Box.Coordinates = Box.StartPoint;
            Canvas.Children.Clear();
            RenderMap();
        }
    }

    private Polyline SquareGen(double x, double y, int color)   // Генерация ячеек с заданными параметрами
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