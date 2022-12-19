using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DingusEngine.GameActor
{
    internal interface IActorManager
    {
        public List<IActor> Actors { get; }

        public T? CreateActor<T>() where T : new();
        public void AddActor(IActor actor);

        //TODO Find actor functions
    }
}
