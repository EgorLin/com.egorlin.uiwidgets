using System;
using EgorLin.Collections.Unsafe;

namespace EgorLin.UIWidgets.Pools
{
#if ECS_COMPILE_IL2CPP_OPTIONS
	[Unity.IL2CPP.CompilerServices.Il2CppSetOptionAttribute(Unity.IL2CPP.CompilerServices.Option.NullChecks, false),
	 Unity.IL2CPP.CompilerServices.Il2CppSetOptionAttribute(Unity.IL2CPP.CompilerServices.Option.ArrayBoundsChecks, false),
	 Unity.IL2CPP.CompilerServices.Il2CppSetOptionAttribute(Unity.IL2CPP.CompilerServices.Option.DivideByZeroChecks, false)]
#endif
    internal static class PoolIntHashSet
    {
        private static readonly PoolInternalBase Pool = new(() => new IntHashSet(),
            obj => ((IntHashSet)obj).Clear());

        public static PooledIntHashSet Spawn()
        {
            return new PooledIntHashSet((IntHashSet) Pool.Spawn());
        }

        public static void Recycle(ref IntHashSet instance)
        {
            Pool.Recycle(instance);
            instance = null;
        }

        public static void Recycle(IntHashSet instance)
        {
            Pool.Recycle(instance);
        }
    }

    internal readonly struct PooledIntHashSet : IDisposable
    {
        public readonly IntHashSet Value;

        public PooledIntHashSet(IntHashSet value)
        {
            Value = value;
        }

        public void Dispose()
        {
            if (Value != null)
            {
                PoolIntHashSet.Recycle(Value);
            }
        }

        public static implicit operator IntHashSet(PooledIntHashSet pooled)
        {
            return pooled.Value;
        }

        public IntHashSet.Enumerator GetEnumerator()
        {
            return Value.GetEnumerator();
        }

        public bool Add(int value)
        {
            return Value.Add(value);
        }

        public void Remove(int key)
        {
            Value.Remove(key);
        }

        public bool Has(int key)
        {
            return Value.Has(key);
        }

        public void Clear()
        {
            Value.Clear();
        }
    }
}
