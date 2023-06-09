namespace ArchPM.NetCore.Tests.TestModels;

public class SimpleClassWithStruct
{
    public SimpleStruct SimpleScruct { get; set; }
}

public struct SimpleStruct
{
    public string QueueName { get; set; }

    public string RoutingKey { get; set; }

    public string ExchangeName { get; set; }

    public bool Redelivered { get; set; }
}
