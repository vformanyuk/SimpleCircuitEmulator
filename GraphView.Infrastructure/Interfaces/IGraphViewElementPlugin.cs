namespace GraphView.Infrastructure.Interfaces
{
    public interface IGraphViewElementPlugin
    {
        string Name { get; }

        CircuitElement GetCircuitElement();

        IToolbarElement ToolbarElement { get; }
    }
}
