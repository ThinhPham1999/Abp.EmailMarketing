using System;
using Microsoft.AspNetCore.Builder;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using Volo.Abp;

namespace Abp.EmailMarketing
{
    public class EmailMarketingWebTestStartup
    {
        public void ConfigureServices(IServiceCollection services)
        {
            services.AddApplication<EmailMarketingWebTestModule>();
        }

        public void Configure(IApplicationBuilder app, ILoggerFactory loggerFactory)
        {
            app.InitializeApplication();
        }
    }
}