﻿using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Panel.Models
{
    public class ApplicationUser : IdentityUser
    {
        [PersonalData]
        public Site Site { get; set; }
    }
}
