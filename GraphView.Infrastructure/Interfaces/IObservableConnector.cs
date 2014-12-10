using System;
using GraphView.Framework.Interfaces;

namespace GraphView.Infrastructure.Interfaces
{
    public interface IObservableConnector
    {
        IDisposable Subscribe(IConnectionPoint connector);
    }
}
