namespace Z.RabbitMQ;

public class RabbitMQOptions
{
    public string Connection { get; init; }

    public int Port { get; init; }

    public bool Enable { get; init; }

    public string UserName { get; init; }
    
    public string Password { get; init; }
    
    public int RetryCount { get; init; }
}
