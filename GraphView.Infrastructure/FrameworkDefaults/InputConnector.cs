using System;

namespace GraphView.Infrastructure.FrameworkDefaults
{
    public class InputConnector<T> : Connector, IObserver<T>
    {
        #region Constructors

        /// <summary>
        /// Initializes a new instance of the <see cref="InputConnector{T}"/> class.
        /// </summary>
        /// <param name="hostingElement">The hosting element.</param>
        /// <param name="valueProcessor">The value processor action.</param>
        public InputConnector(CircuitElement hostingElement, Action<T> valueProcessor) 
            : base(hostingElement, ConnectorType.Input)
        {
            m_valueProcessor = valueProcessor;
        }

        #endregion

        #region Public Methods

        public void OnCompleted()
        {
            // Do nothing
        }

        public void OnError(Exception error)
        {
            // LOG here
        }

        public void OnNext(T value)
        {
            m_valueProcessor.Invoke(value);
        }

        #endregion

        #region Private fields

        /// <summary>
        /// The action which will process value from output connector
        /// </summary>
        private readonly Action<T> m_valueProcessor;

        #endregion
    }
}