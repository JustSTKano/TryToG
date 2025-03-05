using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Media;

namespace TryToG.Cells
{
    internal abstract class DefCell
    {
        /// <summary>
        /// Тип ячейки.
        /// </summary>
        public (int x, int y) Coordinates { get; set; }
        public int Type { get; set; }
    }
}
