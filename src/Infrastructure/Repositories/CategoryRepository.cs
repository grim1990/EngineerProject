using Application.Interfaces.IRepositories;
using Domain.Entities;
using Infrastructure.Data;
using Microsoft.EntityFrameworkCore;

namespace Infrastructure.Repositories;

public class CategoryRepository : ICategoryRepository
{
    private readonly DataContext _context;

    public CategoryRepository(DataContext context)
    {
        _context = context;
    }

    public async Task<Category> GetCategoryByIdAsync(int id)
    {
        var category = await _context.Categories
                              .AsNoTracking()
                              .FirstOrDefaultAsync(category => category.Id == id);

        return category;
    }

    public async Task<List<Category>> GetAllCategoriesAsync(string userId)
    {
        var categories = await _context.Categories
                                .Where(category => category.UserId == userId)
                                .AsNoTracking()
                                .ToListAsync();

        return categories;
    }

    public async Task CreateCategoryAsync(Category category)
    {
        await _context.Categories.AddAsync(category);
        await _context.SaveChangesAsync();
    }

    public async Task EditCategoryAsync(Category category)
    {
        await _context.Categories
               .Where(c => c.Id == category.Id)
               .ExecuteUpdateAsync(p => p
               .SetProperty(x => x.Type, category.Type)
               .SetProperty(x => x.MainCategoryId, category.MainCategoryId)
               .SetProperty(x => x.Name, category.Name)
               .SetProperty(x => x.Description, category.Description)
               .SetProperty(x => x.Value, category.Value)
               .SetProperty(x => x.OneOrMonth, category.OneOrMonth)
               .SetProperty(x => x.EditDate, DateTime.Now)
               .SetProperty(x => x.IsAnnual, category.IsAnnual));
    }

    public async Task DeleteCategoryAsync(Category category)
    {
        await _context.Categories
               .Where(c => c.Id == category.Id)
               .ExecuteDeleteAsync();
    }
}