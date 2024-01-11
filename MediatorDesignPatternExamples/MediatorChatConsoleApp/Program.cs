ChatMediator mediator = new ChatMediator();

ChatUser user1 = new ChatUser(mediator, "User 1");
ChatUser user2 = new ChatUser(mediator, "User 2");
ChatUser user3 = new ChatUser(mediator, "User 3");

mediator.RegisterUser(user1);
mediator.RegisterUser(user2);
mediator.RegisterUser(user3);

user1.SendMessage("Hello, everyone!");

public class ChatMediator
{
    private readonly List<IUser> _users;

    public ChatMediator()
    {
        _users = new List<IUser>();
    }

    public void RegisterUser(IUser user)
    {
        _users.Add(user);
    }

    public void SendMessage(string senderName, string message)
    {
        foreach (var user in _users)
        {
            if (!user.Name.Equals(senderName))
            {
                user.ReceiveMessage(senderName, message);
            }
        }
    }
}

public interface IUser
{
    public string Name { get; }

    void SendMessage(string message);

    void ReceiveMessage(string senderName, string message);
}

public class ChatUser : IUser
{
    private readonly ChatMediator _mediator;

    public ChatUser(
        ChatMediator mediator, 
        string name)
    {
        _mediator = mediator;
        Name = name;
    }

    public string Name { get; }

    public void SendMessage(string message)
    {
        Console.WriteLine($"{Name}: Sending message: {message}");
        _mediator.SendMessage(senderName: Name, message: message);
    }

    public void ReceiveMessage(string senderName, string message)
    {
        Console.WriteLine($"{Name}: Received message '{message}' {senderName}!");
    }
}