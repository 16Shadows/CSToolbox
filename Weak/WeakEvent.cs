using System;
using System.Collections.Generic;

namespace CSToolbox.Weak
{
    /// <summary>
    /// An event using <see cref="WeakAction"/> to hold its subscribers.
    /// </summary>
    public sealed class WeakEvent
    {
        private List<WeakAction> DelegateList { get; } = new();

        /// <summary>
        /// Invokes the event with provided arguments.
        /// </summary>
        /// <param name="arg1">The first argument of the event.</param>
        public void Invoke()
        {
            WeakAction target;
            for (int i = 0; i < DelegateList.Count;)
            {
                target = DelegateList[i];
                if (target.IsAlive)
                {
                    target.Invoke();
                    i++;
                }
                else
                {
                    DelegateList[i] = DelegateList[DelegateList.Count - 1];
                    DelegateList.RemoveAt(DelegateList.Count - 1);
                }
            }
        }

        /// <summary>
        /// Adds a subscriber to this event.
        /// </summary>
        /// <param name="subscriber">The subscriber to add.</param>
        /// <exception cref="ArgumentNullException">Can be thrown if the subscriber is null.</exception>
        public void Subscribe(WeakAction subscriber) =>
            DelegateList.Add(subscriber ?? throw new ArgumentNullException(nameof(subscriber)));

        /// <summary>
        /// Adds a subscriber to this event.
        /// </summary>
        /// <param name="subscriber">The subscriber to add.</param>
        /// <exception cref="ArgumentNullException">Can be thrown if the subscriber is null.</exception>
        public void Subscribe(WeakAction.CallType subscriber) =>
            Subscribe(new WeakAction(subscriber));

        /// <summary>
        /// Removes a subscriber from this event.
        /// </summary>
        /// <param name="subscriber">The subscriber to remove.</param>
        /// <exception cref="ArgumentNullException">Can be thrown if the subscriber is null.</exception>
        public void Unsubscribe(WeakAction subscriber)
        {
            if (subscriber == null)
                throw new ArgumentNullException(nameof(subscriber));

            int id = DelegateList.IndexOf(subscriber);
            if (id != -1)
            {
                DelegateList[id] = DelegateList[DelegateList.Count - 1];
                DelegateList.RemoveAt(DelegateList.Count - 1);
            }
        }

        /// <summary>
        /// Removes a subscriber from this event.
        /// </summary>
        /// <param name="subscriber">The subscriber to remove.</param>
        /// <exception cref="ArgumentNullException">Can be thrown if the subscriber is null.</exception>
        public void Unsubscribe(WeakAction.CallType subscriber) =>
            Unsubscribe(new WeakAction(subscriber));
    }

    /// <summary>
    /// An event using <see cref="WeakAction"/> to hold its subscribers.
    /// </summary>
    /// <typeparam name="Arg1">The type of the first argument.</typeparam>
    public sealed class WeakEvent<Arg1>
    {
        private List<WeakAction<Arg1>> DelegateList { get; } = new();

        /// <summary>
        /// Invokes the event with provided arguments.
        /// </summary>
        /// <param name="arg1">The first argument of the event.</param>
        public void Invoke(Arg1 arg1)
        {
            WeakAction<Arg1> target;
            for (int i = 0; i < DelegateList.Count;)
            {
                target = DelegateList[i];
                if (target.IsAlive)
                {
                    target.Invoke(arg1);
                    i++;
                }
                else
                {
                    DelegateList[i] = DelegateList[DelegateList.Count - 1];
                    DelegateList.RemoveAt(DelegateList.Count - 1);
                }
            }
        }

        /// <summary>
        /// Adds a subscriber to this event.
        /// </summary>
        /// <param name="subscriber">The subscriber to add.</param>
        /// <exception cref="ArgumentNullException">Can be thrown if the subscriber is null.</exception>
        public void Subscribe(WeakAction<Arg1> subscriber) =>
            DelegateList.Add(subscriber ?? throw new ArgumentNullException(nameof(subscriber)));

        /// <summary>
        /// Adds a subscriber to this event.
        /// </summary>
        /// <param name="subscriber">The subscriber to add.</param>
        /// <exception cref="ArgumentNullException">Can be thrown if the subscriber is null.</exception>
        public void Subscribe(WeakAction<Arg1>.CallType subscriber) =>
            Subscribe(new WeakAction<Arg1>(subscriber));

        /// <summary>
        /// Removes a subscriber from this event.
        /// </summary>
        /// <param name="subscriber">The subscriber to remove.</param>
        /// <exception cref="ArgumentNullException">Can be thrown if the subscriber is null.</exception>
        public void Unsubscribe(WeakAction<Arg1> subscriber)
        {
            if (subscriber == null)
                throw new ArgumentNullException(nameof(subscriber));

            int id = DelegateList.IndexOf(subscriber);
            if (id != -1)
            {
                DelegateList[id] = DelegateList[DelegateList.Count - 1];
                DelegateList.RemoveAt(DelegateList.Count - 1);
            }
        }

        /// <summary>
        /// Removes a subscriber from this event.
        /// </summary>
        /// <param name="subscriber">The subscriber to remove.</param>
        /// <exception cref="ArgumentNullException">Can be thrown if the subscriber is null.</exception>
        public void Unsubscribe(WeakAction<Arg1>.CallType subscriber) =>
            Unsubscribe(new WeakAction<Arg1>(subscriber));
    }

    /// <summary>
    /// An event using <see cref="WeakAction"/> to hold its subscribers.
    /// </summary>
    /// <typeparam name="Arg1">The type of the first argument.</typeparam>
    /// <typeparam name="Arg2">The type of the second argument.</typeparam>
    public sealed class WeakEvent<Arg1, Arg2>
    {
        private List<WeakAction<Arg1, Arg2>> DelegateList { get; } = new();

        /// <summary>
        /// Invokes the event with provided arguments.
        /// </summary>
        /// <param name="arg1">The first argument of the event.</param>
        public void Invoke(Arg1 arg1, Arg2 arg2)
        {
            WeakAction<Arg1, Arg2> target;
            for (int i = 0; i < DelegateList.Count;)
            {
                target = DelegateList[i];
                if (target.IsAlive)
                {
                    target.Invoke(arg1, arg2);
                    i++;
                }
                else
                {
                    DelegateList[i] = DelegateList[DelegateList.Count - 1];
                    DelegateList.RemoveAt(DelegateList.Count - 1);
                }
            }
        }

        /// <summary>
        /// Adds a subscriber to this event.
        /// </summary>
        /// <param name="subscriber">The subscriber to add.</param>
        /// <exception cref="ArgumentNullException">Can be thrown if the subscriber is null.</exception>
        public void Subscribe(WeakAction<Arg1, Arg2> subscriber) =>
            DelegateList.Add(subscriber ?? throw new ArgumentNullException(nameof(subscriber)));

        /// <summary>
        /// Adds a subscriber to this event.
        /// </summary>
        /// <param name="subscriber">The subscriber to add.</param>
        /// <exception cref="ArgumentNullException">Can be thrown if the subscriber is null.</exception>
        public void Subscribe(WeakAction<Arg1, Arg2>.CallType subscriber) =>
            Subscribe(new WeakAction<Arg1, Arg2>(subscriber));

        /// <summary>
        /// Removes a subscriber from this event.
        /// </summary>
        /// <param name="subscriber">The subscriber to remove.</param>
        /// <exception cref="ArgumentNullException">Can be thrown if the subscriber is null.</exception>
        public void Unsubscribe(WeakAction<Arg1, Arg2> subscriber)
        {
            if (subscriber == null)
                throw new ArgumentNullException(nameof(subscriber));

            int id = DelegateList.IndexOf(subscriber);
            if (id != -1)
            {
                DelegateList[id] = DelegateList[DelegateList.Count - 1];
                DelegateList.RemoveAt(DelegateList.Count - 1);
            }
        }

        /// <summary>
        /// Removes a subscriber from this event.
        /// </summary>
        /// <param name="subscriber">The subscriber to remove.</param>
        /// <exception cref="ArgumentNullException">Can be thrown if the subscriber is null.</exception>
        public void Unsubscribe(WeakAction<Arg1, Arg2>.CallType subscriber) =>
            Unsubscribe(new WeakAction<Arg1, Arg2>(subscriber));
    }

    /// <summary>
    /// An event using <see cref="WeakAction"/> to hold its subscribers.
    /// </summary>
    /// <typeparam name="Arg1">The type of the first argument.</typeparam>
    /// <typeparam name="Arg2">The type of the second argument.</typeparam>
    public sealed class WeakEvent<Arg1, Arg2, Arg3>
    {
        private List<WeakAction<Arg1, Arg2, Arg3>> DelegateList { get; } = new();

        /// <summary>
        /// Invokes the event with provided arguments.
        /// </summary>
        /// <param name="arg1">The first argument of the event.</param>
        public void Invoke(Arg1 arg1, Arg2 arg2, Arg3 arg3)
        {
            WeakAction<Arg1, Arg2, Arg3> target;
            for (int i = 0; i < DelegateList.Count;)
            {
                target = DelegateList[i];
                if (target.IsAlive)
                {
                    target.Invoke(arg1, arg2, arg3);
                    i++;
                }
                else
                {
                    DelegateList[i] = DelegateList[DelegateList.Count - 1];
                    DelegateList.RemoveAt(DelegateList.Count - 1);
                }
            }
        }

        /// <summary>
        /// Adds a subscriber to this event.
        /// </summary>
        /// <param name="subscriber">The subscriber to add.</param>
        /// <exception cref="ArgumentNullException">Can be thrown if the subscriber is null.</exception>
        public void Subscribe(WeakAction<Arg1, Arg2, Arg3> subscriber) =>
            DelegateList.Add(subscriber ?? throw new ArgumentNullException(nameof(subscriber)));

        /// <summary>
        /// Adds a subscriber to this event.
        /// </summary>
        /// <param name="subscriber">The subscriber to add.</param>
        /// <exception cref="ArgumentNullException">Can be thrown if the subscriber is null.</exception>
        public void Subscribe(WeakAction<Arg1, Arg2, Arg3>.CallType subscriber) =>
            Subscribe(new WeakAction<Arg1, Arg2, Arg3>(subscriber));

        /// <summary>
        /// Removes a subscriber from this event.
        /// </summary>
        /// <param name="subscriber">The subscriber to remove.</param>
        /// <exception cref="ArgumentNullException">Can be thrown if the subscriber is null.</exception>
        public void Unsubscribe(WeakAction<Arg1, Arg2, Arg3> subscriber)
        {
            if (subscriber == null)
                throw new ArgumentNullException(nameof(subscriber));

            int id = DelegateList.IndexOf(subscriber);
            if (id != -1)
            {
                DelegateList[id] = DelegateList[DelegateList.Count - 1];
                DelegateList.RemoveAt(DelegateList.Count - 1);
            }
        }

        /// <summary>
        /// Removes a subscriber from this event.
        /// </summary>
        /// <param name="subscriber">The subscriber to remove.</param>
        /// <exception cref="ArgumentNullException">Can be thrown if the subscriber is null.</exception>
        public void Unsubscribe(WeakAction<Arg1, Arg2, Arg3>.CallType subscriber) =>
            Unsubscribe(new WeakAction<Arg1, Arg2, Arg3>(subscriber));
    }

    /// <summary>
    /// An event using <see cref="WeakAction"/> to hold its subscribers.
    /// </summary>
    /// <typeparam name="Arg1">The type of the first argument.</typeparam>
    /// <typeparam name="Arg2">The type of the second argument.</typeparam>
    public sealed class WeakEvent<Arg1, Arg2, Arg3, Arg4>
    {
        private List<WeakAction<Arg1, Arg2, Arg3, Arg4>> DelegateList { get; } = new();

        /// <summary>
        /// Invokes the event with provided arguments.
        /// </summary>
        /// <param name="arg1">The first argument of the event.</param>
        public void Invoke(Arg1 arg1, Arg2 arg2, Arg3 arg3, Arg4 arg4)
        {
            WeakAction<Arg1, Arg2, Arg3, Arg4> target;
            for (int i = 0; i < DelegateList.Count;)
            {
                target = DelegateList[i];
                if (target.IsAlive)
                {
                    target.Invoke(arg1, arg2, arg3, arg4);
                    i++;
                }
                else
                {
                    DelegateList[i] = DelegateList[DelegateList.Count - 1];
                    DelegateList.RemoveAt(DelegateList.Count - 1);
                }
            }
        }

        /// <summary>
        /// Adds a subscriber to this event.
        /// </summary>
        /// <param name="subscriber">The subscriber to add.</param>
        /// <exception cref="ArgumentNullException">Can be thrown if the subscriber is null.</exception>
        public void Subscribe(WeakAction<Arg1, Arg2, Arg3, Arg4> subscriber) =>
            DelegateList.Add(subscriber ?? throw new ArgumentNullException(nameof(subscriber)));

        /// <summary>
        /// Adds a subscriber to this event.
        /// </summary>
        /// <param name="subscriber">The subscriber to add.</param>
        /// <exception cref="ArgumentNullException">Can be thrown if the subscriber is null.</exception>
        public void Subscribe(WeakAction<Arg1, Arg2, Arg3, Arg4>.CallType subscriber) =>
            Subscribe(new WeakAction<Arg1, Arg2, Arg3, Arg4>(subscriber));

        /// <summary>
        /// Removes a subscriber from this event.
        /// </summary>
        /// <param name="subscriber">The subscriber to remove.</param>
        /// <exception cref="ArgumentNullException">Can be thrown if the subscriber is null.</exception>
        public void Unsubscribe(WeakAction<Arg1, Arg2, Arg3, Arg4> subscriber)
        {
            if (subscriber == null)
                throw new ArgumentNullException(nameof(subscriber));

            int id = DelegateList.IndexOf(subscriber);
            if (id != -1)
            {
                DelegateList[id] = DelegateList[DelegateList.Count - 1];
                DelegateList.RemoveAt(DelegateList.Count - 1);
            }
        }

        /// <summary>
        /// Removes a subscriber from this event.
        /// </summary>
        /// <param name="subscriber">The subscriber to remove.</param>
        /// <exception cref="ArgumentNullException">Can be thrown if the subscriber is null.</exception>
        public void Unsubscribe(WeakAction<Arg1, Arg2, Arg3, Arg4>.CallType subscriber) =>
            Unsubscribe(new WeakAction<Arg1, Arg2, Arg3, Arg4>(subscriber));
    }

    /// <summary>
    /// An event using <see cref="WeakAction"/> to hold its subscribers.
    /// </summary>
    /// <typeparam name="Arg1">The type of the first argument.</typeparam>
    /// <typeparam name="Arg2">The type of the second argument.</typeparam>
    public sealed class WeakEvent<Arg1, Arg2, Arg3, Arg4, Arg5>
    {
        private List<WeakAction<Arg1, Arg2, Arg3, Arg4, Arg5>> DelegateList { get; } = new();

        /// <summary>
        /// Invokes the event with provided arguments.
        /// </summary>
        /// <param name="arg1">The first argument of the event.</param>
        public void Invoke(Arg1 arg1, Arg2 arg2, Arg3 arg3, Arg4 arg4, Arg5 arg5)
        {
            WeakAction<Arg1, Arg2, Arg3, Arg4, Arg5> target;
            for (int i = 0; i < DelegateList.Count;)
            {
                target = DelegateList[i];
                if (target.IsAlive)
                {
                    target.Invoke(arg1, arg2, arg3, arg4, arg5);
                    i++;
                }
                else
                {
                    DelegateList[i] = DelegateList[DelegateList.Count - 1];
                    DelegateList.RemoveAt(DelegateList.Count - 1);
                }
            }
        }

        /// <summary>
        /// Adds a subscriber to this event.
        /// </summary>
        /// <param name="subscriber">The subscriber to add.</param>
        /// <exception cref="ArgumentNullException">Can be thrown if the subscriber is null.</exception>
        public void Subscribe(WeakAction<Arg1, Arg2, Arg3, Arg4, Arg5> subscriber) =>
            DelegateList.Add(subscriber ?? throw new ArgumentNullException(nameof(subscriber)));

        /// <summary>
        /// Adds a subscriber to this event.
        /// </summary>
        /// <param name="subscriber">The subscriber to add.</param>
        /// <exception cref="ArgumentNullException">Can be thrown if the subscriber is null.</exception>
        public void Subscribe(WeakAction<Arg1, Arg2, Arg3, Arg4, Arg5>.CallType subscriber) =>
            Subscribe(new WeakAction<Arg1, Arg2, Arg3, Arg4, Arg5>(subscriber));

        /// <summary>
        /// Removes a subscriber from this event.
        /// </summary>
        /// <param name="subscriber">The subscriber to remove.</param>
        /// <exception cref="ArgumentNullException">Can be thrown if the subscriber is null.</exception>
        public void Unsubscribe(WeakAction<Arg1, Arg2, Arg3, Arg4, Arg5> subscriber)
        {
            if (subscriber == null)
                throw new ArgumentNullException(nameof(subscriber));

            int id = DelegateList.IndexOf(subscriber);
            if (id != -1)
            {
                DelegateList[id] = DelegateList[DelegateList.Count - 1];
                DelegateList.RemoveAt(DelegateList.Count - 1);
            }
        }

        /// <summary>
        /// Removes a subscriber from this event.
        /// </summary>
        /// <param name="subscriber">The subscriber to remove.</param>
        /// <exception cref="ArgumentNullException">Can be thrown if the subscriber is null.</exception>
        public void Unsubscribe(WeakAction<Arg1, Arg2, Arg3, Arg4, Arg5>.CallType subscriber) =>
            Unsubscribe(new WeakAction<Arg1, Arg2, Arg3, Arg4, Arg5>(subscriber));
    }
}