using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace AliensKiller
{
    public class AlienBullet : MovingObjects
    {
        public new const string CollisionGroupString = "alienbullet";

        public AlienBullet(MatrixCoordinates topLeft)
            : base(topLeft, new char[,] { { '▼' } }, new MatrixCoordinates(1, 0))
        {
        }

        public override bool CanCollideWith(string otherCollisionGroupString)
        {
            return otherCollisionGroupString == "spaceship";
        }

        public override string GetCollisionGroupString()
        {
            return AlienBullet.CollisionGroupString;
        }
    }
}
