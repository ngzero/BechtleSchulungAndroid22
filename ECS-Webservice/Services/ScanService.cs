using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using ECS_Webservice.Controllers;
using ECS_Webservice.Responses;
using ECS_Webservice.Services.Interfaces;

namespace ECS_Webservice.Services
{
    public class ScanService : IScanService
    {
        public Dictionary<int, int> Locations { get; set; } = new Dictionary<int, int>();
        public Dictionary<int, List<string>> Sessions { get; set; } = new Dictionary<int, List<string>>();

        private object oLock = new object();

        public int GetPersonCount(LocationDto location)
        {
            lock (oLock)
                return Locations.ContainsKey(location.Id) ? Locations[location.Id] : 0;
        }

        CheckBarcodeResponse IScanService.ScanBarcode(string barcode, LocationDto location, SessionDto session)
        {
            lock (oLock)
            {
                if (!Locations.ContainsKey(location.Id)) Locations.Add(location.Id, 0);
                if (!Sessions.ContainsKey(session.Id)) Sessions.Add(session.Id, new List<string>());

                var barcodeList = Sessions[session.Id];
                var status = ScanBarcodeResponseEnum.None;
                if (barcodeList.Contains(barcode))
                {
                    barcodeList.Remove(barcode);
                    Locations[location.Id]--;
                    status = ScanBarcodeResponseEnum.Logout;
                }
                else
                {
                    barcodeList.Add(barcode);
                    Locations[location.Id]++;
                    status = ScanBarcodeResponseEnum.Login;
                }

                return new CheckBarcodeResponse { PersonCount = Locations[location.Id], Status = status };
            }
        }
    }
}
