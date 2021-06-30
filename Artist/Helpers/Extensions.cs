using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Artist.Helpers
{
    public static class Extensions
    {
        /// <summary>
        /// Assign values of object properties
        /// </summary>
        /// <typeparam name="T1">Source object type</typeparam>
        /// <typeparam name="T2">Target object type</typeparam>
        /// <param name="self">Source object</param>
        /// <param name="source">Target object</param>
        /// <param name="ignore">Names of properties to ignore</param>
        public static void CopyPropertiesFrom<T1, T2>(this T1 self, T2 source, string[] ignore = null)
        {
            if (ignore == null)
                ignore = new string[] { string.Empty };

            var ownProperties = typeof(T1).GetProperties().Where(p => p.CanRead && p.CanWrite && !ignore.Contains(p.Name)).ToList();
            var sourceProperties = typeof(T2).GetProperties().Where(p => p.CanRead && p.CanWrite).ToList();

            foreach (var property in sourceProperties)
            {
                ownProperties.FirstOrDefault(p => p.Name == property.Name)
                    ?.SetValue(self, property.GetValue(source));
            }
        }

        public static BonusProgram[] AsArray(this string programs)
        {
            if (!string.IsNullOrWhiteSpace(programs))
            {
                return JsonConvert.DeserializeObject<BonusProgram[]>(programs)
                .Where(bp => bp.Enable)
                .OrderBy(bp => bp.Ordinal)
                .ToArray();
            }

            return null;
        }
    }
}
