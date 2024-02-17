using CSToolbox.Extensions;
using System;
using System.Collections.Generic;

namespace CSToolbox.Services
{
    /// <summary>
    /// Implements a dynamically constructed service provider.
    /// </summary>
    public class DynamicServiceProvider : IServiceProvider
    {
        private Dictionary<Type, object> Services { get; } = new Dictionary<Type, object>();

        public object? GetService(Type serviceType)
        {
            _ = Services.TryGetValue(serviceType, out object? service);
            return service;
        }

        /// <summary>
        /// Adds specified service to this provider as specified service type.
        /// If the service type is derived from another service type, it will only be added as itself, not as its parent.
        /// Use <see cref="AddService(object, bool)"/> to add service as all its interfaces.
        /// </summary>
        /// <param name="serviceType">Type of service to add the object as</param>
        /// <param name="service">Object to add as service</param>
        /// <returns>True if the service was added successfully, false if a service with the same type is already registered</returns>
        public bool AddService(Type serviceType, object service)
        {
            if (!serviceType.IsInterface)
                throw new ArgumentException($"{serviceType.FullName} is not an interface.", nameof(serviceType));
            else if (!service.GetType().Is(serviceType))
                throw new ArgumentException($"{service.GetType().FullName} does not implement {serviceType.FullName}.", nameof(service));
            else if (Services.ContainsKey(serviceType))
                return false;

            Services.Add(serviceType, service);
            return true;
        }

        /// <summary>
        /// Adds a service as all the interfaces it implements
        /// </summary>
        /// <param name="service">The service to add</param>
        /// <param name="overrideRegistered">If set to true, this operation will override already added services (interfaces) which are implemented by this service.</param>
        /// <returns>True if at least one service was added as the result of this operation.</returns>
        public bool AddService(object service, bool overrideRegistered = false)
        {
            Type[] types = service.GetType().GetInterfaces();
            if (types.Length == 0)
                return false;

            if (overrideRegistered)
            {
                foreach (Type type in types)
                    Services[type] = service;
                return true;
            }
            else
            {
                bool any = false;
                foreach (Type type in types)
                {
                    if (Services.ContainsKey(type))
                        continue;
                    Services.Add(type, service);
                    any = true;
                }
                return any;
            }

        }

        /// <summary>
        /// Removes a previously added service type.
        /// </summary>
        /// <param name="serviceType">The type to remove.</param>
        /// <returns>True if the serivce was removed, false if there was no service to begin with.</returns>
        public bool RemoveService(Type serviceType)
        {
            return Services.Remove(serviceType);
        }
    }
}
