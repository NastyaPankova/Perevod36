namespace Perevod36.Client.Shared.Dto.Orders;

//один заказ
public record OrderDto(
    string OrderNumber,
    DateOnly Created,
    string TranslationTypeName,
    string Customer,
    string Manager,
    string Translator,
    int NumberOfPages,
    decimal Cost
);