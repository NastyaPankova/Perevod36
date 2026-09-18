using Microsoft.EntityFrameworkCore;
using Perevod36.Client.Shared.Dto.Orders;
using Perevod36.Data;
using Perevod36.Features.Common;
using System.Data;


namespace Perevod36.Features.Orders;


//расширение для текущего app, добавляющее маршруты (по типу app.MapOrders вместо системного app.Map в program.cs)
public static class ReportsEndpoints
{
    // app - экземпрял текущего приложения 
    public static IEndpointRouteBuilder MapReportsEndpoints(this IEndpointRouteBuilder app)
    {

        var reportsGroup = app.MapGroup("/api/v1/reports");

        reportsGroup.MapGet("getReportsByPeriod/", GetReportsByPeriod);
        reportsGroup.MapGet("getOrdersTypeStatistics/", GetOrdersTypeStatistics);
        reportsGroup.MapGet("getOrdersByUser/", GetOrdersByUser);
        //если передаем что-то, кроме int, метод не вызывается
        reportsGroup.MapGet("getOrderById/{id:int}", GetOrderById);

        return app;
    }

    //date придет из URL, db из DI, в mapget параметры не передаются
    public static async Task<IResult> GetReportsByPeriod(DateTime fromDate, DateTime toDate, AppDbContext db)
    {
        var from = DateOnly.FromDateTime(fromDate);
        var to = DateOnly.FromDateTime(toDate);

        var orders = await db.Orders
            .AsNoTracking()
            .Where(o => o.Created >= from && o.Created <= to)
            .Select(s => new OrderDto
            (
                s.Id.Id,
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

        var response = new ReportsByPeriodDto
        (
            from,
            to,
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
                s.Id.Id,
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

    public static async Task<IResult> GetOrderById(int id, AppDbContext db)
    {
        var orderId = new OrderId(id);
        var order = await db.Orders
              .AsNoTracking()
              .Include(o => o.TranslationType)
              .Include(o => o.Customer)
              .Include(o => o.Manager)
              .Include(o => o.Translator)
              .FirstOrDefaultAsync(o => o.Id == orderId);
        if (order is not null)
        {
            var response = new OrderDto
                (
                order.Id.Id,
                order.OrderNumber,
                order.Created,
                order.TranslationType.TranslationTypeName,
                order.Customer.FullName,
                order.Manager.FullName,
                order.Translator.FullName,
                order.NumberOfPages,
                order.Cost);
            return Results.Ok(response);
        }
        else
            return Results.NotFound("Заказ не найден");
    }

}