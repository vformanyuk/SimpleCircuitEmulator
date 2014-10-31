namespace GraphView.Infrastructure.Interfaces
{
    public interface IGraphViewElementPlugin
    {
        string Name { get; }

        ICircuitElement GetCircuitElement();

        ToolbarElement ToolbarElement { get; }
    }
}
