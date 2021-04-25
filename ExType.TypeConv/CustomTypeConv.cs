using System;
using System.Linq;
using System.Reflection;

namespace ExType.TypeConv
{
    /// <summary>
    /// Convert between custom types !!! READ DOCS !!!
    /// </summary>
    public static class CustomTypeConv
    {
        /// <summary>
        /// Converts to a specified target type that supports new() !!! READ DOCS !!!
        /// </summary>
        /// <param name="obj"></param>
        /// <typeparam name="T"></typeparam>
        /// <returns></returns>
        public static T To<T>(this object obj) where T : new() => obj.To(new T());

        /// <summary>
        /// Converts to a specified target type using a given instance as a base !!! READ DOCS !!!
        /// </summary>
        /// <param name="obj"></param>
        /// <param name="target"></param>
        /// <typeparam name="T"></typeparam>
        /// <returns></returns>
        public static T To<T>(this object obj, T target)
        {
            var sourceMembers = obj.Members();
            var targetMembers = typeof(T).GetMembers();

            foreach (var member in sourceMembers)
            {
                // search for a matching member
                var name = member.Name;
                var type = member.MemberType;
                var matching = targetMembers.FirstOrDefault(m => m.Name == name && m.MemberType == type);
                
                // if none, leave as default
                if (matching == null) continue;

                switch (matching)
                {
                    // if it's a property copy the value
                    case PropertyInfo p:
                        try
                        {
                            p.SetValue(target, obj.GetValue(member, out _));
                        }
                        catch (Exception)
                        {
                            // ignored
                        }
                        continue;
                    // if it's a field copy the value
                    case FieldInfo f:
                        try
                        {
                            f.SetValue(target, obj.GetValue(member, out _));
                        }
                        catch (Exception)
                        {
                            // ignored
                        }
                        continue;
                    default:
                        continue;
                }
            }

            return target;
        }
    }
}