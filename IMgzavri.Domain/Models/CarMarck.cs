﻿using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace IMgzavri.Domain.Models
{
    public class CarMarck
    {
        [DatabaseGenerated(DatabaseGeneratedOption.None)]
        public int Id { get; set; }    

        public string Code { get; set; }

        public string Name { get; set; }
    }
}
