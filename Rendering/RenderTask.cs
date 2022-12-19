using DingusEngine.StandardComponents;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DingusEngine.Rendering
{
    public class ERenderTask : IRenderTask
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

        public ERenderTask(ASprite sprite, ATransform transform)
        {
            _sprite = sprite;
            _transform = transform;
        }
    }
}
