using System;
using System.Collections.Generic;
using GraphView.Framework.Interfaces;
using GraphView.Infrastructure.Interfaces;

namespace GraphView.Infrastructure.FrameworkDefaults
{
    public class OutputConnector<T> : Connector, IObservable<T>, IObservableConnector
    {
        #region Constructors

        /// <summary>
        /// Initializes a new instance of the <see cref="OutputConnector{T}"/> class.
        /// </summary>
        /// <param name="hostingElement">The hosting element.</param>
        public OutputConnector(CircuitElement hostingElement) 
            : base(hostingElement, ConnectorType.Output)
        {
            m_observers = new List<IObserver<T>>();
        }

        #endregion

        #region Public Methods

        /// <summary>
        /// Iterates through observers and tell them that output value changed.
        /// </summary>
        /// <param name="value">The value.</param>
        public void OutputValue(T value)
        {
            foreach (var observer in m_observers)
            {
                observer.OnNext(value);
            }
        }

        /// <summary>
        /// Subscribes the specified observer.
        /// </summary>
        /// <param name="observer">The observer.</param>
        /// <returns></returns>
        public IDisposable Subscribe(IObserver<T> observer)
        {
            return new SubscriptionToken(observer, this);
        }

        /// <summary>
        /// Subscribes the specified point.
        /// </summary>
        /// <param name="point">The point.</param>
        /// <returns></returns>
        public IDisposable Subscribe(IConnectionPoint point)
        {
            var observer = point as IObserver<T>;
            if (observer != null)
            {
                return Subscribe(observer);
            }

            return null;
        }

        #endregion

        #region Private fields

        /// <summary>
        /// The observers
        /// </summary>
        private readonly List<IObserver<T>> m_observers;

        #endregion

        #region Nested type: SubscriptionToken

        /// <summary>
        /// Subscription token. Dispose method performs unsubscription
        /// </summary>
        private class SubscriptionToken : IDisposable
        {
            #region Constructors

            /// <summary>
            /// Initializes a new instance of the <see cref="SubscriptionToken"/> class.
            /// </summary>
            /// <param name="observer">The observer.</param>
            /// <param name="connector">The connector.</param>
            public SubscriptionToken(IObserver<T> observer, OutputConnector<T> connector)
            {
                m_connector = connector;
                m_observer = observer;
                m_connector.m_observers.Add(observer);
            }

            #endregion

            #region Public Methods

            /// <summary>
            /// Performs application-defined tasks associated with freeing, releasing, or resetting unmanaged resources.
            /// Primarily remove observer from subscribers list
            /// </summary>
            public void Dispose()
            {
                m_connector.m_observers.Remove(m_observer);
            }

            #endregion

            #region Private fields

            /// <summary>
            /// The output connector which owns subscriptions
            /// </summary>
            private readonly OutputConnector<T> m_connector;
            /// <summary>
            /// The observer newly subscribed to output connector values changing stream
            /// </summary>
            private readonly IObserver<T> m_observer;

            #endregion
        }

        #endregion
    }
}