using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using ECS_Webservice.Controllers;
using ECS_Webservice.Responses;

namespace ECS_Webservice.Requests
{
    public class CheckBarcodeRequest
    {
        public string Barcode { get; set; }
        public LocationDto Location { get; set; }
        public SessionDto Session { get; set; }
    }
}
