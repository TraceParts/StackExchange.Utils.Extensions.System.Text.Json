using System.Net.Http;
using System.Text;
using System.Text.Json;

namespace StackExchange.Utils.Extensions.System.Text.Json
{
    internal static class ExtensionsForHttp
    {
        /// <summary>
        /// Adds JSON (System.Text.Json-serialized) content as the body for this request.
        /// </summary>
        /// <param name="builder">The builder we're working on.</param>
        /// <param name="obj">The object to serialize as JSON in the body.</param>
        /// <param name="serializerSettings">The System.Text.Json setting to use when serializing (if null, serializer take global setting or default).</param>
        /// <returns>The request builder for chaining.</returns>
        public static IRequestBuilder SendSystemTextJson(this IRequestBuilder builder, object obj, JsonSerializerOptions serializerSettings = null) =>
            builder.SendContent(new StringContent(JsonSerializer.Serialize(obj, serializerSettings), Encoding.UTF8, "application/json"));
    }
}
