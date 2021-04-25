using System;
using System.Linq;
using System.Reflection;
using ExType.Shared;

namespace ExType.TypeConv
{
    public static class TypeTools
    {
        /// <summary>
        /// Gets a list of members of the object
        /// </summary>
        /// <param name="obj"></param>
        /// <returns></returns>
        public static MemberInfo[] Members(this object obj) => obj.GetType().GetMembers();
        
        /// <summary>
        /// Gets a list of names of members of the object
        /// </summary>
        /// <param name="obj"></param>
        /// <returns></returns>
        public static string[] MemberNames(this object obj) => obj.Members().Select(m => m.Name).ToArray();

        /// <summary>
        /// Gets members of the object with the matching name, or an empty array if none found
        /// </summary>
        /// <param name="obj"></param>
        /// <param name="name"></param>
        /// <returns></returns>
        public static MemberInfo[] GetMembersByName(this object obj, string name) => obj.GetType().GetMember(name);

        /// <summary>
        /// Invokes a method on the given object
        /// </summary>
        /// <param name="obj">The object on which to call the method - ignored for static methods</param>
        /// <param name="method">The method to call</param>
        /// <param name="returnType">The return type of the method</param>
        /// <param name="parameters">Parameters to pass to the method</param>
        /// <returns>The result of the method</returns>
        public static object InvokeMethod(this object obj, MethodInfo method, out Type returnType, params object[] parameters)
        {
            returnType = method.ReturnType;
            return method.Invoke(obj, parameters);
        }
        
        /// <summary>
        /// Invokes a method on the given object by member
        /// </summary>
        /// <param name="obj">The object on which to call the method - ignored for static methods</param>
        /// <param name="member">The method to call</param>
        /// <param name="returnType">The return type of the method</param>
        /// <param name="parameters">Parameters to pass to the method</param>
        /// <returns>The result of the method</returns>
        /// <exception cref="TypeMemberException">The member given was not a method</exception>
        public static object InvokeMethod(this object obj, MemberInfo member, out Type returnType, params object[] parameters)
        {
            if (member.MemberType == MemberTypes.Method)
                return obj.InvokeMethod((MethodInfo) member, out returnType, parameters);
            throw new TypeMemberException("Specified member was not a method");
        }

        /// <summary>
        /// Invokes a method on the given object by member name
        /// </summary>
        /// <param name="obj">The object on which to call the method - ignored for static methods</param>
        /// <param name="memberName">The name of the method to call</param>
        /// <param name="returnType">The return type of the method</param>
        /// <param name="parameters">Parameters to pass to the method</param>
        /// <returns>The result of the method</returns>
        /// <exception cref="TypeMemberException">The member given did not exist or was not a method</exception>
        public static object InvokeMethod(this object obj, string memberName, out Type returnType, out MethodInfo method,
            params object[] parameters)
        {
            method = (MethodInfo) obj.GetMembersByName(memberName).FirstOrDefault(m => m.MemberType == MemberTypes.Method);
            if (method == null)
                throw new TypeMemberException($"no members called {memberName} either exist or are methods");

            return obj.InvokeMethod(method, out returnType, parameters);
        }

        /// <summary>
        /// Gets the value of a field or property
        /// </summary>
        /// <param name="obj"></param>
        /// <param name="member"></param>
        /// <param name="type"></param>
        /// <returns></returns>
        /// <exception cref="TypeAccessException">Member was not a field or property</exception>
        public static object GetValue(this object obj, MemberInfo member, out Type type)
        {  
            switch (member)
            {
                case FieldInfo f:
                    type = typeof(FieldInfo);
                    return obj.GetValue(f);
                case PropertyInfo p:
                    type = typeof(PropertyInfo);
                    return obj.GetValue(p);
                default:
                    throw new TypeAccessException("Member was not a field or property");
            }
        }
        
        /// <summary>
        /// Gets the value of a field
        /// </summary>
        /// <param name="obj"></param>
        /// <param name="field"></param>
        /// <returns></returns>
        public static object GetValue(this object obj, FieldInfo field) => field.GetValue(obj);

        /// <summary>
        /// Gets the value of a property
        /// </summary>
        /// <param name="obj"></param>
        /// <param name="property"></param>
        /// <returns></returns>
        public static object GetValue(this object obj, PropertyInfo property) => property.GetValue(obj);
    }
}