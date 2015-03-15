using System;
using GraphView.Framework.Interfaces;

namespace GraphView.Infrastructure.Interfaces
{
    public interface IObservableConnector : IConnectionPoint
    {
        void Input(double voltage);

        event EventHandler<double> OnVoltageChanged;
    }
}
