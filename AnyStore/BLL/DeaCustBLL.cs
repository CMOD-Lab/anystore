using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AnyStore.BLL
{
    class DeaCustBLL
    {
        public int id { get; set; }
        public string type { get; set; } = string.Empty;
        public string name { get; set; } = string.Empty;
        public string email { get; set; } = string.Empty;
        public string contact { get; set; } = string.Empty;
        public string address { get; set; } = string.Empty;
        public DateTime added_date { get; set; }
        public int added_by { get; set; }
    }
}
