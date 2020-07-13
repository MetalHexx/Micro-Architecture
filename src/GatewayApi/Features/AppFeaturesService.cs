using GatewayApi.Features.Models;
using Microsoft.Extensions.Options;
using MongoDB.Driver;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace GatewayApi.Features
{
    public class AppFeaturesService: IAppFeaturesService
    {
        private readonly IMongoCollection<AppFeatures> _appFeatures;
        private readonly AppFeaturesOptions _appFeaturesOptions;

        public AppFeaturesService(IOptions<AppFeaturesDatabaseSettings> settings, IOptions<AppFeaturesOptions> appFeaturesOptions)
        {
            _appFeaturesOptions = appFeaturesOptions.Value;
            var clientSettings = new MongoClientSettings()
            {
                Server = new MongoServerAddress(settings.Value.Host, settings.Value.Port),
                ServerSelectionTimeout = TimeSpan.FromMilliseconds(settings.Value.ConnectionTimeoutMs)
            };
            var client = new MongoClient(clientSettings);
            var database = client.GetDatabase(settings.Value.DatabaseName);
            _appFeatures = database.GetCollection<AppFeatures>(settings.Value.CollectionName);            
        }

        public async Task<AppFeatures> Get()
        {
            try
            {                
                var appFeatures = (await _appFeatures.FindAsync(settings => true)).FirstOrDefault();
                await Remove(appFeatures);
                appFeatures = null;


                if (appFeatures == null)
                {
                    //TODO: Log first record creation
                    return await Create(_appFeaturesOptions);
                }
                return appFeatures;
            }
            catch (TimeoutException ex)
            {
                //TODO: Log database timeout
                return _appFeaturesOptions;
            }
        }

        public async Task Remove(AppFeatures features)
        {
            await _appFeatures.DeleteOneAsync(f => f.Id == features.Id);
        }

        public async Task<AppFeatures> Create(AppFeatures features)
        {
            await _appFeatures.InsertOneAsync(features);
            return features;
        }
    }

    public interface IAppFeaturesService
    {
        Task<AppFeatures> Get();
        Task Remove(AppFeatures features);
    }
}
