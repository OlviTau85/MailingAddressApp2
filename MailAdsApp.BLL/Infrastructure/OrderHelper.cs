using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace MailAdsApp.BLL.Infrastructure
{
    public static class OrderHelper
    {
        public static IOrderedEnumerable<T> ApplyOrder<T>(this IEnumerable<T> source, string property, string methodName)
        {
            var arg = Expression.Parameter(typeof(T), "x");
            Expression expr = arg;
            expr = Expression.Property(expr, property);
            var lambda = Expression.Lambda(expr, arg);
            var method = typeof(Enumerable).GetGenericMethod(methodName, new[] { typeof(T), expr.Type }, new[] { source.GetType(), lambda.GetType() });

            return (IOrderedEnumerable<T>)method.Invoke(null, new object[] { source, lambda });
        }

        public static MethodInfo GetGenericMethod(this Type type, string name, Type[] genericTypeArgs, Type[] paramTypes)
        {
            var methods =
                from abstractGenericMethod in type.GetMethods(BindingFlags.NonPublic | BindingFlags.Public | BindingFlags.Instance | BindingFlags.Static)
                where abstractGenericMethod.Name == name
                where abstractGenericMethod.IsGenericMethod
                let pa = abstractGenericMethod.GetParameters()
                where pa.Length == paramTypes.Length
                select abstractGenericMethod.MakeGenericMethod(genericTypeArgs) into concreteGenericMethod
                where concreteGenericMethod.GetParameters().Select(p => p.ParameterType).SequenceEqual(paramTypes, new TestAssignable())
                select concreteGenericMethod;
            return methods.FirstOrDefault();
        }
    }
}
