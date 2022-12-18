using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GameEngine.GameComponent
{
    internal abstract class Component : IComponent
    {
        public string Name
        {
            get { return _name; }
            set { _name = value; }
        }
        private string _name = "New Component";

        public abstract void Update();
    }
}
