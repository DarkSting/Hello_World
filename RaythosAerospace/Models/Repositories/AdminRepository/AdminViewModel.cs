﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace RaythosAerospace.Models.Repositories.AdminRepository
{
    public class AdminViewModel
    {
        public string Email { get; set; }

        public string OldPassword { get; set; }
        public string Password { get; set; }

        public AdminModel Admin { get; set; }
    }
}
