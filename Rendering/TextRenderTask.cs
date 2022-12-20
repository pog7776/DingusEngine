using DingusEngine.GameActor;
using DingusEngine.Rendering;
using DingusEngine.StandardComponents;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Media;
using System.Windows.Media.TextFormatting;

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
            TextRender.Brush = Brushes.Red;
        }

        //public void Action(DrawingContext g)
        public void Action(DrawingContext g)
        {
            //g.DrawText(TextRender.Text, TextRender.Font, TextRender.Brush, Transform.Position.X, Transform.Position.Y);
            //DrawingVisual drawingVisual = new DrawingVisual();
            //DrawingContext drawingContext = drawingVisual.RenderOpen();

            g.DrawText(
                new FormattedText(TextRender.Text,
                CultureInfo.GetCultureInfo("en-us"),
                FlowDirection.LeftToRight,
                TextRender.Font,
                TextRender.FontSize,
                TextRender.Brush),
                new System.Windows.Point(Transform.Position.X, Transform.Position.Y));

            // Close the DrawingContext to persist changes to the DrawingVisual.
            //drawingContext.Close();
        }
    }
}
