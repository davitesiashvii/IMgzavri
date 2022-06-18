using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace IMgzavri.Queries.ViewModels.Car
{
    public class CarMarckVm
    {
        public Guid Id { get; set; }
        public string Name { get; set; }

        public Guid? ManufacturerId { get; set; }

        public bool IsManufacturer { get; set; }

        public List<CarMarckVm> Models { get; set; }
    }
}
