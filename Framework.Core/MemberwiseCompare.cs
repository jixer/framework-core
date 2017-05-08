using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Reflection;
using System.Text;

namespace jixer.Framework.Core
{
    public class MemberwiseCompare
    {
        #region Private Members
        internal static IDictionary<Type, Func<object, object, bool>> _funcs;
        internal static MethodInfo _areEqualMethodInfo;
        #endregion

        #region Constructors
        static MemberwiseCompare()
        {
            _funcs = new Dictionary<Type, Func<object, object, bool>>();
            _areEqualMethodInfo = typeof(MemberwiseCompare).GetTypeInfo().GetDeclaredMethods("AreEqual").Where(x => !x.IsPublic).Single();
        }
        #endregion

        #region Public Static Methods
        /// <summary>
        /// Used to register a type in the comparison function cache
        /// </summary>
        /// <typeparam name="T">Type to register</typeparam>
        /// <remarks>
        /// If the type is already registered it will not be registered again.  
        /// 
        /// If a type is not registered through here, lazy loading will be utilized to cache the comparison function.
        /// </remarks>
        public static void Register<T>()
        {
            Register(typeof(T));
        }

        /// <summary>
        /// Used to register a type in the comparison function cache
        /// </summary>
        /// <param name="t">Type to register</param>
        /// <remarks>
        /// If the type is already registered it will not be registered again.  
        /// 
        /// If a type is not registered through here, lazy loading will be utilized to cache the comparison function.
        /// </remarks>
        public static void Register(Type t)
        {
            if (!_funcs.ContainsKey(t))
            {
                _funcs.Add(t, MakeEqualsMethod(t));
            }
        }

        /// <summary>
        /// Compare two instances of the same object for equality
        /// </summary>
        /// <typeparam name="T">Type of objects</typeparam>
        /// <param name="t1">Left instance to compare</param>
        /// <param name="t2">Right instance to compare</param>
        /// <returns>True if the objects are the same</returns>
        public static bool AreEqual<T>(T t1, T t2)
        {
            return AreEqual(typeof(T), t1, t2);
        }
        
        #endregion

        #region Private Methods
        /// <summary>
        /// Generates the compiled function based upon a linq expression
        /// </summary>
        /// <param name="type">Type to generate a comparison function for</param>
        /// <returns></returns>
        protected static Func<object, object, bool> MakeEqualsMethod(Type type)
        {
            ParameterExpression pThis = Expression.Parameter(typeof(object), "x");
            ParameterExpression pThat = Expression.Parameter(typeof(object), "y");

            // cast to the subclass type
            UnaryExpression pCastThis = Expression.Convert(pThis, type);
            UnaryExpression pCastThat = Expression.Convert(pThat, type);

            // compound AND expression using short-circuit evaluation
            Expression last = null;
            foreach (PropertyInfo property in type.GetTypeInfo().DeclaredProperties)
            {
                Expression equals;
                if (property.PropertyType.GetTypeInfo().IsClass && property.PropertyType.Name != "String")
                {
                    var exprCall = Expression.Call(
                        _areEqualMethodInfo,
                        Expression.Constant(property.PropertyType),
                        Expression.Property(pCastThis, property),
                        Expression.Property(pCastThat, property));
                    equals = Expression.Equal(exprCall, Expression.Constant(true));
                }
                else
                {
                    equals = Expression.Equal(
                        Expression.Property(pCastThis, property),
                        Expression.Property(pCastThat, property)
                    );
                }

                if (last == null)
                    last = equals;
                else
                    last = Expression.AndAlso(last, equals);
            }

            var oneOrMoreNull =
                Expression.Or(
                    Expression.Equal(pCastThis, Expression.Constant(Convert.ChangeType(null, type))),
                    Expression.Equal(pCastThat, Expression.Constant(Convert.ChangeType(null, type))));

            Expression expr =
                Expression.Or(
                    Expression.AndAlso(
                        oneOrMoreNull,
                        Expression.Equal(pCastThis, pCastThat)
                    ),
                    Expression.AndAlso(
                        Expression.IsFalse(oneOrMoreNull),
                        last));

            // compile method
            return Expression.Lambda<Func<object, object, bool>>(expr, pThis, pThat).Compile();
        }

        /// <summary>
        /// Compare two instances of the same object for equality
        /// </summary>
        /// <param name="t">Type of objects</typeparam>
        /// <param name="t1">Left instance to compare</param>
        /// <param name="t2">Right instance to compare</param>
        /// <returns>True if the objects are the same</returns>
        /// <remarks>
        /// This was kept as a private since there is no checking to ensure that 't1' and 't2' are of type 't'.
        /// 
        /// Only the generic wrapper for this function was implemented as a public
        /// </remarks>
        protected static bool AreEqual(Type t, object t1, object t2)
        {
            Register(t);
            var func = _funcs[t];
            return func.Invoke(t1, t2);
        }
        #endregion
    }
}
