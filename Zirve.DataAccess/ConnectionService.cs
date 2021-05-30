using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Extensions.Configuration;

namespace Zirve.DataAccess
{
    public class ConnectionServices
    {
        public static string connstring = "";
        public static string Set(IConfiguration config)
        {
            connstring = config.GetConnectionString("ConStr");
            return connstring;
        }
    }
}
