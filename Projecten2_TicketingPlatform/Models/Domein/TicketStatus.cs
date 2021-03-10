using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Reflection;
using System.Threading.Tasks;

namespace Projecten2_TicketingPlatform
{
    public enum TicketStatus
    {
        Aangemaakt,
        [Display(Name ="In behandeling")]
        InBehandeling,
        Afgehandeld,
        Geannuleerd,
        [Display(Name = "Wachten op informatie klant")]
        WachtenOpInformatieKlant,
        [Display(Name = "Informatie klant ontvangen")]
        InformatieKlantOntvangen,
        [Display(Name = "In development")]
        InDevelopment,
        Standaard,
        [Display(Name = "Alle tickets")]
        Alle
    }
}

public static class EnumExtensions
{
    public static string GetDisplayAttributeFrom(this Enum enumValue, Type enumType)
    {
        MemberInfo info = enumType.GetMember(enumValue.ToString()).First();

        string displayName;
        if (info != null && info.CustomAttributes.Any())
        {
            DisplayAttribute nameAttr = info.GetCustomAttribute<DisplayAttribute>();
            displayName = nameAttr != null ? nameAttr.Name : enumValue.ToString();
        }
        else
        {
            displayName = enumValue.ToString();
        }
        return displayName;
    }
}
