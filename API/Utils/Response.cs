using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace API.Utils
{
    public class Response
    {
        public Response(bool success,object data,ResponseError error){
            this.Succes=success;
            this.Data=data;
            this.Error=error;
        }
        public string ApiVersion { get; set; }="1.0";

        public bool Succes { get; }
        public object Data { get; }
        public ResponseError Error { get; }
    }
}