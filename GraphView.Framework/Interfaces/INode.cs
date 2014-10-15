namespace GraphView.Framework.Interfaces
{
    public interface INode : ISelectable
    {
        string Name { get; set; }
        double X { get; set; }
        double Y { get; set; }
    }
}
