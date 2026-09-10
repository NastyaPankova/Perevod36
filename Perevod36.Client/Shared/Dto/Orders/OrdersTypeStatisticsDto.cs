namespace Perevod36.Client.Shared.Dto.Orders;

public record OrdersTypeStatisticsDto(
   string TranslationTypeName,
   int OrderCount
);