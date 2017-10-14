using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace JackTest
{
    [Serializable()]
    class Lotto
    {
        public string Name { get; private set; }
        List<Draw> drawList;
        public Lotto(string _name)
        {
            Name = _name;
            drawList = new List<Draw>();
        }

        /// <summary>
        /// Добавить тираж в лотерею
        /// </summary>
        /// <param name="_draw">Тираж</param>
        public void AddDraw(Draw _draw)
        {
            if(_draw !=null)
                drawList.Add(_draw);
        }

        /// <summary>
        /// Получить все тиражи
        /// </summary>
        /// <returns>Получить массив всех тиражей</returns>
        public Draw[] GetAllDraws()
        {
            Draw[] draws = new Draw[drawList.Count];
            drawList.CopyTo(draws);
            return draws;
        }
    }
}
