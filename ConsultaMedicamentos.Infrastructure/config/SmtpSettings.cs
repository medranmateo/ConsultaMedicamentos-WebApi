using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ConsultaMedicamentos.Infrastructure.config
{
    public class SmtpSettings
    {
        public string Server { get; set; } = string.Empty;
        public int Port { get; set; }
        public string User { get; set; } = string.Empty;
        public string Pass { get; set; } = string.Empty;
        public string FromAddress { get; set; } = string.Empty;
    }
}
