using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using GraphView.Framework.Interfaces;
using GraphView.Framework.Routers;

namespace GraphView
{
    public class Connection : IConnection
    {

        public Connection(IConnectionPoint source, IConnectionPoint destination)
        {
            StartPoint = source;
            EndPoint = destination;
            Router = new DirectLineRouter();
        }

        public IConnectionPoint StartPoint { get; private set; }
        public IConnectionPoint EndPoint { get; private set; }
        public IRouter Router { get; private set; }
        public bool IsSelected { get; set; }
    }
}
