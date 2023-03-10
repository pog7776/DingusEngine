using System;
using System.Drawing;
using System.Windows.Forms;
using System.Numerics;
using DingusEngine.Actors;
using DingusEngine.GameActor;
using DingusEngine.StandardComponents;
using DingusEngine.Rendering;
using Timer = System.Windows.Forms.Timer;
using Microsoft.VisualBasic.Devices;
using System.Runtime.InteropServices;
using WriteLine = System.Diagnostics.Debug;
using System.Security.Policy;
using DingusEngine.Actors;
using DingusEngine.GameActor;
using DingusEngine.Input;
using System.Threading;
using System.Windows.Threading;
using System.Windows.Input;
using GameEngine.Actors;

namespace DingusEngine
{
    public class EGameEngine : Form
    {
        public static EGameEngine Engine = null;

        // Timer to control the game loop
        //private Timer gameTimer;
        private DispatcherTimer gameTimer;

        // The current game state
        private GameState gameState;

        // The current frame rate
        private int frameRate;

        // The time elapsed since the last frame
        public float DeltaTime => _deltaTime;
        private float _deltaTime;

        public EActorManager ActorManager;
        public EInputManager InputManager;

        Graphics g;
        private Image _bufferImage;
        public ERenderHandler RenderHandler;

        public EGameEngine()
        {
            Engine = this;
            ActorManager = new EActorManager();
            InputManager = new EInputManager();
            // Set the size and title of the window
            this.ClientSize = new Size(800, 600);
            this.Text = "Dingus Engine";
            this.Resize += new EventHandler(OnResize);

            // Init Graphics
            g = this.CreateGraphics();
            RenderHandler = new ERenderHandler();

            // Create a buffer image with the same size as the form
            _bufferImage = new Bitmap(this.ClientSize.Width, this.ClientSize.Height);

            // Enable double buffering for the form
            this.SetStyle(ControlStyles.DoubleBuffer | ControlStyles.UserPaint | ControlStyles.AllPaintingInWmPaint, true);
            this.UpdateStyles();

            // Initialize the game state and frame rate
            gameState = GameState.Running;
            frameRate = 60;

            // Initialize the timer
            //gameTimer = new Timer();
            gameTimer = new DispatcherTimer();
            //gameTimer.Interval = 1000 / frameRate;
            gameTimer.Interval = new TimeSpan(1000 / frameRate);
            gameTimer.Tick += new EventHandler(OnTick);
            gameTimer.Start();

            Start();
        }

        // Before game starts
        private void Start()
        {
            #region Test Actors

            TestActor ta   = ActorManager.CreateActor<TestActor>();

            MovingActor ma = ActorManager.CreateActor<MovingActor>();

            TextActor txa  = ActorManager.CreateActor<TextActor>();

            Player player  = ActorManager.CreateActor<Player>();

            ASprite s = ta.GetComponent<ASprite>();
            ATransform tf = ta.GetComponent<ATransform>();

            //InputManager.OnKeyDown(Key.Space, delegate
            InputManager.OnKeyDown(MouseButtons.Left, delegate
            {
                //s.Visible = !s.Visible;
                s.SetScale(1f);
                //tf.Position = new Vector3(tf.Position.X, tf.Position.Y, -10);
            });

            //InputManager.OnKeyUp(Key.Space, delegate
            InputManager.OnKeyUp(MouseButtons.Left, delegate
            {
                s.SetScale(0.3f);
                //tf.Position = new Vector3(tf.Position.X, tf.Position.Y, 20);
            });

            #endregion
        }

        // The game loop
        private void OnTick(object sender, EventArgs e)
        {
            // Update the elapsed time
            _deltaTime = (float) gameTimer.Interval.TotalMilliseconds;

            // Update the game state
            Update(DeltaTime);

            // Render the game
            Render();

            // Check for input events
            ProcessInput();
        }

        // Update the game state
        private void Update(float elapsedTime)
        {
            // Update the game logic here

            foreach(Actor actor in ActorManager.Actors)
            {
                actor.Update();
                foreach(IComponent component in actor.Components)
                {
                    component.Update();
                }
            }
        }

        protected override void OnPaint(PaintEventArgs e)
        {
            // Paint the buffer image when reloaded
            base.OnPaint(e);
            e.Graphics.DrawImage(_bufferImage, 0, 0, _bufferImage.Width, _bufferImage.Height);
        }

        private void OnResize(object sender, EventArgs e)
        {
            // Form has been resized
            // TODO Crashes on minimize
            _bufferImage = new Bitmap(this.ClientSize.Width, this.ClientSize.Height);
        }

        // Render the game
        private void Render()
        {
            // Render the game graphics here

            // Create a Graphics object from the form's handle            
            if(g == null)
            {
                g = this.CreateGraphics();
            }

            // Clear the screen.
            // Using the buffer?? Not sure why, but it works.
            using (Graphics g = Graphics.FromImage(_bufferImage))
            {
                g.Clear(Color.White);
            }

            // Draw on the buffer image
            using (Graphics g = Graphics.FromImage(_bufferImage))
            {
                // Render each assigned task
                foreach(IRenderTask task in RenderHandler.Tasks)
                {
                    task.Action(g);
                }
            }

            // Invalidate the form to trigger a repaint
            this.Invalidate();
        }

        // Process input events
        private void ProcessInput()
        {
            // Check for input events here
            Dispatcher.CurrentDispatcher.Invoke((Action)delegate
            {
                InputManager.Update();
            });

            //DispatcherHelper.CheckBeginInvokeOnUI(Action action)
        }

        // The main entry point for the application
        [STAThread]
        static void Main()
        {
            Application.Run(new EGameEngine());
        }
    }

    // TODO handle this? IDK put it in it's own file or something?
    public enum GameState
    {
        Running,
        Paused,
        GameOver,
        // Add additional game states here
    }
}