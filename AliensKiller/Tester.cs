using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace AliensKiller
{
    public class Tester
    {
        const int WorldRows = 50;
        const int WorldCols = 79;
        const int RacketLength = 1;
        public static void Initialize(Engine engine)
        {
            int startRow = 1;
            int startCol = 5;
            int endRow = WorldRows - 35;
            int endCol = WorldCols - 5;

            for (int j = startRow; j < endRow; j = j + 5)
            {
                for (int i = startCol; i < endCol; i = i + 12)
                {
                    Alien currAlien = new Alien(new MatrixCoordinates(j, i));

                    engine.AddObject(currAlien);
                }
            }

            StartTitle newTitle = new StartTitle(new MatrixCoordinates(29, 27));
            engine.AddObject(newTitle);

            for (int row = 0; row < WorldRows; row++)
            {
                Wall leftWall = new Wall(new MatrixCoordinates(row, 1), new char[,] { { '|' } });
                engine.AddObject(leftWall);

                Wall rightWall = new Wall(new MatrixCoordinates(row, 78), new char[,] { { '|' } });
                engine.AddObject(rightWall);
            }

            for (int col = 1; col < WorldCols; col++)
            {
                Wall ceilingWall = new Wall(new MatrixCoordinates(0, col), new char[,] { { '-' } });
                engine.AddObject(ceilingWall);
            }


        }



        static void Main(string[] args)
        {
            Stopwatch stopWatch = new Stopwatch();
            Console.SetWindowSize(Console.LargestWindowWidth, Console.LargestWindowHeight);
            IRenderer renderer = new ConsoleRenderer(WorldRows, WorldCols);
            IUserInterface keyboard = new KeyboardInterface();
            Engine gameEngine = new Engine(renderer, keyboard);
            Random random = new Random();

            Spaceship theSpace = new Spaceship(new MatrixCoordinates(WorldRows - 3, WorldCols / 2 - 1), RacketLength);

            gameEngine.AddObject(theSpace);

            keyboard.OnActionPressed += (sender, eventInfo) =>
            {
                Bullet newBullet = new Bullet(new MatrixCoordinates(theSpace.TopLeft.Row + 4, theSpace.TopLeft.Col + 3), new MatrixCoordinates(-5, 0));
                gameEngine.AddObject(newBullet);
                int randomNumber = random.Next(0, 3);
                if (randomNumber % 3 == 0)
                {
                    AlienBullet theAlienBullet = new AlienBullet(new MatrixCoordinates(0, random.Next(2,75)));
                    gameEngine.AddObject(theAlienBullet);
                }
            };

            keyboard.OnLeftPressed += (sender, eventInfo) =>
            {
                //startCol
                gameEngine.MovePlayerSpaceshipLeft(5);
                int randomNumber = random.Next(0, 3);
                if (randomNumber % 3 == 0)
                {
                    AlienBullet theAlienBullet = new AlienBullet(new MatrixCoordinates(0, random.Next(2, 75)));
                    gameEngine.AddObject(theAlienBullet);
                }
            };

            keyboard.OnRightPressed += (sender, eventInfo) =>
            {
                //worldCols
                gameEngine.MovePlayerSpaceshipRight(WorldCols);
                int randomNumber = random.Next(0, 3);
                if (randomNumber % 3 == 0)
                {
                    AlienBullet theAlienBullet = new AlienBullet(new MatrixCoordinates(0, random.Next(2, 75)));
                    gameEngine.AddObject(theAlienBullet);
                }
            };

            keyboard.OnStartPressed += (sender, eventInfo) =>
            {
                stopWatch.Start();
                Bullet newBullet = new Bullet(new MatrixCoordinates(theSpace.TopLeft.Row + 4, theSpace.TopLeft.Col + 3), new MatrixCoordinates(-5, 0));
                gameEngine.AddObject(newBullet);

                //gameEngine.CalculatePoints();

            };

            //Stopwatch timer = new Stopwatch();
            //timer.Start();
            //if (timer.Elapsed.Seconds == 20)
            //{
            //    AlienBullet theAlienBullet = new AlienBullet(new MatrixCoordinates(0, 30));
            //    gameEngine.AddObject(theAlienBullet);
            //
            //    timer.Restart();
            //}

            Initialize(gameEngine);


            gameEngine.Run(stopWatch, false);



        }
    }
}
