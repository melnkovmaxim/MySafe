﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MySafe.Models.Request.Users
{
    public class TwoFactorRequest : IToken
    {
        public string AccessToken { get; set; }
    }
}