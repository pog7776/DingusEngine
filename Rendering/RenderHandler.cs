using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DingusEngine.Rendering
{
    public class ERenderHandler : IRenderHandler
    {
        public List<IRenderTask> Tasks
        {
            get { return _renderTasks; }
        }
        private List<IRenderTask> _renderTasks;

        public ERenderHandler()
        {
            _renderTasks = new List<IRenderTask>();
        }

        public void AddTask(IRenderTask task)
        {
            _renderTasks.Add(task);
            RefreshTasks();
        }

        public void RemoveTask(IRenderTask task)
        {
            _renderTasks.Remove(task);
            RefreshTasks();
        }

        public void RefreshTasks()
        {
            _renderTasks = _renderTasks.OrderBy(x => x.Transform.Position.Z).ToList();
        }
    }
}
