using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Options;
using StackExchange.Redis;
using StackExchange.Redis.Extensions.Core.Configuration;
using System;

namespace weatherapp.Configuration
{
    public class RedisConnectionFactory : IRedisConnectionFactory
    {
        private readonly Lazy<ConnectionMultiplexer> _connection;

        public RedisConnectionFactory(IConfiguration configuration, IWebHostEnvironment env)
        {
            if (env.IsEnvironment("Local"))
            {
               this._connection = new Lazy<ConnectionMultiplexer>(() => ConnectionMultiplexer.Connect("localhost:6379"));
            }
            else
            {
                this._connection = new Lazy<ConnectionMultiplexer>(() => ConnectionMultiplexer.Connect(configuration.GetValue<string>("redis:name")));
            }
        }

        public IDatabase GetDatabase()
        {
            return this._connection.Value.GetDatabase();
        }
    }
}
