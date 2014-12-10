using GraphView.Framework.Interfaces;
using GraphView.Infrastructure.FrameworkDefaults;

namespace GraphView
{
    public class ConnectionFactory : IConnectionsFactory
    {
        public IConnection CreateConnection(IConnectionPoint sourcePoint, IConnectionPoint destinationPoint)
        {
            return new Connection(sourcePoint, destinationPoint);
        }
    }
}
