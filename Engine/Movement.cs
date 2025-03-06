using System.Windows.Input;
using TryToG.Data;

namespace TryToG.Engine
{
    class Movement
    {

        public static void Move(Reader Reader, Key key)
        {
            var nextCell = GetNextLocation(Reader.Player.Coordinates, key);   // корды игрока на след ячейке

            if(Reader.Box.Exists(b => b.Coordinates.x == nextCell.x && b.Coordinates.y == nextCell.y))
            {
                int index = Reader.Box.FindIndex(b => b.Coordinates.x == nextCell.x && b.Coordinates.y == nextCell.y);

                var nextCellBox = GetNextLocation(Reader.Box[index].Coordinates, key);

                if (Reader.Cell[nextCellBox.y, nextCellBox.x].Type != 1)
                {

                    Reader.Box[index].Coordinates = nextCellBox;
                    Reader.Player.Coordinates = nextCell;
                }

            }
            else if (Reader.Cell[nextCell.y, nextCell.x].Type != 1)
            {
                Reader.Player.Coordinates = nextCell;
            }
        }
        private static (int x, int y) GetNextLocation((int x, int y) player, Key key) => key switch
        {
            Key.Right => (player.x + 1, player.y),
            Key.Left => (player.x - 1, player.y),
            Key.Up => (player.x, player.y - 1),
            Key.Down => (player.x, player.y + 1),
            _ => player
        };





    }
}
