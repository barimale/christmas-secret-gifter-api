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
    public class CategoryRepository : ICategoryRepository
    {
        private readonly IMapper _mapper;
        private readonly AdministrationConsoleDbContext _context;
        private readonly ILogger<CategoryRepository> _logger;

        public CategoryRepository(
            ILogger<CategoryRepository> logger,
            AdministrationConsoleDbContext context,
            IMapper mapper)
        {
            _logger = logger;
            _context = context;
            _mapper = mapper;
        }

        public async Task<Category> AddAsync(Category item, CancellationToken cancellationToken)
        {
            try
            {
                cancellationToken.ThrowIfCancellationRequested();

                var mapped = _mapper.Map<CategoryEntry>(item);
                var result = await _context.Categories.AddAsync(mapped, cancellationToken);

                await _context.SaveChangesAsync(cancellationToken);

                var mappedResult = _mapper.Map<Category>(result.Entity);

                return mappedResult;
            }
            catch (Exception ex)
            {
                _logger.LogError(ex.Message);
                throw new SystemException("Adding failed", ex);
            }
        }

        public async Task<Category> UpdateAsync(Category item, CancellationToken cancellationToken)
        {
            try
            {
                cancellationToken.ThrowIfCancellationRequested();

                var mapped = _mapper.Map<CategoryEntry>(item);

                var found = await _context
                   .Categories
                   .Include(p => p.TranslatableDetails)
                   .ThenInclude(pp => pp.Language)
                   .AsQueryable()
                   .FirstOrDefaultAsync(p => p.Id == item.Id, cancellationToken);

                found.TranslatableDetails.Clear();
                found.TranslatableDetails = mapped.TranslatableDetails;
                found.KeyName = mapped.KeyName;

                await _context.SaveChangesAsync(cancellationToken);

                var result = _context.Update<CategoryEntry>(found);

                await _context.SaveChangesAsync(cancellationToken);

                var mappedResult = _mapper.Map<Category>(result.Entity);

                return mappedResult;
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

                var found = await _context.Categories.FirstOrDefaultAsync(p => p.Id == id, cancellationToken);

                var deleted = _context
                    .Categories
                    .Remove(found);

                var result = await _context.SaveChangesAsync(cancellationToken);

                return result;
            }
            catch (Exception ex)
            {
                _logger.LogError(ex.Message);
                throw new SystemException("Deleting failed", ex);
            }
        }

        public async Task<Category> GetByIdAsync(string id, CancellationToken cancellationToken)
        {
            try
            {
                cancellationToken.ThrowIfCancellationRequested();

                var found = await _context
                    .Categories
                    .Include(p => p.TranslatableDetails)
                    .ThenInclude(pp => pp.Language)
                    .AsQueryable()
                    .FirstOrDefaultAsync(p => p.Id == id, cancellationToken);

                if (found == null)
                {
                    return null;
                }

                var mappedResult = _mapper.Map<Category>(found);

                return mappedResult;
            }
            catch (Exception ex)
            {
                _logger.LogError(ex.Message);
            }

            return null;
        }

        public async Task<Category[]> GetAllAsync(CancellationToken? cancellationToken)
        {
            try
            {
                cancellationToken?.ThrowIfCancellationRequested();

                var allOfThem = await _context
                    .Categories
                    .Include(p => p.TranslatableDetails)
                    .ThenInclude(pp => pp.Language)
                    .ToArrayAsync(cancellationToken?? default);

                var mapped = allOfThem.Select(p => _mapper.Map<Category>(p));

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
