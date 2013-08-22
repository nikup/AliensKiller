using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AliensKiller
{
    public static class CollisionDispatcher
    {
        public static void HandleCollisions(List<MovingObjects> movingObjects, List<GameObject> staticObjects)
        {
            HandleMovingWithStaticCollisions(movingObjects, staticObjects);
        }

        private static void HandleMovingWithStaticCollisions(List<MovingObjects> movingObjects, List<GameObject> staticObjects)
        {
            foreach (var movingObject in movingObjects)
            {
                int verticalIndex = VerticalCollisionIndex(movingObject, staticObjects);
                int horizontalIndex = HorizontalCollisionIndex(movingObject, staticObjects);

                MatrixCoordinates movingCollisionForceDirection = new MatrixCoordinates(0, 0);

                if (verticalIndex != -1)
                {
                    movingCollisionForceDirection.Row = -movingObject.Speed.Row;
                    staticObjects[verticalIndex].RespondToCollision(
                        new CollisionData(new MatrixCoordinates(movingObject.Speed.Row, 0),
                            movingObject.GetCollisionGroupString())
                            );
                }

                if (horizontalIndex != -1)
                {
                    movingCollisionForceDirection.Col = -movingObject.Speed.Col;
                    staticObjects[horizontalIndex].RespondToCollision(
                        new CollisionData(new MatrixCoordinates(0, movingObject.Speed.Col),
                            movingObject.GetCollisionGroupString())
                            );
                }

                int diagonalIndex = -1;
                if (horizontalIndex == -1 && verticalIndex == -1)
                {
                    diagonalIndex = DiagonalCollisionIndex(movingObject, staticObjects);
                    if (diagonalIndex != -1)
                    {
                        movingCollisionForceDirection.Row = -movingObject.Speed.Row;
                        movingCollisionForceDirection.Col = -movingObject.Speed.Col;

                        staticObjects[diagonalIndex].RespondToCollision(
                        new CollisionData(new MatrixCoordinates(movingObject.Speed.Row, 0),
                            movingObject.GetCollisionGroupString())
                            );
                    }
                }

                List<string> hitByMovingCollisionGroups = new List<string>();

                if (verticalIndex != -1)
                {
                    hitByMovingCollisionGroups.Add(staticObjects[verticalIndex].GetCollisionGroupString());
                }

                if (horizontalIndex != -1)
                {
                    hitByMovingCollisionGroups.Add(staticObjects[horizontalIndex].GetCollisionGroupString());
                }

                if (diagonalIndex != -1)
                {
                    hitByMovingCollisionGroups.Add(staticObjects[diagonalIndex].GetCollisionGroupString());
                }

                if (verticalIndex != -1 || horizontalIndex != -1 || diagonalIndex != -1)
                {
                    movingObject.RespondToCollision(
                        new CollisionData(movingCollisionForceDirection,
                            hitByMovingCollisionGroups)
                            );
                }
            }
        }

        public static int VerticalCollisionIndex(MovingObjects moving, List<GameObject> objects)
        {
            List<MatrixCoordinates> profile = moving.GetCollisionProfile();

            List<MatrixCoordinates> verticalProfile = new List<MatrixCoordinates>();

            foreach (var coord in profile)
            {
                verticalProfile.Add(new MatrixCoordinates(coord.Row + moving.Speed.Row, coord.Col));
            }

            int collisionIndex = GetCollisionIndex(moving, objects, verticalProfile);

            return collisionIndex;
        }

        public static int HorizontalCollisionIndex(MovingObjects moving, List<GameObject> objects)
        {
            List<MatrixCoordinates> profile = moving.GetCollisionProfile();

            List<MatrixCoordinates> horizontalProfile = new List<MatrixCoordinates>();

            foreach (var coord in profile)
            {
                horizontalProfile.Add(new MatrixCoordinates(coord.Row, coord.Col + moving.Speed.Col));
            }

            int collisionIndex = GetCollisionIndex(moving, objects, horizontalProfile);

            return collisionIndex;
        }

        public static int DiagonalCollisionIndex(MovingObjects moving, List<GameObject> objects)
        {
            List<MatrixCoordinates> profile = moving.GetCollisionProfile();

            List<MatrixCoordinates> horizontalProfile = new List<MatrixCoordinates>();

            foreach (var coord in profile)
            {
                horizontalProfile.Add(new MatrixCoordinates(coord.Row + moving.Speed.Row, coord.Col + moving.Speed.Col));
            }

            int collisionIndex = GetCollisionIndex(moving, objects, horizontalProfile);

            return collisionIndex;
        }

        private static int GetCollisionIndex(MovingObjects moving, ICollection<GameObject> objects, List<MatrixCoordinates> movingProfile)
        {
            int collisionIndex = 0;

            foreach (var obj in objects)
            {
                if (moving.CanCollideWith(obj.GetCollisionGroupString()) || obj.CanCollideWith(moving.GetCollisionGroupString()))
                {
                    List<MatrixCoordinates> objProfile = obj.GetCollisionProfile();

                    if (ProfilesIntersect(movingProfile, objProfile))
                    {
                        return collisionIndex;
                    }
                }

                collisionIndex++;
            }

            return -1;
        }

        private static bool ProfilesIntersect(List<MatrixCoordinates> firstProfile, List<MatrixCoordinates> secondProfile)
        {
            foreach (var firstCoord in firstProfile)
            {
                foreach (var secondCoord in secondProfile)
                {
                    if (firstCoord.Equals(secondCoord))
                    {
                        return true;
                    }
                }
            }

            return false;
        }
    }
}
