using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;

namespace Whoops.Services
{
    public class MockContactSender : IContactSender
    {
        public void Send(string to,string from,string email,string message)
        {
            Debug.WriteLine($"to: {to}, from: {from}, email: {email}, message: {message}");
        }
    }
}
