using System.Linq;
using System.Web.Mvc;
using WebApplication.Models;

namespace WebApplication.Controllers
{
    public class LandingPageController : Controller
    {
        private ITCenterEntities1 db = new ITCenterEntities1();

        public ActionResult Index()
        {
            var landingContent = db.LandingContents.FirstOrDefault();
            if (landingContent == null)
            {
                landingContent = new LandingContent
                {
                    HeaderTitle = "Welcome to IT-Center",
                    HeaderSubtitle = "Trung tâm hỗ trợ kỹ thuật",
                    AboutTitle = "Về chúng tôi",
                    AboutContent = "Nội dung mặc định về trung tâm"
                };
            }
            return View(landingContent);
        }
    }
}