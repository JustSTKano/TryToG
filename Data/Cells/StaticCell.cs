
namespace TryToG.Data.Cells
{
    public class StaticCell
    {
        internal CellType Type { get; private set; }

        internal StaticCell(CellType type)
        {
            this.Type = type;
        }
    }
}
