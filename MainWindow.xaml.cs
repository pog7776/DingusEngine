using DingusEngine.Actors;
using DingusEngine.GameActor;
using DingusEngine.Input;
using DingusEngine.Rendering;
using DingusEngine.StandardComponents;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.IO;
using System.Linq;
using System.Security.Policy;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Media.Media3D;
using System.Windows.Navigation;
using System.Windows.Shapes;
using System.Windows.Threading;

namespace DingusEngine
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class EGameEngine : Window
    {
        public static EGameEngine Engine = null;

        // Timer to control the game loop
        private DispatcherTimer gameTimer;

        // The current game state
        private GameState gameState;

        // The current frame rate
        private int frameRate;

        // The time elapsed since the last frame
        public float DeltaTime => _deltaTime;
        private float _deltaTime;

        public EActorManager actorManager;
        public EInputManager InputManager;

        private DrawingVisual _buffer;
        public Canvas Canvas => _canvas;
        private Canvas _canvas;
        public ERenderHandler RenderHandler;

        public EGameEngine()
        {
            InitializeComponent();
            Engine = this;
            actorManager = new EActorManager();
            InputManager = new EInputManager();
            // Set the size and title of the window
            //this.Width = 800;
            //this.Height = 600;
            this.Title = "Dingus Engine";
            this.SizeChanged += OnResize;

            // Init Graphics
            _buffer = new DrawingVisual();
            _canvas = (Canvas)this.FindName("RenderLayer");
            RenderHandler = new ERenderHandler();

            this.SetValue(RenderOptions.EdgeModeProperty, EdgeMode.Aliased);
            //RenderOptions.EdgeModeProperty = EdgeMode.Aliased;
            this.SetValue(RenderOptions.BitmapScalingModeProperty, BitmapScalingMode.HighQuality);

            // Initialize the game state and frame rate
            gameState = GameState.Running;
            frameRate = 60;

            // Initialize the timer
            gameTimer = new DispatcherTimer();
            gameTimer.Interval = new TimeSpan(10000 / frameRate);
            gameTimer.Tick += OnTick;
            gameTimer.Start();

            Start();
        }

        // Before game starts
        private void Start()
        {
            #region Test Actors

            //TestActor ta = actorManager.CreateActor<TestActor>();

            //MovingActor ma = actorManager.CreateActor<MovingActor>();

            TextActor txa = actorManager.CreateActor<TextActor>();

            Player player = actorManager.CreateActor<Player>();

            //ASprite s = ta.GetComponent<ASprite>();
            //ATransform tf = ta.GetComponent<ATransform>();

            //InputManager.OnKeyDown(Key.Space, delegate
            //{
            //    s.SetScale(1f);
            //});

            //InputManager.OnKeyUp(Key.Space, delegate
            //{
            //    s.SetScale(0.3f);
            //});

            #endregion
        }

        // The game loop
        private void OnTick(object sender, EventArgs e)
        {
            // Update the elapsed time
            _deltaTime = (float)gameTimer.Interval.TotalMilliseconds;

            // Update the game state
            switch (gameState)
            {
                case GameState.Running:
                    // Update the game
                    Update();
                    // Render the game
                    Render();
                    break;
                case GameState.Paused:
                    // Render the pause screen
                    RenderPause();
                    break;
                case GameState.GameOver:
                    // Render the game over screen
                    RenderGameOver();
                    break;
            }

            // Draw the buffer image to the form
            //g.Close();
            //this.InvalidateVisual();
        }

        // Update the game
        private void Update()
        {
            // Update the input
            InputManager.Update();

            // Update the actors
            actorManager.Update();
        }

        // Render the game
        private void Render()
        {
            _canvas.Width = this.ActualWidth;
            _canvas.Height = this.ActualHeight;
            //_canvas.TransformToAncestor(this);
            //_canvas.TranslatePoint(new Point(0,0), this);
            _canvas.Children.Clear();
            _buffer.Children.Clear();

            using (DrawingContext context = _buffer.RenderOpen())
            {
                // Draw something using the DrawingContext
                //context.DrawImage(image.Source, new Rect(image.RenderTransform.Value.OffsetX, image.RenderTransform.Value.OffsetY, image.Source.Width, image.Source.Height));
                foreach (IRenderTask task in RenderHandler.Tasks)
                {
                    task.Action(context);
                }

                // Debug
                // Actually, hacky workaround to make the canvas full screen
                SolidColorBrush brush = new SolidColorBrush();
                brush.Color = Colors.Blue;

                Pen pen = new Pen();
                pen.Thickness = 1;
                pen.Brush = brush;
                //Rect canvasBounds = new Rect(0, 0, this.Width - 50, this.Height - 50);
                Rect canvasBounds = new Rect(0, 0, _canvas.Width, _canvas.Height);
                context.DrawRectangle(null, pen, canvasBounds);
            }

            Image img = new Image();
            img.Source = new DrawingImage(_buffer.Drawing);
            _canvas.Children.Add(img);
        }

        // Render the pause screen
        private void RenderPause()
        {
            // Render the pause screen
        }

        // Render the game over screen
        private void RenderGameOver()
        {
            // Render the game over screen
        }

        // Handle window resize events
        private void OnResize(object sender, EventArgs e)
        {
            // Update the buffer image size
            //_buffer = new DrawingVisual();
            //g = _buffer.RenderOpen();
        }
    }

    public enum GameState
    {
        Running,
        Paused,
        GameOver,
        // Add additional game states here
    }
}
