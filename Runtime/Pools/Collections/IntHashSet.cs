using System;
using System.Collections;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using EgorLin.UIWidgets.Pools.Raw;

namespace EgorLin.Collections.Unsafe
{
    [Serializable]
    internal sealed class IntHashSet : IEnumerable<int>
    {
        public int length;
        public int capacity;
        public int capacityMinusOne;
        public int lastIndex;
        public int freeIndex;

        public IntPinnedArray buckets;
        public IntPinnedArray slots;

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public IntHashSet() : this(0)
        {
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public IntHashSet(int capacity)
        {
            lastIndex = 0;
            length = 0;
            freeIndex = -1;

            capacityMinusOne = HashHelpers.GetCapacity(capacity);
            this.capacity = capacityMinusOne + 1;
            buckets = new IntPinnedArray(this.capacity);
            slots = new IntPinnedArray(this.capacity * 2);
        }

        IEnumerator<int> IEnumerable<int>.GetEnumerator()
        {
            return GetEnumerator();
        }

        IEnumerator IEnumerable.GetEnumerator()
        {
            return GetEnumerator();
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public Enumerator GetEnumerator()
        {
            Enumerator e;
            e.set = this;
            e.index = 0;
            e.current = default;
            return e;
        }

        public unsafe struct Enumerator : IEnumerator<int>
        {
            public IntHashSet set;

            public int index;
            public int current;

            public bool MoveNext()
            {
                {
                    int* slotsPtr = set.slots.ptr;
                    for (int len = set.lastIndex; index < len; ++index)
                    {
                        int v = slotsPtr[index] - 1;
                        if (v < 0)
                        {
                            continue;
                        }

                        current = v;
                        ++index;

                        return true;
                    }

                    index = set.lastIndex + 1;
                    current = default;
                    return false;
                }
            }

            public int Current => current;

            object IEnumerator.Current => current;

            void IEnumerator.Reset()
            {
                index = 0;
                current = default;
            }

            public void Dispose()
            {
            }
        }
    }
}