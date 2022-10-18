using Confluent.Kafka;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Infraestructure.Kafka
{
    public static class KafkaConfig
    {
        public static ProducerConfig config = new ProducerConfig
        {
            BootstrapServers = "config.KafkaConfig.Endpoint",
            SaslUsername = "config.KafkaConfig.User",
            SaslPassword = "config.KafkaConfig.Password"
        };
    }
}
