﻿// Description: Entity Framework Bulk Operations & Utilities (EF Bulk SaveChanges, Insert, Update, Delete, Merge | LINQ Query Cache, Deferred, Filter, IncludeFilter, IncludeOptimize | Audit)
// Website & Documentation: https://github.com/zzzprojects/Entity-Framework-Plus
// Forum: https://github.com/zzzprojects/EntityFramework-Plus/issues
// License: https://github.com/zzzprojects/EntityFramework-Plus/blob/master/LICENSE
// More projects: http://www.zzzprojects.com/
// Copyright (c) 2016 ZZZ Projects. All rights reserved.


#if EF7
using System.Data.Common;
using Microsoft.Data.Entity;

namespace Z.EntityFramework.Plus
{
    public static partial class Extensions
    {
        /// <summary>A DbContext extension method that creates a new store command.</summary>
        /// <param name="context">The context to act on.</param>
        /// <returns>The new store command from the DbContext.</returns>
        public static DbCommand CreateStoreCommand(this DbContext context)
        {
            var entityConnection = context.Database.GetDbConnection();
            var command = entityConnection.CreateCommand();
            // TODO: support transaction
            //command.Transaction = entityConnection.GetStoreTransaction();

            var commandTimeout = context.Database.GetCommandTimeout();
            if (commandTimeout.HasValue)
            {
                command.CommandTimeout = commandTimeout.Value;
            }

            return command;
        }
    }
}
#endif