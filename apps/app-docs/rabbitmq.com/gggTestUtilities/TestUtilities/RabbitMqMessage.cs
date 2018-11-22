using System;
using System.Text;
using System.Threading;
using Newtonsoft.Json;

namespace TestUtilities
{
    public class RabbitMqMessage
    {
        public int ManagedThreadId { get; set; }
        public DateTime Date { get; set; }
        public string Message { get; set; }
        public RabbitMqMessage(string message)
        {
            ManagedThreadId = Thread.CurrentThread.ManagedThreadId;
            Date = DateTime.UtcNow;
            Message = message;
        }

        public byte[] ToByteArray()
        {
            string serializeObject = JsonConvert.SerializeObject(this);
            byte[] bytes = Encoding.ASCII.GetBytes(serializeObject);
            return bytes;

        }

        public static RabbitMqMessage FromByteArray(byte[] body)
        {
            string stringParam = Encoding.ASCII.GetString(body);
            RabbitMqMessage rabbitMqMessage = JsonConvert.DeserializeObject<RabbitMqMessage>(stringParam);
            return rabbitMqMessage;
        }

        public string ToJsonString()
        {
            return JsonConvert.SerializeObject(this);
        }
    }
}