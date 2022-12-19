using DingusEngine.StandardComponents;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DingusEngine.Rendering
{
    public interface IRenderTask
    {
        public ASprite Sprite { get; }
        public ATransform Transform { get; }
        //public int Priority { get; set; }
    }
}
