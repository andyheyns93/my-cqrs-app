using CarCatalog.Core.Configuration;
using CarCatalog.Core.Interfaces.Messaging.RabbitMq;
using RabbitMQ.Client;
using RabbitMQ.Client.Events;
using Serilog;
using System;
using System.IO;

namespace CarCatalog.Infrastructure.Messaging.RabbitMq
{
    public class RabbitMqClient : IRabbitMqClient<IModel>
    {
        private readonly object sync_root = new object();
        private readonly ILogger _logger;
        private readonly RabbitMqConfiguration _rabbitMqConfiguration;

        private IConnection _connection;
        private bool _disposed;

        public RabbitMqClient(ILogger logger, RabbitMqConfiguration rabbitMqConfiguration)
        {
            _logger = logger;
            _rabbitMqConfiguration = rabbitMqConfiguration;
        }

        public IModel CreateModel()
        {
            if (!IsConnected)
                TryConnect();

            if (!IsConnected)
            {
                _logger.Fatal("No RabbitMQ connections are available to perform this action");
                throw new InvalidOperationException("No RabbitMQ connections are available to perform this action");
            }
            return _connection.CreateModel();
        }

        public bool IsConnected => _connection != null && _connection.IsOpen && !_disposed;

        public bool TryConnect()
        {
            _logger.Information("RabbitMQ Client is trying to connect");
            lock (sync_root)
            {
                _connection = GetConnection();
                if (IsConnected)
                {
                    _connection.ConnectionShutdown += OnConnectionShutdown;
                    _connection.CallbackException += OnCallbackException;
                    _connection.ConnectionBlocked += OnConnectionBlocked;
                    _logger.Information($"RabbitMQ persistent connection acquired a connection {_connection.Endpoint.HostName} and is subscribed to failure events");
                    return true;
                }
                else
                {
                    _logger.Fatal("RabbitMQ connections could not be created and opened");
                    return false;
                }
            }
        }

        public void Dispose()
        {
            if (_disposed) return;
            _disposed = true;
            try
            {
                _connection.Dispose();
            }
            catch (IOException ex)
            {
                _logger.Fatal(ex, ex.Message);
            }
        }

        private IConnection GetConnection()
        {
            if (_connection == null)
                _connection = GetInstance();
            return _connection;
        }

        private IConnection GetInstance() {
            try
            {
                var factory = new ConnectionFactory
                {
                    HostName = _rabbitMqConfiguration.Hostname,
                    Port = _rabbitMqConfiguration.Port,
                    UserName = _rabbitMqConfiguration.UserName,
                    Password = _rabbitMqConfiguration.Password
                };
                return factory.CreateConnection();
            } catch(Exception ex)
            {
                _logger.Fatal(ex, ex.Message);
                return null;
            }
        }

        private void OnCallbackException(object sender, CallbackExceptionEventArgs e)
        {
            if (_disposed) return;
            _logger.Warning("A RabbitMQ connection throw exception. Trying to re-connect...");
            TryConnect();
        }

        private void OnConnectionBlocked(object sender, ConnectionBlockedEventArgs e)
        {
            if (_disposed) return;
            _logger.Warning("A RabbitMQ connection is shutdown. Trying to re-connect...");
            TryConnect();
        }
        private void OnConnectionShutdown(object sender, ShutdownEventArgs reason)
        {
            if (_disposed) return;
            _logger.Warning("A RabbitMQ connection is on shutdown. Trying to re-connect...");
            TryConnect();
        }
    }
}
