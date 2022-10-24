using Microsoft.AspNetCore.Mvc;

namespace Application.Controllers.Base
{
    public abstract class BaseController : Controller
    {
        public string GetLoggedUsername() => User.Identity!.Name!;
    }
}