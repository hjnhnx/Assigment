using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Asignment_UWP.Entity
{
    class Credential
    {
        public string access_token { get; set; }
        public string refresh_token { get; set; }
        public string acount_id { get; set; }
        public string created_at { get; set; }
        public string updated_at { get; set; }
        public int status { get; set; }
    }
}
