using System.Text.Json;
using System.Text.Json.Serialization;

namespace CourseWork.Extensions;

public static class SessionExtensions 
{
    public static void SetJson(this ISession session, string key, object value) {
        JsonSerializerOptions options = new()
        {
            ReferenceHandler = ReferenceHandler.IgnoreCycles,
            WriteIndented = true
        };
        session.SetString(key, JsonSerializer.Serialize(value, options));
    }
    
    public static T? GetJson<T>(this ISession session, string key) {
        var sessionData = session.GetString(key);
        return sessionData == null
            ? default : JsonSerializer.Deserialize<T>(sessionData);
    }
}