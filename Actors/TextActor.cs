using DingusEngine.GameActor;
using DingusEngine.Rendering;
using DingusEngine.StandardComponents;
using GameEngine.Rendering;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Numerics;
using System.Text;
using System.Threading.Tasks;

namespace GameEngine.Actors
{
    internal class TextActor : Actor
    {
        ATextRender text;

        public TextActor()
        {
            Transform.Position = new Vector3(50, 10, 10);
            text = AddComponent<ATextRender>();
            text.Text = "Hello World!!";
        }

        public override void Update()
        {
            //throw new NotImplementedException();
        }
    }
}
