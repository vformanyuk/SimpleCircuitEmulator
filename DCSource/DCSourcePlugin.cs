using System;
using System.ComponentModel.Composition;
using GraphView.Infrastructure;
using GraphView.Infrastructure.Interfaces;

namespace DCSource
{
    [Export(typeof(IGraphViewElementPlugin))]
    public class DcSourcePlugin : IGraphViewElementPlugin
    {
        public DcSourcePlugin()
        {
            Name = "Dc Source";
        }

        public string Name { get; private set; }
        public ICircuitElement GetCircuitElement()
        {
            throw new NotImplementedException();
        }

        public ToolbarElement ToolbarElement { get; private set; }
    }
}
