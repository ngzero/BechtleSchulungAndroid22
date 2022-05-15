using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ECS_Webservice.Responses
{
    public class CheckBarcodeResponse
    {
        public ScanBarcodeResponseEnum Status { get; set; }
        public int PersonCount { get; set; }
    }

    public enum ScanBarcodeResponseEnum
    {
        None,
        Login,
        Logout
    }
}
