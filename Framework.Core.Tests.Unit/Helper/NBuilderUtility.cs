using FizzWare.NBuilder;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace jixer.Framework.Core.Tests.Unit.Helper
{
    public static class NBuilderUtility
    {
        public static T Create<T>()
        {
            return (T)Create(typeof(T));
        }

        private static object Create(Type type)
        {
            if (type.IsInterface)
            {
                var firstConcreteImplementation = type.Assembly.GetTypes().FirstOrDefault(t => type.IsAssignableFrom(t) && !t.IsInterface);
                if (firstConcreteImplementation != null)
                    type = firstConcreteImplementation;
                else
                    return null;
            }

            var baseType = Build(type) ?? Build(Nullable.GetUnderlyingType(type));

            var complexTypeProperties = baseType.GetType().GetProperties().Where(p => !p.PropertyType.Namespace.Contains("System")).ToList();

            if (!complexTypeProperties.Any())
                return baseType;

            foreach (var complexTypeProperty in complexTypeProperties)
                complexTypeProperty.SetValue(baseType, Create(complexTypeProperty.PropertyType), null);

            return baseType;
        }

        private static object Build(Type type)
        {
            var builderClassType = typeof(Builder<>);
            Type[] args = { type };
            var genericBuilderType = builderClassType.MakeGenericType(args);

            //var builder = Activator.CreateInstance(genericBuilderType);
            //var createNewMethodInfo = builder.GetType().GetMethod("CreateNew");
            var createNewMethodInfo = genericBuilderType.GetMethod("CreateNew");
            //var objectBuilder = createNewMethodInfo.Invoke(builder, null);
            var objectBuilder = createNewMethodInfo.Invoke(null, null);
            var buildMethodInfo = objectBuilder.GetType().GetMethod("Build");
            return buildMethodInfo.Invoke(objectBuilder, null);
        }
    }

}