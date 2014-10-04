using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography.X509Certificates;
using System.Text;
using System.Threading.Tasks;

namespace GraphView.Framework.Interfaces
{
    public interface INode : ISelectable
    {
        string Name { get; set; }
        double X { get; set; }
        double Y { get; set; }
    }
}
