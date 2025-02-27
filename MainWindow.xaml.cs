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

namespace TryToG;

/// <summary>
/// Interaction logic for MainWindow.xaml
/// </summary>
public partial class MainWindow : Window
 
{
    private const int SizeCell = 50;                // Размер ячейки
    private const string Pathlevel = "Levels\\";
    private int lvl = 1;
    private int SZlvl;
    private (int x, int y) StartPoint { get; set; }
    private (int x, int y) Player { get; set; }   // Матрица игрока
    private int[][] Map { get; set; }            //Матрица карты

    public MainWindow()
    {
        InitializeComponent();
        
        ReadMap();
        RenderMap();
        

    }

    private void ReadMap()   //Чтение файла с картой, заполнеие матрицы
    {
        var str = File.ReadAllLines($"{Pathlevel}level{lvl}.txt");

        var sPP = str.First().Split().Select(int.Parse).ToArray();
        Player = (sPP[0], sPP[1]);
        StartPoint = Player;
        SZlvl = sPP[2];
        Map = new int[str.Length-1][];
        var temp = 0;
        foreach (var line in str.Skip(1))
        {
            Map[temp] = line.Split().Select(int.Parse).ToArray();
            temp++;
        }

    }

    private void RenderMap()  // Отрисовка карты и игрока
    {
        Canvas.Children.Clear();

        for (int i = 1; i < Map.Length + 1; i++)
        {
            for (int j = 1; j < SZlvl + 1; j++)
            {
                Canvas.Children.Add(SquareGen(j * SizeCell, i * SizeCell, Map[i - 1][j - 1]));
            }
        }
        Canvas.Children.Add(SquareGen(Player.x * SizeCell, Player.y * SizeCell, 2));
    }
    private void Window_KeyDown(object sender, KeyEventArgs e)    //Управление
    {
        Player = (Player.x - 1, Player.y - 1);
         switch (e.Key)
         {
             case Key.Right: if (Map[Player.y][Player.x+1] != 1) { Player = (Player.x+1, Player.y); }; break;
             case Key.Left: if (Map[Player.y][Player.x-1] != 1) { Player = (Player.x-1, Player.y); }; break;
             case Key.Up: if (Map[Player.y-1][Player.x] != 1) { Player = (Player.x, Player.y-1); }; break;
             case Key.Down: if (Map[Player.y+1][Player.x] != 1) { Player = (Player.x, Player.y+1); }; break;
             default: break;

         }
        Player = (Player.x + 1, Player.y + 1);
        RenderMap();
        WinLose(Map[Player.y - 1][Player.x - 1]);
        
    }
    private void WinLose(int check) //Проверка ячейки ()
    {
        if (check == 3)
        {
            MessageBox.Show("Вы победили!");

            lvl++;
            FileInfo what = new FileInfo($"{Pathlevel}level{lvl}.txt");
            if (what.Exists)
            {
                Canvas.Children.Clear();
                ReadMap();
                RenderMap();
            }
            else
            {
                MessageBox.Show("Сорян, уровней нет больше. Давай по новой!");
                lvl = 1;
                Canvas.Children.Clear();
                ReadMap();
                RenderMap();
            }

        }
        if (check == 4)
        {
            MessageBox.Show("Вы проиграли!");
            Player = StartPoint;
            Canvas.Children.Clear();
            RenderMap();
        }
    }

    private Polyline SquareGen(double x, double y, int color = 0)   // Генерация ячеек с заданными параметрами
    {
        Polyline Square = new Polyline();

        Square.Stroke = Brushes.Black;
        switch (color)
        {
            case 0: Square.Fill = Brushes.White; break;
            case 1: Square.Fill = Brushes.Black; break;
            case 2: Square.Fill = Brushes.Red; break;
            case 3: Square.Fill = Brushes.Green; break;
            case 4: Square.Fill = Brushes.Blue; break;
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