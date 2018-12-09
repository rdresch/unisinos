using System.Web.Mvc;

namespace WebApplication1.Controllers
{
    public class HomeController : Controller
    {
        public ActionResult Index()
        {
            ViewBag.Title = "Home Page";

            RabbitMQBroker.RabbitMQBroker.StartConsumer();

            return View();
        }

        [HttpPost]
        public void SendMessage(string message)
        {
            RabbitMQBroker.RabbitMQBroker.SendMessage(message);
        }

        [HttpGet]
        public JsonResult ReadMessages()
        {
            return Json(RabbitMQBroker.RabbitMQBroker.ReadMessages(), JsonRequestBehavior.AllowGet);
        }
    }
}
