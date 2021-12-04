using Albergue.Administrator.Entities;
using Albergue.Administrator.Model;
using Albergue.Administrator.Repository;
using AutoMapper;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using System;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace Albergue.Administrator.SQLite.Database.Repositories
{
    public class ItemRepository : IItemRepository
    {
        private readonly IMapper _mapper;
        private readonly AdministrationConsoleDbContext _context;
        private readonly ILogger<ItemRepository> _logger;

        public ItemRepository(ILogger<ItemRepository> logger, AdministrationConsoleDbContext context, IMapper mapper)
        {
            _logger = logger;
            _context = context;
            _mapper = mapper;
        }

        public async Task<ShopItem> AddAsync(ShopItem item, CancellationToken cancellationToken)
        {
            try
            {
                cancellationToken.ThrowIfCancellationRequested();

                var mapped = _mapper.Map<ShopItemEntry>(item);

                var result = await _context.ShopItems.AddAsync(mapped, cancellationToken);

                await _context.SaveChangesAsync(cancellationToken);

                return _mapper.Map<ShopItem>(result.Entity);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex.Message);
                throw new SystemException("Adding failed", ex);
            }
        }

        public async Task<ShopItem> UpdateAsync(ShopItem item, CancellationToken cancellationToken)
        {
            try
            {
                cancellationToken.ThrowIfCancellationRequested();

                var mapped = _mapper.Map<ShopItemEntry>(item);

                var found = await _context
                    .ShopItems
                    .Include(p => p.Images)
                    .Include(p => p.TranslatableDetails)
                    .ThenInclude(pp => pp.Language)
                    .AsQueryable()
                    .FirstOrDefaultAsync(p => p.Id == item.Id, cancellationToken);

                found.Images.Clear();
                found.TranslatableDetails.Clear();

                found.Images = mapped.Images;
                found.TranslatableDetails = mapped.TranslatableDetails;
                found.Price = mapped.Price;
                found.CategoryId = mapped.CategoryId;
                found.Active = mapped.Active;

                await _context.SaveChangesAsync(cancellationToken);

                var result = _context
                    .Update<ShopItemEntry>(found);

                await _context.SaveChangesAsync(cancellationToken);

                return _mapper.Map<ShopItem>(result.Entity);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex.Message);
                throw new SystemException("Updating failed", ex);
            }
        }

        public async Task<int> DeleteAsync(string id, CancellationToken cancellationToken)
        {
            try
            {
                cancellationToken.ThrowIfCancellationRequested();

                var toBeDeleted = await _context
                    .ShopItems
                    .FirstOrDefaultAsync(p => p.Id == id, cancellationToken);

                var deleted = _context.ShopItems.Remove(toBeDeleted);

                return await _context.SaveChangesAsync(cancellationToken);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex.Message);
                throw new SystemException("Deleting failed", ex);
            }
        }

        public async Task<ShopItem> GetByIdAsync(string id, CancellationToken cancellationToken)
        {
            try
            {
                cancellationToken.ThrowIfCancellationRequested();

                var found = await _context
                    .ShopItems
                    .Include(p => p.Images)
                    .Include(p => p.TranslatableDetails)
                    .ThenInclude(pp => pp.Language)
                    .AsQueryable()
                    .FirstOrDefaultAsync(p => p.Id == id, cancellationToken);

                if (found == null)
                {
                    return null;
                }

                return _mapper.Map<ShopItem>(found);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex.Message);
            }

            return null;
        }

        public async Task<ShopItem[]> GetAllAsync(CancellationToken? cancellationToken)
        {
            try
            {
                cancellationToken?.ThrowIfCancellationRequested();

                var allOfThem = await _context
                    .ShopItems
                    .Include(p => p.Images)
                    .Include(p => p.TranslatableDetails)
                    .ThenInclude(pp => pp.Language)
                    .ToArrayAsync(cancellationToken?? default);

                var mapped = allOfThem.Select(p => _mapper.Map<ShopItem>(p));

                return mapped.ToArray();
            }
            catch (Exception ex)
            {
                _logger.LogError(ex.Message);
            }

            return null;
        }

        public async Task<ShopItem[]> GetByCategoryIdAsync(string categoryId, CancellationToken cancellationToken)
        {
            try
            {
                cancellationToken.ThrowIfCancellationRequested();

                var allOfThem = await _context
                    .ShopItems
                    .Include(p => p.Images)
                    .Include(p => p.TranslatableDetails)
                    .ThenInclude(pp => pp.Language)
                    .AsQueryable()
                    .Where(p => p.CategoryId == categoryId)
                    .ToArrayAsync(cancellationToken);

                var mapped = allOfThem.Select(p => _mapper.Map<ShopItem>(p));

                return mapped.ToArray();
            }
            catch (Exception ex)
            {
                _logger.LogError(ex.Message);
            }

            return null;
        }
    }
}
