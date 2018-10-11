using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Runtime.Serialization.Formatters.Binary;
using System.Text;
using JsonSerializerSettings = Newtonsoft.Json.JsonSerializerSettings;

namespace UtilitiesClassLibrary
{
    public class GggUtilities
    {
        /// <summary>
        /// https://stackoverflow.com/questions/4865104/convert-any-object-to-a-byte
        /// </summary>
        /// <param name="obj"></param>
        /// <returns></returns>
        public static byte[] ObjectToByteArray(object obj)
        {
            if (obj == null)
            {
                return null;
            }
            BinaryFormatter bf = new BinaryFormatter();
            using (MemoryStream ms = new MemoryStream())
            {
                bf.Serialize(ms, obj);
                return ms.ToArray();
            }
        }


        public static byte[] DynamicToByteArray(object message)
        {
            string serializeObject = JsonConvert.SerializeObject(message);
            byte[] bytes = Encoding.UTF8.GetBytes(serializeObject);
            return bytes;
        }

        // statics
        public static string HostUrl = "http://localhost:15672/#/queues";
        public static string HostName = "localhost";
        // exchanges
        public static string ExchangeDefault = "";
        public static string ExchangeLogs = "logs";
        // queues
        public static string QueueHello = "hello";
        public static string QueueDurable = "durable_queue";
        public static string QueueTaskQueue = "task_queue";
        // routing keys
        public static string RoutingKeyHello = "hello";
        public static string RoutingKeyTaskQueue = "task_queue";
        // logs
        public static string Path = "C:\\temp\\Ggg.RabbitMQ.Solution.log";
        public static List<string> Contents = new List<string>();
        public static void WriteLog(object sender, object message)
        {
            Type messageType = message.GetType();
            if (messageType != typeof(string))
            {
                JsonSerializerSettings jsonSerializerSettings = new JsonSerializerSettings()
                {
                    ReferenceLoopHandling = ReferenceLoopHandling.Ignore
                };
                jsonSerializerSettings.Formatting = Formatting.Indented;
                message = JsonConvert.SerializeObject(message, jsonSerializerSettings);
            }
            Type type = sender.GetType();
            string typeFullName = type.FullName;
            if (sender is string)
            {
                typeFullName = (string)sender;
            }
            string logMessage = DateTime.Now.ToString("G") + Environment.NewLine;
            logMessage += typeFullName + Environment.NewLine;
            logMessage += message + Environment.NewLine;
            Contents.Add(logMessage);
            Debug.WriteLine(logMessage);
            Console.WriteLine(logMessage);
            Trace.WriteLine(logMessage);
        }

        public static void ShowLog()
        {
            File.WriteAllLines(Path, Contents);
            Debug.WriteLine("Logs created at:");
            Debug.WriteLine(Path);
        }


        public static string MessageUseFuncTests = "Please use FuncTests project for samples.";


    }
}
