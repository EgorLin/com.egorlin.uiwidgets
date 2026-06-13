namespace EgorLin.UIWidgets.Pools
{
#if ECS_COMPILE_IL2CPP_OPTIONS
	[Unity.IL2CPP.CompilerServices.Il2CppSetOptionAttribute(Unity.IL2CPP.CompilerServices.Option.NullChecks, false),
	 Unity.IL2CPP.CompilerServices.Il2CppSetOptionAttribute(Unity.IL2CPP.CompilerServices.Option.ArrayBoundsChecks, false),
	 Unity.IL2CPP.CompilerServices.Il2CppSetOptionAttribute(Unity.IL2CPP.CompilerServices.Option.DivideByZeroChecks, false)]
#endif
    internal static class PoolClass<T> where T : class, new()
    {
        public static readonly PoolInternalBase Pool = new(() => new T(), null);

        public static T Spawn()
        {
            return (T) Pool.Spawn();
        }

        public static void Recycle(ref T instance)
        {
            Pool.Recycle(instance);
            instance = null;
        }

        public static void Recycle(T instance)
        {
            Pool.Recycle(instance);
        }
    }
    
#if ECS_COMPILE_IL2CPP_OPTIONS
	[Unity.IL2CPP.CompilerServices.Il2CppSetOptionAttribute(Unity.IL2CPP.CompilerServices.Option.NullChecks, false),
	 Unity.IL2CPP.CompilerServices.Il2CppSetOptionAttribute(Unity.IL2CPP.CompilerServices.Option.ArrayBoundsChecks, false),
	 Unity.IL2CPP.CompilerServices.Il2CppSetOptionAttribute(Unity.IL2CPP.CompilerServices.Option.DivideByZeroChecks, false)]
#endif
    internal static class PoolClassInterface<T> where T : class
    {
        private static readonly PoolInternalBase Pool = new(null, null);

        public static TReal Spawn<TReal>() where TReal : class, new()
        {
            var instance = (T) Pool.Spawn();
            if (instance == null)
            {
                return new TReal();
            }

            return (TReal) (object) instance;
        }

        public static void Recycle(ref T instance)
        {
            Pool.Recycle(instance);
            instance = null;
        }

        public static void Recycle(T instance)
        {
            Pool.Recycle(instance);
        }
    }
    
#if ECS_COMPILE_IL2CPP_OPTIONS
	[Unity.IL2CPP.CompilerServices.Il2CppSetOptionAttribute(Unity.IL2CPP.CompilerServices.Option.NullChecks, false),
	 Unity.IL2CPP.CompilerServices.Il2CppSetOptionAttribute(Unity.IL2CPP.CompilerServices.Option.ArrayBoundsChecks, false),
	 Unity.IL2CPP.CompilerServices.Il2CppSetOptionAttribute(Unity.IL2CPP.CompilerServices.Option.DivideByZeroChecks, false)]
#endif
    internal static class PoolClassCustom<T> where T : class
    {
        // ReSharper disable once StaticMemberInGenericType
        private static readonly PoolInternalBase PoolCustom = new(null, null);

        public static TCustom Spawn<TCustom>() where TCustom : class, T, new()
        {
            object objBase = PoolCustom.Spawn();
            if (objBase != null && !(objBase is TCustom))
            {
                PoolCustom.Recycle(objBase);
            }

            if (objBase as TCustom == null)
            {
                return new TCustom();
            }

            return (TCustom) objBase;
        }

        public static void Recycle<TCustom>(TCustom instance) where TCustom : class, T
        {
            PoolCustom.Recycle(instance);
        }
    }
    
#if ECS_COMPILE_IL2CPP_OPTIONS
	[Unity.IL2CPP.CompilerServices.Il2CppSetOptionAttribute(Unity.IL2CPP.CompilerServices.Option.NullChecks, false),
	 Unity.IL2CPP.CompilerServices.Il2CppSetOptionAttribute(Unity.IL2CPP.CompilerServices.Option.ArrayBoundsChecks, false),
	 Unity.IL2CPP.CompilerServices.Il2CppSetOptionAttribute(Unity.IL2CPP.CompilerServices.Option.DivideByZeroChecks, false)]
#endif
    internal static class PoolClassMainThread<T> where T : class, new()
    {
        private static readonly PoolInternalBaseNoStackPool Pool = new(() => new T(), null);

        public static T Spawn()
        {
            return (T) Pool.Spawn();
        }

        public static void Recycle(ref T instance)
        {
            Pool.Recycle(instance);
            instance = null;
        }

        public static void Recycle(T instance)
        {
            Pool.Recycle(instance);
        }
    }
}