using Microsoft.VisualBasic.Devices;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Windows.Input;

namespace DingusEngine.Input
{
    public class EInputBinding : IInputBinding
    {
        // Input key
        public Key KeyBind => _keyBind;
        private Key _keyBind;

        // All actions to invoke on keypress
        public List<Action> Actions => _actions;
        private List<Action> _actions;

        public EInputBinding(Key key)
        {
            _keyBind = key;
            _actions = new List<Action>();
        }

        public void CallActions()
        {
            foreach(Action action in _actions)
            {
                action?.Invoke();
            }
        }

        public void RegisterBinding(Action action)
        {
            _actions.Add(action);
        }

        public void UnregisterBinding(Action action)
        {
            _actions.Remove(action);
            // Should i return a token or something to keep track of the action?
        }
    }
}
