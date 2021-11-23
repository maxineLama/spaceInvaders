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
        public EnemyBlock(Vecteur2D pos, int baseWidth)
        {
            this.Position = pos;
            this.baseWidth = baseWidth;
            enemyShips = new HashSet<SpaceShip>();
            size = new Size(0, 0);
            
        }
        public void AddLine(int nbShips, int nbLives, Bitmap shipImage)
        {
            

            for (int i = 0; i < nbShips; i++)
            {
                Vecteur2D p = new Vecteur2D(Position.x+(baseWidth/nbShips)/2*i, Position.y );
                
                SpaceShip sp = new SpaceShip(p, nbLives,shipImage);
                enemyShips.Add(sp);
               
            }
            UpdateSize();

        }
        public void UpdateSize()
        {

            size = new Size(baseWidth, (int)Position.y);

            
        }
        public override void Draw(Game gameInstance, Graphics graphics)
        {
            foreach(SpaceShip sp in enemyShips)
            {
                graphics.DrawImage(sp.Image, (float)sp.Position.x, (float)sp.Position.y, sp.Image.Width, sp.Image.Height);
            }
            
        }

        public override bool EnCollision(Missile m)
        {
            foreach (SpaceShip sp in enemyShips)
            {

                if (sp.EnCollision(m))
                {
                    sp.Lives = 0;
                    
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

            foreach (SpaceShip sp in enemyShips)
            {
                sp.Position.x += 1;

                if (Position.x == gameInstance.gameSize.Width - 10)
                {
                    Position.y += 1000;
                }
            }

        }
    }
}
