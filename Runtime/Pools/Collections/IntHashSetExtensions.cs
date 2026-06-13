using System.Runtime.CompilerServices;
using EgorLin.UIWidgets.Pools.Raw;

namespace EgorLin.Collections.Unsafe
{
    internal static unsafe class IntHashSetExtensions
    {
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static bool Add(this IntHashSet hashSet, in int value)
        {
            int rem = value & hashSet.capacityMinusOne;

            {
                int* slotsPtr = hashSet.slots.ptr;
                int* bucketsPtr = hashSet.buckets.ptr;
                int* slot;
                for (int i = *(bucketsPtr + rem) - 1; i >= 0; i = *(slot + 1))
                {
                    slot = slotsPtr + i;
                    if (*slot - 1 == value)
                    {
                        return false;
                    }
                }
            }

            int newIndex;
            if (hashSet.freeIndex >= 0)
            {
                newIndex = hashSet.freeIndex;
                hashSet.freeIndex = *(hashSet.slots.ptr + newIndex + 1);
            }
            else
            {
                if (hashSet.lastIndex == hashSet.capacity * 2)
                {
                    int newCapacityMinusOne = HashHelpers.GetCapacity(hashSet.length);
                    int newCapacity = newCapacityMinusOne + 1;

                    hashSet.slots.Resize(newCapacity * 2);

                    var newBuckets = new IntPinnedArray(newCapacity);

                    {
                        int* slotsPtr = hashSet.slots.ptr;
                        int* bucketsPtr = newBuckets.ptr;
                        for (int i = 0, len = hashSet.lastIndex; i < len; i += 2)
                        {
                            int* slotPtr = slotsPtr + i;

                            int newResizeIndex = (*slotPtr - 1) & newCapacityMinusOne;
                            int* newCurrentBucket = bucketsPtr + newResizeIndex;

                            *(slotPtr + 1) = *newCurrentBucket - 1;

                            *newCurrentBucket = i + 1;
                        }
                    }

                    hashSet.buckets.Dispose();

                    hashSet.buckets = newBuckets;
                    hashSet.capacityMinusOne = newCapacityMinusOne;
                    hashSet.capacity = newCapacity;

                    rem = value & newCapacityMinusOne;
                }

                newIndex = hashSet.lastIndex;
                hashSet.lastIndex += 2;
            }

            {
                int* slotsPtr = hashSet.slots.ptr;
                int* bucketsPtr = hashSet.buckets.ptr;
                int* bucket = bucketsPtr + rem;
                int* slot = slotsPtr + newIndex;

                *slot = value + 1;
                *(slot + 1) = *bucket - 1;

                *bucket = newIndex + 1;
            }

            ++hashSet.length;
            return true;
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static bool Remove(this IntHashSet hashSet, in int value)
        {
            {
                int* slotsPtr = hashSet.slots.ptr;
                int* bucketsPtr = hashSet.buckets.ptr;
                int rem = value & hashSet.capacityMinusOne;

                int next;
                int num = -1;

                for (int i = *(bucketsPtr + rem) - 1; i >= 0; i = next)
                {
                    int* slot = slotsPtr + i;
                    int* slotNext = slot + 1;

                    if (*slot - 1 == value)
                    {
                        if (num < 0)
                        {
                            *(bucketsPtr + rem) = *slotNext + 1;
                        }
                        else
                        {
                            *(slotsPtr + num + 1) = *slotNext;
                        }

                        *slot = -1;
                        *slotNext = hashSet.freeIndex;

                        if (--hashSet.length == 0)
                        {
                            hashSet.lastIndex = 0;
                            hashSet.freeIndex = -1;
                        }
                        else
                        {
                            hashSet.freeIndex = i;
                        }

                        return true;
                    }

                    next = *slotNext;
                    num = i;
                }

                return false;
            }
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static void CopyTo(this IntHashSet hashSet, int[] array)
        {
            {
                int* slotsPtr = hashSet.slots.ptr;
                var num = 0;
                for (int i = 0, li = hashSet.lastIndex, len = hashSet.length; i < li && num < len; ++i)
                {
                    int v = *(slotsPtr + i) - 1;
                    if (v < 0)
                    {
                        continue;
                    }

                    array[num] = v;
                    ++num;
                }
            }
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static bool Has(this IntHashSet hashSet, in int key)
        {
            {
                int* slotsPtr = hashSet.slots.ptr;
                int* bucketsPtr = hashSet.buckets.ptr;
                int rem = key & hashSet.capacityMinusOne;

                int next;
                for (int i = *(bucketsPtr + rem) - 1; i >= 0; i = next)
                {
                    int* slot = slotsPtr + i;
                    if (*slot - 1 == key)
                    {
                        return true;
                    }

                    next = *(slot + 1);
                }

                return false;
            }
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static void Clear(this IntHashSet hashSet)
        {
            if (hashSet.lastIndex <= 0)
            {
                return;
            }

            hashSet.slots.Clear();
            hashSet.buckets.Clear();
            hashSet.lastIndex = 0;
            hashSet.length = 0;
            hashSet.freeIndex = -1;
        }
    }
}