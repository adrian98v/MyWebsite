using Microsoft.AspNetCore.Mvc;

namespace MyWebsite.Controllers
{
    public class LanguageController : Controller
    {


        public IActionResult changeLanguage(string lang, string returnUrl)
        {

            Response.Cookies.Append("lang", lang);

            return Redirect(returnUrl);
        }


    }
}
