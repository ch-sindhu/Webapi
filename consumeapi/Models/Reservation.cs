using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace consumeapi.Models
{
    public class Reservation
    {
         public int Id { set; get; }
         public string Name { set; get; }
        public string StartLocation { set; get; }

        public string EndLocation { set; get; }

    }
}
