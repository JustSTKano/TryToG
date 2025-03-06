using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TryToG.Data.Cells
{
    class PlayerCell : DynamicCell
    {
        internal PlayerCell((int x, int y) coordinates) : base(CellType.Player, coordinates) { }
    }
}
