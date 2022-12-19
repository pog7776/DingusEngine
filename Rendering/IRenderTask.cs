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
        public ATransform Transform { get; }

        public void Action(Graphics g);
    }
}
