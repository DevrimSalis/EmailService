using System;
using System.Threading.Tasks;
using EmailService.Infrastructure.Settings;
using Microsoft.Extensions.Options;
using Newtonsoft.Json;
using StackExchange.Redis;

namespace EmailService.Infrastructure.Cache
{
    public class RedisCache : ICache
    {
        private readonly IDatabase _database;

        public RedisCache(IDatabase database)
        {
            _database = database;
        }

        public RedisCache(IOptions<AppSettings> appSettings)
        {
            var settings = appSettings.Value;
            var redis = ConnectionMultiplexer.Connect(settings.RedisConnectionString);
            _database = redis.GetDatabase();
        }

        public async Task<T> GetAsync<T>(string key)
        {
            var value = await _database.StringGetAsync(key);
            if (value.IsNull)
            {
                return default(T);
            }

            return JsonConvert.DeserializeObject<T>(value);
        }

        public async Task SetAsync<T>(string key, T value, TimeSpan expiration)
        {
            var jsonValue = JsonConvert.SerializeObject(value);
            await _database.StringSetAsync(key, jsonValue, expiration);
        }
    }
}
