namespace RestaurantApp.Web.Models
{
    public class MenuVM
    {
        public IEnumerable<MenuItemVM>? MenuItems {  get; set; }
        public IEnumerable<CategoryVM>? Categories { get; set; }
        public int PageCount { get; set; }
        public int PageSize { get; set; }
        public int PageNumber { get; set; }

    }
}
