﻿    using System;
    using System.Collections.Generic;
    using System.Drawing;
    using System.Linq;
    using System.Text;

    /// <summary>
    /// Summary description for Class1
    /// </summary>
    ///
namespace SpaceInvaders
{
    class SpaceShip : SimpleObject
    {
        public Missile missile;
        public double speedPixelPerSecond;

        public SpaceShip(Vecteur2D pos, int lives, Bitmap img)
        {
            Position = pos;
            Lives = lives;
            Image = img;
            missile = null;
            speedPixelPerSecond = 400;
        }
        public void shoot(Game gameInstance, double deltaT)
        {
            if (missile == null || !missile.IsAlive())
            {
                Bitmap img2 = SpaceInvaders.Properties.Resources.shoot1;
                Vecteur2D pos = new Vecteur2D(Position.x + Image.Width / 2 - 1, Position.y - Image.Height ) ;
                missile = new Missile(pos, 1, img2);
                gameInstance.gameObjects.Add(missile);
                gameInstance.Update(deltaT);


            }
        }

        public override void Draw(Game gameInstance, Graphics graphics)
        {
            base.Draw(gameInstance, graphics);
        }

        public override bool IsAlive()
        {
            return base.IsAlive();
        }

        public override void Update(Game gameInstance, double deltaT)
        {


        }
        public override bool EnCollision(Missile m)
        {
            return base.EnCollision(m);
        }


    }
}