﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Auth.Responses
{
    public class LoginResponse
    {
        public string Token { get; set; }
        public string Email { get; set; }
    }
}
