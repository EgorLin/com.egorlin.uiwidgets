using System;
using System.Collections;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using Unity.Collections.LowLevel.Unsafe;

namespace EgorLin.UIWidgets.Pools.Raw
{
    [Serializable]
    internal unsafe struct PinnedArray<T> : IDisposable, IEnumerable<T> where T : unmanaged
    {
        public T[] data;
        public ulong handle;
        public T* ptr;

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public PinnedArray(int size)
        {
            data = new T[size];
            ptr = (T*) UnsafeUtility.PinGCArrayAndGetDataAddress(data, out handle);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public void Dispose()
        {
            UnsafeUtility.ReleaseGCObject(handle);
            ptr = (T*) IntPtr.Zero;
            data = null;
        }

        IEnumerator<T> IEnumerable<T>.GetEnumerator()
        {
            return GetEnumerator();
        }

        IEnumerator IEnumerable.GetEnumerator()
        {
            return GetEnumerator();
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public void Resize(int newSize)
        {
            UnsafeUtility.ReleaseGCObject(handle);
            var newArray = new T[newSize];
            int len = data.Length;
            Array.Copy(data, 0, newArray, 0, newSize >= len ? len : newSize);
            data = newArray;
            ptr = (T*) UnsafeUtility.PinGCArrayAndGetDataAddress(data, out handle);
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
            e.length = data.Length;
            e.ptr = ptr;
            e.current = default;
            e.index = 0;
            return e;
        }

        public struct Enumerator : IEnumerator<T>
        {
            public T* ptr;

            public int length;
            public T current;
            public int index;

            [MethodImpl(MethodImplOptions.AggressiveInlining)]
            public bool MoveNext()
            {
                if (index >= length)
                {
                    return false;
                }

                current = ptr[index++];

                return true;
            }

            public void Reset()
            {
                index = 0;
                current = default;
            }

            public T Current => current;
            object IEnumerator.Current => current;

            public void Dispose()
            {
            }
        }
    }
}