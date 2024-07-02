namespace Ramand_Application.Model
{
    public static class IdentityErrorMessage
    {
        public static string DuplicateEmailMessage => "DuplicateEmail";
        public static string DuplicateUserNameMessage => "DuplicateUserName";
        public static string InvalidEmailMessage => "InvalidEmail";
        public static string InvalidUserNameMessage => "InvalidUserName";
        public static string InvalidTokenMessage => "InvalidToken";
        public static string PasswordMismatchMessage => "PasswordMismatch";
        public static string UserLockoutNotEnabledMessage => "UserLockoutNotEnabled";
        public static string PasswordRequiresUniqueCharsMessage => "PasswordRequiresUniqueChars";
    }
}