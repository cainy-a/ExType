using System;
using System.Linq;
using System.Reflection;

namespace ExType.TypeConv
{
    public static class CustomTypeConv
    {
        public static T To<T>(this object obj) where T : new() => obj.To(new T());

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