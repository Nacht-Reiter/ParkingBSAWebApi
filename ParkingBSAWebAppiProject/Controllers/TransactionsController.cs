using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using ParkingBSA;

namespace ParkingBSAWebApi.Controllers
{
    [Produces("application/json")]
    [Route("api/Transactions")]
    public class TransactionsController : Controller
    {
        private Parking _Parking { get; set; } = Parking.Instanse;

        // GET: api/transactions
        [HttpGet]
        public JsonResult Get()
        {
            return Json(_Parking.TransactionsList);
        }

        // GET: api/transactions/5
        [HttpGet("{id}")]
        public ActionResult Get(string id)
        {
            try
            {
                return Json(_Parking.TransactionsList.Where<Transaction>(x => x.CarID == id));
            }
            catch (InvalidOperationException)
            {
                return StatusCode(404);
            }
        }

        // GET: api/transactions
        [HttpGet("log")]
        public ActionResult GetLog()
        {
            try
            {
                using (StreamReader sr = new StreamReader("Transactions.log"))
                {

                    return Json(sr.ReadToEnd());
                }
            }
            catch (FileNotFoundException)
            {
                return StatusCode(404);
            }
        }

        // PUT api/transactions/10
        [HttpPut("{income}")]
        public ActionResult Put(decimal income, [FromBody]Car car)
        {
            if (car == null)
            {
                return StatusCode(400); ;
            }
            try
            {
                _Parking.CarsList.Where<Car>(x => x.ID == car.ID).First().AddIncome(income);
                return StatusCode(200);
            }
            catch (InvalidOperationException)
            {
                return StatusCode(404);
            }
        }
    }
}
