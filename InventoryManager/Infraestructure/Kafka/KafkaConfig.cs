using Confluent.Kafka;
using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Infraestructure.Kafka
{
    public static class KafkaConfig
    {
        private static readonly IConfiguration _configuration;

        public static ProducerConfig config = new ProducerConfig
        {
            BootstrapServers = _configuration["KafkaConfig.Endpoint"],
            SaslUsername = _configuration["KafkaConfig.User"],
            SaslPassword = _configuration["KafkaConfig.Password"],
            SecurityProtocol = SecurityProtocol.SaslSsl,
            SaslMechanism = SaslMechanism.ScramSha512,
            SslCaLocation = _configuration["KafkaConfig.Location"],
            SocketTimeoutMs = int.Parse(_configuration["KafkaConfig.Timeout"]),
            Partitioner = Partitioner.Random
        };
    }
}
