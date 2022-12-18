using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Numerics;
using GameEngine.GameActor;
using GameEngine.StandardComponents;

namespace GameEngine.Actors
{
    internal class TestActor : Actor
    {
        public TestActor()
        {
            this.Name = "Test Actor :)";
            Sprite sprite = AddComponent<Sprite>();
            sprite.Image = Image.FromFile("Assets\\Sprites\\TestSprite\\sprite.png");

            Transform transform = GetComponent<Transform>();
            //transform.Position.X = 100;
            //transform.Position.Y = 100;
            transform.Position = new Vector3(0, 0, 0);
        }

        public override void Update()
        {
            throw new NotImplementedException();
        }
    }
}
