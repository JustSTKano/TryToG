using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TryToG.Data.Cells
{
    internal class BoxCell : DynamicCell
    {
        internal BoxCell((int x, int y) coordinates) : base(CellType.Box, coordinates) { }
    }
}
