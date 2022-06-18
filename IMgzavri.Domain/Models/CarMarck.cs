using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace IMgzavri.Domain.Models
{
    public class CarMarck
    {
        public Guid Id { get; set; }    

        public Guid? ManufacturerId { get; set; }

        public int Type { get; set; }

        public string Name { get; set; }
    }
}
