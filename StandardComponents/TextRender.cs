using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DingusEngine.GameComponent;
using DingusEngine.Rendering;
using System.Windows;
using System.Windows.Media;
using System.Windows.Media.TextFormatting;

namespace DingusEngine.StandardComponents
{
    internal class ATextRender : Component
    {
        public string Text { get; set; }

        public Typeface Font
        {
            get { return _font; }
            set { _font = value; }
        }
        private Typeface _font = new Typeface("Arial");

        public int FontSize
        {
            get { return _fontSize; }
            set { _fontSize = value; }
        }
        private int _fontSize = 12;

        public Brush Brush
        {
            get { return _brush; }
            set { _brush = value; }
        }
        private Brush _brush = new SolidColorBrush(Colors.Black);

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
            Name = "Text";
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
