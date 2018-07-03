using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ProductsCatalog.Models
{
    public class WebApiCall
    {
        public int Id { get; set; }
        public string CallRequest { get; set; }
        public string CallResponse { get; set; }
        public DateTime TimeStamp { get; set; }
        public string UserId { get; set; }

    }
}
