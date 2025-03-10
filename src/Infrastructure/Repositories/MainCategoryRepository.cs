using Application.Interfaces.IRepositories;
using Domain.Entities;
using Infrastructure.Data;
using Microsoft.EntityFrameworkCore;

namespace Infrastructure.Repositories;

public class MainCategoryRepository : IMainCategoryRepository
{
    private readonly DataContext _context;

    public MainCategoryRepository(DataContext context)
    {
        _context = context;
    }

    public async Task<MainCategory> GetMainCategoryByIdAsync(int id)
    {
        var mainCategory = await _context.MainCategories
                                  .AsNoTracking()
                                  .FirstOrDefaultAsync(m => m.Id == id);

        return mainCategory;
    }

    public async Task<List<MainCategory>> GetAllMainCategoriesAsync(string userId)
    {
        var mainCategories = await _context.MainCategories
                                    .Where(m => m.UserId == userId)
                                    .AsNoTracking()
                                    .ToListAsync();

        return mainCategories;
    }

    public async Task CreateMainCategoryAsync(MainCategory mainCategory)
    {
        await _context.MainCategories.AddAsync(mainCategory);
        await _context.SaveChangesAsync();
    }

    public async Task EditMainCategoryAsync(MainCategory mainCategory)
    {
        await _context.MainCategories
               .Where(m => m.Id == mainCategory.Id)
               .ExecuteUpdateAsync(p => p
               .SetProperty(x => x.Name, mainCategory.Name)
               .SetProperty(x => x.Description, mainCategory.Description)
               .SetProperty(x => x.EditDate, DateTime.Now)
               .SetProperty(x => x.IsAnnual, mainCategory.IsAnnual));
    }

    public async Task DeleteMainCategoryAsync(MainCategory mainCategory, List<Category> categories, List<Operation> operations)
    {
        _context.Operations.RemoveRange(operations);
        _context.Categories.RemoveRange(categories);
        _context.MainCategories.Remove(mainCategory);
        await _context.SaveChangesAsync();
    }
}