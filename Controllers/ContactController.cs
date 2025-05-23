using Microsoft.AspNetCore.Mvc;
using MyWebsite.Models;
using System.Net;
using System.Net.Mail;


namespace MyWebsite.Controllers
{

    [ApiController]
    [Route("api/contact")]

    public class ContactController : Controller
    {
        private readonly IConfiguration _config;

        public ContactController(IConfiguration config)
        {
            _config = config;
        }



        [HttpPost]
        public IActionResult sendEmail([FromBody] ContactFormModel emailForm)
        {
            try
            {
                var fromEmail = _config["EmailSettings:SenderEmail"];
                var myEmail = _config["EmailSettings:MyEmail"];
         
                var apiKey = _config["MailjetApiKey"];
                var apiSecret = _config["MailjetApiSecret"];


                var mensaje = new MailMessage();
                mensaje.From = new MailAddress(fromEmail);

                mensaje.To.Add(myEmail);
                mensaje.Subject = "Mensaje desde portfolio";

                mensaje.Body = $"Nombre: {emailForm.Name}\nEmail: {emailForm.Email}\n\nMensaje: {emailForm.Message}";


                var cliente = new SmtpClient("in-v3.mailjet.com", 587);



                cliente.Credentials = new NetworkCredential(apiKey, apiSecret);



                cliente.Send(mensaje);

                return Ok("correo enviado correctamente");

            }catch (Exception ex)
            {
                return StatusCode(500, "Error al enviar el correo.");
            }
            
        }



    }
}
