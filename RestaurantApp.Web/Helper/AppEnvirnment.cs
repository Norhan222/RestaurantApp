using RestaurantApp.Application.Interfaces;

namespace RestaurantApp.Web.Helper
{
    public class AppEnvirnment : IAppEnvirnment
    {
        private readonly IWebHostEnvironment _environment;

        public AppEnvirnment(IWebHostEnvironment environment)
        {
            _environment = environment;
        }
        public string WebPathRoot => _environment.WebRootPath;
    }
}
