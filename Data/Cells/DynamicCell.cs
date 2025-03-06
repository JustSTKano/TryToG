
namespace TryToG.Data.Cells
{
    public class DynamicCell : StaticCell
    {
        internal DynamicCell(CellType type, (int x, int y) coordinates) : base(type)
        {
            this.Coordinates = coordinates;
        }

        public (int x, int y) Coordinates { get; set; }

    }
}
