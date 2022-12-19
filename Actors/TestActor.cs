using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Numerics;
using DingusEngine.GameActor;
using DingusEngine.StandardComponents;

namespace DingusEngine.Actors
{
    internal class TestActor : Actor
    {
        private Vector2 dimensions;
        private int tick = 0;

        ASprite sprite;

        public TestActor()
        {
            this.Name = "Test Actor :)";
            sprite = AddComponent<ASprite>();
            sprite.Image = Image.FromFile("Assets\\Sprites\\TestSprite\\sprite.png");

            //Transform = GetComponent<ATransform>();
            //transform.Position.X = 100;
            //transform.Position.Y = 100;

            //Transform.Position = new Vector3(0, 0, 0);
            dimensions = new Vector2(sprite.Image.Width, sprite.Image.Height);
        }

        public override void Update()
        {
            Point cursorPos = GameEngine.Engine.PointToClient(Cursor.Position);
            Transform.Position = new Vector3(cursorPos.X - (dimensions.X / 2), cursorPos.Y - (dimensions.Y / 2), 0);

            if(tick == 60*5)
            {
                sprite.Visible = false;
            }
            tick++;
        }
    }
}
