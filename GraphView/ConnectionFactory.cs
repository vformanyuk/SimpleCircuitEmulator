using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using GraphView.Framework.Interfaces;

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
