﻿using Microsoft.EntityFrameworkCore;
using Ordering.Domain.Common;
using System;
using System.Threading;
using System.Threading.Tasks;

namespace Ordering.Infrastructure.Persistence
{
    public class OrderContext : DbContext
    {
        public OrderContext(DbContextOptions<OrderContext> options) : base(options)
        {  }


        public override Task<int> SaveChangesAsync(CancellationToken cancellationToken = default)
        {
            foreach (var entry in ChangeTracker.Entries<EntityBase>())
            {
                switch (entry.State)
                {   
                    case EntityState.Modified:
                        entry.Entity.LastModifiedBy = "User";
                        entry.Entity.LastModifiedDate = DateTime.UtcNow;
                        break;
                    case EntityState.Added:
                        entry.Entity.CreatedBy = "User";
                        entry.Entity.CreatedDate = DateTime.UtcNow;
                        break;

                    default:
                        break;
                }
            }
            
            return base.SaveChangesAsync(cancellationToken);
        }

    }
}