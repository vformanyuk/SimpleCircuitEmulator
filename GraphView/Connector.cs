using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using GraphView.Framework.Interfaces;

namespace GraphView
{
    public class Connector : IConnectionPoint
    {
        public bool IsConnected { get; set; }

        public bool CanConnect(IConnectionPoint hoveringConnector)
        {
            return true;
        }
    }
}
