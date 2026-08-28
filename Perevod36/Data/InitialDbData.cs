using Perevod36.Features.Customers;
using Perevod36.Features.Languages;
using Perevod36.Features.Managers;
using Perevod36.Features.Offices;
using Perevod36.Features.TranslationTypes;
using Perevod36.Features.Translators;

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
        { "Ivanov", new Customer {UserName = "ivan@mail.ru", Email = "ivan@mail.ru", FullName = "Иванов Иван Иванович", PhoneNumber = "89549572289"} },
        { "Abdula", new Customer {UserName = "bul@gmail.com", Email = "bul@gmail.com", FullName = "Abdula Bula",  PhoneNumber = "89437683455"} },
        { "Tinky", new Customer {UserName = "tele@gmail.com", Email = "tele@gmail.com",FullName = "Tinky Winky",  PhoneNumber = "89462389045"} },
    };

    public static readonly Dictionary<string, Manager> Managers = new()
    {
        {"Petrova", new Manager {UserName = "petrova_m@mail.ru", Email = "petrova_m@mail.ru", FullName = "Петрова Марина", PhoneNumber = "89347105439"} },
        {"Kuznetsov", new Manager {UserName = "kuz@mail.ru", Email = "kuz@mail.ru", FullName = "Кузнезов Александр", PhoneNumber = "89674356730" } },
    };

    public static readonly Dictionary<string, Translator> Translators = new()
    {
        {"Strizh", new Translator {UserName = "strizh@mail.ru", Email = "strizh@mail.ru", FullName = "Стрижак Федр", PhoneNumber = "89476564438", IsFreelance = true} },
        {"Fed", new Translator {UserName = "fed@gmail.com", Email = "fed@gmail.com", FullName = "Федченко Юлия", PhoneNumber = "89432389112", IsFreelance = false} },
        {"Presnakova", new Translator {UserName = "presnakova@mail.ru", Email = "presnakova@mail.ru", FullName = "Преснякова Анжелика", PhoneNumber = "89123428451", IsFreelance = false} },
    };

}
