namespace backend
{
    public class User
    {
        public int Id { get; set; }
        public string Username { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string Password { get; set; }

        public User() { }

        public User(int id, string username, string firstName, string lastName, string password)
        {
            Id = id;
            Username = username;
            FirstName = firstName;
            LastName = lastName;
            Password = password;
        }

    }
}