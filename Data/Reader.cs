﻿using System.IO;
using System.Windows;
using TryToG.Data.Cells;

namespace TryToG.Data
{
    class Reader
    {
        /// <summary>
        /// Путь до файлов уровней
        /// </summary>
        public string Pathlevel;
        /// <summary>
        /// Переменная текущего уровня
        /// </summary>
        public int lvl = 1;
        /// <summary>
        /// Размерность игрового поля
        /// </summary>
        public (int x, int y) Size { get; private set; }

        public StaticCell[,] Cells { get; private set; } = new StaticCell[0, 0];

        public List<BoxCell> Boxes { get; private set; } = new List<BoxCell>();

        public PlayerCell Player { get; private set; }
        /// <summary>
        /// Получение размерности игрокого поля
        /// </summary>
        /// <param name="pos"></param>
        /// <returns></returns>
        private static (int x, int y) GetMaxPositions(string[] pos) => (pos.Select(s => s.Split().Length).ToArray().Max(), pos.Length);
        
        /// <summary>
        /// Проверка наличия файлов
        /// </summary>
        /// <param name="levelsFolder"></param>
        public Reader(string levelsFolder)
        {
            Pathlevel = levelsFolder;
            Player = new((1, 1));

            if (Directory.Exists(Pathlevel))
            {
                ReadMap();
            }           
        }

        /// <summary>
        /// Чтение файла, заполнение объектов
        /// </summary>
        public void ReadMap() 
        {
            BoxInit();

            var str = File.ReadAllLines($"{Pathlevel}level{lvl}.txt");

            Size = GetMaxPositions(str);
            Cells = new StaticCell[Size.y, Size.x];

            for (int i = 0; i < Size.y; i++)
            {
                var linestr = str[i].Split().Select(int.Parse).ToArray();

                for (int j = 0; j < Size.x; j++)
                {
                    if (new int[] { 0, 1, 2, 3 }.Contains(linestr[j]))
                    {
                        Cells[i, j] = new StaticCell((CellType)linestr[j]);
                    }
                    if (linestr[j] == 4)
                    {
                        Cells[i, j] = new StaticCell(CellType.None);

                        Boxes.Add(new((j, i)));
                    }
                    if (linestr[j] == 5)
                    {
                        Cells[i, j] = new StaticCell(CellType.None);

                        Player = new((j, i));
                    } 
                }
            }
        }

        private void BoxInit()
        {
            Boxes = new List<BoxCell>();
        }
    }
}
