namespace Portsea.Utils.Security
{
    public class PasswordStrengthEvaluator
    {
        private readonly int passwordLength;

        private readonly PasswordStrengthRules passwordRules;

        private int digitCount;

        private int uppercaseCharacterCount;

        private int lowercaseCharacterCount;

        private int specialCharacterCount;

        public PasswordStrengthEvaluator(string password, PasswordStrengthRules passwordRules)
        {
            this.passwordRules = passwordRules;
            password = password.Trim();
            this.passwordLength = password.Length;
            foreach (var character in password)
            {
                this.EvaluateCharacter(character);
            }
        }

        public bool StrongEnough()
        {
            return this.CheckMinimumLength() &&
                this.CheckLowercaseCharacters() &&
                this.CheckUppercaseCharacters() &&
                this.CheckDigits() &&
                this.CheckSpecialCharacters();
        }

        private void EvaluateCharacter(char character)
        {
            if (char.IsDigit(character))
            {
                this.digitCount++;
            }
            else if (char.IsUpper(character))
            {
                this.uppercaseCharacterCount++;
            }
            else if (char.IsLower(character))
            {
                this.lowercaseCharacterCount++;
            }
            else
            {
                this.specialCharacterCount++;
            }
        }

        private bool CheckMinimumLength()
        {
            return this.passwordLength >= this.passwordRules.MinimumLength;
        }

        private bool CheckLowercaseCharacters()
        {
            return this.lowercaseCharacterCount >= this.passwordRules.MinimumLowercaseCharacters;
        }

        private bool CheckUppercaseCharacters()
        {
            return this.uppercaseCharacterCount >= this.passwordRules.MinimumUppercaseCharacters;
        }

        private bool CheckDigits()
        {
            return this.digitCount >= this.passwordRules.MinimumDigits;
        }

        private bool CheckSpecialCharacters()
        {
            return this.specialCharacterCount >= this.passwordRules.MinimumSpecialCharacters;
        }
    }
}
