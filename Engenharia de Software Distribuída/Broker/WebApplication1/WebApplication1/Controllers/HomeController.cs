using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace WebApplication1.Controllers
{
    public class HomeController : Controller
    {
        public ActionResult Index()
        {
            ViewBag.Title = "Home Page";

            return View();
        }

        [HttpGet]
        public List<string> ReadMessage()
        {
            RabbitMQBroker.RabbitMQBroker.StartConsumer();
            RabbitMQBroker.RabbitMQBroker.SendMessage("teste");

            return RabbitMQBroker.RabbitMQBroker.ReadMessages();
        }
    }
}
