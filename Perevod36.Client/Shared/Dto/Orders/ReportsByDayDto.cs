namespace Perevod36.Client.Shared.Dto.Orders;

//вся информация для отображения
public record ReportsByDayDto(
    DateOnly Date,
    decimal Total,
    List<OrderDto> Orders
);