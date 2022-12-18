using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using GameEngine.GameComponent;

namespace GameEngine.StandardComponents
{
    internal class Sprite : Component
    {
        public Image Image
        {
            get { return _image; }
            set { _image = value; }
        }
        private Image _image;

        public Sprite()
        {
            this.Name = "Sprite";
        }

        // TODO create a function that is called each frame or something
        public override void Update()
        {
            // TODO Render Logic
        }
    }
}