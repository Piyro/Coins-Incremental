using System;
using System.Collections.Generic;

namespace CoinTowerIdle.Events
{
    public static class EventBus
    {
        private static readonly Dictionary<Type, Delegate> Events =
            new();

        public static void Subscribe<T>(Action<T> callback)
        {
            var type = typeof(T);

            if (Events.ContainsKey(type))
                Events[type] = Delegate.Combine(Events[type], callback);
            else
                Events.Add(type, callback);
        }

        public static void Unsubscribe<T>(Action<T> callback)
        {
            var type = typeof(T);

            if (!Events.ContainsKey(type))
                return;

            var current = Delegate.Remove(Events[type], callback);

            if (current == null)
                Events.Remove(type);
            else
                Events[type] = current;
        }

        public static void Publish<T>(T gameEvent)
        {
            var type = typeof(T);

            if (Events.TryGetValue(type, out var del))
                ((Action<T>)del)?.Invoke(gameEvent);
        }
    }
}