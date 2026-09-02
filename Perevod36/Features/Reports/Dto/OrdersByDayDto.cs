namespace Perevod36.Features.Reports.Dto;

//вся информация для отображения
public record OrdersByDayDto(
    DateOnly Date,
    decimal Total,
    List<OrderDto> Orders
);