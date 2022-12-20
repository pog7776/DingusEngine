using DingusEngine.GameActor;
using DingusEngine.StandardComponents;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Numerics;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;

namespace GameEngine.Actors
{
    internal class Player : Actor
    {
        ASprite sprite;
        float speed = 5f;

        public Player()
        {
            sprite = AddComponent<ASprite>();
            sprite.SetSprite("Assets\\Sprites\\TestSprite\\sprite2.png");

            Engine.InputManager.OnKeyHeld(Key.W, delegate { Move(new Vector3(0, -1, 0)); });
            Engine.InputManager.OnKeyHeld(Key.A, delegate { Move(new Vector3(-1, 0, 0)); });
            Engine.InputManager.OnKeyHeld(Key.S, delegate { Move(new Vector3(0, 1, 0)); });
            Engine.InputManager.OnKeyHeld(Key.D, delegate { Move(new Vector3(1, 0, 0)); });

            Engine.InputManager.OnKeyDown(Key.Up,   delegate { speed++; });
            Engine.InputManager.OnKeyDown(Key.Down, delegate { speed--; });
        }

        public override void Update()
        {
            //throw new NotImplementedException();
        }

        public void Move(Vector3 direction)
        {
            Transform.Position += direction * speed;
        }
    }
}
