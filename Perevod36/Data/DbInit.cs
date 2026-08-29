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

        //ТИПЫ ПЕРЕВОДА
        var translationTypes = InitialDbData.TranslationTypes.Values;
        await dbContext.TranslationTypes.AddRangeAsync(translationTypes);
        await dbContext.SaveChangesAsync();

        //ОФИСЫ
        var offices = InitialDbData.Offices.Values;
        await dbContext.Offices.AddRangeAsync(offices);
        await dbContext.SaveChangesAsync();

        //ЯЗЫКИ
        var languages = InitialDbData.Languages.Values;
        await dbContext.Languages.AddRangeAsync(languages);
        await dbContext.SaveChangesAsync();

        //таблицы пользователей заполняются Table-Per-Type
        //сущность customer автоматически записывается в таблицу customers

        //ЗАКАЗЧИКИ
        var customers = InitialDbData.Customers.Values;
        foreach (var customer in customers)
        {
            var result = await userManager.CreateAsync(customer, customer.Email + "A!123");

            if (!result.Succeeded)
                Console.WriteLine(result.Errors.ToList());
        }

        //МЕНЕДЖЕРЫ

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

        //ПЕРЕВОДЧИКИ
        var translators = InitialDbData.Translators.Values;
        foreach (var translator in translators)
        {
            var result = await userManager.CreateAsync(translator, translator.Email + "A!123");

            if (!result.Succeeded)
                Console.WriteLine(result.Errors.ToList());
        }

        //ПЕРЕВОДЧИКИ И ЯЗЫКИ
        InitialDbData.Translators["Strizh"].Languages.Add(InitialDbData.Languages["Eng"]);
        InitialDbData.Translators["Strizh"].Languages.Add(InitialDbData.Languages["Rus"]);
        InitialDbData.Translators["Strizh"].Languages.Add(InitialDbData.Languages["Tgk"]);
        InitialDbData.Translators["Fed"].Languages.Add(InitialDbData.Languages["Eng"]);
        InitialDbData.Translators["Fed"].Languages.Add(InitialDbData.Languages["Chi"]);
        InitialDbData.Translators["Presnakova"].Languages.Add(InitialDbData.Languages["Bel"]);
        await dbContext.SaveChangesAsync();

        //ЗАКАЗЫ

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

        translationType = InitialDbData.TranslationTypes["Passport"];
        orderCustomer = InitialDbData.Customers["Ivanov"];
        var order_2 = new Order
        {
            NumberOfPages = 1,
            TranslationType = translationType,
            Customer = orderCustomer,
            TranslatorId = new TranslatorId(InitialDbData.Translators["Fed"].Id).Id,
            ManagerId = new ManagerId(InitialDbData.Managers["Petrova"].Id).Id
        };
        order_2.CalculateCost();
        order_2.CreateOrderNumber();
        await dbContext.Orders.AddAsync(order_2);

        translationType = InitialDbData.TranslationTypes["MarriageCertificate"];
        orderCustomer = InitialDbData.Customers["Abdula"];
        var order_3 = new Order
        {
            NumberOfPages = 1,
            TranslationType = translationType,
            Customer = orderCustomer,
            TranslatorId = new TranslatorId(InitialDbData.Translators["Fed"].Id).Id,
            ManagerId = new ManagerId(InitialDbData.Managers["Petrova"].Id).Id
        };
        order_3.CalculateCost();
        order_3.CreateOrderNumber();
        await dbContext.Orders.AddAsync(order_3);

        translationType = InitialDbData.TranslationTypes["CarLicense"];
        orderCustomer = InitialDbData.Customers["Tinky"];
        var order_4 = new Order
        {
            NumberOfPages = 1,
            TranslationType = translationType,
            Customer = orderCustomer,
            TranslatorId = new TranslatorId(InitialDbData.Translators["Strizh"].Id).Id,
            ManagerId = new ManagerId(InitialDbData.Managers["Kuznetsov"].Id).Id
        };
        order_4.CalculateCost();
        order_4.CreateOrderNumber();
        await dbContext.Orders.AddAsync(order_4);

        translationType = InitialDbData.TranslationTypes["Diploma"];
        orderCustomer = InitialDbData.Customers["Ivanov"];
        var order_5 = new Order
        {
            NumberOfPages = 5,
            TranslationType = translationType,
            Customer = orderCustomer,
            TranslatorId = new TranslatorId(InitialDbData.Translators["Presnakova"].Id).Id,
            ManagerId = new ManagerId(InitialDbData.Managers["Kuznetsov"].Id).Id
        };
        order_5.CalculateCost();
        order_5.CreateOrderNumber();
        await dbContext.Orders.AddAsync(order_5);

        await dbContext.SaveChangesAsync();

    }
}
