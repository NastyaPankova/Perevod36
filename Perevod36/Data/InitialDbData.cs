using Perevod36.Features.Customers;
using Perevod36.Features.Languages;
using Perevod36.Features.Offices;
using Perevod36.Features.TranslationTypes;

namespace Perevod36.Data;

public class InitialDbData
{
    public static readonly Dictionary<string, TranslationType> TranslationTypes = new()
    {
        {"Passport", new TranslationType {TranslationTypeName = "Паспорт", PricePerPage = 800 } },
        {"MarriageCertificate", new TranslationType {TranslationTypeName = "Свидетельство о браке", PricePerPage = 600 } },
        {"CarLicense", new TranslationType {TranslationTypeName = "Водительское удостоверение", PricePerPage = 300 } },
        {"Diploma", new TranslationType {TranslationTypeName = "Диплом", PricePerPage = 700 } },
        {"Page", new TranslationType {TranslationTypeName = "Постраничный перевод", PricePerPage = 250 } },
    };
    
    public static readonly Dictionary<string, Office> Offices = new()
    {
        { "Office_1", new Office {OfficeName = "ТРЦ 'Галерея Чижова'", PhoneNumber = "89461237384", Addres = "ул. Кольцовская, 35"} },
        { "Office_2", new Office {OfficeName = "ТЦ 'Максимир'", PhoneNumber = "89677843309", Addres = "Ленинский пр-т, 174П"} },
    };

    public static readonly Dictionary<string, Language> Languages = new()
    {
        {"Eng", new Language {LanguageCode = "Eng", LanguageName = "Английский" } },
        {"Rus", new Language {LanguageCode = "Rus", LanguageName = "Русский" } },
        {"Tgk", new Language {LanguageCode = "Tgk", LanguageName = "Таджикский" } },
        {"Chi", new Language {LanguageCode = "Chi", LanguageName = "Китайский" } },
        {"Bel", new Language {LanguageCode = "Bel", LanguageName = "Белорусский" } },
    };

    public static readonly Dictionary<string, Customer> Customers = new()
    {
        { "Ivanov", new Customer {UserName = "ivan@mail.ru", FullName = "Иванов Иван Иванович", Email = "ivan@mail.ru", PhoneNumber = "89549572289"} }, 
    };

    
}
