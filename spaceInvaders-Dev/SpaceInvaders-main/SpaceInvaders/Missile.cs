using System;
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


        public Missile(Vecteur2D pos, int lives, Bitmap img):base(Side.Neutral)
        {
            Position = pos;
            Lives = lives;
            Image = img;
            Vitesse = 220;
        }
       


        public override void Draw(Game gameInstance, Graphics graphics)
        {
            if (IsAlive())
            {
                base.Draw(gameInstance, graphics);
            }
        }

        public override bool IsAlive()
        {
            return base.IsAlive();
        }

        
        public override void Update(Game gameInstance, double deltaT)
        {

            
            if ( gameInstance.state == Game.GameState.Play) 
            {
                if (Position.y < 0 || Position.y > gameInstance.gameSize.Height)
                {
                    Lives = 0;
                    return;



                }


                foreach (GameObject obj in gameInstance.gameObjects.ToList())
                {
                    PlayerSpaceship p = (PlayerSpaceship)gameInstance.playerSpaceShip;
                    p.Collision(this);
                   
                    if ((Missile)gameInstance.playerSpaceShip.missile != null && this != (Missile)gameInstance.playerSpaceShip.missile)
                    {

                        Missile m = (Missile)gameInstance.playerSpaceShip.missile;
                        m.Collision(this);
                       


                    }

                    if (obj is Bunker)
                    {
                        Bunker b = (Bunker)obj;

                        b.CollisionParPixel(this);
                            
                             
                           
                       
                       }




                        if (obj is EnemyBlock)
                        {

                            EnemyBlock e = (EnemyBlock)obj;
                            e.Collision(this);
                            break;

                        }


                    }


                }
                 if (IsAlive())
                 {
                      if (this.side == Side.Ally)
                      {
                           Position.y -= Vitesse * deltaT;
                      }
                      else
                      {
                           Position.y += Vitesse * deltaT;
                      }

                   
                  }
            
            

        }



        protected override void OnCollision(Missile m, int numberOfPixelsInCollision)
        {
            if (m != null || !(m.IsAlive()))
            {
                m.Lives = 0 ;
                Lives = 0;

            }
           
            
        }
    }
}