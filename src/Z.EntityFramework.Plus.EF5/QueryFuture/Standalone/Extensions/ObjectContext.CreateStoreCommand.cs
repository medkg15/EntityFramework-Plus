﻿// Description: Entity Framework Bulk Operations & Utilities (EF Bulk SaveChanges, Insert, Update, Delete, Merge | LINQ Query Cache, Deferred, Filter, IncludeFilter, IncludeOptimize | Audit)
// Website & Documentation: https://github.com/zzzprojects/Entity-Framework-Plus
// Forum: https://github.com/zzzprojects/EntityFramework-Plus/issues
// License: https://github.com/zzzprojects/EntityFramework-Plus/blob/master/LICENSE
// More projects: http://www.zzzprojects.com/
// Copyright (c) 2016 ZZZ Projects. All rights reserved.


#if STANDALONE && (EF5 || EF6)
using System.Data.Common;
#if EF5
using System.Data.EntityClient;
using System.Data.Objects;

#elif EF6
using System.Data.Entity.Core.EntityClient;
using System.Data.Entity.Core.Objects;

#endif

namespace Z.EntityFramework.Plus
{
    public static partial class QueryFutureExtensions
    {
        /// <summary>An ObjectContext extension method that creates store command .</summary>
        /// <param name="context">The context to act on.</param>
        /// <returns>The new store command from the context.</returns>
        internal static DbCommand CreateStoreCommand(this ObjectContext context)
        {
            var entityConnection = (EntityConnection) context.Connection;
            var command = entityConnection.StoreConnection.CreateCommand();
            command.Transaction = entityConnection.GetStoreTransaction();

            if (context.CommandTimeout.HasValue)
            {
                command.CommandTimeout = context.CommandTimeout.Value;
            }

            return command;
        }
    }
}
#endif