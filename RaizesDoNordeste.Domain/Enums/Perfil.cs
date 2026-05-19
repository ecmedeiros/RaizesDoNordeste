using System.ComponentModel;
using System.Reflection;

namespace RaizesDoNordeste.Domain.Enums
{
    public enum Perfil
    {
        [Description("Admin")]
        Admin = 1,
        [Description("Gerente")]
        Gerente = 2,
        [Description("Cliente")]
        Cliente = 3
    }

    public static class EnumExtensions
    {
        public static string GetDescription(this Enum value)
        {
            return value.GetType()
                        .GetField(value.ToString())
                        ?.GetCustomAttribute<DescriptionAttribute>()
                        ?.Description ?? value.ToString();
        }
    }
}
