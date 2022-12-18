using System;
using System.Collections.Generic;
using System.Linq;
using System.Numerics;
using System.Text;
using System.Threading.Tasks;
using GameEngine.GameComponent;

namespace GameEngine.StandardComponents
{
    internal class Transform : Component
    {
        public Vector3 Position
        {
            get { return _position;  }
            set { _position = value; }
        }
        private Vector3 _position;

        public Vector3 Rotation
        {
            get { return _rotation; }
            set { _rotation = value; }
        }
        private Vector3 _rotation;

        public Transform()
        {
            Position = new Vector3(0, 0, 0);
            Rotation = new Vector3(0, 0, 0);
        }

        public override void Update()
        {
            throw new NotImplementedException();
        }
    }
}
