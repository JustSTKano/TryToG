using System;
using System.Numerics;
using System.Windows.Input;
using TryToG.Data;
using TryToG.Data.Cells;

namespace TryToG.Engine
{
    class Control
    {
        /// <summary>
        /// Проверка захвата объекта
        /// </summary>
        public static bool GrabStatus = false;

        /// <summary>
        /// Проверка перемещения
        /// </summary>
        public static bool MoveStatus = false;

        /// <summary>
        /// Индекс захваченного объекта
        /// </summary>
        public static int idGrab;

        /// <summary>
        /// Получение клавиши управления
        /// </summary>
        /// <param name="Reader"></param>
        /// <param name="key"></param>
        public static void GetCommand(Reader Reader, Key key)
        {
            if (key == Key.Space) { Grab(Reader); }
            else Action(Reader, key);
     
        }
        /// <summary>
        /// Перемещение игрока и/или связанных с ним объекта/ов
        /// </summary>
        /// <param name="Reader"></param>
        /// <param name="key"></param>
        public static void Action(Reader Reader, Key key)
        {
            Reader.Player.Coordinates = Move(Reader, key, Reader.Player.Coordinates);

            if (GrabStatus && MoveStatus)
            {
                if (key != Key.Down)
                {
                    Reader.Boxes[idGrab].Coordinates = Move(Reader, key, Reader.Boxes[idGrab].Coordinates);
                    GrabStatus = MoveStatus;
                }
                
            }
        }
        /// <summary>
        /// Захват ячейки, получение индекса связанного объекта
        /// </summary>
        /// <param name="Reader"></param>
        public static void Grab(Reader Reader )
        {
            if (!GrabStatus)
            {
                (int x, int y) getcord = (Reader.Player.Coordinates.x, Reader.Player.Coordinates.y + 1);

                if (Reader.Boxes.Exists(b => b.Coordinates.x == getcord.x && b.Coordinates.y == getcord.y))
                {
                    GrabStatus = true;
                    idGrab = Reader.Boxes.FindIndex(b => b.Coordinates.x == getcord.x && b.Coordinates.y == getcord.y);
                }
            }
            else { GrabStatus = false; }
        }
        /// <summary>
        /// Логика перемещения
        /// </summary>
        /// <param name="Reader"></param>
        /// <param name="key"></param>
        /// <param name="location"></param>
        /// <returns></returns>
        public static (int x, int y)Move(Reader Reader, Key key, (int x, int y)location)
        {
            var nextCell = GetNextLocation(location, key);   // корды игрока на след ячейке
            
            if (Reader.Boxes.Exists(b => b.Coordinates.x == nextCell.x && b.Coordinates.y == nextCell.y))
            {
                int index = Reader.Boxes.FindIndex(b => b.Coordinates.x == nextCell.x && b.Coordinates.y == nextCell.y);

                var nextCellBox = GetNextLocation(Reader.Boxes[index].Coordinates, key);

                if (Reader.Cells[nextCellBox.y, nextCellBox.x].Type != CellType.Wall && !(Reader.Boxes.Exists(b => b.Coordinates.x == nextCellBox.x && b.Coordinates.y == nextCellBox.y)))
                {
                    Reader.Boxes[index].Coordinates = nextCellBox;
                    MoveStatus = true;
                    return nextCell;
                }
            }
            else if (Reader.Cells[nextCell.y, nextCell.x].Type != CellType.Wall)
            {
                MoveStatus = true;
                return nextCell;
            }
            MoveStatus = false;
            return location;
        }

        /// <summary>
        /// Получение возможной следующей позиции
        /// </summary>
        /// <param name="Position"></param>
        /// <param name="key"></param>
        /// <returns></returns>
        private static (int x, int y) GetNextLocation((int x, int y) Position, Key key) => key switch
        {
            Key.Right => (Position.x + 1, Position.y),
            Key.Left => (Position.x - 1, Position.y),
            Key.Up => (Position.x, Position.y - 1),
            Key.Down => (Position.x, Position.y + 1),
            _ => Position
        };
    }
}
