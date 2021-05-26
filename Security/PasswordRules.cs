namespace Portsea.Utils.Security
{
    public class PasswordRules
    {
        public int MinimumLength { get; set; }

        public int MinimumLowercaseCharacters { get; set; }

        public int MinimumUppercaseCharacters { get; set; }

        public int MinimumDigits { get; set; }

        public int MinimumSpecialCharacters { get; set; }
    }
}
