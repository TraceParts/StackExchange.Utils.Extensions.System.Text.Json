# StackExchange.Utils.Http.Extensions.System.Text.Json

Currently StackExchange.Utils.Http has dependency on Jil for Json serialization and deserialization.

This package allows to use [System.Text.Json](https://learn.microsoft.com/en-us/dotnet/api/system.text.json?view=net-8.0) instead of [JIL](https://github.com/kevin-montrose/Jil)

[![StackExchange.Utils.Http.Extensions.System.Text.Json](https://img.shields.io/badge/nuget-v0.0.1-green)](https://www.nuget.org/packages/StackExchange.Utils.Http.Extensions.System.Text.Json/0.0.1)

### How to use it

```c#
var result = await Http.Request("https://example.com")  
                       .SendSystemTextJson(new { name = "my thing" })
                       .ExpectSystemTextJson<MyType>(MyJsonSerializerSettings)
                       .GetAsync()
```

*If serializerSettings is null, JsonSerializer will use default settings from DefaultSettings.*

Of course, you can use all other features from StackExchange, like this:
```c#
var result = await Http.Request("https://example.com")
                       .IgnoredResponseStatuses(HttpStatusCode.NotFound)
                       .WithTimeout(TimeSpan.FromSeconds(20))
                       .SendSystemTextJson(new { name = "my thing" })
                       .ExpectSystemTextJson<MyType>()
                       .GetAsync();
```
