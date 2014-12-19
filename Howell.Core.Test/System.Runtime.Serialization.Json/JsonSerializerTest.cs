using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.IO;
namespace System.Runtime.Serialization.Json
{
    static class JsonSerializerTest
    {
        public static void Test()
        {
            JsonSerializer<Config> serializer = new JsonSerializer<Config>();
            var config = new Config()
            {
                encoding = "UTF-8",
                plugins = new string[] { "python", "C++", "C#" },
                indent = new Indent() { length = 4, use_space = false }
            };
            String jsonString = serializer.ToJsonString(config);
            Console.WriteLine("ToJsonString:{0}", jsonString);
            Config newConfig = serializer.FromJsonString(jsonString);
            Console.WriteLine("encoding:{0} indent.length:{1}", newConfig.encoding, newConfig.indent.length);
            using (FileStream file = File.Create("Config.json"))
            {
                serializer.ToStream(config, file);
            }
            using (FileStream file = File.Open("Config.json", FileMode.Open))
            {
                newConfig = serializer.FromStream(file);
                Console.WriteLine("encoding:{0} indent.length:{1}", newConfig.encoding, newConfig.indent.length);
            }
        }
    }

    [DataContract(Namespace = "http://www.howell.net.cn")]
    class Config
    {
        [DataMember(Order = 0)]
        public string encoding { get; set; }
        [DataMember(Order = 1)]
        public string[] plugins { get; set; }
        [DataMember(Order = 2)]
        public Indent indent { get; set; }
    }
    [DataContract(Namespace = "http://www.howell.net.cn")]
    class Indent
    {
        [DataMember(Order = 0)]
        public int length { get; set; }
        [DataMember(Order = 1)]
        public bool use_space { get; set; }
    }
}
