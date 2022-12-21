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
using System.Windows.Controls;

namespace DingusEngine.StandardComponents
{
    internal class ATextRender : Component
    {
        public string Text {
            get { return TextBlock.Text; }
            set { TextBlock.Text = value; }
        }

        public TextBlock TextBlock { get; set; }
        private TextBlock _textBlock;

        public FontFamily Font
        {
            get { return TextBlock.FontFamily; }
            set { TextBlock.FontFamily = value; }
        }

        public double FontSize
        {
            get { return TextBlock.FontSize; }
            set { TextBlock.FontSize = value; }
        }

        //public Brush Brush
        //{
        //    get { return _brush; }
        //    set { _brush = value; }
        //}
        //private Brush _brush = new SolidColorBrush(Colors.Black);

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
            TextBlock = new TextBlock();
            Font = new FontFamily("Arial");
            FontSize= 12;
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
