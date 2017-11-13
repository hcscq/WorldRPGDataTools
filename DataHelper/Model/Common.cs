using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataHelper.Model
{
    public class Common
    {
        public bool Changed { get; set; }
        public string Key
        {

            get; set;
        }
        public static bool MarkChange = false;
    }
}
