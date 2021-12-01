using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Drawing;
using System.Media;

namespace SpaceInvaders
{
    class PlayerSpaceship : SpaceShip
    {

        
       
        public PlayerSpaceship(Vecteur2D pos, int lives, Bitmap img): base(pos, lives, img)
        {
            score = 0;
            this.side = Side.Ally;
            p= new SoundPlayer();
           
           
        }
        public static int score
        {
            get;
            set;
        }
        Random random = new Random(100);

        public SoundPlayer p;
        public override void Draw(Game gameInstance, Graphics graphics)
        {
            base.Draw(gameInstance, graphics);
            Font drawFont = new Font("Arial", 12);
            SolidBrush drawBrush = new SolidBrush(Color.Black);
            float x = 5f;
            float y = 0f;
           
             graphics.DrawString("Vie(s) : "+(Lives), drawFont, drawBrush, x, y);

            float xScore = 500f;
            float yScore= 0f;
            graphics.DrawString("Score : " + score, drawFont, drawBrush, xScore, yScore);
            double r= random.NextDouble();
            if(score >= 900 )
            {
                float xScore2 = 500f;
                float yScore2 = 20f;
                graphics.DrawString("Boost activé ", drawFont, new SolidBrush(Color.Red), xScore2, yScore2);
                
            }
        }
        public override void Update(Game gameInstance, double deltaT)
        {
            if (IsAlive()&& gameInstance.state==Game.GameState.Play)
            {
                

                if (gameInstance.keyPressed.Contains(System.Windows.Forms.Keys.Right))
                {
                    if (gameInstance.playerSpaceShip.Position.x <= gameInstance.gameSize.Width - gameInstance.playerSpaceShip.Image.Width)
                    {
                        gameInstance.playerSpaceShip.Position.x += speedPixelPerSecond * deltaT;
                    }

                }
                else if (gameInstance.keyPressed.Contains(System.Windows.Forms.Keys.Left))
                {
                    if (gameInstance.playerSpaceShip.Position.x - 1 > 0)
                    {
                        gameInstance.playerSpaceShip.Position.x -= speedPixelPerSecond * deltaT;
                    }
                }
                else if (gameInstance.keyPressed.Contains(System.Windows.Forms.Keys.Space))
                {
                    if (this.IsAlive())
                    {

                        p.Stream = SpaceInvaders.Properties.Resources.lazer7;
                        p.Play();
                        shoot(gameInstance, deltaT);
                       
                    }



                }
                else if (score >= 900)
                {
                    missile.Vitesse += 20.0;
                }
            }

        }
       public static void Score()
       {
            score+=100;
            
        }
        protected override void OnCollision(Missile m, int numberOfPixelsInCollision)
        {
            base.OnCollision(m, numberOfPixelsInCollision);
           
        }

    }
}
