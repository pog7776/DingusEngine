using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;

namespace DingusEngine.Input
{
    public class EInputManager : IInputManager
    {
        public  Dictionary<Key, IInputBinding> InputBindings => _inputBindings;
        private Dictionary<Key, IInputBinding> _inputBindings;

        public  Dictionary<MouseButton, IInputBinding> MouseBindings => _mouseBindings;
        private Dictionary<MouseButton, IInputBinding> _mouseBindings;

        private float time = 0;
        private float _pollRate = 0.1f;
        public float PollRate
        {
            get => _pollRate;
            set => _pollRate = value;
        }

        public EInputManager()
        {
            EGameEngine.Engine.KeyDown += HandleKeyboard;
            EGameEngine.Engine.KeyUp += HandleKeyboard;
            //EGameEngine.Engine.MouseDown += Update;


            _inputBindings = new Dictionary<Key, IInputBinding>();
            _mouseBindings = new Dictionary<MouseButton, IInputBinding>();

            foreach (MouseButton mb in Enum.GetValues(typeof(MouseButton)))
            {
                if (!MouseBindings.ContainsKey(mb))
                {
                    _mouseBindings.Add(mb, new EInputBinding(mb));
                }
            }

            foreach (Key k in Enum.GetValues(typeof(Key)))
            {
                if (!InputBindings.ContainsKey(k) && k != Key.None)
                {
                    _inputBindings.Add(k, new EInputBinding(k));
                }
            }
        }

        public void Update()
        {
            //HandleMouse(e);
            //HandleKeyboard(e);

            time += EGameEngine.Engine.DeltaTime;
            if(time > PollRate) { time = 0; }
        }

        private void HandleMouse()
        {
            // On KeyDown and KeyUp
            foreach (KeyValuePair<MouseButton, IInputBinding> mb in MouseBindings)
            {
                if ((MouseButtonState)mb.Key == MouseButtonState.Pressed)
                {
                    MouseBindings[mb.Key].UpdatePressed(true);
                }
                else
                {
                    MouseBindings[mb.Key].UpdatePressed(false);
                }

                if (MouseBindings[mb.Key].KeyStateChange)
                {
                    MouseBindings[mb.Key].CallActions();
                }
            }

            // Call held actions
            if (time >= PollRate)
            {
                //foreach (Key k in Enum.GetValues(typeof(Key)))
                foreach (KeyValuePair<MouseButton, IInputBinding> mb in MouseBindings)
                {
                    if ((MouseButtonState)mb.Key == MouseButtonState.Pressed)
                    {
                        MouseBindings[mb.Key].OnKeyHeldActions.ForEach(x => x?.Invoke());
                    }
                }
            }
        }

        private void HandleKeyboard(object sender, KeyEventArgs e)
        {
            // On KeyDown and KeyUp
            //foreach (KeyValuePair<Key, IInputBinding> k in InputBindings)
            //{
                //IInputBinding k = InputBindings[e.Key];
                if (e.IsDown)
                {
                    InputBindings[e.Key].UpdatePressed(true);
                }
                else
                {
                    InputBindings[e.Key].UpdatePressed(false);
                }

                if (InputBindings[e.Key].KeyStateChange)
                {
                    InputBindings[e.Key].CallActions();
                }
            //}

            // Call held actions
            //if (time >= PollRate)
            if(e.IsRepeat)
            {
                InputBindings[e.Key].OnKeyHeldActions.ForEach(x => x?.Invoke());
            }
        }

        // Keyboard Binding
        public void OnKeyHeld(Key k, Action action)             => InputBindings[k].OnKeyHeld(action);
        public void UnregisterOnKeyHeld(Key k, Action action)   => InputBindings[k].UnregisterOnKeyHeld(action);
        public void OnKeyDown(Key k, Action action)             => InputBindings[k].OnKeyDown(action);
        public void UnregisterOnKeyDown(Key k, Action action)   => InputBindings[k].UnregisterOnKeyDown(action);
        public void OnKeyUp(Key k, Action action)               => InputBindings[k].OnKeyUp(action);
        public void UnregisterOnKeyUp(Key k, Action action)     => InputBindings[k].UnregisterOnKeyUp(action);

        // Mouse Binding
        public void OnKeyHeld(MouseButton mb, Action action)           => MouseBindings[mb].OnKeyHeld(action);
        public void UnregisterOnKeyHeld(MouseButton mb, Action action) => MouseBindings[mb].UnregisterOnKeyHeld(action);
        public void OnKeyDown(MouseButton mb, Action action)           => MouseBindings[mb].OnKeyDown(action);
        public void UnregisterOnKeyDown(MouseButton mb, Action action) => MouseBindings[mb].UnregisterOnKeyDown(action);
        public void OnKeyUp(MouseButton mb, Action action)             => MouseBindings[mb].OnKeyUp(action);
        public void UnregisterOnKeyUp(MouseButton mb, Action action)   => MouseBindings[mb].UnregisterOnKeyUp(action);
    }
}
