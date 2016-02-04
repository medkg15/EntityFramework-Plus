﻿// Description: Entity Framework Bulk Operations & Utilities (EF Bulk SaveChanges, Insert, Update, Delete, Merge | LINQ Query Cache, Deferred, Filter, IncludeFilter, IncludeOptimize | Audit)
// Website & Documentation: https://github.com/zzzprojects/Entity-Framework-Plus
// Forum: https://github.com/zzzprojects/EntityFramework-Plus/issues
// License: https://github.com/zzzprojects/EntityFramework-Plus/blob/master/LICENSE
// More projects: http://www.zzzprojects.com/
// Copyright (c) 2016 ZZZ Projects. All rights reserved.

using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;

namespace Z.EntityFramework.Plus
{
    public static partial class QueryAddOrAppendOrderExtensions
    {
        public static IQueryable<T> AddOrAppendOrderBy<T>(this IQueryable<T> query, params string[] columns)
        {
            return new QueryAddOrAppendOrderExpressionVisitor<T>().OrderBy(query, columns);
        }

        public static IQueryable<T> AddOrAppendOrderBy<T, TKey>(this IQueryable<T> query, IComparer<TKey> comparer, params string[] columns)
        {
            return new QueryAddOrAppendOrderExpressionVisitor<T>().OrderBy(query, columns, comparer);
        }

        public static IQueryable<TSource> AddOrAppendOrderBy<TSource, TKey>(this IQueryable<TSource> query, Expression<Func<TSource, TKey>> keySelector)
        {
            return new QueryAddOrAppendOrderExpressionVisitor<TSource, TKey>().OrderBy(query, keySelector);
        }

        public static IQueryable<TSource> AddOrAppendOrderBy<TSource, TKey>(this IQueryable<TSource> query, Expression<Func<TSource, TKey>> keySelector, IComparer<TKey> comparer)
        {
            return new QueryAddOrAppendOrderExpressionVisitor<TSource, TKey>().OrderBy(query, keySelector, comparer);
        }
    }
}