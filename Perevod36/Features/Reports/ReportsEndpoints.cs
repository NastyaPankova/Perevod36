using Microsoft.EntityFrameworkCore;
using Perevod36.Data;

namespace Perevod36.Features.Orders;

public static class ReportsEndpoints
{
    
    public static IEndpointRouteBuilder MapOrderEndpoints(this IEndpointRouteBuilder app)
    {
        
        var group = app.MapGroup("/api/v1/orders");

        
        group.MapGet("/by-day/{date:datetime}", async (DateTime date, AppDbContext db) =>
        {
           
            var ordersFromDb = await db.Orders
                .AsNoTracking()
                .Where(o => o.Created == DateOnly.FromDateTime(date))
                .Include(o => o.TranslationType)
                .ToListAsync();

            decimal totalSum = ordersFromDb.Sum(o => o.Cost);

            var orderDtos = ordersFromDb.Select(o => new {
                o.OrderNumber,
                o.TranslationType.TranslationTypeName,
                o.NumberOfPages,
                o.Cost
            }).ToList();

            return Results.Ok(new
            {
                Date = date.ToString("dd.MM.yyyy"),
                Total = totalSum,
                Orders = orderDtos
            });
        });

        return app;
    }
}