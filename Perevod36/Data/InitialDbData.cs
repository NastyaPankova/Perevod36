using Perevod36.Features.Offices;

namespace Perevod36.Data;

public class InitialDbData
{
    public static readonly Dictionary<string, Office> Offices = new()
    {
        { "Office_1", new Office {OfficeName = "ТРЦ 'Галерея Чижова'", PhoneNumber = "89461237384", Addres = "ул. Кольцовская, 35"} },
        { "Office_2", new Office {OfficeName = "ТЦ 'Максимир'", PhoneNumber = "89677843309", Addres = "Ленинский пр-т, 174П"} },
    };
}
