using API.Models;
using DataAccess.Data;
using Microsoft.EntityFrameworkCore;

namespace API.Services
{
    public class CoffeeShopService : ICoffeeShopService
    {
        private readonly ApplicationDbContext _dbContext;

        public CoffeeShopService(ApplicationDbContext dbContext)
        {
            _dbContext = dbContext;
        }

        public async Task<List<CoffeeShopDto>> GetListAsync()
        {
            return await _dbContext.CoffeeShops.Select(x => new CoffeeShopDto
            {
                Id = x.Id,
                Name = x.Name,
                OpeningHours = x.OpeningHours,
                Address = x.Address
            }).ToListAsync();
        }
    }
}
