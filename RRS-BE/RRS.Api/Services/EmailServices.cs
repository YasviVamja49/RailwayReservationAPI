using Microsoft.AspNetCore.Identity.UI.Services;
using System.Net;
using System.Net.Mail;
using System.Threading.Tasks;
using MailKit.Net.Smtp;
using Microsoft.Extensions.Options;
using MailKit;
using RRS.Api.Models;
using System;
using SmtpClient = MailKit.Net.Smtp.SmtpClient;
using MimeKit;
using RRS.Api.ViewModels;

namespace RRS.Api.Services
{
    
        public class EmailSenderService : IEmailSender
        {

            private readonly SmtpSettings _smtpSettings;
            public EmailSenderService(IOptions<SmtpSettings> smtpSettings)
            {
                _smtpSettings = smtpSettings.Value;

            }

            public async Task<string> SendEmailAsync(string recipientEmail,TicketDTOoutput ticket)
            {
                var message = new MimeMessage();
                message.From.Add(MailboxAddress.Parse(_smtpSettings.SenderEmail));
                message.To.Add(MailboxAddress.Parse(recipientEmail));
                message.Subject = "Your booking is confirmed";
                message.Body = new TextPart("plain")
                {
                    Text = $"Dear Traveller,\n\nYour booking is confirmed.\n\nDetails:\nTicket no: {ticket.Ticketno}\nBerth Number: {ticket.Berthno}\nCoach Number: {ticket.Coachno}\nDeparture Date: {ticket.Arrivaldate}\nBooking Date: {ticket.Bookingdate}\nBooking Status: {ticket.Bookingstatus}\nClass: {ticket.Ticketclass}\nPassenger Name: {ticket.PassengerName}\nTrain Number: {ticket.TrainNo}"
                };

                var client = new SmtpClient();
                try
                {
                    await client.ConnectAsync(_smtpSettings.server, _smtpSettings.port, true);
                    await client.AuthenticateAsync(new NetworkCredential(_smtpSettings.SenderEmail, _smtpSettings.Password));
                    await client.SendAsync(message);
                    await client.DisconnectAsync(true);
                    return "Email Sent Successfully";


                }
                catch (Exception ex)
                {
                    throw ex;
                }
                finally
                {
                    client.Dispose();
                }

            }

        public async Task<string> SendCancellationEmailAsync(string recipientEmail, TicketDTOoutput ticket)
        {
            var message = new MimeMessage();
            message.From.Add(MailboxAddress.Parse(_smtpSettings.SenderEmail));
            message.To.Add(MailboxAddress.Parse(recipientEmail));
            message.Subject = "Cancellation of Your Booking";
            message.Body = new TextPart("plain")
            {
                Text = $"Dear Traveller,\n\nYour booking has been canceled.\n\nDetails:\nTicket no: {ticket.Ticketno}\nBerth Number: {ticket.Berthno}\nCoach Number: {ticket.Coachno}\nDeparture Date: {ticket.Arrivaldate}\nBooking Date: {ticket.Bookingdate}\nBooking Status: {ticket.Bookingstatus}\nClass: {ticket.Ticketclass}\nPassenger Name: {ticket.PassengerName}\nTrain Number: {ticket.TrainNo}"
            };

            var client = new SmtpClient();
            try
            {
                await client.ConnectAsync(_smtpSettings.server, _smtpSettings.port, true);
                await client.AuthenticateAsync(new NetworkCredential(_smtpSettings.SenderEmail, _smtpSettings.Password));
                await client.SendAsync(message);
                await client.DisconnectAsync(true);
                return "Cancellation Email Sent Successfully";
            }
            catch (Exception ex)
            {
                throw ex;
            }
            finally
            {
                client.Dispose();
            }
        }

    }
}
    

