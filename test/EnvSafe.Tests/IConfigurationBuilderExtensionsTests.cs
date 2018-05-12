using Microsoft.Extensions.Configuration;
using System;
using Xunit;

namespace EnvSafe.Tests
{
    public class IConfigurationBuilderExtensionsTests
    {
        [Fact(DisplayName = "Add EnvSafe should succeed")]
        public void AddEnvironmentVariablesSafe_Should_Succeed()
        {
            Environment.SetEnvironmentVariable("HELLO", "SARASA");
            Environment.SetEnvironmentVariable("WORLD", "SARASA");

            new ConfigurationBuilder().AddEnvironmentVariablesSafe();

            Environment.SetEnvironmentVariable("HELLO", null);
            Environment.SetEnvironmentVariable("WORLD", null);
        }

        [Fact(DisplayName = "Add EnvSafe without required env var should throw MissingEnvVarsException")]
        public void AddEnvironmentVariablesSafe_MissingEnvVar_ShouldThrow_MissingEnvVars()
        {
            Environment.SetEnvironmentVariable("HELLO", "SARASA");

            var ex = Assert.Throws<MissingEnvVarsException>(()
                => new ConfigurationBuilder().AddEnvironmentVariablesSafe());

            Assert.Contains("WORLD", ex.Message);
            Environment.SetEnvironmentVariable("HELLO", null);
        }
    }
}