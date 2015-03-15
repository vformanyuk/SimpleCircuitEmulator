using System.ComponentModel.Composition;
using GraphView.Infrastructure;
using GraphView.Infrastructure.Interfaces;

namespace Diod
{
    [Export(typeof(IGraphViewElementPlugin))]
    public class DiodPlugin : IGraphViewElementPlugin
    {
        private static readonly IToolbarElement s_ToolbarElement = new DiodToolbarElement();

        public string Name
        {
            get { return "Diod"; }
        }

        public CircuitElement GetCircuitElement()
        {
            return new DiodViewModel();
        }

        public IToolbarElement ToolbarElement
        {
            get { return s_ToolbarElement; }
        }
    }
}
