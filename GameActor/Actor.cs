using System;
using System.Collections.Generic;
using System.ComponentModel;
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
        public GameEngine Engine
        {
            get { return _engine; }
        }
        private GameEngine _engine;

        // Constructor
        public Actor()
        {
            _components = new List<IComponent>();
            Transform = AddComponent<ATransform>();
            _engine = GameEngine.Engine;
        }

        // TODO register update with a list of update actions in the program.cs file
        public abstract void Update();

        public T? AddComponent<T>(IComponent component)
        {
            if (component != null)
            {
                component.Owner = this;
                Components.Add(component);
                Console.WriteLine(component.Name + " added to " + this.Name);
                component.Start();
                return (T) component;
            }
            else
            {
                Console.WriteLine("Component added to " + this.Name + " is null");
            }

            return default;
        }

        public T? AddComponent<T>() where T : new()
        {
            if (Components != null)
            {
                if (typeof(T).GetInterfaces().Contains(typeof(IComponent)))
                {
                    IComponent? component = new T() as IComponent;

                    if (component != null)
                    {
                        component.Owner = this;
                        Components.Add(component);
                        Console.WriteLine(component.Name + " added to " + this.Name);
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
            return default;
        }
        
        public void RemoveComponent(IComponent component)
        {
            if (component != null)
            {
                string componentName = component.Name;
                // Try remove the component
                if (!Components.Remove(component))
                {
                    Console.WriteLine("No component matching to remove from " + this.Name);
                }
                else
                {
                    Console.WriteLine("Removed component: " + componentName + " from " + this.Name);
                }
            }
            else
            {
                Console.WriteLine("Component to remove from " + this.Name + " is null");
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
                    Console.WriteLine("Removed component: " + componentName + " from " + this.Name);
                }
                else
                {
                    Console.WriteLine("No component of type: " + typeof(T) + " present on Actor: " + this.Name);
                }
            }
            else
            {
                Console.WriteLine(typeof(T) + " is not an IComponent.");
            }
        }

        public T? GetComponent<T>()
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
                    Console.WriteLine("No component of type: " + typeof(T) + " present on Actor: " + this.Name);
                }
            }
            else
            {
                Console.WriteLine(typeof(T) + " is not an IComponent.");
            }
            return default;
        }
    }
}
