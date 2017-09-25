using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace JackTest
{
    class Lotto
    {
        public string Name { get; private set; }
        List<Draw> drawList;
        public Lotto(string _name)
        {
            Name = _name;
            drawList = new List<Draw>();
        }

        public void AddDraw(Draw _draw)
        {
            if(_draw !=null)
                drawList.Add(_draw);
        }

        public Draw[] GetAllDraws()
        {
            Draw[] draws = new Draw[drawList.Count];
            drawList.CopyTo(draws);
            return draws;
        }
    }
}
