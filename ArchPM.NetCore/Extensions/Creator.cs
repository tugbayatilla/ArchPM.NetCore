using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Reflection;
using System.Text;

namespace ArchPM.NetCore.Extensions
{
    /// <summary>
    /// 
    /// </summary>
    public static class Creator
    {
        /// <summary>
        /// 
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="args">The arguments.</param>
        /// <returns></returns>
        public delegate T ObjectActivator<T>(params object[] args);
        /// <summary>
        /// Gets the activator.
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="ctor">The ctor.</param>
        /// <returns></returns>
        public static ObjectActivator<T> GetActivator<T>(ConstructorInfo ctor)
        {
            //taken from rogerjohansson.blog
            Type type = ctor.DeclaringType;
            ParameterInfo[] paramsInfo = ctor.GetParameters();

            //create a single param of type object[]
            ParameterExpression param = Expression.Parameter(typeof(object[]), "args");

            Expression[] argsExp = new Expression[paramsInfo.Length];

            //pick each arg from the params array 
            //and create a typed expression of them
            for (int i = 0; i < paramsInfo.Length; i++)
            {
                Expression index = Expression.Constant(i);
                Type paramType = paramsInfo[i].ParameterType;

                Expression paramAccessorExp = Expression.ArrayIndex(param, index);
                Expression paramCastExp = Expression.Convert(paramAccessorExp, paramType);

                argsExp[i] = paramCastExp;
            }

            //make a NewExpression that calls the
            //ctor with the args we just created
            NewExpression newExp = Expression.New(ctor, argsExp);

            //create a lambda with the New
            //Expression as body and our param object[] as arg
            LambdaExpression lambda = Expression.Lambda(typeof(ObjectActivator<T>), newExp, param);

            //compile it
            ObjectActivator<T> compiled = (ObjectActivator<T>)lambda.Compile();
            return compiled;
        }

        /// <summary>
        /// Creates the sample data.
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="configuration">The configuration.</param>
        /// <returns></returns>
        public static Object CreateInstance(Type type)
        {
            if (type.ContainsGenericParameters)
            {
                type = type.MakeGenericType(type.GenericTypeArguments);
            }

            var ctors = type.GetConstructors();
            var ctor = ctors[0];
            var ctorParametersInfo = ctor.GetParameters();
            List<Object> constructorArguments = new List<object>();
            foreach (var info in ctorParametersInfo)
            {
                constructorArguments.Add(info.ParameterType.GetDefault());
            }

            ConstructorInfo ctorInfo = type.GetConstructors().First();
            ObjectActivator<Object> createdActivator = GetActivator<Object>(ctorInfo);
            //create an instance:
            Object instance = createdActivator(constructorArguments.ToArray());

            return instance;

        }

        public static T CreateInstance<T>()
            where T : class
        {
            return CreateInstance(typeof(T)) as T;
        }
    }
}
