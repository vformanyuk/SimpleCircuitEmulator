namespace GraphView.Framework.Interfaces
{
    public interface IConnectionPoint
    {
        bool IsConnected { get; set; }
        bool CanConnect(IConnectionPoint connectionPoint);
    }
}
