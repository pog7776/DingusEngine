using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DingusEngine.GameComponent
{
    public abstract class Component : IComponent
    {
        public string Name
        {
            get { return _name; }
            set { _name = value; }
        }
        private string _name = "New Component";

        public IActor Owner
        {
            get { return _owner; }
            set { _owner = value; }
        }
        private IActor _owner;

        public EGameEngine Engine
        {
            get { return _engine; }
        }
        private EGameEngine _engine;

        public Component()
        {
            // Get the GameEngine
            _engine = EGameEngine.Engine;
        }

        public abstract void Start();
        public abstract void Update();
    }
}
