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
        public async Task<AppFeatures> Get(string id) => 
            (await _appFeatures.FindAsync(f => f.Id == id)).FirstOrDefault();
        

        public async Task<AppFeatures> GetAll()
        {
            try
            {                
                var appFeatures = (await _appFeatures.FindAsync(f => true)).FirstOrDefault();


                if (appFeatures == null)
                {
                    //TODO: Log first record creation
                    await Create(_appFeaturesOptions);
                    appFeatures = (await _appFeatures.FindAsync(f => true)).FirstOrDefault();
                    return _appFeaturesOptions;
                }
                return appFeatures;
            }
            catch (TimeoutException ex)
            {
                //TODO: Log database timeout
                return _appFeaturesOptions;
            }
        }

        public async Task Remove(AppFeatures features) => 
            await _appFeatures.DeleteOneAsync(f => f.Id == features.Id);

        public async Task Remove(string id) => await 
            _appFeatures.DeleteOneAsync(f => f.Id == id);

        public async Task Create(AppFeatures features) => 
            await _appFeatures.InsertOneAsync(features);

        public Task Update(string id, AppFeatures features) =>
            _appFeatures.ReplaceOneAsync(f => f.Id == id, features);
    }

    public interface IAppFeaturesService
    {
        Task<AppFeatures> Get(string id);
        Task<AppFeatures> GetAll();
        Task Remove(AppFeatures features);
        Task Remove(string id);
        Task Create(AppFeatures features);
        Task Update(string id, AppFeatures features);
    }
}
