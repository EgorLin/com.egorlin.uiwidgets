using System;
using System.Runtime.CompilerServices;
using Unity.Collections.LowLevel.Unsafe;

namespace EgorLin.UIWidgets.Pools.Raw
{
    [Serializable]
    internal unsafe struct IntPinnedArray : IDisposable
    {
        public int[] data;
        public ulong handle;
        public int* ptr;

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public IntPinnedArray(int size)
        {
            data = new int[size];
            ptr = (int*) UnsafeUtility.PinGCArrayAndGetDataAddress(data, out handle);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public void Dispose()
        {
            UnsafeUtility.ReleaseGCObject(handle);
            ptr = (int*) IntPtr.Zero;
            data = null;
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public void Resize(int newSize)
        {
            UnsafeUtility.ReleaseGCObject(handle);
            var newArray = new int[newSize];
            int len = data.Length;
            Array.Copy(data, 0, newArray, 0, newSize >= len ? len : newSize);
            data = newArray;
            ptr = (int*) UnsafeUtility.PinGCArrayAndGetDataAddress(data, out handle);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public void Clear()
        {
            Array.Clear(data, 0, data.Length);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public Enumerator GetEnumerator()
        {
            Enumerator e;
            e.lengthMinusOne = data.Length - 1;
            e.ptr = ptr;
            e.index = -1;
            return e;
        }

        public struct Enumerator
        {
            public int* ptr;

            public int lengthMinusOne;
            public int index;

            [MethodImpl(MethodImplOptions.AggressiveInlining)]
            public bool MoveNext()
            {
                return ++index <= lengthMinusOne;
            }

            public int Current => ptr[index];
        }
    }
}