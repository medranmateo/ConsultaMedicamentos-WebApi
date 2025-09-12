using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ConsultaMedicamentos.Domain.IServices
{
    public interface IEmailService
    {
        public Task<bool> SendEmailAsync(IEnumerable<string> to, string subject, string body);
    }
}
