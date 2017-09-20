using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace JackWin2
{
    class GameSet
    {
        private SortedList<int,Run> runList;

        public GameSet()
        {
            runList = new SortedList<int, Run>();
        }

        /// <summary>
        /// Добавляет тиражи в список тиражей
        /// </summary>
        /// <param name="_list">Списо добавляемых тиражей</param>
        public void AddRunList(SortedList<int, Run> _list)
        {
            foreach (Run r in _list.Values)
                if(this.runList.IndexOfKey(r.runNumber)==-1) this.runList.Add(r.runNumber,r);
        }

        /// <summary>
        /// Возвращает все тиражи
        /// </summary>
        /// <returns>Список всех тиражей</returns>
        public SortedList<int,Run> GetAllGames()
        {
            return runList;
        }

    }
}
