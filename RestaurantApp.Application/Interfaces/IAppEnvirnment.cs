using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RestaurantApp.Application.Interfaces
{
    public interface IAppEnvirnment
    {
        string WebPathRoot { get; }
    }
}
