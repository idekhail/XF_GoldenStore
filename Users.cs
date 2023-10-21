using SQLite;

using System;
using System.Collections.Generic;
using System.Text;

namespace XF_GoldenStore.Model
{
    public class Users
    {
        [PrimaryKey, AutoIncrement]
        public int Id { get; set; }
        public string Username { get; set; }
        public string Password { get; set; }
        public string Mobile { get; set; }
        public string Zone { get; set; }
        public string City { get; set; }
        public string Email { get; set; }
        public string ShopName { get; set; }
        public string Url1 { get; set; }

    }
}
