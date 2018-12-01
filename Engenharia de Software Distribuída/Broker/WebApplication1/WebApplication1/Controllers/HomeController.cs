using System.Collections.Generic;
using System.Web.Mvc;

namespace WebApplication1.Controllers
{
    public class HomeController : Controller
    {
        public ActionResult Index()
        {
            ViewBag.Title = "Home Page";

            RabbitMQBroker.RabbitMQBroker.StartConsumer();
            RabbitMQBroker.RabbitMQBroker.SendMessage("teste");

            return View();
        }

        [HttpGet]
        public string[] ReadMessage()
        {
            return RabbitMQBroker.RabbitMQBroker.ReadMessages().ToArray();
        }
    }
}
