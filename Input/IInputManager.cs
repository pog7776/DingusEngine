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
        public void RegisterBinding(Key k, Action action);
        public void UnregisterBinding(Key k, Action action);
    }
}
