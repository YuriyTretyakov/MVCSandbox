using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Whoops.Services
{
    public interface IContactSender
    {
        void Send(string to, string from, string email, string message);
    }
}
