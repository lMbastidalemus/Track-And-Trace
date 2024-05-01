using Microsoft.AspNetCore.Mvc;
using System.Net;
using System.Net.Mail;
using System.Text;

namespace PL.Controllers
{
    public class PaqueteAdministrador : Controller
    {
        [HttpGet]
        public IActionResult GetAllPaquete()
        {
            using (HttpClient client = new HttpClient())
            {
                client.BaseAddress = new Uri("https://localhost:7283/api/");
                var responseTask = client.GetAsync("PaqueteCrud/GetAll");

                responseTask.Wait();

                var result = responseTask.Result;

                if (result.IsSuccessStatusCode)
                {
                    var readTask = result.Content.ReadAsAsync<List<ML.Paquete>>(); 
                    readTask.Wait();

                    var data = readTask.Result.ToList();
                    if (data != null)
                    {
                        return View(data);
                    }
                    else
                    {
                        RedirectToAction("Index");
                    }
                }
                else
                {
                    RedirectToAction("Index");
                }
            }
            return View();
        }

        [HttpPost]
        public IActionResult EnviarTablaPorCorreo()
        {
            try
            {
                var paquetes = BL.Paquete.GetAll();

                var tableHtml = new StringBuilder();
                tableHtml.Append("<table>");
                tableHtml.Append("<tr>");
                tableHtml.Append("<th>#</th>");
                tableHtml.Append("<th>Nombre</th>");
                tableHtml.Append("<th>Instruccion Entrega</th>");
                tableHtml.Append("<th>Peso</th>");
                tableHtml.Append("<th>Direccion de Origen</th>");
                tableHtml.Append("<th>Direccion de Entrega</th>");
                tableHtml.Append("<th>Fecha Estima de Entrega</th>");
                tableHtml.Append("<th>Numero Guia</th>");
                tableHtml.Append("</tr>");

                foreach (var paquete in paquetes.Item3.Paquetes)
                {
                    tableHtml.Append("<tr>");
                    tableHtml.Append($"<td>{paquete.IdPaquete}</td>");
                    tableHtml.Append($"<td>{paquete.InstruccionEntrega}</td>");
                    tableHtml.Append($"<td>{paquete.Peso}</td>");
                    tableHtml.Append($"<td>{paquete.DireccionOrigen}</td>");
                    tableHtml.Append($"<td>{paquete.DireccionEntrega}</td>");
                    tableHtml.Append($"<td>{paquete.FechaEstimadaEntrega}</td>");
                    tableHtml.Append($"<td>{paquete.NumeroGuia}</td>");
                    tableHtml.Append("</tr>");
                }
                tableHtml.Append("</table>");

                using (SmtpClient smtpClient = new SmtpClient("smtp.gmail.com"))
                {
                    smtpClient.Port = 587;
                    smtpClient.UseDefaultCredentials = false;
                    smtpClient.EnableSsl = true;
                    smtpClient.Credentials = new NetworkCredential("oa400449@uaeh.edu.mx", "dfnuttgwcgyfzcnz");

                    using (MailMessage mailMessage = new MailMessage())
                    {
                        mailMessage.From = new MailAddress("oa400449@uaeh.edu.mx");
                        mailMessage.To.Add("oa400449@uaeh.edu.mx"); // Dirección de correo a la que se enviará el correo
                        mailMessage.Subject = "Tabla de Paquetes";
                        mailMessage.Body = "Hola, adjunto la actualizacion de los paquetes.";
                        mailMessage.IsBodyHtml = true;

                        mailMessage.Attachments.Add(new Attachment(new MemoryStream(Encoding.UTF8.GetBytes(tableHtml.ToString())), "tabla_paquetes.html", "text/html"));

                        smtpClient.Send(mailMessage);
                    }
                }

                return RedirectToAction("GetAllPaquete");
            }
            catch (Exception ex)
            {
                return RedirectToAction("Index");
            }
        }

        //[HttpPost]
        //public IActionResult EnviarTablaPorCorreo()
        //{
        //    try
        //    {
        //        var paquete = BL.Paquete.GetAll();

        //        var paqueteList = paquete.Item3.Paquetes;

        //        if (paqueteList != null)
        //        {
        //            // Ruta al archivo HTML de la plantilla
        //            string filePath = Path.Combine(Directory.GetCurrentDirectory(), "Template", "tabla_paquetes.html");

        //            // Cargar el contenido del archivo HTML en una cadena
        //            string htmlContent = System.IO.File.ReadAllText(filePath);

        //            // Reemplazar los valores de los paquetes en el HTML
        //            htmlContent = ReplacePaquetesData(htmlContent, paqueteList);

        //            using (SmtpClient smtpClient = new SmtpClient("smtp.gmail.com"))
        //            {
        //                smtpClient.Port = 587;
        //                smtpClient.UseDefaultCredentials = false;
        //                smtpClient.EnableSsl = true;
        //                smtpClient.Credentials = new NetworkCredential("oa400449@uaeh.edu.mx", "dfnuttgwcgyfzcnz");

        //                using (MailMessage mailMessage = new MailMessage())
        //                {
        //                    mailMessage.From = new MailAddress("oa400449@uaeh.edu.mx");
        //                    mailMessage.To.Add("oa400449@uaeh.edu.mx");
        //                    mailMessage.Subject = "Tabla de Paquetes";
        //                    mailMessage.Body = "Hola, adjunto la actualización de los paquetes.";
        //                    mailMessage.IsBodyHtml = true;

        //                    // Adjuntar el HTML de la tabla al correo electrónico
        //                    mailMessage.Attachments.Add(new Attachment(new MemoryStream(Encoding.UTF8.GetBytes(htmlContent)), "tabla_paquetes.html", "text/html"));

        //                    smtpClient.Send(mailMessage);
        //                }
        //            }

        //            return RedirectToAction("GetAllPaquete");
        //        }
        //        else
        //        {
        //            return RedirectToAction("Index");
        //        }
        //    }
        //    catch (Exception ex)
        //    {

        //        return RedirectToAction("Index");
        //    }
        //}


        //// Método para reemplazar los valores de los paquetes en el HTML
        //private string ReplacePaquetesData(string htmlContent, List<ML.Paquete>? paquetes)
        //{
        //    StringBuilder tableHtml = new StringBuilder();
        //    foreach (var paquete in paquetes)
        //    {
        //        tableHtml.Append($"<tr>");
        //        tableHtml.Append($"<td>{paquete.IdPaquete}</td>");
        //        tableHtml.Append($"<td>{paquete.InstruccionEntrega}</td>");
        //        tableHtml.Append($"<td>{paquete.Peso}</td>");
        //        tableHtml.Append($"<td>{paquete.DireccionOrigen}</td>");
        //        tableHtml.Append($"<td>{paquete.DireccionEntrega}</td>");
        //        tableHtml.Append($"<td>{paquete.FechaEstimadaEntrega}</td>");
        //        tableHtml.Append($"<td>{paquete.NumeroGuia}</td>");
        //        tableHtml.Append($"</tr>");
        //    }


        //    return htmlContent.Replace("<!-- Aquí irán los datos de la tabla -->", $"<tbody>{tableHtml.ToString()}</tbody>");

        //}

    }
}
//https://localhost:7283/api/PaqueteCrud/GetAll