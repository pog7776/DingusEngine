using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Numerics;
using DingusEngine.GameActor;
using DingusEngine.StandardComponents;

namespace GameEngine.Actors
{
    internal class MovingActor : Actor
    {
        ASprite sprite;
        int tick = 0;

        public MovingActor()
        {
            Transform.Position = new Vector3(100, 100, 0);

            Name = "Moving";
            sprite = AddComponent<ASprite>();
            sprite.SetSprite("Assets\\Sprites\\TestSprite\\aware.gif");
            sprite.SetScale(0.3f);
        }

        public override void Update()
        {
            Transform.Position = new Vector3(Transform.Position.X + MathF.Sin(tick) * 10, Transform.Position.Y, Transform.Position.Z);

            tick++;
        }
    }
}
