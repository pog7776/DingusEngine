using System;
using System.Collections.Generic;
using System.Linq;
using System.Numerics;
using System.Security.Policy;
using System.Text;
using System.Threading.Tasks;
using DingusEngine.GameComponent;
using DingusEngine.Rendering;

namespace DingusEngine.StandardComponents
{
    public class ASprite : Component
    {
        public ERenderTask RenderTask { get; set; }

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

        public Vector2 Dimensions => new Vector2(Image.Width * Scale.X, Image.Height * Scale.Y);

        public ASprite()
        {
            this.Name = "Sprite";
            _scale = Vector2.One;
        }

        public override void Start()
        {
            RenderTask = new ERenderTask(this, Owner.Transform);

            if (Visible)
            {
                Engine.RenderHandler.AddTask(RenderTask);
            }
        }

        // TODO create a function that is called each frame or something
        public override void Update()
        {
            
        }

        public void SetSprite(string path)
        {
            if (File.Exists(path))
            {
                Image = Image.FromFile(path);
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