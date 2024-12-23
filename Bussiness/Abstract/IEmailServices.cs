using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Bussiness.Abstract
{
    public interface IEmailServices
    {
        Task SendingEmail(string email, string url);
        Task ConfirmEmail(string userid, string token);
    }
}
