using System.Text;
using api.Repositories;
using RabbitMQ.Client;
using RabbitMQ.Client.Events;

namespace api.Workers;

public class Worker : BackgroundService
{
    private readonly ILogger<Worker> _logger;
    private IMeasurementRepository _measurementRepository;
    private IConnection _connection;
    private IModel _channel;
    private readonly string _queueName;
    
    public Worker(ILogger<Worker> logger, IMeasurementRepository measurementRepository)
    {
        _logger = logger;
        _queueName = Environment.GetEnvironmentVariable("QUEUE_NAME") ?? "q";
        _measurementRepository = measurementRepository;
    }

    public override Task StartAsync(CancellationToken cancellationToken)
    {
        string host = Environment.GetEnvironmentVariable("HOST") ?? "localhost";
        int port;
        if (!int.TryParse(Environment.GetEnvironmentVariable("PORT"), out port))
        {
            port = 56272;
        }
        
        var factory = new ConnectionFactory
        {
            HostName = host, 
            Port = port,
            DispatchConsumersAsync = true
        };
        _connection = factory.CreateConnection();
        _channel = _connection.CreateModel();
        _channel.QueueDeclare(_queueName, false, false, false);
        
        return base.StartAsync(cancellationToken);
    }

    protected override Task ExecuteAsync(CancellationToken stoppingToken)
    {
        var consumer = new AsyncEventingBasicConsumer(_channel);
        consumer.Received +=  async (model, ea) =>
        {
            var body = ea.Body.ToArray();
            var message = Encoding.UTF8.GetString(body);
            _logger.LogInformation(" [x] Received {0}", message);
            _measurementRepository.Add(message);
        };
        
        _channel.BasicConsume(queue: _queueName,
            autoAck: true,
            consumer: consumer); 
        return Task.CompletedTask;
    }
    
    public override async Task StopAsync(CancellationToken cancellationToken)
    {
        await base.StopAsync(cancellationToken);
        _connection.Close();
        _logger.LogInformation("RabbitMQ connection is closed.");
    }
}