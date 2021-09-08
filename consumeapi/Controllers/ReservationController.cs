using consumeapi.Models;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.JsonPatch;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Hosting.Internal;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;


namespace consumeapi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ReservationController : ControllerBase
    {
        private IRepository repository;

        private readonly IHostingEnvironment _hostingEnvironment; 

        public ReservationController(IRepository repo,IHostingEnvironment hostingEnvironment)
        {
            repository = repo;
            _hostingEnvironment = hostingEnvironment;
        }

        [HttpGet]
        public IEnumerable<Reservation> GetReservation()
        {
            var reservation= repository.Reservations;
            return reservation;
        }
        [HttpGet("{id}")]
        public ActionResult<Reservation> Get(int id)
        {
            if (id == 0)
                return BadRequest("value must be passed in the request body.");
            return Ok(repository[id]);
        }
        [HttpPost]
        public Reservation Post([FromBody] Reservation res) => repository.AddReservation(new Reservation
        {
            Name=res.Name,
            StartLocation=res.StartLocation,
            EndLocation=res.EndLocation
        });
        [HttpPut]
        public Reservation Put([FromBody] Reservation res) => repository.UpdateReservation(res);
        [HttpPatch]
        public StatusCodeResult Patch(int id, [FromBody] JsonPatchDocument<Reservation> patch)
        {
            var res = (Reservation)((OkObjectResult)Get(id).Result).Value;
            if(res!=null)
            {
                patch.ApplyTo(res);
                return Ok();
            }
            return NotFound();
        }
        [HttpDelete("{id}")]
        public void Delete(int id) => repository.DeleteReservation(id);
        [HttpPost("UploadFile")]
        public async Task<string> UploadFile([FromForm] IFormFile file)
        {
            string path = Path.Combine(_hostingEnvironment.WebRootPath, "Images/" + file.FileName);
            using (var stream = new FileStream(path, FileMode.Create))
            {
                await file.CopyToAsync(stream);
            }
            return "https://localhost:44389/Images/" + file.FileName;




        }

    }
}
