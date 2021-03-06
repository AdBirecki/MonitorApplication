﻿using MonitorApplication_Models.FileModels;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace MonitorApplication_Models.UserModels
{
    public class User
    {
        public int UserId { get; set; }
        public string Name { get; set; }
        public string Surname { get; set; }
        public string Username { get; set; }
        public string Password { get; set; }
        public int Age { get; set; }

        public List<UserOrders> UserOrders {get; set;}
        public List<UserAppFile> UserAppFiles { get; set; }
    }
}
