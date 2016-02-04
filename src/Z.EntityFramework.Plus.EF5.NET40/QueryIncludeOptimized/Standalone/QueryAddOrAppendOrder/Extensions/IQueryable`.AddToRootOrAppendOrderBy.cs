﻿// Description: Entity Framework Bulk Operations & Utilities (EF Bulk SaveChanges, Insert, Update, Delete, Merge | LINQ Query Cache, Deferred, Filter, IncludeFilter, IncludeOptimize | Audit)
// Website & Documentation: https://github.com/zzzprojects/Entity-Framework-Plus
// Forum: https://github.com/zzzprojects/EntityFramework-Plus/issues
// License: https://github.com/zzzprojects/EntityFramework-Plus/blob/master/LICENSE
// More projects: http://www.zzzprojects.com/
// Copyright (c) 2016 ZZZ Projects. All rights reserved.


#if STANDALONE
using System.Linq;

namespace Z.EntityFramework.Plus
{
    internal static partial class QueryAddOrAppendOrderExtensions
    {
        internal static IQueryable<T> AddToRootOrAppendOrderBy<T>(this IQueryable<T> query, params string[] columns)
        {
            var visitor = new QueryAddOrAppendOrderExpressionVisitor<T> {AddToRoot = true};
            return visitor.OrderBy(query, columns);
        }
    }
}
#endif