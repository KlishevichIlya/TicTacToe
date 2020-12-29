﻿using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Lab5.Models
{
    public class User : IdentityUser
    {
        public string Name { get; set; }
        public int Year { get; set; }

     
    }
}
