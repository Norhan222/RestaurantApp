using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Sockets;
using System.Text;
using System.Threading.Tasks;

namespace RestaurantApp.Domain.Models
{
    public class AppUser:IdentityUser
    {
        //public int Id { get; set; }
        //public string Name { get; set; }
        //public string Phone { get; set; }
        //public string Email { get; set; }
        //public string Password { get; set; }
        //public string Role { get; set; }
        public string Address { get; set; }
       // public ICollection<Order> Orders { get; set; }
    }
}
