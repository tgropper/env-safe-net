using EnvSafe;
using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using System.Linq;

namespace Microsoft.Extensions.Configuration
{
    public static class IConfigurationBuilderExtensions
    {
        public static IConfigurationBuilder AddEnvironmentVariablesSafe(this IConfigurationBuilder builder)
            => builder.AddEnvironmentVariablesSafe(string.Empty);

        public static IConfigurationBuilder AddEnvironmentVariablesSafe(this IConfigurationBuilder builder, string prefix)
        {
            var envVars = Environment.GetEnvironmentVariables()
                .Cast<DictionaryEntry>()
                .Select(x => (string)x.Key)
                .Where(x => x.StartsWith(prefix, StringComparison.OrdinalIgnoreCase))
                .ToArray();

            var exampleVars = File.ReadAllLines("env.example");
            var missingVars = new List<string>();
            foreach (var exampleVar in exampleVars)
                if (!envVars.Contains(exampleVar)) missingVars.Add(exampleVar);

            if (missingVars.Any()) throw new MissingEnvVarsException(missingVars.ToArray());

            return builder.AddEnvironmentVariables();
        }
    }
}