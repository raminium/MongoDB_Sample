using Microsoft.Extensions.Options;
using MongoDB.Driver;
using MongoDB_Sample.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MongoDB_Sample.Services
{
    public class LogsService
    {
        private readonly IMongoCollection<Log> _LogsCollection;

        public LogsService(
            IOptions<List<Models.MongoDatabaseSettings>> apiLogDatabaseSettings)
        {
            var mongoClient = new MongoClient(
                apiLogDatabaseSettings.Value.Where(d=>d.DatabaseName== "APILog").FirstOrDefault().ConnectionString);

            var mongoDatabase = mongoClient.GetDatabase(
                apiLogDatabaseSettings.Value.Where(d => d.DatabaseName == "APILog").FirstOrDefault().DatabaseName);

            _LogsCollection = mongoDatabase.GetCollection<Log>(
                apiLogDatabaseSettings.Value.Where(d => d.DatabaseName == "APILog").FirstOrDefault().CollectionName);
        }

        public async Task<List<Log>> GetAsync() =>
            await _LogsCollection.Find(_ => true).ToListAsync();

        public async Task<Log?> GetAsync(string id) =>
            await _LogsCollection.Find(x => x.Id == id).FirstOrDefaultAsync();

        public async Task CreateAsync(Log newLog) =>
            await _LogsCollection.InsertOneAsync(newLog);

        public async Task CreateManyAsync(List<Log> newLogs) =>
            await _LogsCollection.InsertManyAsync(newLogs);

        public async Task CreateBulkAsync(List<Log> newLogs) =>
            await _LogsCollection.BulkWriteAsync(newLogs.Select(l=>new InsertOneModel<Log>(l)));

        public async Task UpdateAsync(string id, Log updatedLog) =>
            await _LogsCollection.ReplaceOneAsync(x => x.Id == id, updatedLog);

        public async Task RemoveAsync(string id) =>
            await _LogsCollection.DeleteOneAsync(x => x.Id == id);

        public async Task<List<Log>> GetFromDateAsync(DateTime date) =>
            await _LogsCollection.Find(x => x.RequestDate >= date).SortByDescending(x=>x.RT).ToListAsync();

    }
}

