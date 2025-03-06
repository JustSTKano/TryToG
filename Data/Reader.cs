using System.IO;
using System.Windows;
using TryToG.Data.Cells;

namespace TryToG.Data
{
    class Reader
    {
        
        private readonly string Pathlevel;
        public int lvl = 1;
        public (int x, int y) Size { get; set; }

        public StaticCell[,] Cell { get; set; } = new StaticCell[0, 0];
        public List<DynamicCell> Box = new List<DynamicCell>();
        public DynamicCell Player = new();

        private static (int x, int y) GetMaxPositions(string[] pos) => (pos.Select(s => s.Split().Length).ToArray().Max(), pos.Length);


            
        public Reader(string levelsFolder)
        {
            Pathlevel = levelsFolder;

            if (Directory.Exists(Pathlevel))
            {
                ReadMap();
            }           
        }

        public void ReadMap()   //Чтение файла с картой, заполнение матрицы
        {
            CleanBox();

            var str = File.ReadAllLines($"{Pathlevel}level{lvl}.txt");

            Size = GetMaxPositions(str);
            Cell = new StaticCell[Size.y, Size.x];

            for (int i = 0; i < Size.y; i++)
            {
                var linestr = str[i].Split().Select(int.Parse).ToArray();

                for (int j = 0; j < Size.x; j++)
                {
                    Cell[i, j] = new()
                    {
                        Type = linestr[j],
                    };
                    if (linestr[j] == 4)
                    {
                        Cell[i, j].Type = 0;
                        Box.Add(new()
                        {
                            Type = linestr[j],
                            Coordinates = (j, i)
                        });
                    }
                    if (linestr[j] == 5)
                    {
                        Cell[i, j].Type = 0;
                        Player = new()
                        {
                            Type = linestr[j],
                            Coordinates = (j, i)
                        };
                    } 
                }
            }
        }
        public void WinLose(int check) //Проверка ячейки ()
        {
            if (check == 3)
            {
                lvl++;
                FileInfo what = new FileInfo($"{Pathlevel}level{lvl}.txt");
                if (what.Exists)
                {
                    ReadMap();
                }
                else
                {
                    MessageBox.Show("Сорян, уровней нет больше. Давай по новой!");
                    lvl = 1;
                    ReadMap();
                }

            }
            if (check == 2)
            {
                ReadMap();
            }
        }

        public void CleanBox()
        {
            Box = new List<DynamicCell>();
        }

    }
}
