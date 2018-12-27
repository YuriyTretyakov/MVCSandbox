using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Whoops.Services.ViberCommunication
{
    public class WebHookRequest
    {
     
            public string url { get; set; }
            public List<string> event_types { get; set; }
            public bool send_name { get; set; }
            public bool send_photo { get; set; }
        
    }
}
