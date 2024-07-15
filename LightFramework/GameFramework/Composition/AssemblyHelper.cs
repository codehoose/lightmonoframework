using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;

namespace GameFramework.Composition
{
    internal static class AssemblyHelper
    {
        /// <summary>
        /// Get a list of types that implement the given interface
        /// </summary>
        /// <typeparam name="T">Interface to search for</typeparam>
        /// <returns>The list of types that implement the given interface</returns>
        public static List<Type> GetImplementorsOf<T>() where T : class
        {
            return Assembly.GetEntryAssembly().GetTypes()
                           .Concat(Assembly.GetAssembly(typeof(AssemblyHelper)).GetTypes())
                           .Where(t => t.GetInterface(typeof(T).Name) != null && !t.IsAbstract)
                           .ToList();
        }

        /// <summary>
        /// Determine if the object has a custom attribute
        /// </summary>
        /// <typeparam name="T">Attribute type</typeparam>
        /// <param name="obj">The object</param>
        /// <returns></returns>
        internal static bool HasAttribute<T>(this object obj) where T: Attribute
        {
            return obj.GetType().GetCustomAttribute(typeof(T)) != null;
        }
    }
}
