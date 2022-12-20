using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Numerics;
using System.Security.Policy;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using DingusEngine.GameComponent;
using DingusEngine.Rendering;

namespace DingusEngine.StandardComponents
{
    public class ASprite : Component
    {
        public SpriteRenderTask RenderTask { get; set; }

        public bool Visible {
            get { return _visible; }
            set
            {
                _visible = value;
                if(value)
                {
                    Engine.RenderHandler.AddTask(RenderTask);
                }
                else
                {
                    Engine.RenderHandler.RemoveTask(RenderTask);
                }
            }
        }
        private bool _visible = true;

        public Image Image
        {
            get { return _image; }
            set { _image = value; }
        }
        private Image _image;

        public Vector2 Scale
        {
            get { return _scale; }
            set { _scale = value; }
        }
        private Vector2 _scale;

        public Vector2 Dimensions => new Vector2((float)Image.Width * Scale.X, (float)Image.Height * Scale.Y);

        public ASprite()
        {
            this.Name = "Sprite";
            _scale = Vector2.One;
            _image = new Image();
        }

        public override void Start()
        {
            RenderTask = new SpriteRenderTask(this, Owner.Transform);

            if (Visible)
            {
                Engine.RenderHandler.AddTask(RenderTask);
            }
        }

        public override void Update()
        {
            // TODO create a function that is called each frame or something
        }

        public void SetSprite(string path)
        {
            //path = @"file:///" + Directory.GetCurrentDirectory() + "\\" + path;
            if (File.Exists(path))
            {
                //Image = Image.FromFile(path);

                Image.Source = new BitmapImage(new Uri(@"file:///" + Directory.GetCurrentDirectory() + "\\" + path));
                //Image.Source = new BitmapImage(new Uri(path));

                //FileStream fileStream =
                //    new FileStream(path, FileMode.Open, FileAccess.Read);
                //Image = new BitmapImage();
                //Image.BeginInit();
                //Image.StreamSource = fileStream;
                //Image.EndInit();
            }
            else
            {
                Visible = false;
                MessageBox.Show("\"" + Owner.Name + "\" sprite path is invalid.");
            }
        }

        public void SetScale(float scale)
        {
            Scale = new Vector2(scale, scale);
        }
    }
}