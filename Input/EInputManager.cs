using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;
using System.Windows.Threading;

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
            //EGameEngine.Engine.PreviewKeyDown += Window_PreviewKeyDown;
            //EGameEngine.Engine.PreviewKeyDown += Window_PreviewKeyUp;

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

            //foreach (KeyValuePair<Key, IInputBinding> k in InputBindings)
            //{
            //    if(k.Key.IsDown && !InputBindings[e.Key].KeyStateChange)
            //}

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
            // Update Binding state
            InputBindings[e.Key].UpdatePressed(e.IsDown);

            // On KeyDown and KeyUp
            if (e.IsDown)
            {
                InputBindings[e.Key].CallDown();
            }
            else
            {
                InputBindings[e.Key].CallUp();
            }

            // Call held actions
            //if (time >= PollRate)
            if(e.IsDown && !InputBindings[e.Key].KeyStateChange)
            {
                InputBindings[e.Key].CallHeld();
            }
        }

        private void Window_PreviewKeyDown(object sender, KeyEventArgs e)
        {
            // Check if the key is already down
            if (InputBindings[e.Key].IsPressed)
            {
                // The key is already down, so generate a repeat key press event
                InputBindings[e.Key].CallHeld();
                return;
            }

            // The key is not already down, so start a timer to generate repeat key press events
            InputBindings[e.Key].UpdatePressed(e.IsDown);
            DispatcherTimer timer = new DispatcherTimer();
            timer.Interval = TimeSpan.FromMilliseconds(6); // Set the repeat rate to 250ms
            timer.Tick += (s, args) =>
            {
                // Generate a repeat key press event
                InputBindings[e.Key].CallHeld();
            };
            timer.Start();

            // Handle the initial key press event
            InputBindings[e.Key].CallHeld();
        }

        private void Window_PreviewKeyUp(object sender, KeyEventArgs e)
        {
            // Reset the key down state when the key is released
            InputBindings[e.Key].UpdatePressed(e.IsDown);
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
