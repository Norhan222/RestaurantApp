namespace RestaurantApp.Web.Models
{
    public class MenuVM
    {
        public IEnumerable<MenuItemVM> MenuItems {  get; set; }
        public IEnumerable<CategoryVM> Categories { get; set; }
    }
}
