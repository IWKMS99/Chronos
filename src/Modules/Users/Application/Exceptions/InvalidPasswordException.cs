namespace Chronos.Users.Application.Exceptions;
public class InvalidPasswordException : Exception {
    public InvalidPasswordException() : base("Invalid password provided.") { }
}