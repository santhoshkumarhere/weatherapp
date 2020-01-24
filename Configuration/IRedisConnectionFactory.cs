using StackExchange.Redis;

namespace weatherapp.Configuration
{
    public interface IRedisConnectionFactory
    {
        IDatabase GetDatabase();
    }
}
