using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using StackExchange.Redis;


namespace Demo.Helpers {

  public sealed class RedisSingleton {
    private static readonly Lazy<RedisSingleton> lazy =
        new Lazy<RedisSingleton>(() => new RedisSingleton());

    private ConnectionMultiplexer _muxer;

    public ConnectionMultiplexer GetMuxer() {
      return _muxer;
    }

    public static RedisSingleton Instance { get { return lazy.Value; } }

    private RedisSingleton() {
      string redisConfigString = ",connectTimeout=10000,syncTimeout= 60000";
      string redisHost = Config.DataBaseConfig.RedisIP + ":" + Config.DataBaseConfig.RedisPort;
      _muxer = ConnectionMultiplexer.Connect(redisHost + redisConfigString);
    }



  }
}
