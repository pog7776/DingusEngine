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

        // Mouse button
        public MouseButtons MouseBind => _mouseBind;
        private MouseButtons _mouseBind;

        // All actions to invoke on keypress
        public List<Action> OnKeyHeldActions => _onKeyHeld;
        private List<Action> _onKeyHeld;

        public List<Action> OnKeyDownActions => _onKeyDown; //?? (_onKeyDown = new List<Action>());
        private List<Action> _onKeyDown;

        public List<Action> OnKeyUpActions => _onKeyUp;
        private List<Action> _onKeyUp;

        public bool IsPressed => _isPressed;
        private bool _isPressed;
        private bool lastPressedState;
        public bool KeyStateChange => (IsPressed != lastPressedState);


        public EInputBinding(Key key)
        {
            _keyBind = key;
            _onKeyHeld = new List<Action>();
            _onKeyDown = new List<Action>();
            _onKeyUp = new List<Action>();
            _isPressed = false;
        }

        public EInputBinding(MouseButtons mb)
        {
            _mouseBind = mb;
            _onKeyHeld = new List<Action>();
            _onKeyDown = new List<Action>();
            _onKeyUp = new List<Action>();
            _isPressed = false;
        }

        public void CallActions()
        {
            if (IsPressed)
            {
                OnKeyDownActions.ForEach(a => a?.Invoke());
            }

            if (!IsPressed)
            {
                OnKeyUpActions.ForEach(a => a?.Invoke());
            }

            lastPressedState = IsPressed;
        }

        // Should i return a token or something to keep track of the action?

        // Register Events on held, down and up
        public void OnKeyHeld(Action action) => _onKeyHeld.Add(action);

        public void UnregisterOnKeyHeld(Action action) => _onKeyHeld.Remove(action);

        public void OnKeyDown(Action action) => _onKeyDown.Add(action);

        public void UnregisterOnKeyDown(Action action) => _onKeyDown.Remove(action);

        public void OnKeyUp(Action action) => _onKeyUp.Add(action);

        public void UnregisterOnKeyUp(Action action) => _onKeyDown.Remove(action);

        // Updaing the pressed state
        public void UpdatePressed(bool state)
        {
            _isPressed = state;
        }
    }
}
