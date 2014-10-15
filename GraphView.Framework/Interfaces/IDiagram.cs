using System.Collections.ObjectModel;

namespace GraphView.Framework.Interfaces
{
    public interface IDiagram
    {
        ObservableCollection<INode> ChildNodes { get; }

        IConnectionsFactory ConnectionsFactory { get; }

        ObservableCollection<IConnection> Connections { get; } 
    }
}
