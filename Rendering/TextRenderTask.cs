using DingusEngine.GameActor;
using DingusEngine.Rendering;
using DingusEngine.StandardComponents;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DingusEngine.Rendering
{
    internal class TextRenderTask : IRenderTask
    {
        public ATextRender TextRender { get; set; }

        public ATransform Transform
        {
            get { return _transform; }
        }
        private ATransform _transform;

        public TextRenderTask(ATextRender textRender, ATransform transform)
        {
            TextRender = textRender;
            _transform = transform;
        }

        public void Action(Graphics g)
        {
            g.DrawString(TextRender.Text, TextRender.Font, TextRender.Brush, Transform.Position.X, Transform.Position.Y);
        }
    }
}
