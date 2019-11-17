using System;

namespace ArchPM.NetCore.Utilities
{
    /// <summary>
    /// 
    /// </summary>
    public static class TypeUtilities
    {
        /// <summary>
        /// Determines whether [is dot net primitive] [the specified accept nullable types].
        /// </summary>
        /// <param name="systemType">Type of the system.</param>
        /// <param name="acceptNullableTypes">if set to <c>true</c> [accept nullable types].</param>
        /// <returns>
        ///   <c>true</c> if [is dot net primitive] [the specified accept nullable types]; otherwise, <c>false</c>.
        /// </returns>
        public static bool IsValueType(this Type systemType, bool acceptNullableTypes = true)
        {
            if (acceptNullableTypes && systemType.Name == "Nullable`1")
            {
                var nullableSystemType = systemType.GetGenericArguments()[0];
                return IsValueType(nullableSystemType, false);
            }

            return systemType.IsValueType;
        }

    }
}
