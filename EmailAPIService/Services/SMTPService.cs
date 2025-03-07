using System.Net;
using System.Net.Mail;

namespace EmailAPIService.Services
{
    public class SMTPService
    {
        private SmtpClient _client;
        private EmailAPIDBContext _context;
        public SMTPService(EmailAPIDBContext context)
        {
            //DbContext for Templates table
            _context = context;
            _client = new SmtpClient("smtp.gmail.com", 587)
            {
                Credentials = new NetworkCredential("email", "password"),
                EnableSsl = true
            };
        }
        // SendEmail method
        public async Task SendEmail(string recipient, int templateId, string placeholders)
        {
            var template = _context.Templates.FirstOrDefault(t => t.Id == templateId);
            if (template == null)
            {
                throw new Exception("Template not found");
            }
            MailMessage mail = new MailMessage("email", recipient)
            {
                Subject = template.Subject,
                Body = template.Body + placeholders
            };

            try
            {
                await _client.SendMailAsync(mail);
            }
            catch (Exception ex)
            {
                throw new Exception("Error sending email", ex);
            }
        }
    }
}
