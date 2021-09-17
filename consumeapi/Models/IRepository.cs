using Microsoft.AspNetCore.JsonPatch;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace consumeapi.Models
{
   public  interface IRepository
    {
         Task<Reservation> Get(int id);
         Task<IEnumerable<Reservation>> Get();
        Task<Reservation> AddReservation(Reservation reservation);
        Task DeleteReservation(int id);
        Task UpdateReservation(int id,Reservation reservation);
        Task UpdatePatchReservation(int id, [FromBody] JsonPatchDocument Reservation);
    }
}
