using Microsoft.EntityFrameworkCore;
using Perevod36.Client.Shared.Dto.Orders;
using Perevod36.Data;


namespace Perevod36.Features.Orders;


//расширение для текущего app, добавляющее маршруты (по типу app.MapOrders вместо системного app.Map в program.cs)
public static class ReportsEndpoints
{
    // app - экземпрял текущего приложения 
    public static IEndpointRouteBuilder MapReportsEndpoints(this IEndpointRouteBuilder app)
    {

        var reportsGroup = app.MapGroup("/api/v1/reports");

        reportsGroup.MapGet("/getReportsByDay/{date:datetime}", GetReportsByDay);
        reportsGroup.MapGet("/getOrdersTypeStatistics/", GetOrdersTypeStatistics);
        reportsGroup.MapGet("/getOrdersByUser/", GetOrdersByUser);

        return app;
    }

    //date придет из URL, db из DI, в mapget параметры не передаются
    public static async Task<IResult> GetReportsByDay(DateTime date, AppDbContext db)
    {

        var orders = await db.Orders
            .AsNoTracking()
            .Where(o => o.Created == DateOnly.FromDateTime(date))
            .Select(s => new OrderDto
            (
                s.OrderNumber,
                s.Created,
                s.TranslationType.TranslationTypeName,
                s.Customer.FullName,
                s.Manager.FullName,
                s.Translator.FullName,
                s.NumberOfPages,
                s.Cost))
            .ToListAsync();

        decimal total = orders.Sum(o => o.Cost);

        var response = new ReportsByDayDto
        (
            DateOnly.FromDateTime(date),
            total,
            orders
        );

        return Results.Ok(response);
    }

    public static async Task<IResult> GetOrdersTypeStatistics(AppDbContext db)
    {

        var orders = (await db.TranslationTypes
        .AsNoTracking()
        .GroupJoin(
            db.Orders,
            translationType => translationType.Id,
            order => order.TranslationTypeId,
            (translationType, ordersGroup) => new OrdersTypeStatisticsDto
            (
                translationType.TranslationTypeName,
                ordersGroup.Count()       
            ))
         .ToListAsync()).
         //todo
         //замечание: сортировка после полной выгрузки из БД, может тормозить при большом кол-ве строк в Orders в БД
         OrderByDescending(dto => dto.OrderCount);

        return Results.Ok(orders);
    }

    public static async Task<IResult> GetOrdersByUser(string email, AppDbContext db)
    {
        //не берем полноценного пользовател, проверяем существование 
        var isUser = await db.Users.AnyAsync(u => u.Email == email);
        if (!isUser) 
            return Results.NotFound("Пользователь не найден");

        var orders = await db.Orders
            .AsNoTracking()
             .Where(o => o.Customer.Email == email)
             .Select(s => new OrderDto
              (
                s.OrderNumber,
                s.Created,
                s.TranslationType.TranslationTypeName,
                s.Customer.FullName,
                s.Manager.FullName,
                s.Translator.FullName,
                s.NumberOfPages,
                s.Cost))
            .ToListAsync();

        //todo
        //добавить на razor пагинацию

        return Results.Ok(orders);

    }
}