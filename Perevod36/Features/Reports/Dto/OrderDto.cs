namespace Perevod36.Features.Reports.Dto;

//один заказ
public record OrderDto(
    string OrderNumber,
    string TranslationTypeName,
    string Customer,
    string Manager,
    string Translator,
    int NumberOfPages,
    decimal Cost
);