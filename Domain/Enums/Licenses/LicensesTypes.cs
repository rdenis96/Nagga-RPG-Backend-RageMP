using System;

namespace Domain.Enums.Licenses
{
    [Flags]
    public enum LicensesTypes
    {
        None = 0,
        Driving = 1,
        Weapons = 2,
        Flying = 4,
        Sailing = 8,
        All = Driving | Weapons | Flying | Sailing
    }
}
