using Makaan.Core.Entities;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Makaan.DAL.Context;
public class AppDbContext : IdentityDbContext<User>
{
    public DbSet<Department> Departments { get; set; }
    public DbSet<Agent> Agents { get; set; }
    public AppDbContext(DbContextOptions opt) : base(opt)   
    {
            
    }
}
