using Accura_Innovatives.Models;
using Microsoft.AspNetCore.Mvc;
using System;
using System.IO;
using System.Net;
using System.Net.Mail;

namespace Accura_Innovatives.Controllers
{
    public class EmailPaySlip : Controller
    {
        private readonly EmployeeMaster1Context _context;
        private readonly IWebHostEnvironment hostingenvironment;
        public EmailPaySlip(EmployeeMaster1Context context, IWebHostEnvironment hc)
        {
            _context = context;
            hostingenvironment = hc;
        }
        public IActionResult Index()
        {
            return View();
        }
        public void SendEmail()
        {
            List<EmployeeMasterData1> ed = _context.EmployeeMasterData1s.Where(x => x.OffEmail != null).ToList();
            // Sender's email address and password
            

            // Receiver's email address
            foreach (var item in ed)
            {
                string senderEmail = "yourEmail@gmail.com";
                string senderPassword = "yourPassword";
                
                    string receiverEmail = item.OffEmail;

                //string fileName = emp.EmpCode + "_" + formattedMonthYear + "_Payslip" + ".pdf";
                //string filePath = Path.Combine(folderPath, fileName);

                // Path to the payslip PDF file
                string payslipFilePath = @"C:\Pdfs\"; 

                // Check if the payslip file exists
                if (System.IO.File.Exists(payslipFilePath))
                {
                    // Configure the SMTP client
                    SmtpClient client = new SmtpClient("smtp.gmail.com", 587);
                    client.EnableSsl = true;
                    client.UseDefaultCredentials = false;
                    client.Credentials = new NetworkCredential(senderEmail, senderPassword);

                    // Create the email message
                    MailMessage message = new MailMessage(senderEmail, receiverEmail);
                    message.Subject = "Monthly Payslip";
                    message.Body = "Please find your monthly payslip attached.";

                    // Attach the payslip PDF to the email
                    message.Attachments.Add(new Attachment(payslipFilePath));

                    try
                    {
                        // Send the email
                        client.Send(message);
                        Console.WriteLine("Email sent successfully!");
                    }
                    catch (Exception ex)
                    {
                        Console.WriteLine("Failed to send email: " + ex.Message);
                    }
                }
                else
                {
                    Console.WriteLine("Payslip file not found.");
                }
            }
        }
    }
}
