using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;

namespace DingusEngine.Input
{
    public interface IInputManager
    {
        public Dictionary<Key, IInputBinding> InputBindings { get; }

        public void Update();

        // Keyboard Binding
        public void OnKeyHeld(Key k, Action action);
        public void UnregisterOnKeyHeld(Key k, Action action);
        public void OnKeyDown(Key k, Action action);
        public void UnregisterOnKeyDown(Key k, Action action);
        public void OnKeyUp(Key k, Action action);
        public void UnregisterOnKeyUp(Key k, Action action);

        // Mouse Binding
        public void OnKeyHeld(MouseButton mb, Action action);
        public void UnregisterOnKeyHeld(MouseButton mb, Action action);
        public void OnKeyDown(MouseButton mb, Action action);
        public void UnregisterOnKeyDown(MouseButton mb, Action action);
        public void OnKeyUp(MouseButton mb, Action action);
        public void UnregisterOnKeyUp(MouseButton mb, Action action);
    }
}
