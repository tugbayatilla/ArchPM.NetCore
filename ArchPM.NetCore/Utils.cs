using System;
using System.Collections.Generic;
using System.IO;
using System.Reflection;

namespace ArchPM.NetCore
{
    /// <summary>
    /// 
    /// </summary>
    public static class Utils
    {
        /// <summary>
        /// The lock
        /// </summary>
        private static readonly object Lock = new object();

        /// <summary>
        /// Creates the unique number.
        /// </summary>
        /// <returns></returns>
        public static ulong CreateUniqueNumber(DateTime? now = null)
        {
            if(!now.HasValue)
            {
                now = DateTime.Now;
            }

            ulong result;
            lock (Lock)
            {
                var unique = $"{now:yyyyMMddHHmmssffffff}";
                result = Convert.ToUInt64(unique);
            }
            return result;
        }

        /// <summary>
        /// Loads the assemblies.
        /// </summary>
        /// <param name="directoryPath">The directory path.</param>
        /// <param name="searchOption">The search option.</param>
        /// <returns></returns>
        public static IEnumerable<Assembly> GetAssembliesInDirectory(string directoryPath = "", SearchOption searchOption = SearchOption.TopDirectoryOnly)
        {
            if (string.IsNullOrEmpty(directoryPath))
            {
                directoryPath = Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "bin"); // note: don't use CurrentEntryAssembly or anything like that.
            }

            var directory = new DirectoryInfo(directoryPath);
            if (!directory.Exists)
            {
                yield break;
            }

            var files = directory.GetFiles("*.dll", searchOption);

            foreach (var file in files)
            {
                // Load the file into the application domain.
                var assemblyName = AssemblyName.GetAssemblyName(file.FullName);
                var assembly = AppDomain.CurrentDomain.Load(assemblyName);
                yield return assembly;
            }
        }

        /// <summary>
        /// Checks the path is a directory
        /// </summary>
        /// <param name="path">The path.</param>
        /// <returns>
        /// True: Directory, False:File
        /// </returns>
        public static bool IsDirectory(string path)
        {
            //false means it's a file
            var result = false;
            // get the file attributes for file or directory
            var attr = File.GetAttributes(path);

            //detect whether its a directory
            if ((attr & FileAttributes.Directory) == FileAttributes.Directory)
            {
                result = true;
            }

            return result;
        }

    }
}
