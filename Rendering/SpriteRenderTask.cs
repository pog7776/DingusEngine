using DingusEngine.StandardComponents;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

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

        public void Action(Graphics g)
        {
            if (Sprite != null || Sprite.Image != null)
            {
                g.DrawImage(Sprite.Image,
                    Transform.Position.X, Transform.Position.Y,
                    Sprite.Scale.X * Sprite.Image.Width, Sprite.Scale.Y * Sprite.Image.Height);
            }
            else
            {
                MessageBox.Show(Sprite.Owner.Name + " sprite is null.");
            }
        }
    }
}
