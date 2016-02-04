﻿// Description: Entity Framework Bulk Operations & Utilities (EF Bulk SaveChanges, Insert, Update, Delete, Merge | LINQ Query Cache, Deferred, Filter, IncludeFilter, IncludeOptimize | Audit)
// Website & Documentation: https://github.com/zzzprojects/Entity-Framework-Plus
// Forum: https://github.com/zzzprojects/EntityFramework-Plus/issues
// License: https://github.com/zzzprojects/EntityFramework-Plus/blob/master/LICENSE
// More projects: http://www.zzzprojects.com/
// Copyright (c) 2016 ZZZ Projects. All rights reserved.


#if EF5 || EF6
using System.Linq;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Z.EntityFramework.Plus;
#if EF5 || EF6
using System.Data.Entity;

#elif EF7
using Microsoft.Data.Entity;

#endif

namespace Z.Test.EntityFramework.Plus
{
    public partial class Audit_EntitySoftAdded
    {
        [TestMethod]
        public void Entity_Basic()
        {
            TestContext.DeleteAll(x => x.AuditEntryProperties);
            TestContext.DeleteAll(x => x.AuditEntries);
            TestContext.DeleteAll(x => x.Entity_Basics);

            TestContext.Insert(x => x.Entity_Basics, 3);

            var audit = AuditHelper.AutoSaveAudit();
            audit.Configuration.SoftAdded(x => true);

            using (var ctx = new TestContext())
            {
                ctx.Entity_Basics.ToList().ForEach(x => x.ColumnInt++);

                ctx.SaveChanges(audit);
            }

            // UnitTest - Audit
            {
                var entries = audit.Entries;

                // Entries
                {
                    // Entries Count
                    Assert.AreEqual(3, entries.Count);

                    // Entries State
                    Assert.AreEqual(AuditEntryState.EntitySoftAdded, entries[0].State);
                    Assert.AreEqual(AuditEntryState.EntitySoftAdded, entries[1].State);
                    Assert.AreEqual(AuditEntryState.EntitySoftAdded, entries[2].State);

                    // Entries EntitySetName
                    Assert.AreEqual(TestContext.TypeName(x => x.Entity_Basics), entries[0].EntitySetName);
                    Assert.AreEqual(TestContext.TypeName(x => x.Entity_Basics), entries[1].EntitySetName);
                    Assert.AreEqual(TestContext.TypeName(x => x.Entity_Basics), entries[2].EntitySetName);

                    // Entries TypeName
                    Assert.AreEqual(typeof (Entity_Basic).Name, entries[0].EntityTypeName);
                    Assert.AreEqual(typeof (Entity_Basic).Name, entries[1].EntityTypeName);
                    Assert.AreEqual(typeof (Entity_Basic).Name, entries[2].EntityTypeName);
                }

                // Properties
                {
                    var propertyIndex = -1;

                    // Properties Count
                    Assert.AreEqual(1, entries[0].Properties.Count);
                    Assert.AreEqual(1, entries[1].Properties.Count);
                    Assert.AreEqual(1, entries[2].Properties.Count);

                    // ColumnInt
                    propertyIndex = 0;
                    Assert.AreEqual("ColumnInt", entries[0].Properties[propertyIndex].PropertyName);
                    Assert.AreEqual("ColumnInt", entries[1].Properties[propertyIndex].PropertyName);
                    Assert.AreEqual("ColumnInt", entries[2].Properties[propertyIndex].PropertyName);
                    Assert.AreEqual(0, entries[0].Properties[propertyIndex].OldValue);
                    Assert.AreEqual(1, entries[1].Properties[propertyIndex].OldValue);
                    Assert.AreEqual(2, entries[2].Properties[propertyIndex].OldValue);
                    Assert.AreEqual(1, entries[0].Properties[propertyIndex].NewValue);
                    Assert.AreEqual(2, entries[1].Properties[propertyIndex].NewValue);
                    Assert.AreEqual(3, entries[2].Properties[propertyIndex].NewValue);
                }
            }

            // UnitTest - Audit (Database)
            {
                using (var ctx = new TestContext())
                {
                    // ENSURE order
                    var entries = ctx.AuditEntries.OrderBy(x => x.AuditEntryID).Include(x => x.Properties).ToList();
                    entries.ForEach(x => x.Properties = x.Properties.OrderBy(y => y.AuditEntryPropertyID).ToList());

                    // Entries
                    {
                        // Entries Count
                        Assert.AreEqual(3, entries.Count);

                        // Entries State
                        Assert.AreEqual(AuditEntryState.EntitySoftAdded, entries[0].State);
                        Assert.AreEqual(AuditEntryState.EntitySoftAdded, entries[1].State);
                        Assert.AreEqual(AuditEntryState.EntitySoftAdded, entries[2].State);

                        // Entries EntitySetName
                        Assert.AreEqual(TestContext.TypeName(x => x.Entity_Basics), entries[0].EntitySetName);
                        Assert.AreEqual(TestContext.TypeName(x => x.Entity_Basics), entries[1].EntitySetName);
                        Assert.AreEqual(TestContext.TypeName(x => x.Entity_Basics), entries[2].EntitySetName);

                        // Entries TypeName
                        Assert.AreEqual(typeof (Entity_Basic).Name, entries[0].EntityTypeName);
                        Assert.AreEqual(typeof (Entity_Basic).Name, entries[1].EntityTypeName);
                        Assert.AreEqual(typeof (Entity_Basic).Name, entries[2].EntityTypeName);
                    }

                    // Properties
                    {
                        var propertyIndex = -1;

                        // Properties Count
                        Assert.AreEqual(1, entries[0].Properties.Count);
                        Assert.AreEqual(1, entries[1].Properties.Count);
                        Assert.AreEqual(1, entries[2].Properties.Count);

                        // ColumnInt
                        propertyIndex = 0;
                        Assert.AreEqual("ColumnInt", entries[0].Properties[propertyIndex].PropertyName);
                        Assert.AreEqual("ColumnInt", entries[1].Properties[propertyIndex].PropertyName);
                        Assert.AreEqual("ColumnInt", entries[2].Properties[propertyIndex].PropertyName);
                        Assert.AreEqual("0", entries[0].Properties[propertyIndex].OldValue);
                        Assert.AreEqual("1", entries[1].Properties[propertyIndex].OldValue);
                        Assert.AreEqual("2", entries[2].Properties[propertyIndex].OldValue);
                        Assert.AreEqual("1", entries[0].Properties[propertyIndex].NewValue);
                        Assert.AreEqual("2", entries[1].Properties[propertyIndex].NewValue);
                        Assert.AreEqual("3", entries[2].Properties[propertyIndex].NewValue);
                    }
                }
            }
        }
    }
}

#endif