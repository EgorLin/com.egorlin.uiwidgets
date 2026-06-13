using System;
using System.Runtime.CompilerServices;
using EgorLin.UIWidgets.Pools.Raw;

namespace EgorLin.Collections.Unsafe
{
    [Serializable]
    internal struct IntHashMapSlot
    {
        public int key;
        public int next;
    }

    [Serializable]
    internal sealed class IntHashMap<T>
    {
        public int length;
        public int capacity;
        public int capacityMinusOne;
        public int lastIndex;
        public int freeIndex;

        public IntPinnedArray buckets;

        public T[] data;
        public PinnedArray<IntHashMapSlot> slots;

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public IntHashMap(in int capacity = 0)
        {
            lastIndex = 0;
            length = 0;
            freeIndex = -1;

            capacityMinusOne = HashHelpers.GetCapacity(capacity - 1);
            this.capacity = capacityMinusOne + 1;

            buckets = new IntPinnedArray(this.capacity);
            slots = new PinnedArray<IntHashMapSlot>(this.capacity);
            data = new T[this.capacity];
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public IntHashMap(IntHashMap<T> other)
        {
            lastIndex = other.lastIndex;
            length = other.length;
            freeIndex = other.freeIndex;

            capacityMinusOne = other.capacityMinusOne;
            capacity = other.capacity;

            buckets = new IntPinnedArray(capacity);
            slots = new PinnedArray<IntHashMapSlot>(capacity);
            data = new T[capacity];

            for (int i = 0, len = capacity; i < len; i++)
            {
                buckets.data[i] = other.buckets.data[i];
                slots.data[i] = other.slots.data[i];
                data[i] = other.data[i];
            }
        }

        ~IntHashMap()
        {
            buckets.Dispose();
            slots.Dispose();
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public Enumerator GetEnumerator()
        {
            Enumerator e;
            e.hashMap = this;
            e.index = 0;
            e.current = default;
            return e;
        }

        public unsafe struct Enumerator
        {
            public IntHashMap<T> hashMap;

            public int index;
            public int current;

            public bool MoveNext()
            {
                for (; index < hashMap.lastIndex; ++index)
                {
                    ref var slot = ref hashMap.slots.ptr[index];
                    if (slot.key - 1 < 0)
                    {
                        continue;
                    }

                    current = index;
                    ++index;

                    return true;
                }

                index = hashMap.lastIndex + 1;
                current = default;
                return false;
            }

            public int Current => current;
        }
    }
}