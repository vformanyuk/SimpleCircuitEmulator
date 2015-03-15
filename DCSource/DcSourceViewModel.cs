using GraphView.Framework.Interfaces;
using GraphView.Infrastructure;
using GraphView.Infrastructure.FrameworkDefaults;

namespace DCSource
{
    public class DcSourceViewModel : CircuitElement
    {
        public IConnectionPoint OutputDcConnectionPoint { get; private set; }

        public DcSourceViewModel()
        {
            View = new DcSourceView { DataContext = this };
            OutputDcConnectionPoint = new Connector();
        }
    }
}
