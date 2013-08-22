using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AliensKiller
{
    public class Wall:GameObject
    {
        public new const string CollisionGroupString = "wall";

        public Wall(MatrixCoordinates upperLeft,char [,] body)
            : base(upperLeft,body)
        {
           
        }

        public override void Update()
        {

        }
        public override bool CanCollideWith(string otherCollisionGroupString)
        {
            return otherCollisionGroupString == "alien" || otherCollisionGroupString == "spaceship";
        }

        public override string GetCollisionGroupString()
        {
            return Wall.CollisionGroupString;
        }

        public override void RespondToCollision(CollisionData collisionData)
        {
            this.IsDestroyed = false;
        }
    }
}
