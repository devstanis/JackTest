using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace JackWin2
{
    class Run
    {
        public int runNumber { get; private set; }
        public string runDate { get; private set; }
        public string jackPot { get; private set; }
        public int[] numbers { get; private set; }
        public bool jackWin { get; private set; }

        /// <summary>
        /// Конструктор тиража
        /// </summary>
        /// <param name="runNum">Номер тиража</param>
        /// <param name="runTime">Дата тиража</param>
        /// <param name="nums">Массив чисел</param>
        /// <param name="jack">Джекпот</param>
        /// <param name="jackwin">Разигран ли джекпот</param>
        public Run(int runNum, string runTime, int[] nums, string jack, bool jackwin)
        {
            this.numbers = nums;
            this.runNumber = runNum;
            this.jackPot = jack;
            this.runDate = runTime;
            this.jackWin = jackwin;
        }

        
        /// <summary>
        /// Возвращает строковое значение из чисел тиража
        /// </summary>
        /// <returns>Строка из чисел, разделенных пробелами</returns>
        public string GetNumbers()
        {
            StringBuilder sb = new StringBuilder();
            foreach (int n in numbers)
            {
                sb.Append(n.ToString());
                sb.Append(" ");
            }
            return sb.ToString();
        }
    }
}
