using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Serialization
{
    [Serializable]
    public class Employee
    {

        [NonSerialized]
        private int x;
        public int Id { get; set; }
        public string Name { get; set; }
        public List<string> Benefits { get; set; } = new List<string>();

    }
}
