using System.Reflection;

namespace PetManager.Core.Model.Type.Tools
{
    public static class EnumUtils
    {

        public static Dictionary<E, string> MapToDescription<E>() 
            where E : Enum
        {
            Dictionary<E, string> result = new Dictionary<E, string>();
            foreach (E item in Enum.GetValues(typeof(E)))
            {
                result.Add(item, EnumUtils.GetDescription(item));
            }

            return result;
        }

        // Source:
        // https://www.netizine.com/post/generating-enum-description-using-isourcegenerator
        public static string GetDescription(this Enum GenericEnum)
        {
            System.Type genericEnumType = GenericEnum.GetType();
            MemberInfo[] memberInfo = genericEnumType.GetMember(GenericEnum.ToString());
            if (memberInfo != null && memberInfo.Length > 0)
            {
                var _Attribs = memberInfo[0].GetCustomAttributes(
                    typeof(System.ComponentModel.DescriptionAttribute), false);

                if (_Attribs != null && _Attribs.Count() > 0)
                {
                    return ((System.ComponentModel.DescriptionAttribute)_Attribs.ElementAt(0)).Description;
                }
            }
            return GenericEnum.ToString();
        }
    }
}
