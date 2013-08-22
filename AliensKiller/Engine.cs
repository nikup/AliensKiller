using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Threading;
using System.Diagnostics;

namespace AliensKiller
{
    public class Engine
    {
        IRenderer renderer;
        IUserInterface userInterface;
        List<GameObject> allObjects;
        List<MovingObjects> movingObjects;
        List<GameObject> staticObjects;
        Spaceship playerSpaceship;
        Alien playerAlien;

        public int sumPoints = 0;

        public Engine(IRenderer renderer, IUserInterface userInterface)
        {
            this.renderer = renderer;
            this.userInterface = userInterface;
            this.allObjects = new List<GameObject>();
            this.movingObjects = new List<MovingObjects>();
            this.staticObjects = new List<GameObject>();
        }

        private void AddStaticObject(GameObject obj)
        {
            this.staticObjects.Add(obj);
            this.allObjects.Add(obj);
        }

        private void AddMovingObject(MovingObjects obj)
        {
            this.movingObjects.Add(obj);
            this.allObjects.Add(obj);
        }

        public virtual void AddObject(GameObject obj)
        {
            if (obj is MovingObjects)
            {
                this.AddMovingObject(obj as MovingObjects);
            }
            else
            {
                if (obj is Spaceship)
                {
                    AddSpaceship(obj);

                }
                else if (obj is Alien)
                {
                    AddAlien(obj);
                }
                else
                {
                    this.AddStaticObject(obj);
                }
            }
        }

        private void AddSpaceship(GameObject obj)
        {
            
            this.playerSpaceship = obj as Spaceship;
            this.AddStaticObject(obj);
        }

        private void AddAlien(GameObject obj)
        {

            this.playerAlien = obj as Alien;
            this.AddStaticObject(obj);
        }

        public virtual void MovePlayerSpaceshipLeft(int startCols)
        {
            if (this.playerSpaceship.TopLeft.Col > startCols)
            {
                this.playerSpaceship.MoveLeft();
            }
        }

        public virtual void MovePlayerSpaceshipRight(int worldCols)
        {
            //Spaceship characters
            if(this.playerSpaceship.TopLeft.Col + 10 < worldCols)
            {
                this.playerSpaceship.MoveRight();
            }
        }

        public virtual void Run(Stopwatch stopWatch, bool end)
        {
            while (true)
            {
                this.renderer.RenderAll();
                if (sumPoints == 18)
                {
                    stopWatch.Stop();
                    Console.SetCursorPosition(27, 27);
                    Console.WriteLine("╔══════════════════════╗");
                    Console.SetCursorPosition(27, 28);
                    Console.WriteLine("║ Your time: {0:3} seconds║", stopWatch.Elapsed.Seconds.ToString());
                    Console.SetCursorPosition(27, 29);
                    Console.WriteLine("╚══════════════════════╝");
                }
                if (end)
                {
                    Console.SetCursorPosition(27, 27);
                    Console.WriteLine("╔══════════════════════╗");
                    Console.SetCursorPosition(27, 28);
                    Console.WriteLine("║ Game Over!           ║");
                    Console.SetCursorPosition(27, 29);
                    Console.WriteLine("╚══════════════════════╝");
                }
                else Console.WriteLine("Points: " + sumPoints.ToString());
                System.Threading.Thread.Sleep(50);

                this.userInterface.ProcessInput();

                this.renderer.ClearQueue();

                foreach (var obj in this.allObjects)
                {
                    obj.Update();
                    this.renderer.EnqueueForRendering(obj);
                }

                CollisionDispatcher.HandleCollisions(this.movingObjects, this.staticObjects);

                List<GameObject> producedObjects = new List<GameObject>();

                foreach (var obj in this.allObjects)
                {
                    producedObjects.AddRange(obj.ProduceObjects());
                }
                foreach (var item in staticObjects)
                {
                    if (item.IsDestroyed &&  (item is Alien)) sumPoints++;
                }
                this.allObjects.RemoveAll(obj => obj.IsDestroyed);
                this.movingObjects.RemoveAll(obj => obj.IsDestroyed);
                this.staticObjects.RemoveAll(obj => obj.IsDestroyed);

                foreach (var obj in producedObjects)
                {
                    this.AddObject(obj);
                }
                
            }
        }
    }
}
