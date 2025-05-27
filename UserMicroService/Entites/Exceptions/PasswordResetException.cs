namespace Entites.Exceptions
{
    public class PasswordResetException : Exception
    {
        public PasswordResetException() : base($"Password reset failed!") { }
    }
}
