
using Microsoft.EntityFrameworkCore;

namespace KoperasiTentera.DB
{
    public class KoperasiTenteraDBContext : DbContext
    {
        public KoperasiTenteraDBContext(DbContextOptions<KoperasiTenteraDBContext> options)
       : base(options)
        {
        }
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

        }
    }
}
