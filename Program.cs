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
using GameEngine.Actors;

namespace DingusEngine
{
    public class EGameEngine : Form
    {
        public static EGameEngine Engine = null;

        // Timer to control the game loop
        private Timer gameTimer;

        // The current game state
        private GameState gameState;

        // The current frame rate
        private int frameRate;

        // The time elapsed since the last frame
        public float DeltaTime { get { return _deltaTime; } }
        private float _deltaTime;

        private List<Actor> actors = new List<Actor>();

        Graphics g;
        private Image _bufferImage;
        public ERenderHandler RenderHandler;

        public EGameEngine()
        {
            Engine = this;
            // Set the size and title of the window
            this.ClientSize = new Size(800, 600);
            this.Text = "Dingus Engine";

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
            gameTimer = new Timer();
            gameTimer.Interval = 1000 / frameRate;
            gameTimer.Tick += new EventHandler(OnTick);
            gameTimer.Start();

            Start();
        }

        // Before game starts
        private void Start()
        {
            #region Test Actors

            TestActor ta = new TestActor();
            actors.Add(ta);

            MovingActor ma = new MovingActor();
            actors.Add(ma);

            #endregion
        }

        // The game loop
        private void OnTick(object sender, EventArgs e)
        {
            // Update the elapsed time
            _deltaTime = (float)gameTimer.Interval / 1000;

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

            foreach(Actor actor in actors)
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
                foreach(ERenderTask task in RenderHandler.Tasks)
                {
                    ASprite s = task.Sprite;
                    if (s != null || s.Image != null)
                    {
                        g.DrawImage(s.Image,
                            task.Transform.Position.X,task.Transform.Position.Y,
                            s.Scale.X * s.Image.Width, s.Scale.Y * s.Image.Height);
                    }
                    else
                    {
                        MessageBox.Show(s.Owner.Name + " sprite is null.");
                    }
                }
            }

            // Invalidate the form to trigger a repaint
            this.Invalidate();
        }

        // Process input events
        private void ProcessInput()
        {
            // Check for input events here
        }

        // The main entry point for the application
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