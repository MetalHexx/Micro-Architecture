using GatewayApi.Features;
using GatewayApi.Features.Models;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Options;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace GatewayApi
{
    public partial class Startup
    {
        public void ConfigureAppFeatures(IServiceCollection services)
        {
            services.Configure<AppFeaturesOptions>(Configuration.GetSection(AppFeaturesOptions.SectionName));
            services.Configure<AppFeaturesDatabaseSettings>(Configuration.GetSection(nameof(AppFeaturesDatabaseSettings)));
            services.AddSingleton<IAppFeaturesService, AppFeaturesService>();
        }
    }
}
