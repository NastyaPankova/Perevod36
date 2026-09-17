namespace Perevod36.Client.Shared.Dto.Orders;

//вся информация для отображения
public record ReportsByPeriodDto(
    DateOnly FromDate,
    DateOnly ToDate,
    decimal Total,
    List<OrderDto> Orders
);