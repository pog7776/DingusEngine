using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Numerics;
using DingusEngine.GameActor;
using DingusEngine.StandardComponents;
using System.Windows;
using System.Windows.Input;

namespace DingusEngine.Actors
{
    internal class TestActor : Actor
    {
        private int tick = 0;
        ASprite sprite;

        public TestActor()
        {
            this.Name = "Test Actor :)";
            sprite = AddComponent<ASprite>();
            //sprite.Image = Image.FromFile("Assets\\Sprites\\TestSprite\\sprite.pngg");
            sprite.SetSprite("Assets\\Sprites\\TestSprite\\sprite.png");

            sprite.Scale = new Vector2(0.01f, 0.01f);
            //sprite.SetScale(0.3f);
        }

        public override void Update()
        {
            //Point cursorPos = Mouse.GetPosition(App.Current.MainWindow);
            //Transform.Position = new Vector3((float)cursorPos.X - (sprite.Dimensions.X / 2), (float)cursorPos.Y - (sprite.Dimensions.Y / 2), 0);

            if (sprite.Scale.X <= 0.3f)
            {
                sprite.Scale += new Vector2(Engine.DeltaTime);
            }

            if(tick == 60*5)
            {
                //sprite.Visible = false;
                Transform.Position = new Vector3(Transform.Position.X, Transform.Position.Y, 1);
            }
            tick++;
        }
    }
}
