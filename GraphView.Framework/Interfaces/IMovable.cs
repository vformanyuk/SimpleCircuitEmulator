namespace GraphView.Framework.Interfaces
{
    public interface IMovable
    {
        bool CanMove { get; }
        bool IsMoving { get; set; }
    }
}
