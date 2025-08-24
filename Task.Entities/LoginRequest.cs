namespace TaskManagementSystem.Task.Entities
{
    /// <summary>
    /// Represents the request body for registering a new user.
    /// </summary>
    public class UserRequest
    {
        /// <summary>
        /// The username chosen by the user.
        /// </summary>
        public string Username { get; set; }

        /// <summary>
        /// The email address of the user. This field is required.
        /// </summary>
        public required string Email { get; set; }

        /// <summary>
        /// The password chosen by the user. This field is required.
        /// </summary>
        public required string Password { get; set; }
       
    }

    public class UserEF
    {
        public int Id { get; set; }  // Primary Key
        public string Username { get; set; }
        public string Email { get; set; }
        public string Password { get; set; }  // store hashed password
    }

}
