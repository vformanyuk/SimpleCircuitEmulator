using System.ComponentModel;
using System.Runtime.CompilerServices;
using GraphView.Framework.Interfaces;

namespace GraphView.Framework
{
    public class DiagramNode : INode, INotifyPropertyChanged
    {
        #region Private Methods

        protected virtual void OnPropertyChanged([CallerMemberName] string propertyName = null)
        {
            PropertyChangedEventHandler handler = PropertyChanged;
            if (handler != null) handler(this, new PropertyChangedEventArgs(propertyName));
        }

        #endregion

        #region INode Members

        public string Name
        {
            get { return m_name; }
            set
            {
                if (m_name == value) return;

                m_name = value;
                OnPropertyChanged();
            }
        }

        public virtual double X
        {
            get { return m_x; }
            set
            {
                if (m_x == value) return;

                m_x = value;
                OnPropertyChanged();
            }
        }

        public virtual double Y
        {
            get { return m_y; }
            set
            {
                if (m_y == value) return;

                m_y = value;
                OnPropertyChanged();
            }
        }

        #endregion

        #region INotifyPropertyChanged Members

        public event PropertyChangedEventHandler PropertyChanged;

        #endregion

        #region Private fields

        private string m_name;
        private double m_x, m_y;

        #endregion

        public virtual bool IsSelected { get; set; }
    }
}