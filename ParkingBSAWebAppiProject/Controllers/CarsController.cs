using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using ParkingBSA;

namespace ParkingBSAWebAppiProject.Controllers
{
    [Produces("application/json")]
    [Route("api/[controller]")]
    public class CarsController : Controller
    {
        private Parking _Parking { get; set; } = Parking.Instanse;

        // GET api/cars
        [HttpGet]
        public JsonResult Get()
        {
            return Json(_Parking.CarsList);
        }

        // GET api/cars/5
        [HttpGet("{id}")]
        public ActionResult Get(string id)
        {
            try
            {
                return Json(_Parking.CarsList.Where<Car>(x => x.ID == id));
            }
            catch (InvalidOperationException)
            {
                return StatusCode(404);
            }
        }

        // POST api/cars
        [HttpPost]
        public StatusCodeResult Post([FromBody]Car car)
        {
            if(car != null && car.Balance >= 0 && !string.IsNullOrWhiteSpace(car.ID))
            {
                _Parking.AddCar(car);
                return Ok();
            }
            else
            {
                return StatusCode(400);
            }
        }


        // DELETE api/cars/5
        [HttpDelete("{id}")]
        public StatusCodeResult Delete(string id)
        {
            try
            {
                Car car = _Parking.CarsList.Where(x => x.ID == id).First();

                if (car.Balance >= 0)
                {
                    _Parking.RemoveCar(car);
                    return StatusCode(200);
                }
                else
                {
                    return StatusCode(402);
                }
        }
            catch (InvalidOperationException)
            {
                return StatusCode(404);
    }
}
    }
}
