using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Windows.Input;

namespace DingusEngine.Input
{
    public class EInputManager : IInputManager
    {
        public Dictionary<Key, IInputBinding> InputBindings => _inputBindings;
        private Dictionary<Key, IInputBinding> _inputBindings;

        public EInputManager()
        {
            _inputBindings = new Dictionary<Key, IInputBinding>();

            foreach(Key k in Enum.GetValues(typeof(Key)))
            {
                if (!_inputBindings.ContainsKey(k) && k != Key.None)
                {
                    _inputBindings.Add(k, new EInputBinding(k));
                }
            }
        }

        public void Update()
        {
            //foreach (Key k in Enum.GetValues(typeof(Key)))
            foreach(KeyValuePair<Key, IInputBinding> k in InputBindings)
            {
                if(Keyboard.IsKeyDown(k.Key))
                {
                    InputBindings[k.Key].Actions.ForEach(x => x?.Invoke());
                }
            }
        }

        public void RegisterBinding(Key k, Action action)
        {
            InputBindings[k].RegisterBinding(action);
        }

        public void UnregisterBinding(Key k, Action action)
        {
            InputBindings[k].UnregisterBinding(action);
        }
    }
}
