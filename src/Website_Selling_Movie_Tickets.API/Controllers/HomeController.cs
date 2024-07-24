using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace Website_Selling_Movie_Tickets.API.Controllers
{
    public class HomeController : ControllerBase
    {
        public IActionResult Index()
        {
            return Redirect("~/swagger");
        }
    }
}
