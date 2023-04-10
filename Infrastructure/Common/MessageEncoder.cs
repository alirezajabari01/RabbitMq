using System.Text;
using System.Text.Json;
using RabbitMQ.Client;

namespace Infrastructure.Common;

public static class MessageEncoder
{
    public static byte[] EncodeMessage<TObject>(this TObject message)
    {
        return JsonSerializer.SerializeToUtf8Bytes(message);
    }

    public static T DecodeMessage<T>(this byte[] message)
    {
        return JsonSerializer.Deserialize<T>(message)!;
    }
}