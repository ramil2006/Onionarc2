﻿using Domain.Common;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Models
{
    public class Room:BaseEntity
    {
        public string Name { get; set; }
        public ICollection<Group> Groups { get; set; }

    }
}
