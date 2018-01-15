using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using UnityEngine;

namespace IsoUnity.Events
{
    public class GameEventUtility
    {
        internal static void Init(Type type, ref Dictionary<GameEventConfig, MethodInfo> calls, ref Dictionary<MethodInfo, GameEventAttribute> attrInfo)
        {
            calls = new Dictionary<GameEventConfig, MethodInfo>();
            attrInfo = new Dictionary<MethodInfo, GameEventAttribute>();

            foreach (var method in type.GetMethods().ToList()
                    .FindAll(m => m.GetCustomAttributes(typeof(GameEventAttribute), true).Length > 0)
                    .ToArray())
            {
                var attr = ((GameEventAttribute)method.GetCustomAttributes(typeof(GameEventAttribute), true)[0]);
                attrInfo.Add(method, attr);
                calls.Add(new GameEventConfig(type, method), method);
            }
        }

        internal static void EventHappened(MonoBehaviour reference, Dictionary<GameEventConfig, MethodInfo> calls, Dictionary<MethodInfo, GameEventAttribute> attrInfo, IGameEvent ge)
        {
            if(reference == null)
            {
                Debug.LogWarning("WTF");
                return;
            }

            if (calls != null && calls.Count > 0)
            {
                var config = new GameEventConfig(ge);
                if (calls.ContainsKey(config))
                {
                    var call = calls[config];

                    if (!attrInfo[call].RequiresReference || ge.belongsTo(reference, reference.GetType().Name))
                    {
                        List<object> parameters = new List<object>();
                        foreach (var p in call.GetParameters())
                            if (ge.Params.Contains(p.Name.ToLower()))
                                parameters.Add(ge.getParameter(p.Name));
                            else parameters.Add(p.DefaultValue);

                        var output = call.Invoke(reference, parameters.ToArray());

                        if (output is IEnumerator)
                            // If we want to autofinish it we use the controller, else just launch it
                            reference.StartCoroutine(attrInfo[call].AutoFinish ? CoroutineController(ge, output as IEnumerator) : output as IEnumerator);
                        else if (attrInfo[call].AutoFinish)
                            // If is not a coroutine and we have to auto finish it, we just do it
                            Game.main.eventFinished(ge);

                    }
                }
            }
        }
        
        private static IEnumerator CoroutineController(IGameEvent ge, IEnumerator toRun)
        {
            // We wrap the coroutine
            while (toRun.MoveNext())
                yield return toRun.Current;

            // And when it finishes, we finish the event
            Game.main.eventFinished(ge);
        }
    }
}