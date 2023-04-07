using System.Text;

namespace Infrastructure.Common;

public static class MessageEncoder
{
    public static byte[] EncodeMessage(this string message)
    {
        return Encoding.UTF8.GetBytes(message);
    }
}