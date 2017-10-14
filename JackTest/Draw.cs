using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace JackTest
{
    [Serializable()]
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
            Nums = new int[_nums.Length];
            Array.Copy(_nums, Nums, _nums.Length);
            JackSum = _jack;
            IsJackWin = _isWin;
        }

        public override string ToString()
        {
            return $"{DrawNum}: {Date}, [{IntArrToString(Nums," ")}], {JackSum} - {IsJackWin}";
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="arr"></param>
        /// <param name="divider"></param>
        /// <returns></returns>
        private string IntArrToString(int[] arr, string divider)
        {
            string res = "";
            for(int i = 0; i < arr.Length; i++)
            {
                res += arr[i].ToString();
                if (i < arr.Length - 1) res += divider;
            }
            return res;
        }
    }
}
