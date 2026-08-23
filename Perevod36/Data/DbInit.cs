using Microsoft.AspNetCore.Identity;
using Perevod36.Features.ApplicationUsers;

namespace Perevod36.Data;

public class DbInit
{
    public static async Task Seed(WebApplication app)
    {
        //скоп для заполнения БД
        using var scope = app.Services.CreateScope();
        var dbContext = scope.ServiceProvider.GetRequiredService<AppDbContext>();
        var userManager = scope.ServiceProvider.GetRequiredService<UserManager<ApplicationUser>>();

        //todo
        //УДАЛЕНИЕ И СОЗДАНИЕ БД
        dbContext.Orders.RemoveRange(dbContext.Orders);
        await dbContext.SaveChangesAsync();

        dbContext.Customers.RemoveRange(dbContext.Customers);
        dbContext.Translators.RemoveRange(dbContext.Translators);
        dbContext.Managers.RemoveRange(dbContext.Managers);
        await dbContext.SaveChangesAsync();

        dbContext.TranslationTypes.RemoveRange(dbContext.TranslationTypes);
        dbContext.Offices.RemoveRange(dbContext.Offices);
        dbContext.Languages.RemoveRange(dbContext.Languages);
        await dbContext.SaveChangesAsync();

        //заполнение БД


    }
}
