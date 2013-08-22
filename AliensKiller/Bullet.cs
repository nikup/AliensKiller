using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AliensKiller
{
    public class Bullet : MovingObjects
    {
        public new const string CollisionGroupString = "bullet";

        public Bullet(MatrixCoordinates topLeft, MatrixCoordinates speed)
            : base(topLeft, new char[,] { { '▀' } }, speed)
        {
        }

        public override bool CanCollideWith(string otherCollisionGroupString)
        {
            return otherCollisionGroupString == "spaceship" || otherCollisionGroupString == "alien"||otherCollisionGroupString=="title";
        }

        public override string GetCollisionGroupString()
        {
            return Bullet.CollisionGroupString;
        }

        public override void RespondToCollision(CollisionData collisionData)
        {
            this.IsDestroyed = true;
        }
    }
}
