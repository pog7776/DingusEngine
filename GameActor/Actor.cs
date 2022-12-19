using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DingusEngine.StandardComponents;

namespace DingusEngine.GameActor
{
    internal abstract class Actor : IActor
    {
        public ATransform Transform
        {
            get { return _transform; }
            set { _transform = value; }
        }
        private ATransform _transform;

        // Actor Name
        public string Name
        {
            get { return _name; }
            set { _name = value; }
        }
        private string _name = "New Actor";

        // Actor components
        public List<IComponent> Components
        {
            get { return _components; }
        }
        private List<IComponent> _components;

        // The GameEngine class
        public EGameEngine Engine
        {
            get { return _engine; }
        }
        private EGameEngine _engine;

        // Constructor
        public Actor()
        {
            _components = new List<IComponent>();
            Transform = AddComponent<ATransform>();
            _engine = EGameEngine.Engine;
        }

        public abstract void Update();

        public T AddComponent<T>(IComponent component)
        {
            if (component != null)
            {
                component.Owner = this;
                Components.Add(component);
                Debug.WriteLine(component.Name + " added to " + this.Name);
                component.Start();
                return (T) component;
            }
            else
            {
                Debug.WriteLine("Component added to " + this.Name + " is null");
                throw new ArgumentException("Component type error");
            }
        }

        public T AddComponent<T>() where T : new()
        {
            _components ??= new List<IComponent>();

            if (typeof(T).GetInterfaces().Contains(typeof(IComponent)))
            {
                IComponent? component = new T() as IComponent;

                if (component != null)
                {
                    component.Owner = this;
                    Components.Add(component);
                    Debug.WriteLine(component.Name + " added to " + this.Name);
                    component.Start();
                    return (T) component;
                }
                else
                {
                    throw new ArgumentException("Type: " + typeof(T) + " is not an IComponent.");
                }
            }
            else
            {
                throw new ArgumentException("Type: " + typeof(T) + " is not an IComponent.");
            }
        }
        
        public void RemoveComponent(IComponent component)
        {
            if (component != null)
            {
                string componentName = component.Name;
                // Try remove the component
                if (!Components.Remove(component))
                {
                    Debug.WriteLine("No component matching to remove from " + this.Name);
                }
                else
                {
                    Debug.WriteLine("Removed component: " + componentName + " from " + this.Name);
                }
            }
            else
            {
                Debug.WriteLine("Component to remove from " + this.Name + " is null");
            }
        }

        public void RemoveComponent<T>()
        {
            if(typeof(T).GetInterfaces().Contains(typeof(IComponent)))
            {
                IComponent? match = Components.FirstOrDefault(predicate: x => x.GetType() == typeof(T));

                if(match != null)
                {
                    string componentName = match.Name;
                    RemoveComponent(match);
                    Debug.WriteLine("Removed component: " + componentName + " from " + this.Name);
                }
                else
                {
                    Debug.WriteLine("No component of type: " + typeof(T) + " present on Actor: " + this.Name);
                }
            }
            else
            {
                Debug.WriteLine(typeof(T) + " is not an IComponent.");
            }
        }

        public T GetComponent<T>()
        {
            if (typeof(T).GetInterfaces().Contains(typeof(IComponent)))
            {
                IComponent? match = Components.FirstOrDefault(predicate: x => x.GetType() == typeof(T));

                if (match != null)
                {
                    string componentName = match.Name;
                    return (T)match;
                }
                else
                {
                    Debug.WriteLine("No component of type: " + typeof(T) + " present on Actor: " + this.Name);
                    return default;
                }
            }
            else
            {
                Debug.WriteLine(typeof(T) + " is not an IComponent.");
                throw new ArgumentException("Type: " + typeof(T) + " is not an IComponent.");
            }
        }
    }
}
