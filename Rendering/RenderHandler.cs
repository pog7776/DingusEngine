using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DingusEngine.Rendering
{
    public class ERenderHandler : IRenderHandler
    {
        public List<ERenderTask> Tasks
        {
            get { return _renderTasks; }
        }
        private List<ERenderTask> _renderTasks;

        public ERenderHandler()
        {
            _renderTasks = new List<ERenderTask>();
        }

        public void AddTask(ERenderTask task)
        {
            _renderTasks.Add(task);
        }

        public void RemoveTask(ERenderTask task)
        {
            _renderTasks.Remove(task);
        }
    }
}
