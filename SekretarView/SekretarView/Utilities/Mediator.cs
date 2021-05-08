using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SekretarView
{
    class Mediator
    {
        static IDictionary<string, List<Action<object>>> callbacks = new Dictionary<string, List<Action<object>>>();

        static public void Register(string token, Action<object> callback)
        {
            if (!callbacks.ContainsKey(token))
            {
                var list = new List<Action<object>>();
                list.Add(callback);
                callbacks.Add(token, list);
            }
            else
            {
                callbacks[token].Add(callback);
            }
        }

        static public void Unregister(string token, Action<object> callback)
        {
            if (callbacks.ContainsKey(token))
                callbacks[token].Remove(callback);
        }

        static public void NotifyColleagues(string token, object args)
        {
            if (callbacks.ContainsKey(token))
                foreach (var callback in callbacks[token])
                    callback(args);
        }
    }
}
