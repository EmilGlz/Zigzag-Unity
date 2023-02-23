public interface IUpdateable
{
    public int interval { get; }
    void Tick(); //I would call it update, but this way it doesn't collide with the unity message
}