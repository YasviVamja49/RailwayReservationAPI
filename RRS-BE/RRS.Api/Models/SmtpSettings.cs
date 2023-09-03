using Microsoft.Extensions.Options;

namespace RRS.Api.Models
{
    public class SmtpSettings
    {
        public string server { get; set; }
        public int port { get; set; }
        public string SenderName { get; set; }

        public string SenderEmail { get; set; }

        public string Password { get; set; }




    }
}
