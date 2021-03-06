﻿namespace Dora.GraphQL
{
    internal static class GraphDefaults
    {
        public static class GraphSchema
        {
            public const string GraphTypeName = "GraphQL";
            public const string QueryFieldName = "Query";
            public const string MutationFieldName = "Mutation";
            public const string SubscriptionFieldName = "Subscription";
        }

        public static class PropertyNames
        {
            public const string HasCustomResolvers = "__HasCustomResolver";
            public const string IncludeAllFields = "__IncludeAllFields";
            public const string IsSubQueryTree = "__IsSubQueryTree";
            public const string QueryResultType = "__QueryResultType";
            public const string InlineArguments = "__InlineArguments";
        }
    }
}
