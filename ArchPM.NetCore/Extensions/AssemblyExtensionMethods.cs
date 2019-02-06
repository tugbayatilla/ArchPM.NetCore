﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;

namespace ArchPM.NetCore.Extensions.AssemblyExtensions
{
    public static class AssemblyExtensionMethods
    {
        /// <summary>
        /// Gets the provider.
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="currentAssembly">The current assembly.</param>
        /// <param name="constructorArguments">The constructor arguments.</param>
        /// <returns></returns>
        public static IEnumerable<T> GetProvider<T>(this Assembly currentAssembly, params Object[] constructorArguments)
        {
            var types = currentAssembly.GetTypes();
            foreach (var type in types)
            {
                if (!type.IsClass) continue;
                if (type.IsAbstract) continue;
                if (type.Name.Contains("<") || type.Name.Contains(">")) continue;
                if (type.GetInterfaces().Contains(typeof(T)) || TypeExtensions.TypeExtensionMethods.RecursivlyCheckBaseType(type, typeof(T)))
                {
                    Type result = type;
                    if (type.ContainsGenericParameters)
                    {
                        result = type.MakeGenericType(type.GenericTypeArguments);
                    }

                    var provider = (T)Activator.CreateInstance(result, constructorArguments);

                    yield return provider;
                }
            }
        }

        /// <summary>
        /// Gets the provider types.
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="currentAssembly">The current assembly.</param>
        /// <returns></returns>
        public static IEnumerable<Type> GetProviderTypes<T>(this Assembly currentAssembly)
        {
            var types = currentAssembly.GetTypes();
            foreach (var type in types)
            {
                if (!type.IsClass) continue;
                if (type.IsAbstract) continue;
                if (type.GetInterfaces().Contains(typeof(T)) || type.BaseType.Name == typeof(T).Name)
                {
                    yield return type;
                }
            }
        }

       


    }
}