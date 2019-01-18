﻿using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using System;
using System.Linq;

namespace Dora.GraphQL.GraphTypes
{
    public partial class GraphValueResolver
    {
        public static GraphValueResolver Complex(Type type)
        {
            if (type.IsEnumerableType(out _))
            {
                throw new GraphException($"GraphValueResolver cannot be created based on an enumerable type '{type}'");
            }

            var name = type.IsGenericType
                ? $"{type.Name.Substring(0, type.Name.IndexOf('`'))}Of{string.Join("", type.GetGenericArguments().Select(it => it.Name))}"
                : type.Name;
            Func<object, object> resolver = rawValue => ResolveComplex(type,rawValue, name);
            return new GraphValueResolver(name, type, false, resolver);
        }

        private static object ResolveComplex(Type type, object rawValue, string name)
        {
            if (type.IsAssignableFrom(rawValue.GetType()))
            {
                return rawValue;
            }
            var jToken = rawValue as JToken;
            if (null != jToken)
            {
                return jToken.ToObject(type);
            }
            throw new GraphException($"Cannot resolve '{rawValue}' as a/an {name} value.");
        }
    }
}