using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AliensKiller
{
    public class Alien : GameObject
    {
        public new const string CollisionGroupString = "alien";
        

        public Alien(MatrixCoordinates topLeft)
            : base(topLeft, 
            new char[,] { 
            { ' ', ' ', '▀', '▄', ' ', ' ', ' ', '▄', '▀', ' ', ' ' },
            {' ', '▄', '█', '▀', '█', '█', '█', '▀', '█', '▄', ' ' },
            {'█', '▀', '█', '▀', '█', '█', '█', '▀', '█', '▀', '█' },
            {'▀', ' ', '▀', '▄', '▄', ' ', '▄', '▄', '▀', ' ', '▀' }})
        {
        }

           public override void Update()
           {
               
           }

        public override bool CanCollideWith(string otherCollisionGroupString)
        {
            return otherCollisionGroupString == "bullet";
        }

        public override void RespondToCollision(CollisionData collisionData)
        {
            this.IsDestroyed = true;
            Console.Beep();
        }

        public override string GetCollisionGroupString()
        {
            return Alien.CollisionGroupString;
        }

        public override IEnumerable<GameObject> ProduceObjects()
        {
            if (this.IsDestroyed)
            {
                List<GameObject> points = new List<GameObject>();
                points.Add(new Point(new MatrixCoordinates( this.topLeft.Row+2,this.TopLeft.Col+5), 5));
                return points;
            }
            else
            {
                return base.ProduceObjects();
            }
        }

       
    }
}
