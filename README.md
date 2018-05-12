# env-safe-net

Inspired by [`dotenv-safe`](https://github.com/rolodato/dotenv-safe).

Using the `EnvSafe` extension does the same thing that `AddEnvironmentVariable()` but ensures that all necessary environment variables are defined after reading from `Environment.GetEnvironmentVariables()`.

## Install [![NuGet](https://img.shields.io/nuget/v/EnvSafe.svg)](https://www.nuget.org/packages/EnvSafe)
From the package manager console:
```
PM> Install-Package EnvSafe
```

## Example
**env.example**
```
REDIS_HOST
REDIS_PORT
REDIS_INSTANCE
```
**launchSettings.json** (or any launch settings file like `docker-compose` or `k8s yml`)
```json
{
  "profiles": {
    "Development": {
      "environmentVariables": {
        "REDIS_HOST": "localhost",
        "REDIS_PORT": "6379"
      }
    }
  }
}
```

**Startup.cs**
```cs
public class Startup
{
    public Startup(IHostingEnvironment env)
    {
        var builder = new ConfigurationBuilder()
            .AddEnvironmentVariablesSafe();
        ..
    }
    ..
}
```

Since the provided `launchSettings.json` file does not contain all the variables defined in `env.example`, an `MissingEnvVarsException` is thrown:
```
The following variables were defined in env.example but are not present in the environment:
  REDIS_INSTANCE
```

## About

It's main purpose is to document the required environment variables the application needs to run correctly, failing fast in case of one of them missing and preventing unexpected behaviours in runtime.

Nowadays the only supported file name is `env.example`, and it has to be in the content root path (next to the `.dll` generated by the `dotnet publish`).

`EnvSafe` extension has the same overloads that `IConfigurationBuilder.AddEnvironmentVariable()`.

## Future Roadmap
- [ ] Allow empty values.
- [ ] Support file by environment (`env.{Environment}.example`) with different required variables.
- [ ] Allow any file name and directory.
- [ ] Use `env.example` as default value (using `.AddEnvironmentVariableSafeOrDefault()`).
- [ ] CI.
