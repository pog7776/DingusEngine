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
        // TODO I think I should split Mouse and Keyboard into seperate classes
        public Key KeyBind { get; }
        public MouseButton MouseBind { get; }
        public List<Action> OnKeyHeldActions { get; }
        public List<Action> OnKeyDownActions { get; }
        public List<Action> OnKeyUpActions   { get; }
        public bool IsPressed { get; }
        public bool KeyStateChange { get; }

        public void OnKeyHeld(Action action);
        public void UnregisterOnKeyHeld(Action action);
        public void OnKeyDown(Action action);
        public void UnregisterOnKeyDown(Action action);
        public void OnKeyUp(Action action);
        public void UnregisterOnKeyUp(Action action);

        public void CallActions();
        public void UpdatePressed(bool state);
    }
}
