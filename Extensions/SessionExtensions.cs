using System.Text.Json;

namespace MagazineOnline.Api.Extensions;

public static class SessionExtensions
{
    public static void SetJson<T>(this ISession session, string key, T value)
    {
        session.SetString(key, JsonSerializer.Serialize(value));
    }

    public static T? GetJson<T>(this ISession session, string key)
    {
        var data = session.GetString(key);
        return data is null ? default : JsonSerializer.Deserialize<T>(data);
    }
}
