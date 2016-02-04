﻿// Description: Entity Framework Bulk Operations & Utilities (EF Bulk SaveChanges, Insert, Update, Delete, Merge | LINQ Query Cache, Deferred, Filter, IncludeFilter, IncludeOptimize | Audit)
// Website & Documentation: https://github.com/zzzprojects/Entity-Framework-Plus
// Forum: https://github.com/zzzprojects/EntityFramework-Plus/issues
// License: https://github.com/zzzprojects/EntityFramework-Plus/blob/master/LICENSE
// More projects: http://www.zzzprojects.com/
// Copyright (c) 2016 ZZZ Projects. All rights reserved.

using System;

namespace Z.EntityFramework.Plus
{
    public static partial class QueryDeferredExtensions
    {
        private static class Error
        {
            internal static Exception ArgumentNull(string paramName)
            {
                return new ArgumentNullException(paramName);
            }

            internal static Exception ArgumentOutOfRange(string paramName)
            {
                return new ArgumentOutOfRangeException(paramName);
            }
        }
    }
}