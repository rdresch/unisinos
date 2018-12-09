using System.Web.Mvc;

namespace WebApplication1.Controllers
{
    public class HomeController : Controller
    {
        public ActionResult Index()
        {
            ViewBag.Title = "Home Page";

            RabbitMQBroker.RabbitMQBrokerClient.StartConsumer();
            RabbitMQBroker.RabbitMQBrokerServer.StartConsumer();

            return View();
        }

        [HttpPost]
        public void SendMessageToServer(string message)
        {
            RabbitMQBroker.RabbitMQBrokerClient.SendMessageToServer(message);
        }

        [HttpGet]
        public JsonResult ReadClientMessages()
        {
            return Json(RabbitMQBroker.RabbitMQBrokerClient.ReadMessages(), JsonRequestBehavior.AllowGet);
        }

        [HttpGet]
        public JsonResult ReadServerMessages()
        {
            return Json(RabbitMQBroker.RabbitMQBrokerServer.ReadMessages(), JsonRequestBehavior.AllowGet);
        }
    }
}
