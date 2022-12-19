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
        public void OnKeyHeld(MouseButtons mb, Action action);
        public void UnregisterOnKeyHeld(MouseButtons mb, Action action);
        public void OnKeyDown(MouseButtons mb, Action action);
        public void UnregisterOnKeyDown(MouseButtons mb, Action action);
        public void OnKeyUp(MouseButtons mb, Action action);
        public void UnregisterOnKeyUp(MouseButtons mb, Action action);
    }
}
