using Domain.Entities;

namespace Infrastructure.Data;

public class DataSeeder
{
    private readonly DataContext _context;

    public DataSeeder(DataContext context)
    {
        _context = context;
    }

    public async Task SeedDefaultData(string userId)
    {
        var mainCategoryOne = new MainCategory
        {
            Name = "Wpływy",
            Description = "Wpływy",
            UserId = userId,
            CreateDate = DateTime.Now,
            EditDate = null,
            IsAnnual = true
        };

        await _context.MainCategories.AddAsync(mainCategoryOne);
        _context.SaveChanges();

        var categoriesOne = new List<Category>()
        {
            new Category
            {
                Type = 1,
                MainCategoryId = mainCategoryOne.Id,
                Name = "Pensja",
                Description = "Pensja",
                UserId = userId,
                Value = 0,
                OneOrMonth = false,
                CreateDate = DateTime.Now,
                EditDate = null,
                IsAnnual = true
            },
            new Category
            {
                Type = 1,
                MainCategoryId = mainCategoryOne.Id,
                Name = "Odsetki bankowe",
                Description = "Odsetki bankowe",
                UserId = userId,
                Value = 0,
                OneOrMonth = false,
                CreateDate = DateTime.Now,
                EditDate = null,
                IsAnnual = true
            },
            new Category
            {
                Type = 1,
                MainCategoryId = mainCategoryOne.Id,
                Name = "Sprzedaż",
                Description = "Sprzedaż",
                UserId = userId,
                Value = 0,
                OneOrMonth = false,
                CreateDate = DateTime.Now,
                EditDate = null,
                IsAnnual = true
            },
            new Category
            {
                Type = 1,
                MainCategoryId = mainCategoryOne.Id,
                Name = "Inne",
                Description = "Inne",
                UserId = userId,
                Value = 0,
                OneOrMonth = false,
                CreateDate = DateTime.Now,
                EditDate = null,
                IsAnnual = true
            }
        };

        var mainCategoryTwo = new MainCategory
        {
            Name = "Jedzenie",
            Description = "Jedzenie",
            UserId = userId,
            CreateDate = DateTime.Now,
            EditDate = null,
            IsAnnual = true
        };

        await _context.MainCategories.AddAsync(mainCategoryTwo);
        _context.SaveChanges();

        var categoriesTwo = new List<Category>()
        {
            new Category
            {
                Type = 0,
                MainCategoryId = mainCategoryTwo.Id,
                Name = "Jedzenie dom",
                Description = "Jedzenie dom",
                UserId = userId,
                Value = 0,
                OneOrMonth = false,
                CreateDate = DateTime.Now,
                EditDate = null,
                IsAnnual = true
            },
            new Category
            {
                Type = 0,
                MainCategoryId = mainCategoryTwo.Id,
                Name = "Restauracje",
                Description = "Restauracje",
                UserId = userId,
                Value = 0,
                OneOrMonth = false,
                CreateDate = DateTime.Now,
                EditDate = null,
                IsAnnual = true
            },
            new Category
            {
                Type = 0,
                MainCategoryId = mainCategoryTwo.Id,
                Name = "Przekąski",
                Description = "Przekąski",
                UserId = userId,
                Value = 0,
                OneOrMonth = false,
                CreateDate = DateTime.Now,
                EditDate = null,
                IsAnnual = true
            }
        };

        var mainCategoryThree = new MainCategory
        {
            Name = "Transport",
            Description = "Transport",
            UserId = userId,
            CreateDate = DateTime.Now,
            EditDate = null,
            IsAnnual = false
        };

        await _context.MainCategories.AddAsync(mainCategoryThree);
        _context.SaveChanges();

        var categoriesThree = new List<Category>()
        {
            new Category
            {
                Type = 0,
                MainCategoryId = mainCategoryThree.Id,
                Name = "Paliwo",
                Description = "Paliwo",
                UserId = userId,
                Value = 0,
                OneOrMonth = false,
                CreateDate = DateTime.Now,
                EditDate = null,
                IsAnnual = false
            },
            new Category
            {
                Type = 0,
                MainCategoryId = mainCategoryThree.Id,
                Name = "Przeglądy i naprawy samochodu",
                Description = "Przeglądy i naprawy samochodu",
                UserId = userId,
                Value = 0,
                OneOrMonth = false,
                CreateDate = DateTime.Now,
                EditDate = null,
                IsAnnual = false
            },
            new Category
            {
                Type = 0,
                MainCategoryId = mainCategoryThree.Id,
                Name = "Ubezpieczenie samochodu",
                Description = "Ubezpieczenie samochodu",
                UserId = userId,
                Value = 0,
                OneOrMonth = false,
                CreateDate = DateTime.Now,
                EditDate = null,
                IsAnnual = false
            },
            new Category
            {
                Type = 0,
                MainCategoryId = mainCategoryThree.Id,
                Name = "Komunikacja miejska",
                Description = "Komunikacja miejska",
                UserId = userId,
                Value = 0,
                OneOrMonth = false,
                CreateDate = DateTime.Now,
                EditDate = null,
                IsAnnual = false
            }
        };

        var mainCategoryFour = new MainCategory
        {
            Name = "Zdrowie",
            Description = "Zdrowie",
            UserId = userId,
            CreateDate = DateTime.Now,
            EditDate = null,
            IsAnnual = false
        };

        await _context.MainCategories.AddAsync(mainCategoryFour);
        _context.SaveChanges();

        var categoriesFour = new List<Category>()
        {
            new Category
            {
                Type = 0,
                MainCategoryId = mainCategoryFour.Id,
                Name = "Leki",
                Description = "Leki",
                UserId = userId,
                Value = 0,
                OneOrMonth = false,
                CreateDate = DateTime.Now,
                EditDate = null,
                IsAnnual = false
            },
            new Category
            {
                Type = 0,
                MainCategoryId = mainCategoryFour.Id,
                Name = "Badania płatne",
                Description = "Badania płatne",
                UserId = userId,
                Value = 0,
                OneOrMonth = false,
                CreateDate = DateTime.Now,
                EditDate = null,
                IsAnnual = false
            },
            new Category
            {
                Type = 0,
                MainCategoryId = mainCategoryFour.Id,
                Name = "Wizyty u lekarza",
                Description = "Wizyty u lekarza",
                UserId = userId,
                Value = 0,
                OneOrMonth = false,
                CreateDate = DateTime.Now,
                EditDate = null,
                IsAnnual = false
            }
        };

        var mainCategoryFive = new MainCategory
        {
            Name = "Mieszkanie",
            Description = "Mieszkanie",
            UserId = userId,
            CreateDate = DateTime.Now,
            EditDate = null,
            IsAnnual = false
        };

        await _context.MainCategories.AddAsync(mainCategoryFive);
        _context.SaveChanges();

        var categoriesFive = new List<Category>()
        {
            new Category
            {
                Type = 0,
                MainCategoryId = mainCategoryFive.Id,
                Name = "Czynsz",
                Description = "Czynsz",
                UserId = userId,
                Value = 0,
                OneOrMonth = false,
                CreateDate = DateTime.Now,
                EditDate = null,
                IsAnnual = false
            },
            new Category
            {
                Type = 0,
                MainCategoryId = mainCategoryFive.Id,
                Name = "Prąd",
                Description = "Prąd",
                UserId = userId,
                Value = 0,
                OneOrMonth = false,
                CreateDate = DateTime.Now,
                EditDate = null,
                IsAnnual = false
            },
            new Category
            {
                Type = 0,
                MainCategoryId = mainCategoryFive.Id,
                Name = "Gaz",
                Description = "Gaz",
                UserId = userId,
                Value = 0,
                OneOrMonth = false,
                CreateDate = DateTime.Now,
                EditDate = null,
                IsAnnual = false
            },
            new Category
            {
                Type = 0,
                MainCategoryId = mainCategoryFive.Id,
                Name = "Telefon",
                Description = "Telefon",
                UserId = userId,
                Value = 0,
                OneOrMonth = false,
                CreateDate = DateTime.Now,
                EditDate = null,
                IsAnnual = false
            },
            new Category
            {
                Type = 0,
                MainCategoryId = mainCategoryFive.Id,
                Name = "Internet",
                Description = "Internet",
                UserId = userId,
                Value = 0,
                OneOrMonth = false,
                CreateDate = DateTime.Now,
                EditDate = null,
                IsAnnual = false
            },
            new Category
            {
                Type = 0,
                MainCategoryId = mainCategoryFive.Id,
                Name = "Wyposażenie",
                Description = "Wyposażenie",
                UserId = userId,
                Value = 0,
                OneOrMonth = false,
                CreateDate = DateTime.Now,
                EditDate = null,
                IsAnnual = false
            },
            new Category
            {
                Type = 0,
                MainCategoryId = mainCategoryFive.Id,
                Name = "Remonty i naprawy",
                Description = "Remonty i naprawy",
                UserId = userId,
                Value = 0,
                OneOrMonth = false,
                CreateDate = DateTime.Now,
                EditDate = null,
                IsAnnual = false
            }
        };

        var mainCategorySix = new MainCategory
        {
            Name = "Higiena",
            Description = "Higiena",
            UserId = userId,
            CreateDate = DateTime.Now,
            EditDate = null,
            IsAnnual = false
        };

        await _context.MainCategories.AddAsync(mainCategorySix);
        _context.SaveChanges();

        var categoriesSix = new List<Category>()
        {
            new Category
            {
                Type = 0,
                MainCategoryId = mainCategorySix.Id,
                Name = "Kosmetyki",
                Description = "Kosmetyki",
                UserId = userId,
                Value = 0,
                OneOrMonth = false,
                CreateDate = DateTime.Now,
                EditDate = null,
                IsAnnual = false
            },
            new Category
            {
                Type = 0,
                MainCategoryId = mainCategorySix.Id,
                Name = "Fryzjer",
                Description = "Fryzjer",
                UserId = userId,
                Value = 0,
                OneOrMonth = false,
                CreateDate = DateTime.Now,
                EditDate = null,
                IsAnnual = false
            }
        };

        var mainCategorySeven = new MainCategory
        {
            Name = "Ubrania",
            Description = "Ubrania",
            UserId = userId,
            CreateDate = DateTime.Now,
            EditDate = null,
            IsAnnual = false
        };

        await _context.MainCategories.AddAsync(mainCategorySeven);
        _context.SaveChanges();

        var categoriesSeven = new List<Category>()
        {
            new Category
            {
                Type = 0,
                MainCategoryId = mainCategorySeven.Id,
                Name = "Odzież",
                Description = "Odzież",
                UserId = userId,
                Value = 0,
                OneOrMonth = false,
                CreateDate = DateTime.Now,
                EditDate = null,
                IsAnnual = false
            },
            new Category
            {
                Type = 0,
                MainCategoryId = mainCategorySeven.Id,
                Name = "Obuwie",
                Description = "Obuwie",
                UserId = userId,
                Value = 0,
                OneOrMonth = false,
                CreateDate = DateTime.Now,
                EditDate = null,
                IsAnnual = false
            },
            new Category
            {
                Type = 0,
                MainCategoryId = mainCategorySeven.Id,
                Name = "Dodatki",
                Description = "Dodatki",
                UserId = userId,
                Value = 0,
                OneOrMonth = false,
                CreateDate = DateTime.Now,
                EditDate = null,
                IsAnnual = false
            }
        };

        var mainCategoryEight = new MainCategory
        {
            Name = "Rozrywka",
            Description = "Rozrywka",
            UserId = userId,
            CreateDate = DateTime.Now,
            EditDate = null,
            IsAnnual = false
        };

        await _context.MainCategories.AddAsync(mainCategoryEight);
        _context.SaveChanges();

        var categoriesEight = new List<Category>()
        {
            new Category
            {
                Type = 0,
                MainCategoryId = mainCategoryEight.Id,
                Name = "Wyjścia",
                Description = "Wyjścia",
                UserId = userId,
                Value = 0,
                OneOrMonth = false,
                CreateDate = DateTime.Now,
                EditDate = null,
                IsAnnual = false
            },
            new Category
            {
                Type = 0,
                MainCategoryId = mainCategoryEight.Id,
                Name = "Koncerty",
                Description = "Koncerty",
                UserId = userId,
                Value = 0,
                OneOrMonth = false,
                CreateDate = DateTime.Now,
                EditDate = null,
                IsAnnual = false
            },
            new Category
            {
                Type = 0,
                MainCategoryId = mainCategoryEight.Id,
                Name = "Książki",
                Description = "Książki",
                UserId = userId,
                Value = 0,
                OneOrMonth = false,
                CreateDate = DateTime.Now,
                EditDate = null,
                IsAnnual = false
            }
        };

        var mainCategoryNine = new MainCategory
        {
            Name = "Inne",
            Description = "Inne",
            UserId = userId,
            CreateDate = DateTime.Now,
            EditDate = null,
            IsAnnual = false
        };

        await _context.MainCategories.AddAsync(mainCategoryNine);
        _context.SaveChanges();

        var categoriesNine = new List<Category>()
        {
            new Category
            {
                Type = 0,
                MainCategoryId = mainCategoryNine.Id,
                Name = "Prezenty",
                Description = "Prezenty",
                UserId = userId,
                Value = 0,
                OneOrMonth = false,
                CreateDate = DateTime.Now,
                EditDate = null,
                IsAnnual = false
            },
            new Category
            {
                Type = 0,
                MainCategoryId = mainCategoryNine.Id,
                Name = "Niezaplanowane",
                Description = "Niezaplanowane",
                UserId = userId,
                Value = 0,
                OneOrMonth = false,
                CreateDate = DateTime.Now,
                EditDate = null,
                IsAnnual = false
            }
        };

        await _context.Categories.AddRangeAsync(categoriesOne);
        await _context.Categories.AddRangeAsync(categoriesTwo);
        await _context.Categories.AddRangeAsync(categoriesThree);
        await _context.Categories.AddRangeAsync(categoriesFour);
        await _context.Categories.AddRangeAsync(categoriesFive);
        await _context.Categories.AddRangeAsync(categoriesSix);
        await _context.Categories.AddRangeAsync(categoriesSeven);
        await _context.Categories.AddRangeAsync(categoriesEight);
        await _context.Categories.AddRangeAsync(categoriesNine);

        var accounts = new List<Account>()
        {
            new Account
            {
                Name = "Revolut",
                Description = "Revolut",
                Value = 0,
                UserId = userId
            },
            new Account
            {
                Name = "Lokata",
                Description = "Lokata",
                Value = 0,
                UserId = userId
            }
        };

        await _context.Accounts.AddRangeAsync(accounts);

        var accountOne = new Account
        {
            Name = "PKO",
            Description = "PKO",
            Value = 0,
            UserId = userId
        };

        await _context.Accounts.AddAsync(accountOne);
        _context.SaveChanges();

        var savings = new List<Saving>()
        {
            new Saving
            {
                Name = "Wakacje",
                Description = "Wakacje",
                AimValue = 5000,
                Value = 0,
                BudgetId = accountOne.Id,
                UserId = userId
            },
            new Saving
            {
                Name = "Aparat",
                Description = "Aparat",
                AimValue = 1000,
                Value = 0,
                BudgetId = accountOne.Id,
                UserId = userId
            },
            new Saving
            {
                Name = "Laptop",
                Description = "Laptop",
                AimValue = 1500,
                Value = 0,
                BudgetId = accountOne.Id,
                UserId = userId
            },
            new Saving
            {
                Name = "Wyjazd w Bieszczady",
                Description = "Wyjazd w Bieszczady",
                AimValue = 1000000,
                Value = 0,
                BudgetId = accountOne.Id,
                UserId = userId
            }
        };

        await _context.Savings.AddRangeAsync(savings);
        await _context.SaveChangesAsync();
    }
}
