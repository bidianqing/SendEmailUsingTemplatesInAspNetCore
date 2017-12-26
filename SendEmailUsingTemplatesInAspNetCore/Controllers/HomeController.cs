using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc;
using SendEmailUsingTemplatesInAspNetCore.Models;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Net;
using System.Net.Mail;

namespace SendEmailUsingTemplatesInAspNetCore.Controllers
{
    public class HomeController : Controller
    {
        private IHostingEnvironment _env;
        public HomeController(IHostingEnvironment env)
        {
            _env = env;
        }
        public IActionResult Index()
        {
            var pathToFile = _env.WebRootPath
                            + Path.DirectorySeparatorChar.ToString()
                            + "Templates"
                            + Path.DirectorySeparatorChar.ToString()
                            + "EmailTemplate"
                            + Path.DirectorySeparatorChar.ToString()
                            + "Register.html";
            string body;
            using (StreamReader SourceReader = System.IO.File.OpenText(pathToFile))
            {
                body = SourceReader.ReadToEnd();
            }
            Dictionary<string, string> dictionary = new Dictionary<string, string>();
            dictionary.Add("UserName", "bidianqing");
            foreach (var item in dictionary)
            {
                body = body.Replace("{" + item.Key + "}", item.Value);
            }
            SendEmail(body);
            return View();
        }

        private void SendEmail(string body)
        {
            SmtpClient client = new SmtpClient("smtp.163.com");
            client.UseDefaultCredentials = false;
            client.Credentials = new NetworkCredential("bidianqing123", "19910916bdq");

            MailMessage mailMessage = new MailMessage();
            mailMessage.From = new MailAddress("bidianqing123@163.com");
            mailMessage.To.Add("bidianqing@qq.com");
            mailMessage.IsBodyHtml = true;
            mailMessage.Body = body;
            mailMessage.Subject = "subject";
            client.Send(mailMessage);
        }

        public IActionResult About()
        {
            ViewData["Message"] = "Your application description page.";

            return View();
        }

        public IActionResult Contact()
        {
            ViewData["Message"] = "Your contact page.";

            return View();
        }

        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}
