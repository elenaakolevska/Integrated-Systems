using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Courses.Domain.Email;

namespace Courses.Service.Interface
{
    public interface IEmailService
    {
        Task SendEmailAsync(EmailMessage message);

    }
}
