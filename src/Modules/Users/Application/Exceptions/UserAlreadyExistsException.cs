namespace Chronos.Modules.Users.Application.Exceptions {
    
    public class UserAlreadyExistsException : Exception {
        public string Email { get; }

        public UserAlreadyExistsException(string email) 
            : base($"User with email '{email}' already exists.") {
            Email = email;
        }

        public UserAlreadyExistsException(string message, Exception innerException) 
            : base(message, innerException) {
        }
    }
}