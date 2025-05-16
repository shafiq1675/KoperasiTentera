

using KoperasiTentera.DB;

namespace KoperasiTentera.Repositories
{
    public interface ILoginRepository
    {
    }
    public class RegistrationRepository: ILoginRepository
    {
        private readonly KoperasiTenteraDBContext _dbContext;
        private static bool _ensureCreated { get; set; } = false;

        public RegistrationRepository(KoperasiTenteraDBContext dbContext)
        {
            _dbContext = dbContext;

            if (!_ensureCreated)
            {
                _dbContext.Database.EnsureCreated();
                _ensureCreated = true;
            }
        }       

    }
}
