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
    public class ReservationController : ControllerBase                /*ApiController */   /*ControllerBase*/
    {
        private readonly IRepository _repository;
        private readonly IHostingEnvironment _hostingEnvironment; 

        public ReservationController(IRepository  repository,IHostingEnvironment hostingEnvironment)
        {
            _repository = repository;
            _hostingEnvironment = hostingEnvironment;
        }

        [HttpGet]
        public async Task<IEnumerable<Reservation>> Get()
        {
            
            return await _repository.Get();
        }
        [HttpGet("{id}")]
        public async Task<ActionResult<Reservation>> Get(int id)
       {
            if (id == 0)
            {
                return BadRequest("value must be passed in the request body.");
            }
                
            return await _repository.Get(id);
        }
        [HttpPost]
        public async Task<ActionResult<Reservation>> Post([FromBody] Reservation res)
        {
             var newres=await _repository.AddReservation(res);
            return CreatedAtAction(nameof(Get), new { id = newres.Id }, newres);
        }

        [HttpPut("{id}")]
        public async Task<IActionResult>  Put([FromRoute]int id,[FromForm] Reservation res)
        {
            await _repository.UpdateReservation(id,res);
            return Ok();
        }
        [HttpPatch("{id}")]
        public async  Task<IActionResult> Patch(int id, [FromBody] JsonPatchDocument Reservation )
        {
            await _repository.UpdatePatchReservation(id, Reservation);
            return Ok();
        }
        [HttpDelete("{id}")]
        public async Task<ActionResult> Delete(int id)
        {
            try
            {
                var restodelete = await _repository.Get(id);
                if (restodelete == null)
                     return NotFound();
                  await  _repository.DeleteReservation(id);
                return NoContent();
                
            }
            catch(Exception e)
            {
                Console.WriteLine(e.ToString());
            }
            return Ok();

            
        }
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
