using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace LiangchenServer
{
    public class UserModel
    {
        public string email { get; set; }
        public string password { get; set; }
        public string username { get; set; }
        public string enabled { get; set; }
    }
}