using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DingusEngine.Rendering
{
    public interface IRenderHandler
    {
        public List<IRenderTask> Tasks { get; }

        public void AddTask(IRenderTask task);
        public void RemoveTask(IRenderTask task);
    }
}
