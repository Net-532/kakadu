using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Kakadu.WebServer
{
    public class HttpResponse
    {
        public HttpStatus Status { get; set; }
        public string Body { get; set; }
        public Dictionary<string, string> Headers { get; set; }

        public HttpResponse()
        {
            Headers = new Dictionary<string, string>();
        }}}
