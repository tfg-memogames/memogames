using System;
using System.Collections.Generic;
using System.Reflection;

namespace IsoUnity.Events
{
    public class GameEventConfig
    {
        public string Name { get; private set; }
        public MethodInfo Methid { get; private set; }
        public Dictionary<string, System.Type> ParameterConfig { get; private set; }

        public Dictionary<string, bool> ParameterHasDefault { get; private set; }
        public Dictionary<string, object> DefaultValue { get; private set; }

        public bool RequiresReference { get; private set; }

        public GameEventConfig(Type t, MethodInfo m)
        {
            Name = SplitCamelCase(m.Name).ToLower();
            ParameterConfig = new Dictionary<string, Type>();
            ParameterHasDefault = new Dictionary<string, bool>();
            DefaultValue = new Dictionary<string, object>();

            var attr = ((GameEventAttribute)m.GetCustomAttributes(typeof(GameEventAttribute), true)[0]);

            if (attr.RequiresReference)
            {
                ParameterConfig.Add(t.Name.ToLower(), t);
                ParameterHasDefault.Add(t.Name.ToLower(), false);
            }

            RequiresReference = attr.RequiresReference;

            foreach (var parameter in m.GetParameters())
            {
                ParameterConfig.Add(parameter.Name.ToLower(), parameter.ParameterType);
                ParameterHasDefault.Add(parameter.Name.ToLower(), parameter.IsOptional);
                if (parameter.IsOptional)
                    DefaultValue.Add(parameter.Name.ToLower(), parameter.DefaultValue);
            }
        }

        public GameEventConfig(string name, Dictionary<string, System.Type> parameterConfig, Dictionary<string, bool> parameterHasDefault)
        {
            Name = name;
            ParameterConfig = parameterConfig;
            ParameterHasDefault = parameterHasDefault;
        }

        public GameEventConfig(IGameEvent gameEvent)
        {
            Name = gameEvent.Name;
            ParameterConfig = new Dictionary<string, Type>();
            foreach (var p in gameEvent.Params)
                if (p != "synchronous")
                    ParameterConfig.Add(p, gameEvent.getParameter(p) != null ? gameEvent.getParameter(p).GetType() : typeof(object));
        }

        public override bool Equals(object other)
        {
            if (!(other is GameEventConfig))
                return false;

            var o = other as GameEventConfig;

            // Yo soy el método
            // Él es el evento

            if (other == null)
                return false;

            // Nuestro nombre tiene que ser igual
            if (o.Name.Equals(Name))
            {
                // Busco en mis parámetros
                foreach (var kvp in ParameterConfig)
                {
                    var otherHasParam = o.ParameterConfig.ContainsKey(kvp.Key);
                    // Si el otro no tiene parámetro pero yo tengo un valor por defecto
                    if ((!otherHasParam && !ParameterHasDefault[kvp.Key])
                        // Si el evento tiene un parámetro pero yo no me lo puedo asignar ó sea object que podría ser cualquier cosa
                        || (otherHasParam && !kvp.Value.IsAssignableFrom(o.ParameterConfig[kvp.Key])))
                    {
                        return false;
                    }
                }
            }

            return true;
        }

        public override int GetHashCode()
        {
            return this.Name.GetHashCode();
        }


        public static string SplitCamelCase(string input)
        {
            return System.Text.RegularExpressions.Regex.Replace(input, "([A-Z])", " $1", System.Text.RegularExpressions.RegexOptions.Multiline).Trim();
        }
    }
}