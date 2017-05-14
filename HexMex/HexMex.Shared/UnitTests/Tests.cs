using System;
using System.Linq;
using System.Reflection;

namespace HexMex.UnitTests
{
    public static class Tests
    {
        public static void RunTests()
        {
            var allTypes = typeof(Tests).Assembly.GetTypes();
            var testMethods = (from type in allTypes
                               where !type.IsAbstract
                               where type.GetCustomAttribute<TestClassAttribute>() != null
                               from method in type.GetMethods(BindingFlags.Public | BindingFlags.Instance)
                               where method.GetParameters().Length == 0
                               where method.GetCustomAttribute<TestMethodAttribute>() != null
                               select new {Method = method, Type = type}).ToArray();
            foreach (var testMethod in testMethods)
            {
                try
                {
                    testMethod.Method.Invoke(Activator.CreateInstance(testMethod.Type), new object[0]);
                }
                catch (TargetInvocationException e)
                {
                    if (e.InnerException != null)
                        throw e.InnerException;
                    throw;
                }
            }
        }
    }
}