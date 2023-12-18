using System;
using System.Threading;
using System.Threading.Tasks;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Hosting;


namespace Angel.Service
{
    public class StartApplicationService : IHostedService
    {
        private readonly IConfiguration _configuration;
        private readonly IHostApplicationLifetime _hostApplicationLifetime;
        private readonly IApplicationSettingService applicationSettings;


        public StartApplicationService(IConfiguration configuration, IHostApplicationLifetime hostApplicationLifetime, IApplicationSettingService _applicationSettings)
        {
            _configuration = configuration;
            _hostApplicationLifetime = hostApplicationLifetime;
            applicationSettings = _applicationSettings;
        }

        public async Task StartAsync(CancellationToken cancellationToken)
        {
            await DoWork();
        }

        public async Task DoWork()
        {
            applicationSettings.SetSettingValue("key", Angel.Utils.XmlHelper.GetXmlCaches());
        }

        public Task StopAsync(CancellationToken cancellationToken)
        {
            return Task.CompletedTask;
        }
    }
}