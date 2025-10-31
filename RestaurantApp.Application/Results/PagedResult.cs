using RestaurantApp.Application.Dtos;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RestaurantApp.Application.Results
{
    public class PagedResult
    {
        public int PageNumber { get; set; }
        public int PageSize { get; set; }
        public int PageCount { get; set; }
        public int TotalCount { get; set; }
        public List<MenuItemDto> menuItems { get; set; }


    }
}
