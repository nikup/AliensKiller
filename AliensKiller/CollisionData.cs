using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AliensKiller
{
    public class CollisionData
    {
        public readonly MatrixCoordinates CollisionForceDirection;
        public readonly List<string> hitObjectsCollisionGroupStrings;

        public CollisionData(MatrixCoordinates collisionForceDirection, string objectCollisionGroupString)
        {
            this.CollisionForceDirection = collisionForceDirection;
            this.hitObjectsCollisionGroupStrings = new List<string>();
            this.hitObjectsCollisionGroupStrings.Add(objectCollisionGroupString);
        }

        public CollisionData(MatrixCoordinates collisionForceDirection, List<string> hitObjectsCollisionGroupStrings)
        {
            this.CollisionForceDirection = collisionForceDirection;

            this.hitObjectsCollisionGroupStrings = new List<string>();

            foreach (var str in hitObjectsCollisionGroupStrings)
            {
                this.hitObjectsCollisionGroupStrings.Add(str);
            }
        }
    }
}
