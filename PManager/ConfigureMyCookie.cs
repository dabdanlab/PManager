using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.Extensions.Options;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace PManager
{
    public class ConfigureMyCookie : IConfigureNamedOptions<CookieAuthenticationOptions>
    {
        public void Configure(string name, CookieAuthenticationOptions options)
        {
            if (name == Startup.CookieScheme)
            {
                
            }
        }

        public void Configure(CookieAuthenticationOptions options) => Configure(Options.DefaultName, options);
    }
}
