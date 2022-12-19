using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DingusEngine.GameComponent;
using DingusEngine.Rendering;
using GameEngine.Rendering;

namespace DingusEngine.StandardComponents
{
    internal class ATextRender : Component
    {
        public string Text { get; set; }

        public Font Font
        {
            get { return _font; }
            set { _font = value; }
        }
        private Font _font = new Font("Arial", 12.0f);

        public Brush Brush
        {
            get { return _brush; }
            set { _brush = value; }
        }
        private Brush _brush = new SolidBrush(Color.Black);

        public TextRenderTask RenderTask { get; set; }

        public bool Visible
        {
            get { return _visible; }
            set
            {
                _visible = value;
                if (value)
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

        public override void Start()
        {
            Name = "Test Text";
            RenderTask = new TextRenderTask(this, Owner.Transform);

            if (Visible)
            {
                Engine.RenderHandler.AddTask(RenderTask);
            }
        }

        public override void Update()
        {
            //throw new NotImplementedException();
        }
    }
}
