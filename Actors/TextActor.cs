using DingusEngine.GameActor;
using DingusEngine.Rendering;
using DingusEngine.StandardComponents;
using DingusEngine.Rendering;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Numerics;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Media;

namespace DingusEngine.Actors
{
    internal class TextActor : Actor
    {
        ATextRender text;

        int tick = 0;
        int totalFames = 0;
        float time = 0;

        public TextActor()
        {
            Transform.Position = new Vector3(10, 10, 10);
            text = AddComponent<ATextRender>();
            text.Text = "0";
            text.FontSize = 20;
        }

        public override void Update()
        {
            time += EGameEngine.Engine.DeltaTime;
            tick++;

            //text.Text = (frame / time).ToString("N0");
            text.Text = "TPS: " + (tick / time).ToString();

            if (time > 1)
            {
                totalFames = tick;
                tick = 0;
                time = 0;
            }
        }
    }
}
