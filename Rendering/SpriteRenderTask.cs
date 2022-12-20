using DingusEngine.StandardComponents;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Policy;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Media.Media3D;

namespace DingusEngine.Rendering
{
    public class SpriteRenderTask : IRenderTask
    {
        public ASprite Sprite
        {
            get { return _sprite; }
        }
        private ASprite _sprite;

        public ATransform Transform
        {
            get { return _transform; }
        }
        private ATransform _transform;

        public SpriteRenderTask(ASprite sprite, ATransform transform)
        {
            _sprite = sprite;
            _transform = transform;
        }

        public void Action(DrawingContext g)
        {
            if (Sprite != null || Sprite.Image != null)
            {
                //! GOOD ONE for reference
                //g.DrawImage(Sprite.Image.Source, new Rect(Transform.Position.X, Transform.Position.Y, Sprite.Image.Source.Width * Sprite.Scale.X, Sprite.Image.Source.Height * Sprite.Scale.Y));

                // Get all the bounds so the image can be cropped
                // Otherwise it renders the offscreen object and moves everything else to fit
                Rect imageBounds = new Rect(Transform.Position.X, Transform.Position.Y, Sprite.Image.Source.Width * Sprite.Scale.X, Sprite.Image.Source.Height * Sprite.Scale.Y);
                Rect canvasBounds = new Rect(0, 0, EGameEngine.Engine.Width, EGameEngine.Engine.Height);

                Rect visibleBounds = Rect.Intersect(imageBounds, canvasBounds);

                if (visibleBounds.Width <= 0 || visibleBounds.Height <= 0)
                {
                    return;
                }

                Rect cropBounds = new Rect(
                visibleBounds.X - Transform.Position.X,
                visibleBounds.Y - Transform.Position.Y,
                visibleBounds.Width,
                visibleBounds.Height);

                CroppedBitmap croppedImage = new CroppedBitmap((BitmapSource)Sprite.Image.Source, new Int32Rect((int)cropBounds.X, (int)cropBounds.Y, (int)cropBounds.Width, (int)cropBounds.Height));

                if (IsOffScreen(imageBounds, visibleBounds))
                {
                    g.DrawImage(croppedImage, visibleBounds);
                }
                else
                {
                    g.DrawImage(Sprite.Image.Source, imageBounds);
                }

                // Debug
                SolidColorBrush brush = new SolidColorBrush();
                brush.Color = Colors.Blue;

                Pen pen = new Pen();
                pen.Thickness = 2;
                pen.Brush = brush;
                g.DrawRectangle(null, pen, visibleBounds);

            }
            else
            {
                MessageBox.Show(Sprite.Owner.Name + " sprite is null.");
            }
        }

        private static bool IsOffScreen(Rect imageBounds, Rect visibleBounds) => (RectArea(visibleBounds) < RectArea(imageBounds));
        private static double RectArea(Rect rect) => rect.Width * rect.Height;
    }
}
