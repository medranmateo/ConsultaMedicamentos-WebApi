using ConsultaMedicamentos.Domain.IServices;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using MailKit.Net.Smtp;
using MimeKit;
using System.Threading.Tasks;

namespace ConsultaMedicamentos.Application.Services
{
    public class EmailService : IEmailService
    {
        private readonly string _smtpServer;
        private readonly int _smtpPort;
        private readonly string _smtpUser;
        private readonly string _smtpPass;
        private readonly string _fromAddress;

        public EmailService(string smtpServer, int smtpPort, string smtpUser, string smtpPass, string fromAddress)
        {
            _smtpServer = smtpServer;
            _smtpPort = smtpPort;
            _smtpUser = smtpUser;
            _smtpPass = smtpPass;
            _fromAddress = fromAddress;
        }

        public async Task<bool> SendEmailAsync(IEnumerable<string> toList, string subject, string body)
        {
            var email = new MimeMessage();
            email.From.Add(new MailboxAddress("Consulta Medicamentos", _fromAddress));

            // Agregar varios destinatarios
            foreach (var to in toList)
            {
                email.To.Add(new MailboxAddress("", to));
            }

            email.Subject = subject;

            email.Body = new TextPart("html")
            {
                Text = body // admite HTML
            };

            using var smtp = new SmtpClient();
            await smtp.ConnectAsync(_smtpServer, _smtpPort, MailKit.Security.SecureSocketOptions.StartTls);
            await smtp.AuthenticateAsync(_smtpUser, _smtpPass);
            await smtp.SendAsync(email);
            await smtp.DisconnectAsync(true);
            return true;
        }
    }
}
