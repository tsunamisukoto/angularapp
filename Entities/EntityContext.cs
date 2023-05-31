using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Entities;
public class EntityContext : DbContext
{
    public EntityContext()
    {
        
    }
    public EntityContext(DbContextOptions<EntityContext> options) : base(options)
    {
    }

    public virtual DbSet<Customer> Customers { get; set; }
}
