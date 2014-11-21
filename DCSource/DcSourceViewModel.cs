using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using GraphView.Framework.Interfaces;
using GraphView.Infrastructure;
using GraphView.Infrastructure.FrameworkDefaults;
using GraphView.Infrastructure.Interfaces;

namespace DCSource
{
    public class DcSourceViewModel : CircuitElement
    {
        public IConnectionPoint OutputDcConnectionPoint { get; private set; }

        public DcSourceViewModel()
        {
            View = new DcSourceView { DataContext = this };
            OutputDcConnectionPoint = new Connector(this, ConnectorType.Output);
        }
    }
}
