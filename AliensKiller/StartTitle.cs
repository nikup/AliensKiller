using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AliensKiller
{
    public class StartTitle : GameObject
    {
        public new const string CollisionGroupString = "title";


        public StartTitle(MatrixCoordinates topLeft)
            : base(topLeft,
            new char[,] { 
                {'╔','═','═','═','═','═','═','═','═','═','═','═','═','═','═','═','═','═','═','═','═','═','═','═','═','═','╗'},
                {'║', 'P','r','e','s','s',' ','E','n','t','e','r',' ','t','o',' ','s','t','a','r','t',' ','g','a','m','e','║'},
            {'╚','═','═','═','═','═','═','═','═','═','═','═','═','═','═','═','═','═','═','═','═','═','═','═','═','═','╝'},})
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
            return StartTitle.CollisionGroupString;
        }

    }
}

