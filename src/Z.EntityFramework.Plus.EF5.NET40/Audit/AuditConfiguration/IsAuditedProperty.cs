﻿// Description: Entity Framework Bulk Operations & Utilities (EF Bulk SaveChanges, Insert, Update, Delete, Merge | LINQ Query Cache, Deferred, Filter, IncludeFilter, IncludeOptimize | Audit)
// Website & Documentation: https://github.com/zzzprojects/Entity-Framework-Plus
// Forum: https://github.com/zzzprojects/EntityFramework-Plus/issues
// License: https://github.com/zzzprojects/EntityFramework-Plus/blob/master/LICENSE
// More projects: http://www.zzzprojects.com/
// Copyright (c) 2016 ZZZ Projects. All rights reserved.

#if EF5
using System.Data.Objects;

#elif EF6
using System.Data.Entity.Core.Objects;

#elif EF7
using Microsoft.Data.Entity.ChangeTracking;

#endif

namespace Z.EntityFramework.Plus
{
    public partial class AuditConfiguration
    {
        /// <summary>Check if the property name is audited.</summary>
        /// <param name="entry">The entry.</param>
        /// <param name="propertyName">The property name.</param>
        /// <returns>true if the property name is audited, false if not.</returns>
#if EF5 || EF6
        public bool IsAuditedProperty(ObjectStateEntry entry, string propertyName)
#elif EF7
        public bool IsAuditedProperty(EntityEntry entry, string propertyName)
#endif
        {
            if (ExcludeIncludePropertyPredicates.Count == 0)
            {
                return true;
            }

            var type = entry.Entity.GetType();
            var key = string.Concat(type.FullName, ";", propertyName);
            bool value;

            if (!IsAuditedDictionary.TryGetValue(key, out value))
            {
                value = true;

                foreach (var excludeIncludePropertyFuncs in ExcludeIncludePropertyPredicates)
                {
                    var maybeIncluded = excludeIncludePropertyFuncs(entry.Entity, propertyName);
                    if (maybeIncluded.HasValue)
                    {
                        value = maybeIncluded.Value;
                    }
                }

                IsAuditedDictionary.TryAdd(key, value);
            }

            return value;
        }
    }
}