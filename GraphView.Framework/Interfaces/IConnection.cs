using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GraphView.Framework.Interfaces
{
    public interface IConnection : ISelectable
    {
        IConnectionPoint StartPoint { get; }
        IConnectionPoint EndPoint { get; }
        IRouter Router { get; }
    }
}
