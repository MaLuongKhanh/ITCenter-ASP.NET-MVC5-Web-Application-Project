using System.Web.Mvc;
using WebApplication.Areas.admin.Attributes;

namespace WebApplication.Areas.admin.Controllers
{
    [AdminAuthorize]
    public abstract class AdminBaseController : Controller
    {
        // Common functionality for admin controllers can go here
    }
} 