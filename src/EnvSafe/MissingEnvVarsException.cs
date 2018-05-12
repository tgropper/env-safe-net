using System;

namespace EnvSafe
{
    public class MissingEnvVarsException : Exception
    {
        public MissingEnvVarsException(string[] missingVars)
            : base($"The following variables were defined in env.example but are not present in the environment:\n  {string.Join(", ", missingVars)}")
        { }
    }
}