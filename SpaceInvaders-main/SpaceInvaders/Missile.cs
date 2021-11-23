﻿using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
/// <summary>
/// Summary description for Class1
/// 
/// </summary>
namespace SpaceInvaders
{
    class Missile : SimpleObject
    {


        public double Vitesse
        {
            get;
            set;
        }


        public Missile(Vecteur2D pos, int lives, Bitmap img)
        {
            Position = pos;
            Lives = lives;
            Image = img;
            Vitesse = 820;
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
            if (Position.y < 0 || Position.y > gameInstance.gameSize.Height)
            {
                Lives = 0;
                return;



            }
            
            
                foreach (GameObject obj in gameInstance.gameObjects.ToList())
                {
                    
                    if(obj.EnCollision(this) && obj is Bunker)
                    {

                    Bunker b = (Bunker)obj;
                    b.CollisionParPixel(this);

                    break;

                    }
                     if ( obj is EnemyBlock)
                       {
                        EnemyBlock e = (EnemyBlock)obj;
                        e.EnCollision(this);
                        break;
                       }
                }
                
                Position.y -= Vitesse * deltaT;
                
             
                 
  
               

   
           





        }
    }
}