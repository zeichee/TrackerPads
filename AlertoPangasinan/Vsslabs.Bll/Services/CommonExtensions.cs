using System.Security;

namespace Vsslabs.Bll.Services
{
    public static class CommonExtensions
    {
        public static SecureString ToSecureString(this string str)
        {
            var secureString = new SecureString();
            foreach (var c in str)
            {
                secureString.AppendChar(c);
            }
            return secureString;
        }
        internal static int GetStartPage(this int i, int pageSize)
        {
            return i == 1 ? 0 : ((i * pageSize)) - pageSize;
        }
    }
}
