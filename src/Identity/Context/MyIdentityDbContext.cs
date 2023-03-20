namespace Identity.Context
{
    using Microsoft.AspNetCore.Identity;
    using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
    using Microsoft.EntityFrameworkCore;

    public class MyIdentityDbContext : IdentityDbContext<IdentityUser>
    {
        public MyIdentityDbContext(DbContextOptions<MyIdentityDbContext> options)
            : base(options)
        {
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);
        }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            //optionsBuilder.en
            //optionsBuilder
            //    .UseLazyLoadingProxies()
            //    .UseSqlite("Data Source=c:\\db\\BrianBotDB.sqlite"); //// TODO pegar o endereco do DB pelo arquivo config
        }
    }
}
