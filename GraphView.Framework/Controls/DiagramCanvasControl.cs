using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Collections.Specialized;
using System.Diagnostics;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;
using System.Windows.Media;
using GraphView.Framework.Interfaces;
using GraphView.Framework.Routers;

namespace GraphView.Framework.Controls
{
    public sealed class DiagramCanvasControl : Canvas
    {
        #region Static fields

        /// <summary>
        /// The diagram property
        /// </summary>
        public static readonly DependencyProperty DiagramProperty = DependencyProperty.Register(
            "Diagram", typeof (IDiagram), typeof (DiagramCanvasControl), new PropertyMetadata(DiagramChanged));

        #endregion

        /// <summary>
        /// Initializes a new instance of the <see cref="DiagramCanvasControl"/> class.
        /// </summary>
        public DiagramCanvasControl()
        {
            m_nodesSet = new Dictionary<INode, NodeContainerControl>();
            m_connections = new Dictionary<IConnection, ConnectionContainerControl>();
            m_selectedNodes = new HashSet<INode>();

            Background = new SolidColorBrush(Colors.White);
        }

        #region Public properties

        /// <summary>
        /// Gets or sets the diagram.
        /// </summary>
        public IDiagram Diagram
        {
            get { return (IDiagram)GetValue(DiagramProperty); }
            set { SetValue(DiagramProperty, value); }
        }

        #endregion

        #region Private Methods

        protected override void OnPreviewMouseDown(MouseButtonEventArgs e)
        {
            base.OnPreviewMouseDown(e);

            m_currentPosition = e.GetPosition(this);
            m_originPoint = m_currentPosition;
            m_hittestElement = this.HitTest<BaseNodeControl>(m_currentPosition);
            if (m_hittestElement != null) // if node based element is clicked
            {
                var connector = m_hittestElement as ConnectorControl;
                if (connector != null)
                {
                    // if it is connector - create virtual connection
                    var virtualConnector = new VirtualConnectionPoint(connector)
                    {
                        X = m_currentPosition.X,
                        Y = m_currentPosition.Y
                    };
                    Children.Add(virtualConnector);

                    var virtualConnection = new VirtualConnection(connector.ConnectionPoint);
                    var virtualConnectionContainer = new ConnectionContainerControl(connector, virtualConnector,
                        virtualConnection);
                    m_connections.Add(virtualConnection, virtualConnectionContainer);
                    Children.Add(virtualConnectionContainer);

                    m_hittestElement = virtualConnector;
                }

                m_hittestElement.CaptureMouse();
            }
        }

        protected override void OnPreviewMouseMove(MouseEventArgs e)
        {
            var position = e.GetPosition(this);

            if (Mouse.LeftButton == MouseButtonState.Pressed)
            {
                var node = m_hittestElement as BaseNodeControl;
                if (node != null)
                {
                    node.X += position.X - m_currentPosition.X;
                    node.Y += position.Y - m_currentPosition.Y;

                    foreach (var selectedNode in m_selectedNodes.Where(n => n != node.DataContext))
                    {
                        selectedNode.X += position.X - m_currentPosition.X;
                        selectedNode.Y += position.Y - m_currentPosition.Y;
                    }

                    var virtualPoint = m_hittestElement as VirtualConnectionPoint;
                    if (virtualPoint != null)
                    {
                        var hittest = this.AreaHitTest<ConnectorControl>(m_currentPosition,
                            Constants.VirtualPointXOffset - 5);
                        if (hittest != null)
                        {
                            Mouse.SetCursor(hittest.ConnectionPoint.CanConnect(virtualPoint.SourceConnectionPoint)
                                ? Cursors.Hand
                                : Cursors.No);
                        }
                    }
                }

                var rectangle = m_hittestElement as SelectionRect;
                if (rectangle != null) // update rectangle width/height
                {
                    if (rectangle.SelectionStartPoint.X < position.X)
                    {
                        Canvas.SetLeft(rectangle, rectangle.SelectionStartPoint.X);
                        rectangle.Width = position.X - rectangle.SelectionStartPoint.X;
                    }
                    else
                    {
                        Canvas.SetLeft(rectangle, position.X);
                        rectangle.Width = rectangle.SelectionStartPoint.X - position.X;
                    }

                    if (rectangle.SelectionStartPoint.Y < position.Y)
                    {
                        Canvas.SetTop(rectangle, rectangle.SelectionStartPoint.Y);
                        rectangle.Height = position.Y - rectangle.SelectionStartPoint.Y;
                    }
                    else
                    {
                        Canvas.SetTop(rectangle, position.Y);
                        rectangle.Height = rectangle.SelectionStartPoint.Y - position.Y;
                    }
                }

                // if no node selected and drag sitance reached add Selection Rectangle
                if (m_hittestElement == null && (Math.Abs(m_currentPosition.X - m_originPoint.X) >= SystemParameters.MinimumHorizontalDragDistance ||
                     Math.Abs(m_currentPosition.Y - m_originPoint.Y) >= SystemParameters.MinimumVerticalDragDistance))
                {
                    m_hittestElement = new SelectionRect(m_currentPosition.X, m_currentPosition.Y);
                    Children.Add((UIElement)m_hittestElement);
                    m_hittestElement.CaptureMouse();
                }
            }

            m_currentPosition = position;

            base.OnPreviewMouseMove(e);
        }

        protected override void OnPreviewMouseUp(MouseButtonEventArgs e)
        {
            if (m_hittestElement != null)
            {
                m_hittestElement.ReleaseMouseCapture();

                var point = m_hittestElement as VirtualConnectionPoint;
                if (point != null)
                {
                    var hittest = this.AreaHitTest<ConnectorControl>(m_currentPosition,
                        Constants.VirtualPointXOffset - 5);
                    if (hittest != null && hittest.ConnectionPoint.CanConnect(point.SourceConnectionPoint))
                    {
                        // if captured element is VirtualConnector and hittest element is ConnectionPoint - create connection
                        var newConnection = m_diagram.ConnectionsFactory.CreateConnection(point.SourceConnectionPoint,
                            hittest.ConnectionPoint);
                        m_diagram.Connections.Add(newConnection);

                        var connectionContainer = new ConnectionContainerControl(point.SourceConnectorControl, hittest,
                            newConnection);
                        m_connections.Add(newConnection, connectionContainer);
                        Children.Add(connectionContainer);
                    }

                    // remove virtual connection and point
                    Children.Remove(point);
                    var virtualConnection = m_connections.Keys.FirstOrDefault(k => k is VirtualConnection);
                    if (virtualConnection != null)
                    {
                        Children.Remove(m_connections[virtualConnection]);
                        m_connections.Remove(virtualConnection);
                    }
                }

                var selectionRect = m_hittestElement as SelectionRect;
                if (selectionRect != null)
                {
                    var startPoint = m_currentPosition;
                    if (selectionRect.SelectionStartPoint.X < m_currentPosition.X ||
                        selectionRect.SelectionStartPoint.Y < m_currentPosition.Y)
                    {
                        startPoint = selectionRect.SelectionStartPoint;
                    }

                    // find elements under selection rect
                    var elements = this.AreaHitTest<NodeContainerControl>(startPoint,
                        selectionRect.ActualWidth,
                        selectionRect.ActualHeight);

                    foreach (var coveredNode in elements.Select(n => n.Node))
                    {
                        coveredNode.IsSelected = true;
                        if (!m_selectedNodes.Contains(coveredNode))
                        {
                            m_selectedNodes.Add(coveredNode);
                        }
                    }

                    // remove selection rectangle
                    Children.Remove(selectionRect);
                }

                var node = m_hittestElement as NodeContainerControl;
                if (node != null && // single node selection
                    (Math.Abs(m_currentPosition.X - m_originPoint.X) <= SystemParameters.MinimumHorizontalDragDistance ||
                     Math.Abs(m_currentPosition.Y - m_originPoint.Y) <= SystemParameters.MinimumVerticalDragDistance))
                {
                    if (m_selectedNodes.Count >= 1) // after mass selection remove selection from nodes other that clicked one
                    {
                        var removeSelection = m_selectedNodes.Where(n => !n.Equals(node.Node)).ToList();
                        foreach (var selectedNode in removeSelection)
                        {
                            selectedNode.IsSelected = false;
                            m_selectedNodes.Remove(selectedNode);
                        }

                        ToggleSelection(node.Node);
                    }
                    else
                    {
                        ToggleSelection(node.Node);
                    }
                }

                var connectionCtrl = m_hittestElement as ConnectionContainerControl;
                if (connectionCtrl != null) // connections selection
                {
                    var connections = m_connections.Where(c => c.Value.Equals(connectionCtrl)).Select(c => c.Key);
                    foreach (var connection in connections)
                    {
                        connection.IsSelected = !connection.IsSelected;
                    }
                }
            }
            else
            {
                // click on canvas clears selection
                m_selectedNodes.Clear();
            }

            m_hittestElement = null;
            base.OnPreviewMouseUp(e);
        }

        /// <summary>
        /// Adds the existing nodes.
        /// </summary>
        private void AddExistingNodes()
        {
            foreach (var node in m_diagram.ChildNodes)
            {
                var control = new NodeContainerControl(node);
                m_nodesSet.Add(node, control);
                Children.Add(control);
            }
        }

        /// <summary>
        /// Toggles node selection.
        /// </summary>
        /// <param name="node">The node.</param>
        private void ToggleSelection(INode node)
        {
            node.IsSelected = !node.IsSelected;
            if (node.IsSelected)
            {
                m_selectedNodes.Add(node);
            }
            else
            {
                m_selectedNodes.Remove(node);
            }
        }

        /// <summary>
        /// Handles the CollectionChanged event of the ChildNodes control.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The <see cref="NotifyCollectionChangedEventArgs"/> instance containing the event data.</param>
        private void ChildNodes_CollectionChanged(object sender, NotifyCollectionChangedEventArgs e)
        {
            switch (e.Action)
            {
                case NotifyCollectionChangedAction.Add:
                    foreach (var node in e.NewItems.OfType<INode>())
                    {
                        var control = new NodeContainerControl(node);
                        m_nodesSet.Add(node, control);
                        Children.Add(control);
                    }
                    break;
                case NotifyCollectionChangedAction.Remove:
                    var toRemove = m_nodesSet.Where(n => e.OldItems.Contains(n.Key)).ToList();
                    foreach (var pair in toRemove)
                    {
                        m_nodesSet.Remove(pair.Key);
                        Children.Remove(pair.Value);
                    }
                    break;
                case NotifyCollectionChangedAction.Reset:
                    Children.Clear();
                    m_nodesSet.Clear();
                    break;
            }
        }

        private void Connections_CollectionChanged(object sender, NotifyCollectionChangedEventArgs e)
        {
            switch (e.Action)
            {
                    case NotifyCollectionChangedAction.Add:
                    foreach (var connection in e.NewItems.OfType<IConnection>())
                    {
                        connection.StartPoint.IsConnected = true;
                        connection.EndPoint.IsConnected = true;
                    }
                    break;
                    case NotifyCollectionChangedAction.Remove:
                    case NotifyCollectionChangedAction.Reset:
                    foreach (var connection in e.NewItems.OfType<IConnection>())
                    {
                        connection.StartPoint.IsConnected = false;
                        connection.EndPoint.IsConnected = false;
                    }
                    break;
            }
        }

        /// <summary>
        /// Diagrams the changed.
        /// </summary>
        /// <param name="d">The d.</param>
        /// <param name="e">The <see cref="DependencyPropertyChangedEventArgs"/> instance containing the event data.</param>
        private static void DiagramChanged(DependencyObject d, DependencyPropertyChangedEventArgs e)
        {
            if (e.NewValue == null) return;

            var canvas = (DiagramCanvasControl) d;
            var diagram = (IDiagram) e.NewValue;

            if (canvas.m_diagram != null)
            {
                canvas.m_diagram.ChildNodes.CollectionChanged -= canvas.ChildNodes_CollectionChanged;
                canvas.m_diagram.Connections.CollectionChanged -= canvas.Connections_CollectionChanged;
            }
            canvas.m_diagram = diagram;
            diagram.ChildNodes.CollectionChanged += canvas.ChildNodes_CollectionChanged;
            diagram.Connections.CollectionChanged += canvas.Connections_CollectionChanged;
            canvas.AddExistingNodes();
        }

        #endregion

        #region Private fields

        private IDiagram m_diagram;
        private readonly Dictionary<INode, NodeContainerControl> m_nodesSet;
        private readonly Dictionary<IConnection, ConnectionContainerControl> m_connections;
        private readonly HashSet<INode> m_selectedNodes; 
        private IInputElement m_hittestElement;
        private Point m_currentPosition, m_originPoint;

        #endregion
    }
}