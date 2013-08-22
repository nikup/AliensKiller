using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AliensKiller
{
    public class Point:GameObject
    {
        public int LifeTime { get; set; }

        public Point(MatrixCoordinates topLeft, int lifeTime)
            : base(topLeft, new char[,]{{'1'}})
        {
            this.LifeTime = lifeTime;
        }

        public override void Update()
        {
            if (this.LifeTime <= 0)
            {
                this.IsDestroyed = true;
            }
            this.LifeTime--;
            
        }

        
        

        public override bool CanCollideWith(string otherCollisionGroupString)
        {
            return otherCollisionGroupString == Wall.CollisionGroupString;
        }
    }
}
