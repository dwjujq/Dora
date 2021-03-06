﻿using System;
using System.Collections.Generic;
using System.Linq;

namespace Dora.GraphQL.GraphTypes
{
    internal static class  TypeExtensions
    {
        public static bool IsEnumerable(this Type type, out Type elementType)
        {
            elementType = null;
            if (type == typeof(string))
            {
                return false;
            }

            if (type.IsArray)
            {
                elementType = type.GetElementType();
                return true;
            }

            if (type.IsGenericType && type.GetGenericTypeDefinition() == typeof(IEnumerable<>))
            {
                elementType = type.GenericTypeArguments[0];
                return true;
            }

            var enumerableType = type.GetInterfaces().Where(it => it.IsGenericType && it.GetGenericTypeDefinition() == (typeof(IEnumerable<>))).FirstOrDefault();
            if (enumerableType != null)
            {
                elementType = enumerableType.GenericTypeArguments[0];
                return true;
            }
            return false;
        }
    }
}
