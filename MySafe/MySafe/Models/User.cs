﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MySafe.Presentation.Models.Abstractions;

namespace MySafe.Presentation.Models
{
    public class User: PresentationModelBase
    {
        public string Login {get; set; }
        public string Password { get; set; }
        public string PasswordConfirmation { get; set; }
        public string Email { get; set; }
        public string PhoneNumber { get; set; }
        public bool UserAgreement { get;set; }
    }
}
