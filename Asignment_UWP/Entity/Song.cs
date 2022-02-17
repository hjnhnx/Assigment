using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Asignment_UWP.Entity
{
    class Song
    {
        public long id { get; set; }
        public string name { get; set; }
        public string description { get; set; }
        public string singer { get; set; }
        public string author { get; set; }
        public string thumbnail { get; set; }
        public string link { get; set; }
        public long account_id { get; set; }
        public string createdAt { get; set; }
        public string updatedAt { get; set; }
        public int status { get; set; }
    }
}
