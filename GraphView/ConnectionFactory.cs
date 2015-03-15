using GraphView.Framework.Interfaces;
using GraphView.Infrastructure.FrameworkDefaults;
using GraphView.Infrastructure.Interfaces;

namespace GraphView
{
    public class ConnectionFactory : IConnectionsFactory
    {
        public IConnection CreateConnection(IConnectionPoint sourcePoint, IConnectionPoint destinationPoint)
        {
            var observableSource = sourcePoint as IObservableConnector;
            var observableDest = destinationPoint as IObservableConnector;

            if (observableSource != null && observableDest != null)
            {
                return new ConductingConnection(observableSource, observableDest);
            }

            return new Connection(sourcePoint, destinationPoint);
        }
    }
}
