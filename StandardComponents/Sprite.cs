using System;
using System.Collections.Generic;
using System.Linq;
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

        public ASprite()
        {
            this.Name = "Sprite";
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
    }
}