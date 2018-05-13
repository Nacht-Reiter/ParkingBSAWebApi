using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using ParkingBSA;

namespace ParkingBSAWebApi.Controllers
{
    [Produces("application/json")]
    [Route("api/Parking")]
    public class ParkingController : Controller
    {
        private Parking _Parking { get; set; } = Parking.Instanse;

        // GET: api/parking
        [HttpGet]
        public JsonResult Get()
        {
            return Json(_Parking.CarsList.Count);
        }

        // GET: api/parking/freespace
        [HttpGet("freespace")]
        public JsonResult GetFreeSpace()
        {
            return Json(_Parking.FreeSpace());
        }

        // GET: api/parking/balance
        [HttpGet("balance")]
        public JsonResult GetBalance()
        {
            return Json(_Parking.Balance);
        }


    }
}
