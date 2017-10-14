using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace JackTest
{
    class Program
    {
        static void Main(string[] args)
        {
            //Lotto lotto = new Lotto("6 из 45");
            //LoadData(lotto);
            //DataIO.SaveData(lotto,Options.l1FilePath);

            Lotto lotto = DataIO.LoadData(Options.l1FilePath);
            foreach(var i in lotto.GetAllDraws())
                Console.WriteLine(i);

            Console.ReadKey();
        }

        static void LoadData(Lotto _lotto)
        {
            _lotto.AddDraw(new Draw(1, new DateTime(2017, 10, 13, 23, 0, 0), 
                new int[] { 21, 15, 12, 3, 44, 2 }, 25279082, true));
        }
    }
}
