using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using TodoProject.EntityLayer.Concrete;

namespace TodoProject.DataAccessLayer.Concrete
{
    public class Context : IdentityDbContext<AppUser,AppRole,int>
    {
        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            base.OnConfiguring(optionsBuilder);
            optionsBuilder.UseSqlServer(@"server=WINDOWSXP\MSSQLSERVER01;
			initial catalog=TodoApi;integrated security=true;TrustServerCertificate=True");
        }

        public DbSet<Todo> Todos { get; set; }
    }
}
