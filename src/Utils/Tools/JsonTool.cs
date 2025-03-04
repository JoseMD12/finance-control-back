using System;
using System.Collections.Generic;
using System.Linq;
using System.Text.Json;
using System.Text.Json.Serialization;
using System.Threading.Tasks;

namespace Utils.Tools
{
    public class JsonTool
    {
        public static void Print(string prefix, object obj)
        {
            var message = JsonSerializer.Serialize(
                obj,
                new JsonSerializerOptions
                {
                    WriteIndented = true,
                    Encoder = System.Text.Encodings.Web.JavaScriptEncoder.UnsafeRelaxedJsonEscaping,
                    ReferenceHandler = ReferenceHandler.IgnoreCycles,
                }
            );

            Console.WriteLine($"{prefix} -> {message}");
        }

        public static void Print(object obj)
        {
            var message = JsonSerializer.Serialize(
                obj,
                new JsonSerializerOptions
                {
                    WriteIndented = true,
                    Encoder = System.Text.Encodings.Web.JavaScriptEncoder.UnsafeRelaxedJsonEscaping,
                    ReferenceHandler = ReferenceHandler.IgnoreCycles,
                }
            );

            Console.WriteLine(message);
        }
    }
}
