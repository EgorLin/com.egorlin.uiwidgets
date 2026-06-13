using System;
using System.Collections.Generic;

namespace EgorLin.UIWidgets.Pools
{
#if ECS_COMPILE_IL2CPP_OPTIONS
	[Unity.IL2CPP.CompilerServices.Il2CppSetOptionAttribute(Unity.IL2CPP.CompilerServices.Option.NullChecks, false),
	 Unity.IL2CPP.CompilerServices.Il2CppSetOptionAttribute(Unity.IL2CPP.CompilerServices.Option.ArrayBoundsChecks, false),
	 Unity.IL2CPP.CompilerServices.Il2CppSetOptionAttribute(Unity.IL2CPP.CompilerServices.Option.DivideByZeroChecks, false)]
#endif
    internal static class PoolStack<T>
    {
        private static readonly PoolInternalBase Pool = new(() => new Stack<T>(),
            obj => ((Stack<T>)obj).Clear());

        public static PooledStack<T> Spawn()
        {
            return new PooledStack<T>((Stack<T>) Pool.Spawn());
        }

        public static void Recycle(ref Stack<T> instance)
        {
            Pool.Recycle(instance);
            instance = null;
        }

        public static void Recycle(Stack<T> instance)
        {
            Pool.Recycle(instance);
        }
    }

    internal readonly struct PooledStack<TItem> : IDisposable
    {
        public readonly Stack<TItem> Value;

        public PooledStack(Stack<TItem> value)
        {
            Value = value;
        }

        public int Count => Value.Count;

        public void Dispose()
        {
            if (Value != null)
            {
                PoolStack<TItem>.Recycle(Value);
            }
        }

        public static implicit operator Stack<TItem>(PooledStack<TItem> pooled)
        {
            return pooled.Value;
        }

        public Stack<TItem>.Enumerator GetEnumerator()
        {
            return Value.GetEnumerator();
        }

        public void Push(TItem item)
        {
            Value.Push(item);
        }

        public TItem Pop()
        {
            return Value.Pop();
        }

        public TItem Peek()
        {
            return Value.Peek();
        }
    }
}
