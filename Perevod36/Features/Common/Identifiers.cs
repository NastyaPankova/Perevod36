namespace Perevod36.Features.Common;

public readonly record struct CustomerId(Guid Id);
public readonly record struct TranslatorId(Guid Id);
public readonly record struct ManagerId(Guid Id);
public readonly record struct TranslationTypeId(int Id);
public readonly record struct OrderId(int Id);
public readonly record struct OfficeId(int Id); 
public readonly record struct LanguageId(int Id);
