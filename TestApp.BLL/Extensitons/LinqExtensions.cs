﻿using System.Collections.Generic;
using System.Linq.Expressions;
using System.Reflection;
using System.Linq;
using System;

namespace TestApp.BLL.Extensitons
{
	public static class LinqExtensions
	{
		private static PropertyInfo GetPropertyInfo(Type objType, string name)
		{
			var properties = objType.GetProperties();
			var matchedProperty = properties
				.FirstOrDefault(p => p.Name.Equals(name, StringComparison.InvariantCultureIgnoreCase));
			if (matchedProperty == null)
				throw new ArgumentException("name");

			return matchedProperty;
		}

		private static LambdaExpression GetOrderExpression(Type objType, PropertyInfo pi)
		{
			var paramExpr = Expression.Parameter(objType);
			var propAccess = Expression.PropertyOrField(paramExpr, pi.Name);
			var expr = Expression.Lambda(propAccess, paramExpr);
			return expr;
		}

		public static IEnumerable<T> OrderBy<T>(this IEnumerable<T> query, string name)
		{
			var propInfo = GetPropertyInfo(typeof(T), name);
			var expr = GetOrderExpression(typeof(T), propInfo);

			var method = typeof(Enumerable).GetMethods().FirstOrDefault(m => m.Name == "OrderBy" && m.GetParameters().Length == 2);
			var genericMethod = method.MakeGenericMethod(typeof(T), propInfo.PropertyType);
			return (IEnumerable<T>)genericMethod.Invoke(null, new object[] { query, expr.Compile() });
		}

		public static IQueryable<T> OrderBy<T>(this IQueryable<T> query, string name)
		{
			var propInfo = GetPropertyInfo(typeof(T), name);
			var expr = GetOrderExpression(typeof(T), propInfo);

			var method = typeof(Queryable).GetMethods().FirstOrDefault(m => m.Name == "OrderBy" && m.GetParameters().Length == 2);
			var genericMethod = method.MakeGenericMethod(typeof(T), propInfo.PropertyType);
			return (IQueryable<T>)genericMethod.Invoke(null, new object[] { query, expr });
		}

		public static IQueryable<T> OrderByDescending<T>(this IQueryable<T> query, string name)
		{
			var propInfo = GetPropertyInfo(typeof(T), name);
			var expr = GetOrderExpression(typeof(T), propInfo);

			var method = typeof(Queryable).GetMethods().FirstOrDefault(m => m.Name == "OrderByDescending" && m.GetParameters().Length == 2);
			var genericMethod = method.MakeGenericMethod(typeof(T), propInfo.PropertyType);
			return (IQueryable<T>)genericMethod.Invoke(null, new object[] { query, expr });
		}
	}
}
