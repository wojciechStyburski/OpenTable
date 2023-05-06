namespace OpenTable.Core.Entities;

public class User
{
    public UserId Id { get; private set; }
    public Email Email { get; private set; }
    public UserName UserName { get; private set; }
    public Password Password { get; private set; }
    public FullName FullName { get; private set; }
    public Role Role { get; set; }
    public DateTime CreatedAt { get; set; }

    public static User Create(Guid id, string email, string userName, string password, string fullName, string role, DateTime createdAt)
    {
        User user = new()
        {
            Id = id, 
            Email = email, 
            UserName = userName, 
            Password = password, 
            FullName = fullName, 
            Role = role,
            CreatedAt = createdAt
        };

        return user;
    }
}