using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using TryToG.Data.Cells;
using TryToG.Data;


namespace TryToG.Engine
{
    class StatusCheck
    {
        public static void WinLose(CellType check, Reader Reader) //Проверка ячейки ()
        {
            

            if (check == CellType.Lose)
            {
                Control.GrabStatus = false;
                Reader.ReadMap();
            }
            if (check == CellType.Win)
            {
                if (CheckNextLvl(Reader))
                {
                    Control.GrabStatus = false;
                    Reader.lvl++;
                    Reader.ReadMap();
                }
                else
                {
                    Control.GrabStatus = false;
                    MessageBox.Show("Сорян, уровней нет больше. Давай по новой!");
                    Reader.lvl = 1;
                    Reader.ReadMap();
                }

            }
            
        }

        public static bool CheckNextLvl (Reader Reader)
        {
            var temp = Reader.lvl;
            temp++;

            FileInfo what = new($"{Reader.Pathlevel}level{temp}.txt");
            return what.Exists;
        }


    }
}
