namespace Perevod36.Client.Shared.Dto.Orders;

//вся информация для отображения
public record OrdersByDayDto(
    DateOnly Date,
    decimal Total,
    List<OrderDto> Orders
);