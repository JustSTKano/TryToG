using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TryToG.Cells
{
    public class BoxCell
    {
        public (int x, int y) StartPoint { get; set; }
        public (int x, int y) Coordinates { get; set; }

        public int Type;
    }
}
