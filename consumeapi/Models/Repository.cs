using Microsoft.AspNetCore.JsonPatch;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace consumeapi.Models
{
    public class Repository : IRepository
    {
        private readonly ReservationContext _context;

       

        public Repository(ReservationContext context)
        {
            _context = context;
        }

        public async Task<Reservation> Get(int id)
        {
            return await _context.Reservations.FindAsync(id);
        }


        public async Task<IEnumerable<Reservation>> Get()
        {
            return await _context.Reservations.ToListAsync();
        }

        public async Task<Reservation> AddReservation(Reservation reservation)
        {
            _context.Reservations.Add(reservation);
            await _context.SaveChangesAsync();
            return reservation;
        }

        public async Task DeleteReservation(int id)
        {
            var todelete =await _context.Reservations.FindAsync(id);
            _context.RemoveRange(todelete);
            await _context.SaveChangesAsync();
            //var todelete = new Reservation() { Id = id };
            //_context.Reservations.Remove(todelete);
            //await _context.SaveChangesAsync();


        }


        public async Task UpdateReservation(int id,Reservation reservation)
        {
            //_context.Entry(reservation).State = EntityState.Modified;
            //    await _context.SaveChangesAsync();
            //    return reservation;
            var res = new Reservation()
            {
                Id=reservation.Id,
                Name = reservation.Name,
                StartLocation=reservation.StartLocation,
                EndLocation=reservation.EndLocation

            };
            _context.Reservations.Update(res);
            await _context.SaveChangesAsync();
        }

        public async Task UpdatePatchReservation(int id, [FromBody] JsonPatchDocument Reservation)
        {
            var res = await _context.Reservations.FindAsync(id);
            if(res!=null)
            {
                Reservation.ApplyTo(res);
                await _context.SaveChangesAsync();
            }
        }
    }
}
