namespace GraphView.Framework.Interfaces
{
    public interface IConnection : ISelectable
    {
        IConnectionPoint StartPoint { get; }
        IConnectionPoint EndPoint { get; }
        IRouter Router { get; }
    }
}
