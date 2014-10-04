using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using GraphView.Framework;
using GraphView.Framework.Interfaces;

namespace GraphView
{
    public class CustomNode : DiagramNode, IMovable
    {
        public CustomNode()
        {
            InputConnector = new Connector();
        }

        public Connector InputConnector { get; set; }

        public override bool IsSelected
        {
            get { return m_isSelected; }
            set
            {
                if (m_isSelected == value)
                {
                    return;
                }

                m_isSelected = value;
                Debug.WriteLine("Node Selected = " + m_isSelected);
                OnPropertyChanged();
            }
        }
        public bool CanMove { get { return true; } }
        public bool IsMoving { get; set; }

        private bool m_isSelected;
    }
}
