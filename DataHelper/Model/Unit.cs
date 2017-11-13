using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataHelper.Model
{
    public class Unit:Common
    {
        private string _Name;

        public string Name { get { return this._Name; } set { Changed = MarkChange; this._Name = value; } }
    }
}
