using DinDin.Domain.Users;
using DinDin.Infra.Postgres;
using DinDin.Infra.Postgres.Models;
using Microsoft.EntityFrameworkCore;

namespace DinDin.Infra.Users
{
    public class UserRepository(DinDinDbContext dbContext) : IUserRepository
    {
        private readonly DinDinDbContext _dbContext = dbContext;

        public async Task Add(User user)
        {
            var userModel = new UserModel
            {
                Name = user.Name,
                Email = user.Email,
                Password = user.Password,
                CreationDate = user.CreationDate
            };

            await _dbContext.AddAsync(userModel);
            await _dbContext.SaveChangesAsync();
        }

        public async Task<User?> GetUserByEmail(string email)
        {
            var userModel = await _dbContext.Users
                .AsNoTracking()
                .FirstOrDefaultAsync(user => user.Email == email);

            if (userModel is null)
                return null;

            return new User 
            {
                Id = userModel.Id,
                Name = userModel.Name,
                Email = userModel.Email,
                Password = userModel.Password,
                CreationDate = userModel.CreationDate,
            };
        }
    }
}