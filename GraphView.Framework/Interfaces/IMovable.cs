using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GraphView.Framework.Interfaces
{
    public interface IMovable
    {
        bool CanMove { get; }
        bool IsMoving { get; set; }
    }
}
