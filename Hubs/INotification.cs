namespace Projet101.Hubs
{
    public interface INotification
    {
        Task ReceiveMessage(object message); // <= The method name is important, it's the name the front is gonna listen to
    }
}
