using System;
using EgorLin.Collections.Unsafe;

namespace EgorLin.UIWidgets.Pools
{
#if ECS_COMPILE_IL2CPP_OPTIONS
	[Unity.IL2CPP.CompilerServices.Il2CppSetOptionAttribute(Unity.IL2CPP.CompilerServices.Option.NullChecks, false),
	 Unity.IL2CPP.CompilerServices.Il2CppSetOptionAttribute(Unity.IL2CPP.CompilerServices.Option.ArrayBoundsChecks, false),
	 Unity.IL2CPP.CompilerServices.Il2CppSetOptionAttribute(Unity.IL2CPP.CompilerServices.Option.DivideByZeroChecks, false)]
#endif
    internal static class PoolIntHashMap<TValue>
    {
        private static readonly PoolInternalBase Pool = new(() => new IntHashMap<TValue>(),
            obj => ((IntHashMap<TValue>)obj).Clear());

        public static PooledIntHashMap<TValue> Spawn()
        {
            return new PooledIntHashMap<TValue>((IntHashMap<TValue>) Pool.Spawn());
        }

        public static void Recycle(ref IntHashMap<TValue> instance)
        {
            Pool.Recycle(instance);
            instance = null;
        }

        public static void Recycle(IntHashMap<TValue> instance)
        {
            Pool.Recycle(instance);
        }
    }

    internal readonly struct PooledIntHashMap<TValue> : IDisposable
    {
        public readonly IntHashMap<TValue> Value;

        public PooledIntHashMap(IntHashMap<TValue> value)
        {
            Value = value;
        }

        public void Dispose()
        {
            if (Value != null)
            {
                PoolIntHashMap<TValue>.Recycle(Value);
            }
        }

        public static implicit operator IntHashMap<TValue>(PooledIntHashMap<TValue> pooled)
        {
            return pooled.Value;
        }

        public IntHashMap<TValue>.Enumerator GetEnumerator()
        {
            return Value.GetEnumerator();
        }

        public bool Add(int key, TValue value, out int slotIndex)
        {
            return Value.Add(key, value, out slotIndex);
        }

        public TValue GetValueByKey(int key)
        {
            return Value.GetValueByKey(key);
        }

        public void Set(int key, TValue value)
        {
            Value.Set(key, value);
        }

        public void Remove(int key)
        {
            Value.Remove(key, out _);
        }

        public bool Has(int key)
        {
            return Value.Has(key);
        }

        public bool TryGetValue(int key, out TValue value)
        {
            return Value.TryGetValue(key, out value);
        }
    }
}
