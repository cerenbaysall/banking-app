using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace BankingApp.AccountAPI
{
    public interface IJwtAuthenticationManager
    {
        string Authenticate(string userName, string password);
    }
}
