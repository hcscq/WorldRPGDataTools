using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WorldRpgEquip.Services;
using WorldRpgModel;

namespace test
{
    class Program
    {
        static void Main(string[] args)
        {
            Boss NN = new WorldRpgModel.Boss();
            NN.Name = "";
            IList<Boss> bossList = BossService.LoadData();
            int a = sizeof(char);
            a *= 4;
        }
    }
}
