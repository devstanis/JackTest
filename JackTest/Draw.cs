using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace JackTest
{
    class Draw
    {
        public DateTime Date { get; private set; }
        public int DrawNum { get; private set; }
        public int[] Nums { get; private set; }
        public int JackSum { get; private set; }
        public bool IsJackWin { get; private set; }

        public Draw(int _drawNum, DateTime _date, int[] _nums,int _jack, bool _isWin)
        {
            DrawNum = _drawNum;
            Date = _date;
            Array.Copy(_nums, Nums, _nums.Length);
            JackSum = _jack;
            IsJackWin = _isWin;
        }
    }
}
