using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AliensKiller
{
    public abstract class GameObject : IRenderable, ICollidable
    {
        public const string CollisionGroupString = "object";

        protected MatrixCoordinates topLeft;
        public MatrixCoordinates TopLeft
        {
            get
            {
                return new MatrixCoordinates(topLeft.Row, topLeft.Col);
            }

            protected set
            {
                this.topLeft = new MatrixCoordinates(value.Row, value.Col);
            }
        }

        protected char[,] body;

        public bool IsDestroyed { get; protected set; }

        protected GameObject(MatrixCoordinates topLeft, char[,] body)
        {
            this.TopLeft = topLeft;

            int imageRows = body.GetLength(0);
            int imageCols = body.GetLength(1);

            this.body = this.CopyBodyMatrix(body);

            this.IsDestroyed = false;
        }

        public abstract void Update();

        public virtual List<MatrixCoordinates> GetCollisionProfile()
        {
            List<MatrixCoordinates> profile = new List<MatrixCoordinates>();

            int bodyRows = this.body.GetLength(0);
            int bodyCols = this.body.GetLength(1);

            for (int row = 0; row < bodyRows; row++)
            {
                for (int col = 0; col < bodyCols; col++)
                {
                    profile.Add(new MatrixCoordinates(row + this.topLeft.Row, col + this.topLeft.Col));
                }
            }

            return profile;
        }

        public virtual void RespondToCollision(CollisionData collisionData)
        {
        }

        public virtual bool CanCollideWith(string otherCollisionGroupString)
        {
            return GameObject.CollisionGroupString == otherCollisionGroupString;
        }

        public virtual string GetCollisionGroupString()
        {
            return GameObject.CollisionGroupString;
        }

        char[,] CopyBodyMatrix(char[,] matrixToCopy)
        {
            int rows = matrixToCopy.GetLength(0);
            int cols = matrixToCopy.GetLength(1);

            char[,] result = new char[rows, cols];

            for (int row = 0; row < rows; row++)
            {
                for (int col = 0; col < cols; col++)
                {
                    result[row, col] = matrixToCopy[row, col];
                }
            }

            return result;
        }

        public virtual MatrixCoordinates GetTopLeft()
        {
            return this.TopLeft;
        }

        public virtual char[,] GetImage()
        {
            return this.CopyBodyMatrix(this.body);
        }

        public virtual IEnumerable<GameObject> ProduceObjects()
        {
            return new List<GameObject>();
        }


    }
}
