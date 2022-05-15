using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using ECS_Webservice.Controllers;
using ECS_Webservice.Responses;

namespace ECS_Webservice.Services.Interfaces
{
    public interface IScanService
    {
        CheckBarcodeResponse ScanBarcode(string barcode, LocationDto location, SessionDto session);

        int GetPersonCount(LocationDto location);
    }
}
