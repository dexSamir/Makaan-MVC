﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Makaan.BL.VM.User;
public class LoginVM
{
    public string UsernameOrEmail { get; set; } 
    public string Password { get; set; }
    public bool RememberMe { get; set; }    
}
