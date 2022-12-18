using System;
using System.Drawing;
using System.Windows.Forms;
using System.Numerics;
using GameEngine.Actors;
using GameEngine.GameActor;
using GameEngine.StandardComponents;
using Timer = System.Windows.Forms.Timer;
using Microsoft.VisualBasic.Devices;
using System.Runtime.InteropServices;
using WriteLine = System.Diagnostics.Debug;
using System.Security.Policy;

namespace GameEngine
{
    public class GameEngine : Form
    {
        // Timer to control the game loop
        private Timer gameTimer;

        // The current game state
        private GameState gameState;

        // The current frame rate
        private int frameRate;

        // The time elapsed since the last frame
        private float elapsedTime;

        private List<Actor> actors = new List<Actor>();

        Graphics g;
        private Image _bufferImage;


        public GameEngine()
        {
            // Set the size and title of the window
            this.ClientSize = new Size(800, 600);
            this.Text = "My Game Engine";

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

        // TODO Remove later
        private Sprite renderSprite;
        private Transform playerTransform;
        private Vector2 dimensions;

        // Before game starts
        private void Start()
        {
            #region Test Actor

            TestActor ta = new TestActor();
            actors.Add(ta);
            playerTransform = ta.GetComponent<Transform>();
            renderSprite = ta.GetComponent<Sprite>();
            dimensions = new Vector2(renderSprite.Image.Width, renderSprite.Image.Height);

            #endregion

            // Init Graphics
            g = this.CreateGraphics();

            // Create a buffer image with the same size as the form
            _bufferImage = new Bitmap(this.ClientSize.Width, this.ClientSize.Height);

            // Enable double buffering for the form
            this.SetStyle(ControlStyles.DoubleBuffer | ControlStyles.UserPaint | ControlStyles.AllPaintingInWmPaint, true);
            this.UpdateStyles();
        }

        // The game loop
        private void OnTick(object sender, EventArgs e)
        {
            // Update the elapsed time
            elapsedTime = (float)gameTimer.Interval / 1000;

            // Update the game state
            Update(elapsedTime);

            // Render the game
            Render();

            // Check for input events
            ProcessInput();
        }

        // Update the game state
        private void Update(float elapsedTime)
        {
            // Update the game logic here
            // TODO handle a list of Update actions owned by actors here

            Point cursorPos = this.PointToClient(Cursor.Position);
            playerTransform.Position = new Vector3(cursorPos.X - (dimensions.X / 2), cursorPos.Y - (dimensions.Y / 2), 0);
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
                g.DrawImage(renderSprite.Image, new Point((int)playerTransform.Position.X, (int)playerTransform.Position.Y));
                // TODO Add a render queue here
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
            Application.Run(new GameEngine());
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