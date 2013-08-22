using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Diagnostics;

namespace AliensKiller
{
    public class Spaceship : GameObject
    {
         public new const string CollisionGroupString = "spaceship";

        public int Width {get; protected set;}

        public Spaceship(MatrixCoordinates topLeft, int width)
            : base(topLeft, 
            new char[,] { 
            {' ', ' ', '▄', '█', '▄', ' ', ' ' },
            {'█', '█', '█', '█', '█', '█', '█' } })
        {
            this.Width = width;
            this.body = GetRacketBody(this.Width);
        }

        char[,] GetRacketBody(int width)
        {
            char[,] body = { 
            { ' ', ' ', '▄', '█', '▄', ' ', ' '},
            { '█', '█', '█', '█', '█', '█', '█'}};

           //for (int i = 0; i < width; i++)
           //{
           //    body[0, i] = '=';
           //}

            return body;
        }

        public void MoveLeft()
        {
            
            this.topLeft.Col--;
        }

        public void MoveRight()
        {
            this.topLeft.Col++;
        }

        public override string GetCollisionGroupString()
        {
            return Spaceship.CollisionGroupString;
        }

        public override bool CanCollideWith(string otherCollisionGroupString)
        {
            return otherCollisionGroupString == "alien" || otherCollisionGroupString == "alienbullet";
        }

        public override void RespondToCollision(CollisionData collisionData)
        {
            this.IsDestroyed = true;
            Console.Beep();
            IRenderer renderer = new ConsoleRenderer(50, 79);
            IUserInterface keyboard = new KeyboardInterface();
            Engine endGame=new Engine(renderer, keyboard);
            Stopwatch pointless=new Stopwatch();
            endGame.Run(pointless,true);
            
        }

        public override void Update()
        {
        }
    }
}
