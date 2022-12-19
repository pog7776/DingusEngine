using DingusEngine.GameActor;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GameEngine.GameActor
{
    public class EActorManager : IActorManager
    {
        public List<IActor> Actors {
            get { return _actors; }
        }
        private List<IActor> _actors;

        public EActorManager()
        {
            _actors = new List<IActor>();
        }

        public T? CreateActor<T>() where T : new()
        {
            if (Actors != null)
            {
                if (typeof(T).GetInterfaces().Contains(typeof(IActor)))
                {
                    IActor? actor = new T() as IActor;

                    if (actor != null)
                    {
                        AddActor(actor);
                        //Actor.Start();
                        return (T)actor;
                    }
                    else
                    {
                        throw new ArgumentException("Type: " + typeof(T) + " is not an IActor.");
                    }
                }
                else
                {
                    throw new ArgumentException("Type: " + typeof(T) + " is not an IActor.");
                }
            }
            return default;
        }

        public void AddActor(IActor actor)
        {
            _actors.Add(actor);
        }
    }
}
