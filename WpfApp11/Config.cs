using System;
using System.IO;
using System.Text;
using System.Text.Json;
// using Newtonsoft.Json;
using JsonSerializer = System.Text.Json.JsonSerializer;

namespace WpfApp11;

// #nullable enable
public class Config
{
    #region MyField

    public Person Person { get; set; }
    // public string Memo { get; set; } // Non-nullable property 'Memo' is uninitialized
    public string? Memo { get; set; }

    #endregion

    public Config()
    {
        this.Person = new Person();
    }

    public static string GetConfigFilePath()
    // public static void GetConfigFilePath()
    {
        // .exe location
        string appFilePath = System.Reflection.Assembly.GetEntryAssembly().Location; // Dereference of a possibly null reference
        return System.Text.RegularExpressions.Regex.Replace(appFilePath, ".exe", "json",
            System.Text.RegularExpressions.RegexOptions.IgnoreCase);
    }

    // public static Config ReadConfig() // Possible null reference return
    public static Config? ReadConfig()
    {
        string configFile = GetConfigFilePath();
        if (File.Exists(configFile) == false)
        {
            return null;
        }
    
        using (var reader = new StreamReader(configFile, Encoding.UTF8))
        {
            // Read settings
            string buf = reader.ReadToEnd();
            
            // Deserialize and return
            // var js = new System.Web.Script.Serialization.JavaScriptSerializer(); // Not found System.Web.Extensions (Visual Studio as well)
            // var js = JsonConvert.DeserializeObject(json);
            // return JsonConvert.DeserializeObject<Config>(buf); // Use "Newtonsoft.Json" instead of System.Web.Extensions
            // WeatherForecast weatherForecast = 
            //     JsonSerializer.Deserialize<WeatherForecast>(jsonString);
            return JsonSerializer.Deserialize<Config>(buf);
        }
    }

    public static void WriteConfig(Config cfg)
    {
        // string buf = JsonConvert.SerializeObject(cfg);
        string buf = JsonSerializer.Serialize(cfg);
        string configFile = GetConfigFilePath();
    
        using (var writer = new StreamWriter(configFile, false, Encoding.UTF8))
        {
            writer.Write(buf);
        }
    }
}