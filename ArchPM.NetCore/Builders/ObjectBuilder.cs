using ArchPM.NetCore.Extensions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Reflection;

namespace ArchPM.NetCore.Builders
{
    /// <summary>
    /// 
    /// </summary>
    public static class ObjectBuilder
    {
        /// <summary>
        /// 
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="args">The arguments.</param>
        /// <returns></returns>
        private delegate T ObjectActivator<out T>(params object[] args);

        /// <summary>
        /// Creates the instance.
        /// </summary>
        /// <param name="type">The type.</param>
        /// <returns></returns>
        public static object CreateInstance(Type type)
        {
            object instance = null;
            if (type.ContainsGenericParameters)
            {
                type = type.MakeGenericType(type.GenericTypeArguments);
            }

            var constructorArguments = new List<object>();
            var ctorInfo = type.GetConstructors().FirstOrDefault();
            if (ctorInfo != null)
            {
                var ctorParametersInfo = ctorInfo.GetParameters();
                foreach (var info in ctorParametersInfo)
                {
                    constructorArguments.Add(info.ParameterType.GetDefault());
                }

                var createdActivator = GetActivator<object>(ctorInfo);
                //create an instance:
                instance = createdActivator(constructorArguments.ToArray());
            }

            return instance;
        }

        /// <summary>
        /// Creates the instance.
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <returns></returns>
        public static T CreateInstance<T>()
        {
            return (T)CreateInstance(typeof(T)) ;
        }

        /// <summary>
        /// Gets the activator.
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="ctor">The ctor.</param>
        /// <returns></returns>
        private static ObjectActivator<T> GetActivator<T>(ConstructorInfo ctor)
        {
            //taken from rogerjohansson.blog
            var paramsInfo = ctor.GetParameters();

            //create a single param of type object[]
            var param = Expression.Parameter(typeof(object[]), "args");

            var argsExp = new Expression[paramsInfo.Length];

            //pick each arg from the params array 
            //and create a typed expression of them
            for (var i = 0; i < paramsInfo.Length; i++)
            {
                Expression index = Expression.Constant(i);
                var paramType = paramsInfo[i].ParameterType;

                Expression paramAccessorExp = Expression.ArrayIndex(param, index);
                Expression paramCastExp = Expression.Convert(paramAccessorExp, paramType);

                argsExp[i] = paramCastExp;
            }

            //make a NewExpression that calls the
            //ctor with the args we just created
            var newExp = Expression.New(ctor, argsExp);

            //create a lambda with the New
            //Expression as body and our param object[] as arg
            var lambda = Expression.Lambda(typeof(ObjectActivator<T>), newExp, param);

            //compile it
            var compiled = (ObjectActivator<T>)lambda.Compile();
            return compiled;
        }

    }
}
