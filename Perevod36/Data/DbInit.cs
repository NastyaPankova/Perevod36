using Microsoft.AspNetCore.Identity;
using Perevod36.Features.ApplicationUsers;
using Perevod36.Features.Common;
using Perevod36.Features.Orders;

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
        var translationTypes = InitialDbData.TranslationTypes.Values;
        await dbContext.TranslationTypes.AddRangeAsync(translationTypes);
        await dbContext.SaveChangesAsync();

        var offices = InitialDbData.Offices.Values;
        await dbContext.Offices.AddRangeAsync(offices);
        await dbContext.SaveChangesAsync();

        var languages = InitialDbData.Languages.Values;
        await dbContext.Languages.AddRangeAsync(languages);
        await dbContext.SaveChangesAsync();

        //таблицы пользователей заполняются Table-Per-Type
        //сущность customer автоматически записывается в таблицу customers
        var customers = InitialDbData.Customers.Values;
        foreach (var customer in customers)
        {
            var result = await userManager.CreateAsync(customer, customer.Email + "A!123");

            if (!result.Succeeded)
                Console.WriteLine(result.Errors.ToList());
        }

        //ссылка на офисы внутри создания менеджера в словаре (new Manager {..., OfficeId = Offices["Office_1"].Id})
        //не работала,т.к. фактически офисы на этапе словаря не были занесены в БД
        InitialDbData.Managers["Petrova"].OfficeId = InitialDbData.Offices["Office_1"].Id;
        InitialDbData.Managers["Kuznetsov"].OfficeId = InitialDbData.Offices["Office_2"].Id;
        var managers = InitialDbData.Managers.Values;
        foreach (var manager in managers)
        {
            var result = await userManager.CreateAsync(manager, manager.Email + "A!123");

            if (!result.Succeeded)
                Console.WriteLine(result.Errors.ToList());
        }

        var translators = InitialDbData.Translators.Values;
        foreach (var translator in translators)
        {
            var result = await userManager.CreateAsync(translator, translator.Email + "A!123");

            if (!result.Succeeded)
                Console.WriteLine(result.Errors.ToList());
        }
        InitialDbData.Translators["Strizh"].Languages.Add(InitialDbData.Languages["Eng"]);
        InitialDbData.Translators["Strizh"].Languages.Add(InitialDbData.Languages["Rus"]);
        InitialDbData.Translators["Strizh"].Languages.Add(InitialDbData.Languages["Tgk"]);
        InitialDbData.Translators["Fed"].Languages.Add(InitialDbData.Languages["Eng"]);
        InitialDbData.Translators["Fed"].Languages.Add(InitialDbData.Languages["Chi"]);
        InitialDbData.Translators["Presnakova"].Languages.Add(InitialDbData.Languages["Bel"]);
        await dbContext.SaveChangesAsync();

        //ссылка на типы должна работать, т.к. всё уже есть в БД
        var translationType = InitialDbData.TranslationTypes["Diploma"];
        var orderCustomer = InitialDbData.Customers["Ivanov"];
        var order_1 = new Order
        {
            NumberOfPages = 2,

            //TranslationTypeId - финальный нужный тип
            //TranslationTypeId = InitialDbData.TranslationTypes["Diploma"].Id,

            //подтягиваем объекты, а не id`шники, т.к. для вычислений внутренний полей order нужны сами объекты
            TranslationType = translationType,
            Customer = orderCustomer,

            //Guid - финальный нужный тип, поэтому customerId.Id
            //CustomerId = new CustomerId(InitialDbData.Customers["Tinky"].Id).Id,
            TranslatorId = new TranslatorId(InitialDbData.Translators["Strizh"].Id).Id,
            ManagerId = new ManagerId(InitialDbData.Managers["Kuznetsov"].Id).Id
        };
        order_1.CalculateCost();
        order_1.CreateOrderNumber();
        await dbContext.Orders.AddAsync(order_1);
        await dbContext.SaveChangesAsync();

    }
}
