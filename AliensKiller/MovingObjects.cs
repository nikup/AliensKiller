using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AliensKiller
{
    public class MovingObjects : GameObject
    {
        public MatrixCoordinates Speed { get; protected set; }

        public MovingObjects(MatrixCoordinates topLeft, char[,] body, MatrixCoordinates speed)
            : base(topLeft, body)
        {
            this.Speed = speed;
        }

        protected virtual void UpdatePosition()
        {
            this.TopLeft += this.Speed;
        }

        public override void Update()
        {
            this.UpdatePosition();
        }
    }
}
