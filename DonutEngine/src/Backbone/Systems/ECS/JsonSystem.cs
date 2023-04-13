using System.Collections.Generic;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using DonutEngine.Backbone;

public static class ComponentSerializer 
{
    private static Dictionary<string, JsonSerializerSettings> serializerSettings = new Dictionary<string, JsonSerializerSettings>();

    public static void RegisterComponentType<T>(JsonSerializerSettings settings = null) where T : Component 
    {
        if (!serializerSettings.ContainsKey(typeof(T).Name)) 
        {
            serializerSettings[typeof(T).Name] = settings ?? new JsonSerializerSettings();
        }
    }

    public static string Serialize(Component component) 
    {
        return JsonConvert.SerializeObject(component, serializerSettings[component.GetType().Name]);
    }

    public static T Deserialize<T>(string json) where T : Component 
    {
        return JsonConvert.DeserializeObject<T>(json, serializerSettings[typeof(T).Name]);
    }

    public static Component Deserialize(string json) 
    {
        JObject jObject = JObject.Parse(json);
        string typeName = jObject.Value<string>("$type");
        Type type = Type.GetType(typeName);
        return (Component)jObject.ToObject(type, JsonSerializer.Create(serializerSettings[type.Name]));
    }
}
