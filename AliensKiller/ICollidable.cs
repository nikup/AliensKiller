using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AliensKiller
{
    public interface ICollidable
    {
        bool CanCollideWith(string objectType);

        List<MatrixCoordinates> GetCollisionProfile();

        void RespondToCollision(CollisionData collisionData);

        string GetCollisionGroupString();
    }
}
