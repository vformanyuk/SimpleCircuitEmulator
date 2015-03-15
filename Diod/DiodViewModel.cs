using GraphView.Infrastructure;

namespace Diod
{
    public class DiodViewModel : CircuitElement
    {
        public DiodViewModel()
        {
            View = new DiodView();
        }
    }
}
