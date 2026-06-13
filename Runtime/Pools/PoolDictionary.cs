using System;
using System.Collections.Generic;

namespace EgorLin.UIWidgets.Pools
{
#if ECS_COMPILE_IL2CPP_OPTIONS
	[Unity.IL2CPP.CompilerServices.Il2CppSetOptionAttribute(Unity.IL2CPP.CompilerServices.Option.NullChecks, false),
	 Unity.IL2CPP.CompilerServices.Il2CppSetOptionAttribute(Unity.IL2CPP.CompilerServices.Option.ArrayBoundsChecks, false),
	 Unity.IL2CPP.CompilerServices.Il2CppSetOptionAttribute(Unity.IL2CPP.CompilerServices.Option.DivideByZeroChecks, false)]
#endif
    internal static class PoolDictionary<TKey, TValue>
    {
        private static readonly PoolInternalBase Pool = new(() => new Dictionary<TKey, TValue>(),
            obj => ((Dictionary<TKey, TValue>)obj).Clear());

        public static PooledDictionary<TKey, TValue> Spawn()
        {
            return new PooledDictionary<TKey, TValue>((Dictionary<TKey, TValue>) Pool.Spawn());
        }

        public static void Recycle(ref Dictionary<TKey, TValue> instance)
        {
            Pool.Recycle(instance);
            instance = null;
        }

        public static void Recycle(Dictionary<TKey, TValue> instance)
        {
            Pool.Recycle(instance);
        }
    }

    internal readonly struct PooledDictionary<TKey, TValue> : IDisposable
    {
        public readonly Dictionary<TKey, TValue> Value;

        public PooledDictionary(Dictionary<TKey, TValue> value)
        {
            Value = value;
        }

        public void Dispose()
        {
            if (Value != null)
            {
                PoolDictionary<TKey, TValue>.Recycle(Value);
            }
        }

        public TValue this[TKey key]
        {
            get => Value[key];
            set => Value[key] = value;
        }

        public static implicit operator Dictionary<TKey, TValue>(PooledDictionary<TKey, TValue> pooled)
        {
            return pooled.Value;
        }

        public Dictionary<TKey, TValue>.Enumerator GetEnumerator()
        {
            return Value.GetEnumerator();
        }

        public void Add(TKey key, TValue value)
        {
            Value.Add(key, value);
        }

        public void Remove(TKey key)
        {
            Value.Remove(key);
        }

        public bool ContainsKey(TKey key)
        {
            return Value.ContainsKey(key);
        }

        public bool ContainsValue(TValue value)
        {
            return Value.ContainsValue(value);
        }

        public bool TryGetValue(TKey key, out TValue value)
        {
            return Value.TryGetValue(key, out value);
        }
    }
}
