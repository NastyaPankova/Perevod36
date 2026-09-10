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

        return app;
    }

    //date придет из URL, db из DI, в mapget параметры не передаются
    public static async Task<IResult> GetReportsByDay(DateTime date, AppDbContext db)
    {

        var orders = await db.Orders
            .AsNoTracking()
            .Where(o => o.Created == DateOnly.FromDateTime(date))
            .Include(o => o.TranslationType)
            .Include(o => o.Customer)
            .Include(o => o.Manager)
            .Include(o => o.Translator)
            .Select(s => new OrderDto
            (
                s.OrderNumber,
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

        var orders = await db.TranslationTypes
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
         .ToListAsync();

        return Results.Ok(orders);
    }
}