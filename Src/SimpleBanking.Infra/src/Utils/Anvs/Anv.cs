namespace SimpleBanking.Infra.Utils.Anvs;

public static partial class Anv
{
    public static AnvEnv EnvLoad(string envName)
      => new AnvEnv(envName, Environment.GetEnvironmentVariable(envName));

    public sealed class AnvEnv
    {
        public string? Value { get; set; }
        public string Name { get; set; }

        public AnvEnv(string name, string? val)
        {
            Name = name;
            Value = val;
        }

        public static implicit operator string?(AnvEnv val) => val.Value;

        public static bool operator true(AnvEnv val) => val.IsDefined();
        public static bool operator false(AnvEnv val) => val.IsNotDefined();

        public string NotNull() => Value ?? throw new ArgumentException($"{Name} not found.");

        public string OrDefault(string fallbackValue) => Value ?? fallbackValue;

        public bool IsNotDefined() => Value is null;
        public bool IsDefined() => Value is not null;
    }
}
