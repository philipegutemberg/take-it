using Microsoft.AspNetCore.Mvc;

namespace Application.Controllers.Base
{
    public abstract class BaseController : Controller
    {
        protected string GetLoggedUsername() => User.Identity!.Name!;
    }
}