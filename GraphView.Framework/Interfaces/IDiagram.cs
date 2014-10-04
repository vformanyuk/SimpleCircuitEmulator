using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GraphView.Framework.Interfaces
{
    public interface IDiagram
    {
        ObservableCollection<INode> ChildNodes { get; }

        IConnectionsFactory ConnectionsFactory { get; }

        ObservableCollection<IConnection> Connections { get; } 
    }
}
