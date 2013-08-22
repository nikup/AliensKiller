using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AliensKiller
{
    public interface IUserInterface
    {
        event EventHandler OnLeftPressed;

        event EventHandler OnRightPressed;

        event EventHandler OnActionPressed;

        event EventHandler OnStartPressed;

        void ProcessInput();
    }
}
