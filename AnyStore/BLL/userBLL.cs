using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AnyStore.BLL
{
    class userBLL
    {
        public int id { get; set; }
        public string first_name { get; set; } = string.Empty;
        public string last_name { get; set; } = string.Empty;
        public string email { get; set; } = string.Empty;
        public string username { get; set; } = string.Empty;
        public string password { get; set; } = string.Empty;
        public string contact { get; set; } = string.Empty;
        public string address { get; set; } = string.Empty;
        public string gender { get; set; } = string.Empty;
        public string user_type { get; set; } = string.Empty;
        public DateTime added_date { get; set; }
        public int added_by { get; set; }
    }
}
