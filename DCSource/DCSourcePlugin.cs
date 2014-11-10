using System;
using System.ComponentModel.Composition;
using GraphView.Infrastructure;
using GraphView.Infrastructure.Interfaces;

namespace DCSource
{
    [Export(typeof(IGraphViewElementPlugin))]
    public class DcSourcePlugin : IGraphViewElementPlugin
    {
        private static IToolbarElement s_toolbarElement;

        public DcSourcePlugin()
        {
            Name = "Dc Source";
            if (s_toolbarElement == null)
            {
                s_toolbarElement = new DcSourceToolbarElement();
            }
        }

        public string Name { get; private set; }

        public CircuitElement GetCircuitElement()
        {
            return new DcSourceViewModel();
        }

        public IToolbarElement ToolbarElement
        {
            get { return s_toolbarElement; }
        }
    }
}
