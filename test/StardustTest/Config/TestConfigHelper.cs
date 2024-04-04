using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xunit.Abstractions;

namespace StardustTest.Config
{
    public static class TestConfigHelper
    {
        public static IConfigurationRoot GetIConfigurationRoot()
        {
            return new ConfigurationBuilder()
                .AddJsonFile("appsettings.json", optional: true, reloadOnChange: true)
                .AddJsonFile("appsettings.local.json", optional: true, reloadOnChange: true)
                .AddEnvironmentVariables()
                .Build();
        }

    }
}
