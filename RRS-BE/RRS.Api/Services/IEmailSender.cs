using RRS.Api.ViewModels;

namespace RRS.Api.Services
{
    public interface IEmailSender
    {
        Task<string> SendEmailAsync(string recipientEmail,TicketDTOoutput ticket);
        Task<string> SendCancellationEmailAsync(string recipientEmail, TicketDTOoutput ticketno);
    }
}
