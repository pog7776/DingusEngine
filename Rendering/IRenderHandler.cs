using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DingusEngine.Rendering
{
    public interface IRenderHandler
    {
        public List<ERenderTask> Tasks { get; }

        public void AddTask(ERenderTask task);
        public void RemoveTask(ERenderTask task);
    }
}
