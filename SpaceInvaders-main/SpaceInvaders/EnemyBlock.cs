using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;

namespace SpaceInvaders
{
    class EnemyBlock : GameObject
    {

        private HashSet<SpaceShip> enemyShips;
        private int baseWidth;

        Size size
        {
            get;
            set;
        }
        Vecteur2D Position
        {
            get;
            set;
        }
        double PositionY;
        int direction;
        double vitesse;
        double randomShootProbability;
        Random random;

        public EnemyBlock(Vecteur2D pos, int baseWidth):base(Side.Enemy)
        {
            this.Position = pos;
            this.PositionY = Position.y;
            this.baseWidth = baseWidth;
            enemyShips = new HashSet<SpaceShip>();
            size = new Size(0, 0);
            direction = 1;
            vitesse = 0.1;
            randomShootProbability = 1;
            random = new Random(100000000);
            
        }
        public void AddLine(int nbShips, int nbLives, Bitmap shipImage )
        {
            

            for (int i = 0; i < nbShips; i++)
            {
                Vecteur2D p = new Vecteur2D((Position.x+(baseWidth/nbShips)/2*i ), PositionY );
                Bitmap b = shipImage;
                SpaceShip sp = new SpaceShip(p, nbLives,b );
                sp.side = Side.Enemy;
                enemyShips.Add(sp);
               
            }
            PositionY += shipImage.Height + 2;

        }
        public void UpdateSize()
        {

            size = new Size(baseWidth, (int)Position.y);

            
        }
        public override void Draw(Game gameInstance, Graphics graphics)
        {
            foreach(SpaceShip sp in enemyShips)
            {
                if (sp.IsAlive())
                {

                    graphics.DrawImage(sp.Image, (float)sp.Position.x, (float)sp.Position.y, sp.Image.Width, sp.Image.Height);
                }
            }
            
        }

        public override bool Collision(Missile m)
        {
            foreach (SpaceShip sp in enemyShips)
            {

                if (sp.Collision(m) )
                {
                    
                    sp.Lives = 0;
                    m.Lives = 0;
                    
                    
                }
            }
            if (IsAlive())
            {
                
                return true;
            }
            return false;
        }
       

        public override bool IsAlive()
        {
            int vivants = 0;
            foreach (SpaceShip sp in enemyShips)
            {
                if (sp.IsAlive())
                {
                    vivants++;
                }
            }
            if (vivants > 0)
            {
                return true;
            }
            return false;
        }
        

        public override void Update(Game gameInstance, double deltaT)
        {
            if (IsAlive())
            {
                foreach (SpaceShip sp in enemyShips)
                {
                    double r = random.NextDouble();
                    if (r <= randomShootProbability * deltaT && sp.IsAlive())
                    {
                        sp.shoot(gameInstance, deltaT);
                        if (sp.missile != null && sp.missile.IsAlive() && sp.IsAlive())
                        {
                            sp.missile.Update(gameInstance,deltaT);
                        }
                    }
                     
                        
                }

                
                go(direction, vitesse);
                
                 if (enemyShips.Last().Position.x >= 560 || enemyShips.First().Position.x <= 10)
                {
                    
                    goDown();
                    direction *= -1;
                    vitesse += 0.02;
                    randomShootProbability+=0.2;
                }

                foreach (GameObject obj in gameInstance.gameObjects.ToList())
                {

                    if (obj is Missile)
                    {
                        Missile m = (Missile)obj;
                        Collision(m);
                    }
                }


            }
        }
        private void go(int x, double vitesse)
        {
            if (x == 1)
            {
                foreach (SpaceShip sp in enemyShips)
                {
                    sp.Position.x += 0.1+ vitesse/2;
                }
            }
            else
            {

                foreach (SpaceShip sp in enemyShips)
                {
                    sp.Position.x -= 0.1+ vitesse/2;
                }

            }
        }
       
        private void goDown()
        {
            foreach (SpaceShip sp in enemyShips)
            {
                sp.Position.y += 20;
            }
        }
        
    }
}
