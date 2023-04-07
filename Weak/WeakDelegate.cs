using System;
using System.Reflection;
using System.Reflection.Emit;

namespace CSToolbox.Weak
{
    /// <summary>
    /// A delegate which does not hold a reference to the object it's invoked on
    /// Slower than <see cref="Delegate"/> but faster than reflection.
    /// </summary>
    /// <typeparam name="RetType">Return type of the delegate.</typeparam>
    public sealed class WeakDelegate<RetType>
    {
        public delegate RetType CallType();
        private delegate RetType InvokeType(object target);

        private InvokeType Invoker { get; }
        private WeakReference Object { get; }

        /// <summary>
        /// Returns true if the target object is still alive and the delegate can be called.
        /// </summary>
        public bool IsAlive => Object.IsAlive;
        /// <summary>
        /// The target object of this delegate or null if it is no longer alive.
        /// </summary>
        public object? Target => Object.Target;
        /// <summary>
        /// The method this delegate invokes.
        /// </summary>
        public MethodInfo Method { get; }

        public WeakDelegate(CallType method)
        {
            if (method == null)
                throw new ArgumentNullException(nameof(method));
            else if (method.Method.IsStatic)
                throw new ArgumentException("Static methods are not supported by WeakDelegate", nameof(method));
            else if (method.Target == null)
                throw new ArgumentNullException(nameof(method), "Method's call target is null");
            
            Object = new WeakReference(method.Target);
            Method = method.Method;

            DynamicMethod invoker = new ("", typeof(RetType), new Type[] { typeof(object) }, typeof(WeakDelegate<RetType>));
            ILGenerator gen = invoker.GetILGenerator();

            gen.Emit(OpCodes.Ldarg_0);

            if (method.Method.IsVirtual)
                gen.Emit(OpCodes.Callvirt, method.Method);
            else
                gen.Emit(OpCodes.Call, method.Method);

            gen.Emit(OpCodes.Ret);
            Invoker = (InvokeType)invoker.CreateDelegate(typeof(InvokeType));
        }

        /// <summary>
        /// Invokes this delegate
        /// </summary>
        /// <returns>The result of invoking this delegate</returns>
        public RetType? Invoke() => Target != null ? Invoker(Target) : default;

        public static implicit operator WeakDelegate<RetType>(CallType target) => new(target);
        public static implicit operator CallType?(WeakDelegate<RetType> source) => (CallType?)(source.IsAlive ? Delegate.CreateDelegate(typeof(CallType), source.Target, source.Method) : null);

        public override bool Equals(object? obj) => 
            obj is WeakDelegate<RetType> other && other.Method.Equals(Method) && other.Target == Target;

        public override int GetHashCode() =>
            HashCode.Combine(Target, Method);
    }

    /// <summary>
    /// A delegate which does not hold a reference to the object it's invoked on
    /// Slower than <see cref="Delegate"/> but faster than reflection.
    /// </summary>
    /// <typeparam name="RetType">Return type of the delegate.</typeparam>
    /// <typeparam name="Arg1">Type of the first argument of the delegate.</typeparam>
    public sealed class WeakDelegate<RetType, Arg1>
    {
        public delegate RetType CallType(Arg1 arg1);
        private delegate RetType InvokeType(object target, Arg1 arg1);

        private InvokeType Invoker { get; }
        private WeakReference Object { get; }

        /// <summary>
        /// Returns true if the target object is still alive and the delegate can be called.
        /// </summary>
        public bool IsAlive => Object.IsAlive;
        /// <summary>
        /// The target object of this delegate or null if it is no longer alive.
        /// </summary>
        public object? Target => Object.Target;
        /// <summary>
        /// The method this delegate invokes.
        /// </summary>
        public MethodInfo Method { get; }

        public WeakDelegate(CallType method)
        {
            if (method == null)
                throw new ArgumentNullException(nameof(method));
            else if (method.Method.IsStatic)
                throw new ArgumentException("Static methods are not supported by WeakDelegate", nameof(method));
            else if (method.Target == null)
                throw new ArgumentNullException(nameof(method), "Method's call target is null");
            
            Object = new WeakReference(method.Target);
            Method = method.Method;

            DynamicMethod invoker = new ("", typeof(RetType), new Type[] { typeof(object), typeof(Arg1) }, typeof(WeakDelegate<RetType, Arg1>));
            ILGenerator gen = invoker.GetILGenerator();

            gen.Emit(OpCodes.Ldarg_0);
            gen.Emit(OpCodes.Ldarg_1);

            if (method.Method.IsVirtual)
                gen.Emit(OpCodes.Callvirt, method.Method);
            else
                gen.Emit(OpCodes.Call, method.Method);

            gen.Emit(OpCodes.Ret);
            Invoker = (InvokeType)invoker.CreateDelegate(typeof(InvokeType));
        }

        /// <summary>
        /// Invokes this delegate
        /// </summary>
        /// <returns>The result of invoking this delegate</returns>
        public RetType? Invoke(Arg1 arg1) => Object.Target != null ? Invoker(Object.Target, arg1) : default;

        public static implicit operator WeakDelegate<RetType, Arg1>(CallType target) => new(target);
        public static implicit operator CallType?(WeakDelegate<RetType, Arg1> source) => (CallType?)(source.IsAlive ? Delegate.CreateDelegate(typeof(CallType), source.Target, source.Method) : null);

        public override bool Equals(object? obj) => 
            obj is WeakDelegate<RetType, Arg1> other && other.Method.Equals(Method) && other.Target == Target;

        public override int GetHashCode() =>
            HashCode.Combine(Target, Method);
    }

    /// <summary>
    /// A delegate which does not hold a reference to the object it's invoked on
    /// Slower than <see cref="Delegate"/> but faster than reflection.
    /// </summary>
    /// <typeparam name="RetType">Return type of the delegate.</typeparam>
    /// <typeparam name="Arg1">Type of the first argument of the delegate.</typeparam>
    /// <typeparam name="Arg2">Type of the second argument of the delegate.</typeparam>
    public sealed class WeakDelegate<RetType, Arg1, Arg2>
    {
        public delegate RetType CallType(Arg1 arg1, Arg2 arg2);
        private delegate RetType InvokeType(object target, Arg1 arg1, Arg2 arg2);

        private InvokeType Invoker { get; }
        private WeakReference Object { get; }

        /// <summary>
        /// Returns true if the target object is still alive and the delegate can be called.
        /// </summary>
        public bool IsAlive => Object.IsAlive;
        /// <summary>
        /// The target object of this delegate or null if it is no longer alive.
        /// </summary>
        public object? Target => Object.Target;
        /// <summary>
        /// The method this delegate invokes.
        /// </summary>
        public MethodInfo Method { get; }

        public WeakDelegate(CallType method)
        {
            if (method == null)
                throw new ArgumentNullException(nameof(method));
            else if (method.Method.IsStatic)
                throw new ArgumentException("Static methods are not supported by WeakDelegate", nameof(method));
            else if (method.Target == null)
                throw new ArgumentNullException(nameof(method), "Method's call target is null");
            
            Object = new WeakReference(method.Target);
            Method = method.Method;

            DynamicMethod invoker = new ("", typeof(RetType), new Type[] { typeof(object), typeof(Arg1), typeof(Arg2) }, typeof(WeakDelegate<RetType, Arg1, Arg2>));
            ILGenerator gen = invoker.GetILGenerator();

            gen.Emit(OpCodes.Ldarg_0);
            gen.Emit(OpCodes.Ldarg_1);
            gen.Emit(OpCodes.Ldarg_2);

            if (method.Method.IsVirtual)
                gen.Emit(OpCodes.Callvirt, method.Method);
            else
                gen.Emit(OpCodes.Call, method.Method);

            gen.Emit(OpCodes.Ret);
            Invoker = (InvokeType)invoker.CreateDelegate(typeof(InvokeType));
        }

        /// <summary>
        /// Invokes this delegate
        /// </summary>
        /// <returns>The result of invoking this delegate</returns>
        public RetType? Invoke(Arg1 arg1, Arg2 arg2) => Object.Target != null ? Invoker(Object.Target, arg1, arg2) : default;

        public static implicit operator WeakDelegate<RetType, Arg1, Arg2>(CallType target) => new(target);
        public static implicit operator CallType?(WeakDelegate<RetType, Arg1, Arg2> source) => (CallType?)(source.IsAlive ? Delegate.CreateDelegate(typeof(CallType), source.Target, source.Method) : null);

        public override bool Equals(object? obj) => 
            obj is WeakDelegate<RetType, Arg1, Arg2> other && other.Method.Equals(Method) && other.Target == Target;

        public override int GetHashCode() =>
            HashCode.Combine(Target, Method);
    }

    /// <summary>
    /// A delegate which does not hold a reference to the object it's invoked on
    /// Slower than <see cref="Delegate"/> but faster than reflection.
    /// </summary>
    /// <typeparam name="RetType">Return type of the delegate.</typeparam>
    /// <typeparam name="Arg1">Type of the first argument of the delegate.</typeparam>
    /// <typeparam name="Arg2">Type of the second argument of the delegate.</typeparam>
    /// <typeparam name="Arg3">Type of the third argument of the delegate.</typeparam>
    public sealed class WeakDelegate<RetType, Arg1, Arg2, Arg3>
    {
        public delegate RetType CallType(Arg1 arg1, Arg2 arg2, Arg3 arg3);
        private delegate RetType InvokeType(object target, Arg1 arg1, Arg2 arg2, Arg3 arg3);

        private InvokeType Invoker { get; }
        private WeakReference Object { get; }

        /// <summary>
        /// Returns true if the target object is still alive and the delegate can be called.
        /// </summary>
        public bool IsAlive => Object.IsAlive;
        /// <summary>
        /// The target object of this delegate or null if it is no longer alive.
        /// </summary>
        public object? Target => Object.Target;
        /// <summary>
        /// The method this delegate invokes.
        /// </summary>
        public MethodInfo Method { get; }

        public WeakDelegate(CallType method)
        {
            if (method == null)
                throw new ArgumentNullException(nameof(method));
            else if (method.Method.IsStatic)
                throw new ArgumentException("Static methods are not supported by WeakDelegate", nameof(method));
            else if (method.Target == null)
                throw new ArgumentNullException(nameof(method), "Method's call target is null");
            
            Object = new WeakReference(method.Target);
            Method = method.Method;

            DynamicMethod invoker = new ("", typeof(RetType), new Type[] { typeof(object), typeof(Arg1), typeof(Arg2), typeof(Arg3) }, typeof(WeakDelegate<RetType, Arg1, Arg2, Arg3>));
            ILGenerator gen = invoker.GetILGenerator();

            gen.Emit(OpCodes.Ldarg_0);
            gen.Emit(OpCodes.Ldarg_1);
            gen.Emit(OpCodes.Ldarg_2);
            gen.Emit(OpCodes.Ldarg_3);

            if (method.Method.IsVirtual)
                gen.Emit(OpCodes.Callvirt, method.Method);
            else
                gen.Emit(OpCodes.Call, method.Method);

            gen.Emit(OpCodes.Ret);
            Invoker = (InvokeType)invoker.CreateDelegate(typeof(InvokeType));
        }

        /// <summary>
        /// Invokes this delegate
        /// </summary>
        /// <returns>The result of invoking this delegate</returns>
        public RetType? Invoke(Arg1 arg1, Arg2 arg2, Arg3 arg3) => Object.Target != null ? Invoker(Object.Target, arg1, arg2, arg3) : default;

        public static implicit operator WeakDelegate<RetType, Arg1, Arg2, Arg3>(CallType target) => new(target);
        public static implicit operator CallType?(WeakDelegate<RetType, Arg1, Arg2, Arg3> source) => (CallType?)(source.IsAlive ? Delegate.CreateDelegate(typeof(CallType), source.Target, source.Method) : null);

        public override bool Equals(object? obj) => 
            obj is WeakDelegate<RetType, Arg1, Arg2, Arg3> other && other.Method.Equals(Method) && other.Target == Target;

        public override int GetHashCode() =>
            HashCode.Combine(Target, Method);
    }

    /// <summary>
    /// A delegate which does not hold a reference to the object it's invoked on
    /// Slower than <see cref="Delegate"/> but faster than reflection.
    /// </summary>
    /// <typeparam name="RetType">Return type of the delegate.</typeparam>
    /// <typeparam name="Arg1">Type of the first argument of the delegate.</typeparam>
    /// <typeparam name="Arg2">Type of the second argument of the delegate.</typeparam>
    /// <typeparam name="Arg3">Type of the third argument of the delegate.</typeparam>
    /// <typeparam name="Arg4">Type of the fourth argument of the delegate.</typeparam>
    public sealed class WeakDelegate<RetType, Arg1, Arg2, Arg3, Arg4>
    {
        public delegate RetType CallType(Arg1 arg1, Arg2 arg2, Arg3 arg3, Arg4 arg4);
        private delegate RetType InvokeType(object target, Arg1 arg1, Arg2 arg2, Arg3 arg3, Arg4 arg4);

        private InvokeType Invoker { get; }
        private WeakReference Object { get; }

        /// <summary>
        /// Returns true if the target object is still alive and the delegate can be called.
        /// </summary>
        public bool IsAlive => Object.IsAlive;
        /// <summary>
        /// The target object of this delegate or null if it is no longer alive.
        /// </summary>
        public object? Target => Object.Target;
        /// <summary>
        /// The method this delegate invokes.
        /// </summary>
        public MethodInfo Method { get; }

        public WeakDelegate(CallType method)
        {
            if (method == null)
                throw new ArgumentNullException(nameof(method));
            else if (method.Method.IsStatic)
                throw new ArgumentException("Static methods are not supported by WeakDelegate", nameof(method));
            else if (method.Target == null)
                throw new ArgumentNullException(nameof(method), "Method's call target is null");
            
            Object = new WeakReference(method.Target);
            Method = method.Method;

            DynamicMethod invoker = new ("", typeof(RetType), new Type[] { typeof(object), typeof(Arg1), typeof(Arg2), typeof(Arg3), typeof(Arg4) }, typeof(WeakDelegate<RetType, Arg1, Arg2, Arg3, Arg4>));
            ILGenerator gen = invoker.GetILGenerator();

            gen.Emit(OpCodes.Ldarg_0);
            gen.Emit(OpCodes.Ldarg_1);
            gen.Emit(OpCodes.Ldarg_2);
            gen.Emit(OpCodes.Ldarg_3);
            gen.Emit(OpCodes.Ldarg, 4);

            if (method.Method.IsVirtual)
                gen.Emit(OpCodes.Callvirt, method.Method);
            else
                gen.Emit(OpCodes.Call, method.Method);

            gen.Emit(OpCodes.Ret);
            Invoker = (InvokeType)invoker.CreateDelegate(typeof(InvokeType));
        }

        /// <summary>
        /// Invokes this delegate
        /// </summary>
        /// <returns>The result of invoking this delegate</returns>
        public RetType? Invoke(Arg1 arg1, Arg2 arg2, Arg3 arg3, Arg4 arg4) => Object.Target != null ? Invoker(Object.Target, arg1, arg2, arg3, arg4) : default;

        public static implicit operator WeakDelegate<RetType, Arg1, Arg2, Arg3, Arg4>(CallType target) => new(target);
        public static implicit operator CallType?(WeakDelegate<RetType, Arg1, Arg2, Arg3, Arg4> source) => (CallType?)(source.IsAlive ? Delegate.CreateDelegate(typeof(CallType), source.Target, source.Method) : null);

        public override bool Equals(object? obj) => 
            obj is WeakDelegate<RetType, Arg1, Arg2, Arg3, Arg4> other && other.Method.Equals(Method) && other.Target == Target;

        public override int GetHashCode() =>
            HashCode.Combine(Target, Method);
    }

    /// <summary>
    /// A delegate which does not hold a reference to the object it's invoked on
    /// Slower than <see cref="Delegate"/> but faster than reflection.
    /// </summary>
    /// <typeparam name="RetType">Return type of the delegate.</typeparam>
    /// <typeparam name="Arg1">Type of the first argument of the delegate.</typeparam>
    /// <typeparam name="Arg2">Type of the second argument of the delegate.</typeparam>
    /// <typeparam name="Arg3">Type of the third argument of the delegate.</typeparam>
    /// <typeparam name="Arg4">Type of the fourth argument of the delegate.</typeparam>
    /// <typeparam name="Arg5">Type of the fifth argument of the delegate.</typeparam>
    public sealed class WeakDelegate<RetType, Arg1, Arg2, Arg3, Arg4, Arg5>
    {
        public delegate RetType CallType(Arg1 arg1, Arg2 arg2, Arg3 arg3, Arg4 arg4, Arg5 arg5);
        private delegate RetType InvokeType(object target, Arg1 arg1, Arg2 arg2, Arg3 arg3, Arg4 arg4, Arg5 arg5);

        private InvokeType Invoker { get; }
        private WeakReference Object { get; }

        /// <summary>
        /// Returns true if the target object is still alive and the delegate can be called.
        /// </summary>
        public bool IsAlive => Object.IsAlive;
        /// <summary>
        /// The target object of this delegate or null if it is no longer alive.
        /// </summary>
        public object? Target => Object.Target;
        /// <summary>
        /// The method this delegate invokes.
        /// </summary>
        public MethodInfo Method { get; }

        public WeakDelegate(CallType method)
        {
            if (method == null)
                throw new ArgumentNullException(nameof(method));
            else if (method.Method.IsStatic)
                throw new ArgumentException("Static methods are not supported by WeakDelegate", nameof(method));
            else if (method.Target == null)
                throw new ArgumentNullException(nameof(method), "Method's call target is null");
            
            Object = new WeakReference(method.Target);
            Method = method.Method;

            DynamicMethod invoker = new ("", typeof(RetType), new Type[] { typeof(object), typeof(Arg1), typeof(Arg2), typeof(Arg3), typeof(Arg4), typeof(Arg5) }, typeof(WeakDelegate<RetType, Arg1, Arg2, Arg3, Arg4, Arg5>));
            ILGenerator gen = invoker.GetILGenerator();

            gen.Emit(OpCodes.Ldarg_0);
            gen.Emit(OpCodes.Ldarg_1);
            gen.Emit(OpCodes.Ldarg_2);
            gen.Emit(OpCodes.Ldarg_3);
            gen.Emit(OpCodes.Ldarg, 4);
            gen.Emit(OpCodes.Ldarg, 5);

            if (method.Method.IsVirtual)
                gen.Emit(OpCodes.Callvirt, method.Method);
            else
                gen.Emit(OpCodes.Call, method.Method);

            gen.Emit(OpCodes.Ret);
            Invoker = (InvokeType)invoker.CreateDelegate(typeof(InvokeType));
        }

        /// <summary>
        /// Invokes this delegate
        /// </summary>
        /// <returns>The result of invoking this delegate</returns>
        public RetType? Invoke(Arg1 arg1, Arg2 arg2, Arg3 arg3, Arg4 arg4, Arg5 arg5) => Object.Target != null ? Invoker(Object.Target, arg1, arg2, arg3, arg4, arg5) : default;

        public static implicit operator WeakDelegate<RetType, Arg1, Arg2, Arg3, Arg4, Arg5>(CallType target) => new(target);
        public static implicit operator CallType?(WeakDelegate<RetType, Arg1, Arg2, Arg3, Arg4, Arg5> source) => (CallType?)(source.IsAlive ? Delegate.CreateDelegate(typeof(CallType), source.Target, source.Method) : null);

        public override bool Equals(object? obj) => 
            obj is WeakDelegate<RetType, Arg1, Arg2, Arg3, Arg4, Arg5> other && other.Method.Equals(Method) && other.Target == Target;

        public override int GetHashCode() =>
            HashCode.Combine(Target, Method);
    }

    /// <summary>
    /// A delegate which does not hold a reference to the object it's invoked on
    /// Slower than <see cref="Delegate"/> but faster than reflection.
    /// </summary>
    public sealed class WeakAction
    {
        public delegate void CallType();
        private delegate void InvokeType(object target);

        private InvokeType Invoker { get; }
        private WeakReference Object { get; }

        /// <summary>
        /// Returns true if the target object is still alive and the delegate can be called.
        /// </summary>
        public bool IsAlive => Object.IsAlive;
        /// <summary>
        /// The target object of this delegate or null if it is no longer alive.
        /// </summary>
        public object? Target => Object.Target;
        /// <summary>
        /// The method this delegate invokes.
        /// </summary>
        public MethodInfo Method { get; }

        public WeakAction(CallType method)
        {
            if (method == null)
                throw new ArgumentNullException(nameof(method));
            else if (method.Method.IsStatic)
                throw new ArgumentException("Static methods are not supported by WeakDelegate", nameof(method));
            else if (method.Target == null)
                throw new ArgumentNullException(nameof(method), "Method's call target is null");
            
            Object = new WeakReference(method.Target);
            Method = method.Method;

            DynamicMethod invoker = new ("", null, new Type[] { typeof(object) }, typeof(WeakAction));
            ILGenerator gen = invoker.GetILGenerator();

            gen.Emit(OpCodes.Ldarg_0);

            if (method.Method.IsVirtual)
                gen.Emit(OpCodes.Callvirt, method.Method);
            else
                gen.Emit(OpCodes.Call, method.Method);

            gen.Emit(OpCodes.Ret);
            Invoker = (InvokeType)invoker.CreateDelegate(typeof(InvokeType));
        }

        /// <summary>
        /// Invokes this delegate
        /// </summary>
        /// <returns>The result of invoking this delegate</returns>
        public void Invoke()
        {
            if (Object.Target != null)
                Invoker(Object.Target);
        }

        public static implicit operator WeakAction(CallType target) => new(target);
        public static implicit operator CallType?(WeakAction source) => (CallType?)(source.IsAlive ? Delegate.CreateDelegate(typeof(CallType), source.Target, source.Method) : null);

        public override bool Equals(object? obj) => 
            obj is WeakAction other && other.Method.Equals(Method) && other.Target == Target;

        public override int GetHashCode() =>
            HashCode.Combine(Target, Method);
    }

    /// <summary>
    /// A delegate which does not hold a reference to the object it's invoked on
    /// Slower than <see cref="Delegate"/> but faster than reflection.
    /// </summary>
    /// <typeparam name="Arg1">The type of the first argument if the delegate.</typeparam>
    public sealed class WeakAction<Arg1>
    {
        public delegate void CallType(Arg1 arg1);
        private delegate void InvokeType(object target, Arg1 arg1);

        private InvokeType Invoker { get; }
        private WeakReference Object { get; }

        /// <summary>
        /// Returns true if the target object is still alive and the delegate can be called.
        /// </summary>
        public bool IsAlive => Object.IsAlive;
        /// <summary>
        /// The target object of this delegate or null if it is no longer alive.
        /// </summary>
        public object? Target => Object.Target;
        /// <summary>
        /// The method this delegate invokes.
        /// </summary>
        public MethodInfo Method { get; }

        public WeakAction(CallType method)
        {
            if (method == null)
                throw new ArgumentNullException(nameof(method));
            else if (method.Method.IsStatic)
                throw new ArgumentException("Static methods are not supported by WeakDelegate", nameof(method));
            else if (method.Target == null)
                throw new ArgumentNullException(nameof(method), "Method's call target is null");
            
            Object = new WeakReference(method.Target);
            Method = method.Method;

            DynamicMethod invoker = new ("", null, new Type[] { typeof(object), typeof(Arg1) }, typeof(WeakAction<Arg1>));
            ILGenerator gen = invoker.GetILGenerator();

            gen.Emit(OpCodes.Ldarg_0);
            gen.Emit(OpCodes.Ldarg_1);

            if (method.Method.IsVirtual)
                gen.Emit(OpCodes.Callvirt, method.Method);
            else
                gen.Emit(OpCodes.Call, method.Method);

            gen.Emit(OpCodes.Ret);
            Invoker = (InvokeType)invoker.CreateDelegate(typeof(InvokeType));
        }

        /// <summary>
        /// Invokes this delegate
        /// </summary>
        /// <returns>The result of invoking this delegate</returns>
        public void Invoke(Arg1 arg1)
        {
            if (Object.Target != null)
                Invoker(Object.Target, arg1);
        }

        public static implicit operator WeakAction<Arg1>(CallType target) => new(target);
        public static implicit operator CallType?(WeakAction<Arg1> source) => (CallType?)(source.IsAlive ? Delegate.CreateDelegate(typeof(CallType), source.Target, source.Method) : null);

        public override bool Equals(object? obj) => 
            obj is WeakAction<Arg1> other && other.Method.Equals(Method) && other.Target == Target;

        public override int GetHashCode() =>
            HashCode.Combine(Target, Method);
    }

    /// <summary>
    /// A delegate which does not hold a reference to the object it's invoked on
    /// Slower than <see cref="Delegate"/> but faster than reflection.
    /// </summary>
    /// <typeparam name="Arg1">The type of the first argument if the delegate.</typeparam>
    /// <typeparam name="Arg2">The type of the second argument if the delegate.</typeparam>
    public sealed class WeakAction<Arg1, Arg2>
    {
        public delegate void CallType(Arg1 arg1, Arg2 arg2);
        private delegate void InvokeType(object target, Arg1 arg1, Arg2 arg2);

        private InvokeType Invoker { get; }
        private WeakReference Object { get; }

        /// <summary>
        /// Returns true if the target object is still alive and the delegate can be called.
        /// </summary>
        public bool IsAlive => Object.IsAlive;
        /// <summary>
        /// The target object of this delegate or null if it is no longer alive.
        /// </summary>
        public object? Target => Object.Target;
        /// <summary>
        /// The method this delegate invokes.
        /// </summary>
        public MethodInfo Method { get; }

        public WeakAction(CallType method)
        {
            if (method == null)
                throw new ArgumentNullException(nameof(method));
            else if (method.Method.IsStatic)
                throw new ArgumentException("Static methods are not supported by WeakDelegate", nameof(method));
            else if (method.Target == null)
                throw new ArgumentNullException(nameof(method), "Method's call target is null");
            
            Object = new WeakReference(method.Target);
            Method = method.Method;

            DynamicMethod invoker = new ("", null, new Type[] { typeof(object), typeof(Arg1), typeof(Arg2) }, typeof(WeakAction<Arg1, Arg2>));
            ILGenerator gen = invoker.GetILGenerator();

            gen.Emit(OpCodes.Ldarg_0);
            gen.Emit(OpCodes.Ldarg_1);
            gen.Emit(OpCodes.Ldarg_2);

            if (method.Method.IsVirtual)
                gen.Emit(OpCodes.Callvirt, method.Method);
            else
                gen.Emit(OpCodes.Call, method.Method);

            gen.Emit(OpCodes.Ret);
            Invoker = (InvokeType)invoker.CreateDelegate(typeof(InvokeType));
        }

        /// <summary>
        /// Invokes this delegate
        /// </summary>
        /// <returns>The result of invoking this delegate</returns>
        public void Invoke(Arg1 arg1, Arg2 arg2)
        {
            if (Object.Target != null)
                Invoker(Object.Target, arg1, arg2);
        }

        public static implicit operator WeakAction<Arg1, Arg2>(CallType target) => new(target);
        public static implicit operator CallType?(WeakAction<Arg1, Arg2> source) => (CallType?)(source.IsAlive ? Delegate.CreateDelegate(typeof(CallType), source.Target, source.Method) : null);

        public override bool Equals(object? obj) => 
            obj is WeakAction<Arg1, Arg2> other && other.Method.Equals(Method) && other.Target == Target;

        public override int GetHashCode() =>
            HashCode.Combine(Target, Method);
    }

    /// <summary>
    /// A delegate which does not hold a reference to the object it's invoked on
    /// Slower than <see cref="Delegate"/> but faster than reflection.
    /// </summary>
    /// <typeparam name="Arg1">The type of the first argument if the delegate.</typeparam>
    /// <typeparam name="Arg2">The type of the second argument if the delegate.</typeparam>
    /// <typeparam name="Arg3">The type of the third argument if the delegate.</typeparam>
    public sealed class WeakAction<Arg1, Arg2, Arg3>
    {
        public delegate void CallType(Arg1 arg1, Arg2 arg2, Arg3 arg3);
        private delegate void InvokeType(object target, Arg1 arg1, Arg2 arg2, Arg3 arg3);

        private InvokeType Invoker { get; }
        private WeakReference Object { get; }

        /// <summary>
        /// Returns true if the target object is still alive and the delegate can be called.
        /// </summary>
        public bool IsAlive => Object.IsAlive;
        /// <summary>
        /// The target object of this delegate or null if it is no longer alive.
        /// </summary>
        public object? Target => Object.Target;
        /// <summary>
        /// The method this delegate invokes.
        /// </summary>
        public MethodInfo Method { get; }

        public WeakAction(CallType method)
        {
            if (method == null)
                throw new ArgumentNullException(nameof(method));
            else if (method.Method.IsStatic)
                throw new ArgumentException("Static methods are not supported by WeakDelegate", nameof(method));
            else if (method.Target == null)
                throw new ArgumentNullException(nameof(method), "Method's call target is null");
            
            Object = new WeakReference(method.Target);
            Method = method.Method;

            DynamicMethod invoker = new ("", null, new Type[] { typeof(object), typeof(Arg1), typeof(Arg2), typeof(Arg3) }, typeof(WeakAction<Arg1, Arg2, Arg3>));
            ILGenerator gen = invoker.GetILGenerator();

            gen.Emit(OpCodes.Ldarg_0);
            gen.Emit(OpCodes.Ldarg_1);
            gen.Emit(OpCodes.Ldarg_2);
            gen.Emit(OpCodes.Ldarg_3);

            if (method.Method.IsVirtual)
                gen.Emit(OpCodes.Callvirt, method.Method);
            else
                gen.Emit(OpCodes.Call, method.Method);

            gen.Emit(OpCodes.Ret);
            Invoker = (InvokeType)invoker.CreateDelegate(typeof(InvokeType));
        }

        /// <summary>
        /// Invokes this delegate
        /// </summary>
        /// <returns>The result of invoking this delegate</returns>
        public void Invoke(Arg1 arg1, Arg2 arg2, Arg3 arg3)
        {
            if (Object.Target != null)
                Invoker(Object.Target, arg1, arg2, arg3);
        }

        public static implicit operator WeakAction<Arg1, Arg2, Arg3>(CallType target) => new(target);
        public static implicit operator CallType?(WeakAction<Arg1, Arg2, Arg3> source) => (CallType?)(source.IsAlive ? Delegate.CreateDelegate(typeof(CallType), source.Target, source.Method) : null);

        public override bool Equals(object? obj) => 
            obj is WeakAction<Arg1, Arg2, Arg3> other && other.Method.Equals(Method) && other.Target == Target;

        public override int GetHashCode() =>
            HashCode.Combine(Target, Method);
    }

    /// <summary>
    /// A delegate which does not hold a reference to the object it's invoked on
    /// Slower than <see cref="Delegate"/> but faster than reflection.
    /// </summary>
    /// <typeparam name="Arg1">The type of the first argument if the delegate.</typeparam>
    /// <typeparam name="Arg2">The type of the second argument if the delegate.</typeparam>
    /// <typeparam name="Arg3">The type of the third argument if the delegate.</typeparam>
    /// <typeparam name="Arg4">The type of the fourth argument if the delegate.</typeparam>
    public sealed class WeakAction<Arg1, Arg2, Arg3, Arg4>
    {
        public delegate void CallType(Arg1 arg1, Arg2 arg2, Arg3 arg3, Arg4 arg4);
        private delegate void InvokeType(object target, Arg1 arg1, Arg2 arg2, Arg3 arg3, Arg4 arg4);

        private InvokeType Invoker { get; }
        private WeakReference Object { get; }

        /// <summary>
        /// Returns true if the target object is still alive and the delegate can be called.
        /// </summary>
        public bool IsAlive => Object.IsAlive;
        /// <summary>
        /// The target object of this delegate or null if it is no longer alive.
        /// </summary>
        public object? Target => Object.Target;
        /// <summary>
        /// The method this delegate invokes.
        /// </summary>
        public MethodInfo Method { get; }

        public WeakAction(CallType method)
        {
            if (method == null)
                throw new ArgumentNullException(nameof(method));
            else if (method.Method.IsStatic)
                throw new ArgumentException("Static methods are not supported by WeakDelegate", nameof(method));
            else if (method.Target == null)
                throw new ArgumentNullException(nameof(method), "Method's call target is null");
            
            Object = new WeakReference(method.Target);
            Method = method.Method;

            DynamicMethod invoker = new ("", null, new Type[] { typeof(object), typeof(Arg1), typeof(Arg2), typeof(Arg3), typeof(Arg4) }, typeof(WeakAction<Arg1, Arg2, Arg3, Arg4>));
            ILGenerator gen = invoker.GetILGenerator();

            gen.Emit(OpCodes.Ldarg_0);
            gen.Emit(OpCodes.Ldarg_1);
            gen.Emit(OpCodes.Ldarg_2);
            gen.Emit(OpCodes.Ldarg_3);
            gen.Emit(OpCodes.Ldarg, 4);

            if (method.Method.IsVirtual)
                gen.Emit(OpCodes.Callvirt, method.Method);
            else
                gen.Emit(OpCodes.Call, method.Method);

            gen.Emit(OpCodes.Ret);
            Invoker = (InvokeType)invoker.CreateDelegate(typeof(InvokeType));
        }

        /// <summary>
        /// Invokes this delegate
        /// </summary>
        /// <returns>The result of invoking this delegate</returns>
        public void Invoke(Arg1 arg1, Arg2 arg2, Arg3 arg3, Arg4 arg4)
        {
            if (Object.Target != null)
                Invoker(Object.Target, arg1, arg2, arg3, arg4);
        }

        public static implicit operator WeakAction<Arg1, Arg2, Arg3, Arg4>(CallType target) => new(target);
        public static implicit operator CallType?(WeakAction<Arg1, Arg2, Arg3, Arg4> source) => (CallType?)(source.IsAlive ? Delegate.CreateDelegate(typeof(CallType), source.Target, source.Method) : null);

        public override bool Equals(object? obj) => 
            obj is WeakAction<Arg1, Arg2, Arg3, Arg4> other && other.Method.Equals(Method) && other.Target == Target;

        public override int GetHashCode() =>
            HashCode.Combine(Target, Method);
    }

    /// <summary>
    /// A delegate which does not hold a reference to the object it's invoked on
    /// Slower than <see cref="Delegate"/> but faster than reflection.
    /// </summary>
    /// <typeparam name="Arg1">The type of the first argument if the delegate.</typeparam>
    /// <typeparam name="Arg2">The type of the second argument if the delegate.</typeparam>
    /// <typeparam name="Arg3">The type of the third argument if the delegate.</typeparam>
    /// <typeparam name="Arg4">The type of the fourth argument if the delegate.</typeparam>
    /// <typeparam name="Arg5">The type of the fifth argument if the delegate.</typeparam>
    public sealed class WeakAction<Arg1, Arg2, Arg3, Arg4, Arg5>
    {
        public delegate void CallType(Arg1 arg1, Arg2 arg2, Arg3 arg3, Arg4 arg4, Arg5 arg5);
        private delegate void InvokeType(object target, Arg1 arg1, Arg2 arg2, Arg3 arg3, Arg4 arg4, Arg5 arg5);

        private InvokeType Invoker { get; }
        private WeakReference Object { get; }

        /// <summary>
        /// Returns true if the target object is still alive and the delegate can be called.
        /// </summary>
        public bool IsAlive => Object.IsAlive;
        /// <summary>
        /// The target object of this delegate or null if it is no longer alive.
        /// </summary>
        public object? Target => Object.Target;
        /// <summary>
        /// The method this delegate invokes.
        /// </summary>
        public MethodInfo Method { get; }

        public WeakAction(CallType method)
        {
            if (method == null)
                throw new ArgumentNullException(nameof(method));
            else if (method.Method.IsStatic)
                throw new ArgumentException("Static methods are not supported by WeakDelegate", nameof(method));
            else if (method.Target == null)
                throw new ArgumentNullException(nameof(method), "Method's call target is null");
            
            Object = new WeakReference(method.Target);
            Method = method.Method;

            DynamicMethod invoker = new ("", null, new Type[] { typeof(object), typeof(Arg1), typeof(Arg2), typeof(Arg3), typeof(Arg4), typeof(Arg5) }, typeof(WeakAction<Arg1, Arg2, Arg3, Arg4, Arg5>));
            ILGenerator gen = invoker.GetILGenerator();

            gen.Emit(OpCodes.Ldarg_0);
            gen.Emit(OpCodes.Ldarg_1);
            gen.Emit(OpCodes.Ldarg_2);
            gen.Emit(OpCodes.Ldarg_3);
            gen.Emit(OpCodes.Ldarg, 4);
            gen.Emit(OpCodes.Ldarg, 5);

            if (method.Method.IsVirtual)
                gen.Emit(OpCodes.Callvirt, method.Method);
            else
                gen.Emit(OpCodes.Call, method.Method);

            gen.Emit(OpCodes.Ret);
            Invoker = (InvokeType)invoker.CreateDelegate(typeof(InvokeType));
        }

        /// <summary>
        /// Invokes this delegate
        /// </summary>
        /// <returns>The result of invoking this delegate</returns>
        public void Invoke(Arg1 arg1, Arg2 arg2, Arg3 arg3, Arg4 arg4, Arg5 arg5)
        {
            if (Object.Target != null)
                Invoker(Object.Target, arg1, arg2, arg3, arg4, arg5);
        }

        public static implicit operator WeakAction<Arg1, Arg2, Arg3, Arg4, Arg5>(CallType target) => new(target);
        public static implicit operator CallType?(WeakAction<Arg1, Arg2, Arg3, Arg4, Arg5> source) => (CallType?)(source.IsAlive ? Delegate.CreateDelegate(typeof(CallType), source.Target, source.Method) : null);

        public override bool Equals(object? obj) => 
            obj is WeakAction<Arg1, Arg2, Arg3, Arg4, Arg5> other && other.Method.Equals(Method) && other.Target == Target;

        public override int GetHashCode() =>
            HashCode.Combine(Target, Method);
    }
}