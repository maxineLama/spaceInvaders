﻿    using System;
    using System.Collections.Generic;
    using System.Drawing;
using System.IO;
using System.Linq;
using System.Media;
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

        static System.Media.SoundPlayer pl = new System.Media.SoundPlayer(@"C:\Users\mlama\Desktop\Esiee\Projet_POO_final\spaceInvaders-Dev\SpaceInvaders-main\gunsalute.wav");


        public SpaceShip(Vecteur2D pos, int lives, Bitmap img):base(Side.Neutral)
        {
            Position = pos;
            Lives = lives;
            Image = img;
            missile = null;
            speedPixelPerSecond = 400;
           
        }
        public void shoot(Game gameInstance, double deltaT)
        {
            if (missile == null || !missile.IsAlive() && this.IsAlive())
            {
                
                Bitmap img2 = SpaceInvaders.Properties.Resources.shoot1;
                Vecteur2D pos = new Vecteur2D(Position.x + Image.Width / 2 - 1, Position.y - Image.Height ) ;
                missile = new Missile(pos, 1, img2);
                if (missile == gameInstance.playerSpaceShip.missile)
                {
                    missile.side = Side.Ally;
                    
                }
                else
                {
                    missile.side = Side.Enemy;
                    missile.Position.y += gameInstance.playerSpaceShip.Image.Height * 2 + 1;
                }

                
               
                gameInstance.gameObjects.Add(missile);
                pl.Play();

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
        public override bool Collision(Missile m)
        {
            return base.Collision(m);
        }

        protected override void OnCollision(Missile m, int numberOfPixelsInCollision)
        {
           
           Lives -= 1; 
            m.Lives =0 ;
                
          
                
            
            
        
        }


    }
}
