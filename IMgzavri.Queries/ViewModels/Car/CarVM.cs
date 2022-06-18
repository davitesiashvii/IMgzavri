using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace IMgzavri.Queries.ViewModels.Car
{
    public class CarVM
    {
        public Guid Id { get; set; }
        public string Manufacturer { get; set; }
        public string Model { get; set; }
        public DateTime CreatedDate { get; set; }
        public string mainImageLink { get; set; }
        public List<string> Images { get; set; }
    }
}
