using Microsoft.VisualBasic.Devices;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;

namespace DingusEngine.Input
{
    public interface IInputBinding
    {
        public Key KeyBind { get; }
        public List<Action> Actions { get; }

        public void RegisterBinding(Action action);
        public void UnregisterBinding(Action action);
        public void CallActions();
    }
}
