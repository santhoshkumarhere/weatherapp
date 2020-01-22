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
        /// <summary>
        ///     The _connection.
        /// </summary>
        private readonly Lazy<ConnectionMultiplexer> _connection;


        private readonly IOptions<RedisConfiguration> redis;

        public RedisConnectionFactory(IOptions<RedisConfiguration> redis, IConfiguration configuration, IWebHostEnvironment env)
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

        public ConnectionMultiplexer Connection()
        {
            return this._connection.Value;
        }
    }
}
