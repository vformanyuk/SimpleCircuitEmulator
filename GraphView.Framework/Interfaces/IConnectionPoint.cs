using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography.X509Certificates;
using System.Text;
using System.Threading.Tasks;

namespace GraphView.Framework.Interfaces
{
    public interface IConnectionPoint
    {
        bool IsConnected { get; set; }
        bool CanConnect(IConnectionPoint connectionPoint);
    }
}
