using Microsoft.Extensions.DependencyInjection;

using MediatR;

Console.WriteLine("Creating services...");
var services = new ServiceCollection();
services.AddScoped<ChatMessageHandler>();
services.AddSingleton<UserRegistrar>();
services.AddMediatR(cfg =>
{
    cfg.RegisterServicesFromAssemblies(typeof(Program).Assembly);
});

Console.WriteLine("Registering users...");
var serviceProvider = services.BuildServiceProvider();
var mediator = serviceProvider.GetRequiredService<IMediator>();
var userRegistrar = serviceProvider.GetRequiredService<UserRegistrar>();

ChatUser user1 = new ChatUser(mediator, "User 1");
ChatUser user2 = new ChatUser(mediator, "User 2");
ChatUser user3 = new ChatUser(mediator, "User 3");

userRegistrar.RegisterUser(user1);
userRegistrar.RegisterUser(user2);
userRegistrar.RegisterUser(user3);

Console.WriteLine("Sending first message...");
await user1.SendMessage("Hello, everyone!");

Console.WriteLine("Sending second message...");
await user1.SendMessage("Hello again, everyone!");

// Chat message request
public class ChatMessage : IRequest
{
    public string Sender { get; }
    public string Content { get; }

    public ChatMessage(string sender, string content)
    {
        Sender = sender;
        Content = content;
    }
}

// Chat message handler (MediatR creates a new instance
// every time it handles a message)
public class ChatMessageHandler : IRequestHandler<ChatMessage>
{
    private readonly UserRegistrar _userRegistrar;

    public ChatMessageHandler(UserRegistrar userRegistrar)
    {
        Console.WriteLine("Constructing chat message handler");
        _userRegistrar = userRegistrar;
    }

    public Task Handle(
        ChatMessage request,
        CancellationToken cancellationToken)
    {
        Console.WriteLine("Entering message handler...");

        // NOTE: this code isn't thread-safe in that someone in the future
        // could be trying to register a user while we're enumerating
        foreach (var user in _userRegistrar.Users)
        {
            if (!string.Equals(user.Name, request.Sender))
            {
                user.ReceiveMessage(
                    request.Content,
                    request.Sender);
            }
        }

        Console.WriteLine("Exiting message handler.");
        return Task.CompletedTask;
    }
}

// we use this class to preserve the list of users
public sealed class UserRegistrar
{
    private readonly List<ChatUser> _users;

    public UserRegistrar()
    {
        _users = new List<ChatUser>();
    }

    public IReadOnlyList<ChatUser> Users => _users;

    public void RegisterUser(ChatUser user)
    {
        _users.Add(user);
    }
}

// Chat user
public sealed class ChatUser
{
    private readonly IMediator _mediator;

    public ChatUser(
        IMediator mediator, 
        string name)
    {
        _mediator = mediator;
        this.Name = name;
    }

    public string Name { get; }

    public async Task SendMessage(string message)
    {
        ChatMessage msg = new(Name, message);
        await _mediator.Send(msg);
    }

    public void ReceiveMessage(
        string message, 
        string sender)
    {
        Console.WriteLine(
            $"{Name} received message from {sender}: " +
            $"{message}");
    }
}