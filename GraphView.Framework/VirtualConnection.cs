using GraphView.Framework.Interfaces;
using GraphView.Framework.Routers;

namespace GraphView.Framework
{
    internal class VirtualConnection : IConnection
    {
        internal VirtualConnection(IConnectionPoint source)
        {
            StartPoint = source;
            EndPoint = null;
            Router = new DirectLineRouter();
        }

        public IConnectionPoint StartPoint { get; private set; }
        public IConnectionPoint EndPoint { get; private set; }
        public IRouter Router { get; private set; }
        public bool IsSelected { get; set; }
    }
}
