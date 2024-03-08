using System;
using System.Net.Http;
using System.Text.Json;
using System.Threading.Tasks;
// ReSharper disable UnusedMember.Global

// ReSharper disable once CheckNamespace
namespace StackExchange.Utils.Extensions.NewtonJson
{
#pragma warning disable 1591
    public static class ExtensionsExpectSystemTextJson
#pragma warning restore 1591
    {
        /// <summary>
        /// <para>Holds handlers for ExpectJson(T) calls, so we don't re-create them in the common "default Options" case.</para>
        /// <para>Without this, we create a new Func for each ExpectJson call even</para>
        /// </summary>
        /// <typeparam name="T">The type being deserialized.</typeparam>
        private static class JsonHandler<T>
        {
            internal static Func<HttpResponseMessage, Task<T>> WithOptions(IRequestBuilder builder, JsonSerializerOptions serializerOptions = null)
            {
                return async responseMessage =>
                {
                    await using (var responseStream =
                                 await responseMessage.Content.ReadAsStreamAsync()) // Get the response here
                    {
                        using (builder.Settings.ProfileGeneral?.Invoke("Deserialize: JSON"))
                        {
                            if (builder.BufferResponse && responseStream.Length == 0)
                            {
                                return default;
                            }

                            return await JsonSerializer.DeserializeAsync<T>(responseStream,
                                serializerOptions);
                        }
                      
                    }
                };
            }
        }

        /// <summary>
        /// Sets the response handler for this request to a JSON deserializer.
        /// </summary>
        /// <typeparam name="T">The type to deserialize to.</typeparam>
        /// <param name="builder">The builder we're working on.</param>
        /// <param name="serializerSettings">The Jil options to use when serializing.</param>
        /// <returns>A typed request builder for chaining.</returns>
        public static IRequestBuilder<T> ExpectSystemTextJson<T>(this IRequestBuilder builder, JsonSerializerOptions serializerSettings = null) =>
            builder.WithHandler(JsonHandler<T>.WithOptions(builder, serializerSettings));
    }
}
