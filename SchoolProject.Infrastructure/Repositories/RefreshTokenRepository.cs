using Microsoft.EntityFrameworkCore;
using SchoolProject.Data.Entities.Identity;
using SchoolProject.Infrastructure.Abstracts;
using SchoolProject.Infrastructure.Data;
using SchoolProject.Infrastructure.InfrastructureBases;

namespace SchoolProject.Infrastructure.Repositories
{
    public class RefreshTokenRepository : GenericRepositoryAsync<UserRefreshToken>, IRefreshTokenRepository
    {
        #region Fields
        private readonly DbSet<UserRefreshToken> _refreshTokens;
        #endregion

        #region Constructors
        public RefreshTokenRepository(ApplicationDbContext dbcontext) : base(dbcontext)
        {
            _refreshTokens = dbcontext.Set<UserRefreshToken>();
        }
        #endregion

        #region Handles Functions
        public async Task<List<UserRefreshToken>> GetRefreshTokensListAsync()
        {
            return await _refreshTokens.ToListAsync();
        }
        public async Task<UserRefreshToken> GetRefreshTokenByIdAsync(int id)
        {

            return await _dbContext.Set<UserRefreshToken>().FindAsync(id);
        }


        #endregion
    }
}
