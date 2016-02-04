﻿// Description: Entity Framework Bulk Operations & Utilities (EF Bulk SaveChanges, Insert, Update, Delete, Merge | LINQ Query Cache, Deferred, Filter, IncludeFilter, IncludeOptimize | Audit)
// Website & Documentation: https://github.com/zzzprojects/Entity-Framework-Plus
// Forum: https://github.com/zzzprojects/EntityFramework-Plus/issues
// License: https://github.com/zzzprojects/EntityFramework-Plus/blob/master/LICENSE
// More projects: http://www.zzzprojects.com/
// Copyright (c) 2016 ZZZ Projects. All rights reserved.


#if EF7
using System.Reflection;
using Microsoft.Data.Entity;

namespace Z.EntityFramework.Plus
{
    public static partial class ObjectContextExtensions
    {
        public static DbContext GetDbContext<TEntity>(this DbSet<TEntity> dbSet) where TEntity : class
        {
            var internalContext = dbSet.GetType().GetField("_context", BindingFlags.NonPublic | BindingFlags.Instance);
            return (DbContext) internalContext.GetValue(dbSet);
        }
    }
}
#endif